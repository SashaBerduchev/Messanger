using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SERVER
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string[] DataInTextBox
        {
            //get { return int.Parse(Mesgrid.ItemsSource.ToString()); }
            set { messagelist.ItemsSource = value; }
        }
        public void SetDataInTextBoxUser(IEnumerable value)
        {
                friendList.ItemsSource = value;
        }

        public void SetFileSream(IEnumerable value)
        {
            messagelist.ItemsSource = value;
        }

        public MainWindow()
        {
            
            Uri address = new Uri("net.tcp://localhost:4000/IContract"); // ADDRESS.   (A)
            // Указание привязки, как обмениваться сообщениями.
            NetTcpBinding binding = new NetTcpBinding();        // BINDING.   (B)
            // Указание контракта.
            Type contract = typeof(IContract);                        // CONTRACT.  (C) 
            // Создание провайдера Хостинга с указанием Сервиса.
            ServiceHost host = new ServiceHost(typeof(Messanger));
            // Добавление "Конечной Точки".
            host.AddServiceEndpoint(contract, binding, address);
            // Начало ожидания прихода сообщений.
            host.Open();

            InitializeComponent();
        }
    }
}
