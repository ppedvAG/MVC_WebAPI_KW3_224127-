using ConfigurationAndLogging.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConfigurationAndLogging.Controllers
{
    public class ConfigurationSampleController : Controller
    {
        public IActionResult Index([FromServices] IConfiguration configuration)
        {
            string myKeyValue = configuration["MyKey"];
            string title = configuration["Position:Title"];
            string name = configuration["Position:Name"];
            string loggingValue = configuration["Logging:LogLevel:Default"];

            return Content($"MyKey value: {myKeyValue} \n" +
                $"Title: {title} \n" +
                $"Name: {name} \n" +
                $"Default Log Level: {loggingValue}");
        }


        public IActionResult Sample2([FromServices] IOptionsSnapshot<PositionOptions> options)
        {
            PositionOptions positionConfigurations = options.Value;
            return Content($"{positionConfigurations.Name} {positionConfigurations.Title}");
        }

        public IActionResult Sample3([FromServices] IOptionsSnapshot<GameSettings> gameSettingsConfiguration)
        {
            GameSettings gameSettings = gameSettingsConfiguration.Value;

            return Content($"");
        }


    }
}
