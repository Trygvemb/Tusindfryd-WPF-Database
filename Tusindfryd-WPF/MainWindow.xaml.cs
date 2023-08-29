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
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;


namespace Tusindfryd_WPF
{

    public partial class MainWindow : Window
    {
        List<Flowersort> flowersorts;
        public MainWindow()
        {
            InitializeComponent();
            flowersorts = new List<Flowersort>();
        }

        private void bOpret_Click(object sender, RoutedEventArgs e)
        {
            CreateFlowerSortDialog dialog = new CreateFlowerSortDialog();
            if ( dialog.ShowDialog() == true)
            {
                Flowersort flowersort = new Flowersort(
                dialog.tbName.Text, 
                dialog.tbSti.Text,
                Convert.ToInt32(dialog.tbProductionTime.Text),
                Convert.ToInt32(dialog.tbHalfLife.Text),
                Convert.ToInt32(dialog.tbSize.Text));

                IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                string connectionString = config.GetConnectionString("MyDBConnection");

                flowersort.InsertIntoDatabase(connectionString);

                flowersorts.Add(flowersort);
            }
            AddToTextBlock();
        }

        public void AddToTextBlock()
        {
            string line = "";

            foreach (Flowersort flowersort in flowersorts)
            {
                line += $"{flowersort.Name}, {flowersort.ProductionTime}, {flowersort.HalfLife}, {flowersort.Size}, {flowersort.PicturePath}\n";
            }
            tbSorter.Text = line;
        }
    }
}
