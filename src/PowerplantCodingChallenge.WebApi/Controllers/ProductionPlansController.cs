using Microsoft.AspNetCore.Mvc;

namespace PowerplantCodingChallenge.WebApi.Controllers;

public class ProductionPlansController(IProductionPlanService productionPlanService) : WebApiBaseController
{
  private readonly IProductionPlanService _productionPlanService = productionPlanService;

  /// <summary>
  /// Calculates the optimal production plan for a given load request.
  /// </summary>
  /// <param name="loadRequest">The load request containing the required load in MWh, fuel costs, and power plant details.</param>
  /// <returns>
  /// A list of <see cref="CalculatePowerProductionPlanResponse"/> objects, 
  /// which indicate the amount of power each power plant should produce to meet the load.
  /// </returns>
  /// <response code="200">Returns the calculated production plan for the given load request.</response>
  /// <response code="400">Returns if the load request is invalid or null.</response>
  /// <example>
  /// POST /productionplan
  /// Content-Type: application/json
  /// {
  ///     "load": 910,
  ///     "fuels": {
  ///         "gas(euro/MWh)": 13.4,
  ///         "kerosine(euro/MWh)": 50.8,
  ///         "co2(euro/ton)": 20,
  ///         "wind(%)": 60
  ///     },
  ///     "powerplants": [
  ///         {
  ///             "name": "gasfiredbig1",
  ///             "type": "gasfired",
  ///             "efficiency": 0.53,
  ///             "pmin": 100,
  ///             "pmax": 460
  ///         },
  ///         ...
  ///     ]
  /// }
  /// </example>
  [HttpPost]
  [Route("/productionplan")]
  [ProducesResponseType(typeof(IList<CalculatePowerProductionPlanResponse>), 200)]
  [ProducesResponseType(400)]
  public ActionResult<IList<CalculatePowerProductionPlanResponse>> CalculateProductionPlan([FromBody] CalculatePowerProductionPlanRequest loadRequest)
  {
    IReadOnlyList<CalculatePowerProductionPlanResponse> result =
        _productionPlanService.CalculateProduction(loadRequest);

    return Ok(result);
  }
}
