using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace AsyncAwait
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // these ebooks are available for free: https://blogs.msdn.microsoft.com/mssmallbiz/2016/07/10/free-thats-right-im-giving-away-millions-of-free-microsoft-ebooks-again-including-windows-10-office-365-office-2016-power-bi-azure-windows-8-1-office-2013-sharepoint-2016-sha/

        private string pdf1 = "http://ligman.me/29jDRJf"; // Microsoft Azure Essentials Fundamentals of Azure
        private string pdf2 = "http://ligman.me/29acqhj"; // Microsoft Azure Essentials Azure Machine Learning
        private string pdf3 = "http://ligman.me/29fJJCS"; // Microsoft Azure Essentials Azure Automation

        private WebClient client = new WebClient();

        private void Download(object sender, RoutedEventArgs e)
        {
            // synchronous download
            //client.DownloadData(pdf1);
            //client.DownloadData(pdf2);
            //client.DownloadData(pdf3);
            //MessageBox.Show("Finished");

            // asynchronous download using events
            //client.DownloadDataCompleted += OnCompleted;
            //client.DownloadDataAsync(new Uri(pdf1));

            // tasks
            var task = client.DownloadDataTaskAsync(pdf1)
                             .ContinueWith(t1 => client.DownloadDataTaskAsync(pdf2)
                                                       .ContinueWith(t2 => client.DownloadDataTaskAsync(pdf3)
                                                                                 .ContinueWith(t3 => MessageBox.Show("Finished"))));
        }

        private int state = 0;

        private void OnCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            switch (state)
            {
                case 0:
                    client.DownloadDataAsync(new Uri(pdf2));
                    state++;
                    break;
                case 1:
                    client.DownloadDataAsync(new Uri(pdf3));
                    state++;
                    break;
                case 2:
                    MessageBox.Show("Finished");
                    break;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
