using System.Text.Json;
using PowerplantCodingChallenge.WebApi.Interfaces;
using PowerplantCodingChallenge.WebApi.Models;
using PowerplantCodingChallenge.WebApi.Services;

namespace PowerplantCodingChallenge.WebApi.UnitTests.Services;

public class ProductionPlanServiceTests
{
  private readonly IProductionPlanService _productionPlanService;
  public ProductionPlanServiceTests()
  {
    _productionPlanService = new ProductionPlanService();
  }

  [Fact]
  public void CalculateProductionShouldReturnEmptyWhenLoadRequestIsNull()
  {
    IReadOnlyList<CalculatePowerProductionPlanResponse> expectedResult = [];

    IReadOnlyList<CalculatePowerProductionPlanResponse> result =
      _productionPlanService.CalculateProduction(null);

    Assert.Equal(result, expectedResult);
  }

  [Fact]
  public void CalculateProductionShouldReturnExpectedResultForV3()
  {
    var payload3Json = """
            {
        "load": 910,
        "fuels":
        {
          "gas(euro/MWh)": 13.4,
          "kerosine(euro/MWh)": 50.8,
          "co2(euro/ton)": 20,
          "wind(%)": 60
        },
        "powerplants": [
          {
            "name": "gasfiredbig1",
            "type": "gasfired",
            "efficiency": 0.53,
            "pmin": 100,
            "pmax": 460
          },
          {
            "name": "gasfiredbig2",
            "type": "gasfired",
            "efficiency": 0.53,
            "pmin": 100,
            "pmax": 460
          },
          {
            "name": "gasfiredsomewhatsmaller",
            "type": "gasfired",
            "efficiency": 0.37,
            "pmin": 40,
            "pmax": 210
          },
          {
            "name": "tj1",
            "type": "turbojet",
            "efficiency": 0.3,
            "pmin": 0,
            "pmax": 16
          },
          {
            "name": "windpark1",
            "type": "windturbine",
            "efficiency": 1,
            "pmin": 0,
            "pmax": 150
          },
          {
            "name": "windpark2",
            "type": "windturbine",
            "efficiency": 1,
            "pmin": 0,
            "pmax": 36
          }
        ]
      }
      """;
    var response3Json = """
            [
          {
              "name": "windpark1",
              "p": 90.0
          },
          {
              "name": "windpark2",
              "p": 21.6
          },
          {
              "name": "gasfiredbig1",
              "p": 460.0
          },
          {
              "name": "gasfiredbig2",
              "p": 338.4
          },
          {
              "name": "gasfiredsomewhatsmaller",
              "p": 0.0
          },
          {
              "name": "tj1",
              "p": 0.0
          }
      ]
      """;

    var loadRequest = JsonSerializer.Deserialize<CalculatePowerProductionPlanRequest>(payload3Json);
    var expectedProductionPlan =JsonSerializer.Deserialize<CalculatePowerProductionPlanResponse[]>(response3Json);

    var productionPlan = _productionPlanService.CalculateProduction(loadRequest);

    var totalOfProducedPowerInMWh = productionPlan.Sum(productionPlan => productionPlan.PowerProducedInMWh);

    Assert.Equal(totalOfProducedPowerInMWh, loadRequest.LoadInMWh); 

    // TODO: Assert the order for cost-effectiveness 
  }
}
