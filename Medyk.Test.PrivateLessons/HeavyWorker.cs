using System;

namespace Medyk.Test.PrivateLessons
{
    public class HeavyWorker
    {
        private readonly IApiAccess _apiAccess;
        private readonly ILogger _logger;
        private readonly IDataValidator _validator;

        public HeavyWorker(IApiAccess apiAccess, 
            ILogger logger, IDataValidator validator)
        {
            _apiAccess = apiAccess;
            _logger = logger;
            _validator = validator;
            _logger.Log($"{nameof(HeavyWorker)} created");
        }

        public ICounter Counter { get; set; }

        public void SendData(int id, string name, object payload)
        {
            _logger.Log($"Sending data {id} {name}");
            var data = (id, name, payload);
            try
            {
                _apiAccess.SendData(data);
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message);
            }
            if (Counter != null)
            {
                Counter.Count++;
                //Counter.IDoNothing();
            }
            _logger.Log($"Data for {id} sent");
        }

        public void ValidateAndSendData(int id, string name, object payload)
        {
            if (!_validator.Disabled && _validator.IsValid(payload))
            {
                SendData(id, name, payload);
            }
        }
    }
}