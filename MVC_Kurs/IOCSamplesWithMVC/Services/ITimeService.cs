namespace IOCSamplesWithMVC.Services
{
    public interface ITimeService
    {
        //Soll den Zeitpunkt zurück geben, wann das Object erstellt wurde
        public string GetObjectInstanceTime();
    }

}
