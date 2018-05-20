using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFService
{
    public class AsyncDataService : IAsyncDataService
    {
        public IAsyncResult BeginGetData(AsyncCallback callback)
        {
            throw new NotImplementedException();
        }

        public Data EndGetData(IAsyncResult asyncResult)
        {
            throw new NotImplementedException();
        }
    }
}
