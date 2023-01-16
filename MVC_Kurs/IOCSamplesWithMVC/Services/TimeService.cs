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

    public class TimeService2 : ITimeService
    {
        private DateTime _currentTime;

        public TimeService2()
        {
            _currentTime = DateTime.Now;
        }


        public string GetObjectInstanceTime()
        {
            return "TimeService2: " + _currentTime.ToShortTimeString();
        }
    }
}
