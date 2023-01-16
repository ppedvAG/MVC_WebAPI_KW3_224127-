using Microsoft.Extensions.DependencyInjection;

namespace IOCContainerIntroSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ServiceCollection wird beim Programmstart befüllt
            //Befüllt wird der IOC-Container mit Diensten, die wir zum späteren Zeitpunkt nach der Initialsiierungphase benötigen


            IServiceCollection serviceCollection = new ServiceCollection();

            //Service TimeService wir in die Service-Collection abgelegt
            serviceCollection.AddSingleton<ITimeService, TimeService>();


            //Initialisierungsphas ist mit dem Befehlt BuildServiceProvider beendet. Danach können keine weitere Dienste hinzugenommen werden
            IServiceProvider serviceProvider =  serviceCollection.BuildServiceProvider();


            //Zugriff auf den Service Provider 
            
            //Variante 1: GetService -> Wenn ein Dienst (Interface oder Klasse) nicht im IOC Container gefunden wird, gibt GetService eine NULL zurück
            ITimeService? timeService1 = serviceProvider.GetService<ITimeService>();

            //Variante 2: Wenn ein Dienst (Interface oder Klasse) nicht im IOC Container gefunden wird, wird ein Fehler ausgegeben (Exception)
            ITimeService timeService2 = serviceProvider.GetRequiredService<ITimeService>();


        }
    }


    public interface ITimeService
    {
        //Soll den Zeitpunkt zurück geben, wann das Object erstellt wurde
        public string GetObjectInstanceTime();
    }

    public class TimeService : ITimeService
    {
        private DateTime _currentTime;

        public TimeService()
        {
            _currentTime = DateTime.Now;
        }


        public string GetObjectInstanceTime()
        {
            return _currentTime.ToShortTimeString();
        }
    }
}