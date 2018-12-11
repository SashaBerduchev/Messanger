using SERVER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


namespace Messanger
{
    [ServiceContract]
    interface IContract
    {
        [OperationContract]
        void Login(string login, string pass);
        [OperationContract]
        string[] GetListofFriends();

        [OperationContract]
        string[] GetData();
        [OperationContract]
        void SetData(string messageset, string listfriend);
        [OperationContract]
        string[] GetPassListofFriends();
        [OperationContract]
        int LoadFileStream(string filename);
        [OperationContract]
        byte[] ImageLoadStream();
    }
}
