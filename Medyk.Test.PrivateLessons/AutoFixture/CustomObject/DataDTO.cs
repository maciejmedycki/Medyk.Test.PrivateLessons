namespace Medyk.Test.PrivateLessons.AutoFixture.CustomObject
{
    public class DataDTO
    {
        public string Name;
        public string WasISet;
        private object _amINull;
        private int _id;
        private byte _youShallNotTouchMe = 5;

        public DataDTO(int id, string name)
        {
            _id = id;
            Name = name;
            IsValid = id > 0 && !string.IsNullOrEmpty(name);
        }

        public double IHaveGetAndSet { get; set; }
        public double IHaveOnlyGet { get; }
        public bool IsValid { get; }

        public string Title { get; set; }
    }
}