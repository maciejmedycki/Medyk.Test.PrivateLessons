using System;

namespace Medyk.Test.PrivateLessons.AutoFixture
{
    public class Person
    {
        private string _pesel;
        private string _surname;

        public string Name { get; set; }

        public string Pesel { get { return _pesel; } set { ValidatePesel(value); _pesel = value; } }

        public string Surname
        {
            get { return _surname; }
            set
            {
                if (value.Length > 50)
                    throw new ArgumentException("Surname is too long. Must be less than 51.", nameof(Surname));
                _surname = value;
            }
        }

        private void ValidatePesel(string pesel)
        {
            if (pesel.Length != 11)
                throw new ArgumentException(nameof(pesel));
        }
    }
}