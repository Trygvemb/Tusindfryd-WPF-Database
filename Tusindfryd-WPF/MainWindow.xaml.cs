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
using System.Data.Common;


namespace Tusindfryd_WPF
{

    public partial class MainWindow : Window
    {
        List<Flowersort> flowersorts;
        public MainWindow()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build(); // Husk at selve json filen skal have navnet appsettings.json
            string connectionString = config.GetConnectionString("MyDBConnection");
            InitializeComponent();
            flowersorts = new List<Flowersort>();
            RetrieveAll(connectionString);
            AddToTextBlock();
        }

        // Henter data fra dabasen og skriver det ind i flowersorts listen. Jeg var noed til at lave metoden her da jeg ellers ikke kunne tilgaa flowersorts listen.
        public void RetrieveAll(string connectionString)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Name, PicturePath, ProductionTime, HalfLife, Size FROM Flowersorts", con);//skriver SQL i C#
                using (SqlDataReader dr = cmd.ExecuteReader())  // laeser dataen
                {
                    while (dr.Read())
                    {
                        Flowersort flowersort = new Flowersort(

                            dr["Name"].ToString(),
                            dr["PicturePath"].ToString(),
                            int.Parse(dr["ProductionTime"].ToString()),
                            int.Parse(dr["HalfLife"].ToString()),
                            int.Parse(dr["Size"].ToString())
                            );

                        flowersorts.Add(flowersort);// tilfoejer det hentede hata til flowersorts 
                    }

                }
            }
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

                // Configurerer Databasen. husk at bruge de 3 using statements; System.Data; Microsoft.Extensions.Configuration.Json; Microsoft.Extensions.Configuration;
                IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build(); // Husk at selve json filen skal have navnet appsettings.json
                string connectionString = config.GetConnectionString("MyDBConnection");

                // kaller metoden og Gemmer informationen i databsen
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
