﻿using System;
using System.Runtime.Serialization;

namespace WCFService
{
    public class DataService : IDataService
    {
        public Data GetData()
        {
            return new Data(Guid.NewGuid().ToString());
        }
    }

    [DataContract]
    public class Data
    {
        [DataMember]
        public string Text { get; set; }

        public Data(string text)
        {
            Text = text;
        }
    }
}
