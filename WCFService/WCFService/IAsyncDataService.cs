using System;
using System.ServiceModel;

namespace WCFService
{
    [ServiceContract]
    public interface IAsyncDataService
    {
        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginGetData(AsyncCallback callback);

        Data EndGetData(IAsyncResult asyncResult);
    }
}
