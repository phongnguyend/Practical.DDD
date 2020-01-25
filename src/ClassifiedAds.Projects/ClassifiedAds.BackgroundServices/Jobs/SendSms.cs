using Microsoft.Extensions.Logging;

namespace ClassifiedAds.BackgroundServices.Jobs
{
    public class SendSms
    {
        private readonly ILogger _logger;

        public SendSms(ILogger<SendEmail> logger)
        {
            _logger = logger;
        }

        public void Run()
        {
            _logger.LogInformation("Inside Send() method.");
        }
    }
}
