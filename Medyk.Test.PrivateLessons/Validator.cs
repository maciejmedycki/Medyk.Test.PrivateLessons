using System;

namespace Medyk.Test.PrivateLessons
{
    public class Validator : IDataValidator
    {
        public bool Disabled { get; set; }
        public DateTime LastCheck { get; set; }

        public string GetValidationMessage(object data)
        {
            return "";
        }

        public bool IsValid(object data)
        {
            LastCheck = DateTime.Now;
            return true;
        }
    }
}