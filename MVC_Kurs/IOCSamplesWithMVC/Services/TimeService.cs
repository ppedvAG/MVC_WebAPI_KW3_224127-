namespace IOCSamplesWithMVC.Services
{
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
