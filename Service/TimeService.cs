namespace AspHangFire.Service
{
    public interface ITimeService
    {
        void PrintNow();
    }
    public class TimeService : ITimeService
    {
        private readonly ILogger<TimeService> _logger; 
        
        public TimeService(ILogger<TimeService> logger)
        {
            _logger = logger;
        }

        public void PrintNow()
        {
            _logger.LogInformation(DateTime.Now.ToString("dd/mm/yyyy hh:mm:ss:tt"));
        }
    }

    
}
