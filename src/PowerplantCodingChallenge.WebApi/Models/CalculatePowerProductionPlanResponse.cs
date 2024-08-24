using System.Text.Json.Serialization;

namespace PowerplantCodingChallenge.WebApi.Models;

public record CalculatePowerProductionPlanResponse
{
  [JsonPropertyName("name")]
  public string PowerPlantName { get; init; } = null!;

  [JsonPropertyName("p")]
  public decimal PowerProducedInMWh { get; init; }
}
