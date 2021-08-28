using System;
using System.Collections.Generic;
using System.Linq;
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
using Microsoft.Win32;
using System.IO;
using System.Threading;

namespace ProxyTester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OpenFileDialog _openFileDialog;
        private List<string> _proxyStringList;
        private readonly List<Proxy.Proxy> _proxyList;
        public MainWindow()
        {
            InitializeComponent();
            _openFileDialog = new OpenFileDialog();
            _openFileDialog.Title = "Please choose proxy file";
            _proxyList = new List<Proxy.Proxy>();

            System.Timers.Timer aTimer = new System.Timers.Timer();

            aTimer.Interval = 1000;
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(delegate ()
            {
                Queue.Content = ThreadPool.PendingWorkItemCount;
            }));
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //WPF wants to keep this
        }

        async private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_openFileDialog.ShowDialog() is true)
                _proxyStringList = new List<string>(await File.ReadAllLinesAsync(_openFileDialog.FileName));

            using(new Helpers.WaitCursor())
            {
                foreach (string _proxy in _proxyStringList)
                {
                    try
                    {
                        _proxyList.Add(new Proxy.Proxy(_proxy, ProxyList, Application.Current.Dispatcher));
                    }
                    catch (Exception ex)
                    {
                        if (ex is FormatException || ex is Proxy.InvalidProxyException)
                            continue;
                        throw new Exception("Unknown error");
                    }

                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _proxyList.Clear();
            _proxyStringList.Clear();
            ProxyList.Items.Clear();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _proxyList.ForEach(prox => prox.Run());
        }
    }
}
