# Energy Insight (In progress)
Web App visualizing your energy consumption. Energy data is collected by reading a smart energy meter, installed at home. 

This project is divided in five parts.
1. Storage
Setting up storage (This projects shows you the setup in AWS DynamoDB)
2. Raspberry Pi setup
Python3 application running on a Raspberry Pi which is connected to the smart meter by USB.
3. ASP.NET Core Web API 
An API for interacting with the storage.
4. ASP.NET Core MVC application
To visualize the energy consumption, giving insight to the user.
5. Deployment
Getting the API and Web app online!

## 1. Storage
Each record returned by the smart energy meter will contain the following data:
* Total consumption (kWh) at a low rate/tariff. 
* Total consumption (kWh) at a high rate/tariff.
* Total return (kWh) at a low rate/tariff.
* Total return (kWh) at a high rate/tariff.
* Current rate/tariff (1: Low, 2: High)
* Current consumption (kW)
* Current return (kW)
* Current consumption (m3) of gas

This project shows the setup for AWS DynamoDb. This service is included in the Free tier subscription of Amazon so feel free to check it out. But you can also skip this step and adjust the code from the API and python application to read/write to your own storage type.
My DynamoDB table looks is setup like this:
* deviceid (string)  [Primary Partition Key]
* timestamp (string) [Primary Sort Key]
* consumedrate1 (Number)
* consumedrate2 (Number)
* returnedrate1 (Number)
* returnedrate2 (Number)
* tariff (Number/string)
* consumed (Number)
* returned (Number)
* gas (Number)

I've also changed the Read/write capacity mode to 'On-demand' instead of 'Provisioned'. The On-demand mode is not included in the free tier but bills you only for the amount of requests made to the table. This is more suited for the project since we like to set it up in a serverless way.

If you're using AWS, make sure to configure two IAM users. One that can read from your dynamodb table (API) and one that can write (python app).

## 2. Raspberry Pi setup
Make sure it has an internet connection and is physically connected to your smart meter.
My smart meter is a Landis + Gyr E350 and is connected with a USB to RJ11 cable. Be aware that when you have an other smart meter, that the serial output will probably differ and so should the code.

My Raspberry Pi is running Rasbian Lite (Buster). Update your pi, enable some interfacing options, configure localization and we need to do a few more things for this python application to run.
1. Install or make sure python3 and its package installer are installed.
``` linux
sudo apt-get install python3
sudo apt-get install python3-pip
```
2. Install required packages for the python application.
* pyserial: Serial library required for reading the serial port
* boto3: AWS SDK
``` linux
sudo pip3 install pyserial 
sudo pip3 install boto3 
```
3. Install AWS cli to configure credentials and region. Configure the aws key and secret for the IAM user created earlier. Also define the region of your dynamodb table.
``` linux
sudo apt-get install awscli
aws configure
```
4. Install Git and clone this repository.
```linux
sudo apt-get install git
git clone <git repository url>
```
5. Run the application :D
```linux
sudo python3 Energy-Insight/'Raspberry Pi'/SmartMeter/Program.py
```
Note that when the application throws a serialException, the app might be looking at a wrong port. You need the change the code (replace '/dev/ttyUSB1' with the correct one)
	
6. Configure the application to run on startup
```linux
sudo nano /etc/rc.local
```
Add the following line before 'exit 0'
``` linux
python3 /home/pi/Energy-Insight/'Raspberry Pi'/SmartMeter/Program.py &
```
## 3. ASP.NET Core Web API 
## 4. ASP.NET Core MVC application
## 5. Deployment
