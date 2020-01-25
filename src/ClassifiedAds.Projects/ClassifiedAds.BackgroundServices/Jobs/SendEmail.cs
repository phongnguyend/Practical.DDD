using Microsoft.Extensions.Logging;

namespace ClassifiedAds.BackgroundServices.Jobs
{
    public class SendEmail
    {
        private readonly ILogger _logger;

        public SendEmail(ILogger<SendEmail> logger)
        {
            _logger = logger;
        }

        public void Run()
        {
            _logger.LogInformation("Inside Send() method.");
        }
    }
}
