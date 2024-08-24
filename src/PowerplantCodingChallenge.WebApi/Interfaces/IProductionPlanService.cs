namespace PowerplantCodingChallenge.WebApi.Interfaces;

public interface IProductionPlanService
{
  /// <summary>
  /// Calculates how much power each power plant should generate to meet the required load, considering fuel costs and plant efficiency.
  /// </summary>
  /// <param name="loadRequest">The load to be produced and details about available power plants and fuel costs.</param>
  /// <returns>A list showing how much power each plant should generate.</returns>
  IReadOnlyList<CalculatePowerProductionPlanResponse> CalculateProduction(CalculatePowerProductionPlanRequest loadRequest);
}
