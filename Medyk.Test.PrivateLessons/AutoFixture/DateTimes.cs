using System;

namespace Medyk.Test.PrivateLessons.AutoFixture
{
    public class DateTimes
    {
        public DateTime AtNoon(DateTime dateTime, string uselessParam, int anotherUselessParam)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 12, 0, 0);
        }
    }
}