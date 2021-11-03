using System;
using System.Collections.Generic;

namespace Medyk.Test.PrivateLessons.AutoFixture.CustomObject
{
    public class DataHandler
    {
        private readonly List<DataDTO> _data = new List<DataDTO>();

        public List<DataDTO> Data
        {
            get
            {
                return _data;
            }
        }

        public void Add(DataDTO dataToAdd)
        {
            _ = dataToAdd ?? throw new ArgumentNullException(nameof(dataToAdd));
            _data.Add(dataToAdd);
        }
    }
}