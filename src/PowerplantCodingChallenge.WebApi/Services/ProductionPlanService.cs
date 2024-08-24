namespace PowerplantCodingChallenge.WebApi.Services;

public class ProductionPlanService : IProductionPlanService
{
  public IReadOnlyList<CalculatePowerProductionPlanResponse> CalculateProduction(CalculatePowerProductionPlanRequest loadRequest)
  {
    if (loadRequest is null) return [];

    IList<CalculatePowerProductionPlanResponse> productionPlanResponses = [];
    decimal remainingLoadToBeProduced = loadRequest.LoadInMWh;

    PowerPlant[] sortedPowerPlantsByCost =
        SortPowerPlantsByCostPerMWh(loadRequest.PowerPlants, loadRequest.Fuels);

    foreach (PowerPlant powerPlant in sortedPowerPlantsByCost)
    {
      if (remainingLoadToBeProduced == decimal.Zero) break;

      // Determine the amount of power to be produced by the current power plant
      decimal powerToBeProduced =
          Math.Min(powerPlant.MaximumPowerOutput, remainingLoadToBeProduced);

      // Check if the current power plant can meet the minimum production requirement
      bool canProduceMinimumPower = powerToBeProduced >= powerPlant.MinimumPowerOutput;

      if (!canProduceMinimumPower)
      {
        productionPlanResponses.Add(new CalculatePowerProductionPlanResponse
        {
          PowerPlantName = powerPlant.Name,
          PowerProducedInMWh = decimal.Zero
        });
        continue;
      }

      productionPlanResponses.Add(new CalculatePowerProductionPlanResponse
      {
        PowerPlantName = powerPlant.Name,
        PowerProducedInMWh = powerToBeProduced
      });

      remainingLoadToBeProduced -= powerToBeProduced;
    }

    return productionPlanResponses.AsReadOnly();
  }

  /// <summary>
  /// Sorts the power plants by their cost per MWh in ascending order. 
  /// The cost is calculated based on fuel prices and plant efficiency.
  /// </summary>
  /// <param name="powerPlants">An array of power plants to be sorted.</param>
  /// <param name="fuelCosts">The current fuel costs for Gas, Kerosine, and other fuels.</param>
  /// <returns>An array of power plants sorted by the cost of producing 1 MWh.</returns>
  private static PowerPlant[] SortPowerPlantsByCostPerMWh(PowerPlant[] powerPlants, Fuels fuelCosts)
  {
    // Order power plants by the calculated cost per MWh
    PowerPlant[] sortedByCost =
        [.. powerPlants.OrderBy(plant => CalculateProductionCostPerMWh(plant, fuelCosts))];

    return sortedByCost;
  }

  /// <summary>
  /// Calculates the cost of producing 1 MWh of electricity for a given power plant.
  /// The cost depends on the type of power plant, its efficiency, and the current fuel prices.
  /// </summary>
  /// <param name="powerPlant">The power plant for which to calculate the cost.</param>
  /// <param name="fuelCosts">The current fuel costs for Gas, Kerosine, etc.</param>
  /// <returns>The cost per MWh of electricity production.</returns>
  /// <exception cref="UnsupportedPowerPlantTypeException">
  /// Thrown when the power plant type is not supported.
  /// </exception>
  private static decimal CalculateProductionCostPerMWh(PowerPlant powerPlant, Fuels fuelCosts)
  {
    decimal unitsOfFuelRequired = CalculateRequiredUnitsOfFuel(powerPlant.Efficiency);

    return powerPlant.Type switch
    {
      PowerPlantType.WindTurbine => decimal.Zero,
      PowerPlantType.GasFired => unitsOfFuelRequired * (decimal)fuelCosts.GasPriceInEuroPerMWh,
      PowerPlantType.TurboJet => unitsOfFuelRequired * (decimal)fuelCosts.KerosinePriceInEuroPerMWh,
      _ => throw new UnsupportedPowerPlantTypeException(powerPlant.Type),
    };

    // Calculate the required units of fuel to produce 1 MWh of electricity based on efficiency
    static decimal CalculateRequiredUnitsOfFuel(float efficiency)
    {
      // Required units of fuel = 1 / efficiency
      // Example: If efficiency is 0.53, you need 1 / 0.53 units of fuel to produce 1 MWh
      return (ushort)Unit.One / (decimal)efficiency;
    }
  }
}
