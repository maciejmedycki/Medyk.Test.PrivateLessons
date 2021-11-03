using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medyk.Test.PrivateLessons
{
    public class ValidatorBetterOne : IDataValidator
    {
        public bool Disabled { get; set; } = true;
        public DateTime LastCheck { get; set; }

        public string GetValidationMessage(object data)
        {
            return "";
        }

        public bool IsValid(object data)
        {
            LastCheck = GetDateNow();
            return true;
        }

        public virtual DateTime GetDateNow()
        {
            return DateTime.Now; 
        }
    }
}
