using Microsoft.AspNetCore.Mvc;
using RazorAdvanced.Models;

namespace RazorAdvanced.ViewComponents
{
    //InvokeAsync wird immer augerufen, ist unsere Möglichkeit mit EF Core etc zu arbeiten.
    //Das Ergebnis wird als IViewComponentResult mit return View(Model) an die Default.cshtml übergeben  
    //Bei der Default.cshtml müssen wir aufpassen, der Speicherort unterliegt auch wieder einer Konvention \Views\Shared\Components\WeatherWidget(NameDerViewComponet)\Default.chhtml
    public class WeatherWidgetViewComponent : ViewComponent
    {
        //View Component haben eine Methode, die immer aufgerufen wird
        public async Task<IViewComponentResult> InvokeAsync(string cityName)
        {
            //WeatherInfo ist das Model
            WeatherInfo response = await GetWeatherInfo(cityName);
            return View(response);
        }


        private async Task<WeatherInfo> GetWeatherInfo(string cityName)
        {
            var obj = new WeatherInfo();
            obj.City = cityName;
            obj.Date = DateTime.Now.ToString("dddd h:mm tt");

            if (cityName == "London")
            {
                obj.Icon = "sunny.png";
                obj.Condition = "Sunny";
                obj.Precipitation = "7%";
                obj.Humidity = "70%";
                obj.Wind = "6 km/h";
            }
            else if (cityName == "New York")
            {
                obj.Icon = "partly_cloudy.png";
                obj.Condition = "Partly Cloudy";
                obj.Precipitation = "17%";
                obj.Humidity = "80%";
                obj.Wind = "16 km/h";
            }
            else if (cityName == "Paris")
            {
                obj.Icon = "rain.png";
                obj.Condition = "Rain";
                obj.Precipitation = "67%";
                obj.Humidity = "20%";
                obj.Wind = "9 km/h";
            }
            else if (cityName == "Berlin")
            {
                obj.Icon = "sunny.png";
                obj.Condition = "Sunny";
                obj.Precipitation = "5%";
                obj.Humidity = "70%";
                obj.Wind = "9 km/h";
            }

            return obj;
        }
    }
}
