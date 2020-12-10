using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly UserService _userService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, UserService userService)
        {
            _logger = logger;

            _userService = userService;
        }

        [HttpGet]
        public void Get()
        {
            _userService.CreateUser(new UserView { Username = "ExampleUser", Password = "Example123" });
        }
    }
}
//создать метод createUser, который принимает UserView