using Messanger;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UIMessager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IContract chanel;
        public MainWindow()
        {
            Uri address = new Uri("net.tcp://localhost:4000/IContract");
            NetTcpBinding binding = new NetTcpBinding();
            EndpointAddress endpoint = new EndpointAddress(address);
            ChannelFactory<IContract> factory = new ChannelFactory<IContract>(binding, endpoint);
            chanel = factory.CreateChannel();
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            chanel.SetData(sendtext.Text, userlist.SelectedItems.ToString());
            MessageBox.Show("Сообщение отправлен", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            sendtext.Clear();
        }


        private void refresh_Btn_Click(object sender, RoutedEventArgs e)
        {

            Thread thr = new Thread(Update);
            thr.Start();
            //Thread.Sleep(200);

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            MemoryStream ms = new MemoryStream();
            byte[] i = chanel.ImageLoadStream();
            MemoryStream memstr = new MemoryStream(i);
            var img = System.Drawing.Image.FromStream(memstr);
            
            ms.Seek(0, SeekOrigin.Begin);
            bi.StreamSource = memstr;
            bi.EndInit();

            image.Source = bi;
        }


        public void Update()
        {
            Dispatcher.Invoke(() =>
            {
                listtext.ItemsSource = chanel.GetData();
            });
        }
        private void refresh_Btn_friend_Click(object sender, RoutedEventArgs e)
        {
            Thread thr = new Thread(UpdateListOfFriends);
            thr.Start();
        }

        public void UpdateListOfFriends()
        {
            Dispatcher.Invoke(() =>
            {
                listfrend.ItemsSource = chanel.GetListofFriends();
            });
        }
        private void listfrend_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            userlist.Items.Add(listfrend.SelectedItem);

        }

        private void fileload_Click(object sender, RoutedEventArgs e)
        {
            Thread thr = new Thread(LoadFile);
            thr.Start();
        }

        private void LoadFile()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.ShowDialog();
            MessageBox.Show("Loading", "Warning");

            int filelength = chanel.LoadFileStream(op.FileName);
            // MessageBox.Show(ex, "Error",  MessageBoxButton.OK, MessageBoxImage.Error);
            MessageBox.Show("Файл отправлено...", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);

        }
    }
}
