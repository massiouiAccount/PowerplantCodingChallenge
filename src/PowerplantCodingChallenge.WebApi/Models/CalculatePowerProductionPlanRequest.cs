using System.Text.Json.Serialization;

namespace PowerplantCodingChallenge.WebApi.Models;

public record CalculatePowerProductionPlanRequest
{
  [JsonPropertyName("load")]
  public double LoadInMWh { get; init; }

  public Fuels Fuels { get; init; } = null!;

  [JsonPropertyName("powerplants")]
  public PowerPlant[] PowerPlants { get; init; } = null!;
}

public record Fuels
{
  [JsonPropertyName("gas(euro/MWh)")]
  public double GasPriceInEuroPerMWh { get; init; }


  [JsonPropertyName("kerosine(euro/MWh)")]
  public double KerosinePriceInEuroPerMWh { get; init; }


  [JsonPropertyName("co2(euro/ton)")]
  public double Co2EmissionAllowancesPriceInEuroPerTon { get; init; }


  [JsonPropertyName("wind(%)")]
  public double WindPercentage { get; init; }
}

public record PowerPlant
{
  public string Name { get; init; } = null!;
  public string Type { get; init; } = null!;
  public double Efficiency { get; init; }

  [JsonPropertyName("pmin")]
  public double MinimumPowerOutput { get; init; }

  [JsonPropertyName("pmax")]
  public double MaximumPowerOutput { get; init; }
}

