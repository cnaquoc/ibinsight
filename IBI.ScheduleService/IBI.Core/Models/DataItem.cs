using System;
using System.Collections.Generic;
using System.Text;

namespace IBI.Core
{
    public class DataItem<T>
    {
        public T Value { get; private set; }
        public String Name { get; private set; }

        public DataItem(T value, String name)
        {
            Value = value;
            Name = name;
        }

        public DataItem()
        {
        }
    }
}
