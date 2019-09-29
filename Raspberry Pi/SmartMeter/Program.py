import sys
import serial
import time
import boto3
import traceback
from decimal import *
from EnergyData import EnergyData
from EnergyTariff import EnergyTariff
from datetime import datetime

def ReadSerialLine():
	return str(port.readline().decode('utf-8').rstrip())

def GetData(start, end):
	try:
		value = ReadSerialLine()
		x = value.index( start ) + len( start )
		y = value.index( end, x )
		return value[x:y]
	except ValueError:
		return "Cannot read data"

device = '644e1dd7-2a7f-18fb-b8ed-ed78c3f92c2b'
dynamodb = boto3.resource('dynamodb').Table('energy')
port = serial.Serial('/dev/ttyUSB1', 115200)

while 1:
	try:
		if(ReadSerialLine().startswith('/')):
			timestamp = datetime.now()
			energyData = EnergyData()

			ReadSerialLine() # 1-3:0.2.8
			ReadSerialLine() # 0-0:1.0.0
			ReadSerialLine() # 0-0:96.1.1
			ReadSerialLine() # 0-0:96.1.1
			
			# 1-0:1.8.1 Consumed Rate 1 (Low)
			data = GetData('1-0:1.8.1(','*kWh)') 
			energyData.ConsumedRate1 = Decimal(data)
			
			# 1-0:2.8.1 Returned Rate 1 (Low)
			data = GetData('1-0:2.8.1(','*kWh)') 
			energyData.ReturnedRate1 = Decimal(data)

			# 1-0:1.8.2 Consumed Rate 2 (High)
			data = GetData('1-0:1.8.2(','*kWh)') 
			energyData.ConsumedRate2 = Decimal(data)
			
			# 1-0:2.8.2 Returned Rate 2 (High)
			data = GetData('1-0:2.8.2(','*kWh)') 
			energyData.ReturnedRate2 = Decimal(data)
			
			# 0-0:96.14. Current Tarriff (1:Low / 2:High)
			data = GetData('0-0:96.14.0(',')')
			energyData.Tariff = EnergyTariff(int(data))
			
			# 1-0:1.7.0 Current Consumption
			data = GetData('1-0:1.7.0(','*kW)')
			energyData.Consumed = Decimal(data)
			
			# 1-0:2.7.0 Current Return
			data = GetData('1-0:2.7.0(','*kW)')
			energyData.Returned = Decimal(data)
			
			ReadSerialLine() # 0-0:96.7.21
			ReadSerialLine() # 0-0:96.7.9
			ReadSerialLine() # 1-0:99.97.0
			ReadSerialLine() # 1-0:32.32.0
			ReadSerialLine() # 1-0:32.36.0
			ReadSerialLine() # 0-0:96.13.1
			ReadSerialLine() # 0-0:96.13.0
			ReadSerialLine() # 1-0:31.7.0
			ReadSerialLine() # 1-0:21.7.0
			ReadSerialLine() # 1-0:22.7.0
			ReadSerialLine() # 0-1:24.1.0
			ReadSerialLine() # 0-1:96.1.0
						
			# 0-1:24.2.1 Gas
			nextLine = ReadSerialLine() 
			data = nextLine.split(')(')[1].replace('*m3)', '')
			energyData.Gas = Decimal(data)
			
			#validation
			if(energyData.IsValid() == False):
				raise ValueError(energyData)

			#Put item to Amazon Dynamo Db Table
			response = dynamodb.put_item(
				Item={
					'deviceid': device,
					'consumedRate1' : energyData.ConsumedRate1,
					'consumedRate2' : energyData.ConsumedRate2,
					'returnedRate1' : energyData.ReturnedRate1,
					'returnedRate2' : energyData.ReturnedRate2,
					'energyTariffId': energyData.Tariff.name,
					'consumed'      : energyData.Consumed,
					'returned'      : energyData.Returned,
					'gas'           : energyData.Gas,
					'timestamp'     : str(timestamp)
				}
			)
			
			print(timestamp)
			#!4962
	except serial.SerialException:
		print('cannot read from serial port')
		time.sleep(10)	
	except Exception as e:
		print("type error: " + str(e))
		print(traceback.format_exc())
		break
port.close()