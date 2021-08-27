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

namespace ProxyTester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OpenFileDialog openFileDialog;
        private String[] proxyList;
        public MainWindow()
        {
            InitializeComponent();
            this.openFileDialog = new OpenFileDialog();
            this.openFileDialog.Title = "Please choose proxy file";
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //WPF wants to keep this
        }

        async private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.openFileDialog.ShowDialog() is true)
                this.proxyList = await File.ReadAllLinesAsync(this.openFileDialog.FileName);
            
        }
    }
}
