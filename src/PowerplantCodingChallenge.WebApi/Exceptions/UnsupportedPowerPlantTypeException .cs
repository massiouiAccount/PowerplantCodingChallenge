namespace PowerplantCodingChallenge.WebApi.Exceptions;

public class UnsupportedPowerPlantTypeException(string powerPlantType) : Exception($"Unsupported power plant type: {powerPlantType}")
{ }
