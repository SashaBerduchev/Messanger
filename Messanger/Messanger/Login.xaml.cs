using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UIMessager;

namespace Messanger
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        IContract chanel;
        public Login()
        {
            Uri address = new Uri("net.tcp://localhost:4000/IContract");
            NetTcpBinding binding = new NetTcpBinding();
            EndpointAddress endpoint = new EndpointAddress(address);
            ChannelFactory<IContract> factory = new ChannelFactory<IContract>(binding, endpoint);
            chanel = factory.CreateChannel();

            InitializeComponent();
        }

        private void btnlog_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = new MainWindow();
            
            if(chanel.GetListofFriends().ToString()==Logintext.Text && chanel.GetPassListofFriends().ToString()==Pass.Password)
            {
                chanel.Login(Logintext.Text, Pass.Password);
                win.Show();
                win.name.Content = Logintext.Text;
            }

            else
            {
                MessageBox.Show("Error", "Loginor password is faild", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Login log = new Login();
            log.Close();
            Logintext.Clear();
            Pass.Clear();
        }

        private void btnaut_Click(object sender, RoutedEventArgs e)
        {
           
            chanel.Login(Logintext.Text, Pass.Password);
           
            Login log = new Login();
            log.Close();
            Logintext.Clear();
            Pass.Clear();
            MainWindow win = new MainWindow();
            win.Show();
            win.name.Content = Logintext.Text;
        }
    }
}
