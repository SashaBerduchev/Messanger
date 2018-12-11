
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SERVER
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    class Messanger : IContract
    {
        static LoginData l = null;
        string[] messagedata;

        byte[] MyData;
        dynamic listfriend;
        dynamic passlist;
        List<MessageDataLoad> messange;

        public string[] GetData()
        {
            messange = new List<MessageDataLoad>();
            MessangerDatabaseEntities contex = new MessangerDatabaseEntities();
            messange = contex.MessageDataLoad.ToList();
            messagedata = messange.Select(x => x.Message).ToArray();
            return messagedata;
        }

        public void Login(string login, string pass)
        {

            MessangerDatabaseEntities contex = new MessangerDatabaseEntities();
            var log = new LoginData
            {
                LogName = login,
                Pass = pass
            };
            l = log;
            contex.LoginData.Add(log);
            contex.SaveChanges();
            loging = contex.LoginData.ToList();
            listfriend = loging.Select(x => new { x.LogName, x.Pass }).ToArray();
            SetDataToWindowUser();
        }

        List<LoginData> loging;

        public string[] GetListofFriends()
        {
            MessangerDatabaseEntities contex = new MessangerDatabaseEntities();
            loging = contex.LoginData.ToList();
            listfriend = loging.Select(x => x.LogName).ToArray();
            passlist = loging.Select(x => x.Pass).ToArray();
            return listfriend;

        }
        public string[] GetPassListofFriends()
        {
            MessangerDatabaseEntities contex = new MessangerDatabaseEntities();
            loging = contex.LoginData.ToList();
            passlist = loging.Select(x => x.Pass).ToArray();
            return passlist;

        }
        public void SetData(string messageset, string listfriend)
        {
            MessangerDatabaseEntities contex = new MessangerDatabaseEntities();
            //var c = contex.LoginData.First(x => x.Id == listfriend);
            var message = new MessageDataLoad
            {
                Message = messageset,
                LoginData = l

            };

            contex.MessageDataLoad.Add(message);
            contex.SaveChanges();
            messange = contex.MessageDataLoad.ToList();
            messagedata = messange.Select(x => x.Message).ToArray();
            SetDataToWindow();
        }



        public int LoadFileStream(string filename)
        {

            MessangerDatabaseEntities contex = new MessangerDatabaseEntities();
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            MyData = new byte[fs.Length];

            using (var reader = new BinaryReader(fs))
            {
                MyData = reader.ReadBytes((int)fs.Length);
            }

            var dataset = new DataDbLoadFile
            {
                DataFile = MyData
            };
            contex.DataDbLoadFile.Add(dataset);
            contex.SaveChanges();
            int data=MyData.Length;
            
            return data;

        }

        public byte[] ImageLoadStream()
        {
            var contex = new MessangerDatabaseEntities();
            //byte[] bytesArr = contex.DataDbLoadFile.FirstOrDefault(f=>f.Id==1).DataFile;
            byte[] bytearr = contex.DataDbLoadFile.FirstOrDefault(f => f.Id == 1).DataFile;
            return bytearr;
            //MemoryStream memstr = new MemoryStream(bytesArr);
            //var img = System.Drawing.Image.FromStream(memstr);
            //return img;

        }

   

        private void SetDataToWindow()
        {
            var window = Application.Current.MainWindow as MainWindow;
            window.DataInTextBox = messagedata.ToArray();
        }

        private void SetDataToWindowUser()
        {
            var window = Application.Current.MainWindow as MainWindow;
            window.SetDataInTextBoxUser(listfriend);
        }

        private void SetFileLoadStream()
        {
            var window = Application.Current.MainWindow as MainWindow;
            window.SetFileSream(MyData);
        }
       
    }
}
