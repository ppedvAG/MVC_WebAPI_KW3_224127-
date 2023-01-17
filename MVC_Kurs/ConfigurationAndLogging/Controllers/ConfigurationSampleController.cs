using ConfigurationAndLogging.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConfigurationAndLogging.Controllers
{
    public class ConfigurationSampleController : Controller
    {
        private ILogger<ConfigurationSampleController> _logger;

        public ConfigurationSampleController(ILogger<ConfigurationSampleController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index([FromServices] IConfiguration configuration)
        {
            string myKeyValue = configuration["MyKey"];
            string title = configuration["Position:Title"];
            string name = configuration["Position:Name"];
            string loggingValue = configuration["Logging:LogLevel:Default"];


            _logger.LogInformation("ConfigurationSampleController->Index");

            return Content($"MyKey value: {myKeyValue} \n" +
                $"Title: {title} \n" +
                $"Name: {name} \n" +
                $"Default Log Level: {loggingValue}");
        }


        public IActionResult Sample2([FromServices] IOptionsSnapshot<PositionOptions> options)
        {
            PositionOptions positionConfigurations = options.Value;

            _logger.LogInformation("ConfigurationSampleController->Sample2");



            return Content($"{positionConfigurations.Name} {positionConfigurations.Title}");
        }

        public IActionResult Sample3([FromServices] IOptionsSnapshot<GameSettings> gameSettingsConfiguration)
        {

            _logger.LogInformation("ConfigurationSampleController->Sample3");

            GameSettings gameSettings = gameSettingsConfiguration.Value;

            return View(gameSettings);
        }


    }
}
