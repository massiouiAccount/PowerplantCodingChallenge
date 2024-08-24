namespace PowerplantCodingChallenge.WebApi;

public static class DependencyInjection
{
  public static IServiceCollection AddWebApiServices(this IServiceCollection services)
  {
    services.AddSingleton<IProductionPlanService, ProductionPlanService>();

    return services;
  }
}
