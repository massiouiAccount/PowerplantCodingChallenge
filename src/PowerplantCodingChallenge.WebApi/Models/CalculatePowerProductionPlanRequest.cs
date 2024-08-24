using System.Text.Json.Serialization;

namespace PowerplantCodingChallenge.WebApi.Models;

public record CalculatePowerProductionPlanRequest
{
  [JsonPropertyName("load")]
  public decimal LoadInMWh { get; init; }

  public Fuels Fuels { get; init; } = null!;

  [JsonPropertyName("powerplants")]
  public PowerPlant[] PowerPlants { get; init; } = null!;
}

public record Fuels
{
  [JsonPropertyName("gas(euro/MWh)")]
  public float GasPriceInEuroPerMWh { get; init; }


  [JsonPropertyName("kerosine(euro/MWh)")]
  public float KerosinePriceInEuroPerMWh { get; init; }


  [JsonPropertyName("co2(euro/ton)")]
  public float Co2EmissionAllowancesPriceInEuroPerTon { get; init; }


  [JsonPropertyName("wind(%)")]
  public float WindPercentage { get; init; }
}

public record PowerPlant
{
  public string Name { get; init; } = null!;
  public string Type { get; init; } = null!;
  public float Efficiency { get; init; }

  [JsonPropertyName("pmin")]
  public uint MinimumPowerOutput { get; init; }

  [JsonPropertyName("pmax")]
  public uint MaximumPowerOutput { get; init; }
}

