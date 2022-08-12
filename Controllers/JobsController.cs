using System;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PocOptimusHangfire.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class JobsController : ControllerBase
  {
    private readonly ILogger<JobsController> _logger;

    public JobsController(ILogger<JobsController> logger)
    {
      _logger = logger;
    }

    [HttpPost]
    [Route("ImediatoSucesso")]
    public IActionResult ImediatoSucesso()
    {
      var jobId = BackgroundJob.Enqueue(() => Service.DelaySuccess());

      return Ok(new { jobId = jobId });
    }
    [HttpPost]
    [Route("ImediatoFalha")]
    public IActionResult ImediatoFalha()
    {
      var jobId = BackgroundJob.Enqueue(() => Service.DelayFail());

      return Ok(new { jobId = jobId });
    }
    [HttpPost]
    [Route("DiarioSucesso")]
    public IActionResult DiarioSucesso()
    {
      var jobId = BackgroundJob.Enqueue(() => Service.DailySuccess());

      return Ok(new { jobId = jobId });
    }
    [HttpPost]
    [Route("InicioAtrasado")]
    public IActionResult InicioAtrasado()
    {
      var jobId = BackgroundJob.Schedule(() => Service.Delayed(),TimeSpan.FromMinutes(1));
      return Ok();
    }
    [HttpPost]
    [Route("Cascata/{jobId}")]
    [Obsolete]
    public IActionResult Cascata(string jobId)
    {
      var jobIdResult = BackgroundJob.ContinueWith(jobId,() => Console.WriteLine("Continuando e executando o servico B!"));
      
      return Ok(jobIdResult);
    }
  }
}
