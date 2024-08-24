using Microsoft.AspNetCore.Mvc;

namespace PowerplantCodingChallenge.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public abstract class WebApiBaseController : ControllerBase
{}
