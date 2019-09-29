from EnergyTariff import EnergyTariff

class EnergyData:
	ConsumedRate1 = -1
	ConsumedRate2 = -1
	ReturnedRate1 = -1
	ReturnedRate2 = -1
	Tariff = EnergyTariff.Unknown
	Consumed = -1
	Returned = -1
	Gas = -1

	def IsValid(self):
		return self.ConsumedRate1 >= 0 \
		and self.ConsumedRate2 >= 0 \
		and self.ReturnedRate1 >= 0 \
		and self.ReturnedRate2 >= 0 \
		and self.Tariff != EnergyTariff.Unknown \
		and self.Consumed >= 0 \
		and self.Returned >= 0 \
		and self.Gas >= 0