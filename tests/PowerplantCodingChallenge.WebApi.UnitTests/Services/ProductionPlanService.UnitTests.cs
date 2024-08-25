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
}
