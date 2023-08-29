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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Tusindfryd_WPF
{
    /// <summary>
    /// Interaction logic for CreateFlowerSortDialog.xaml
    /// </summary>
    public partial class CreateFlowerSortDialog : Window
    {
        public CreateFlowerSortDialog()
        {
            InitializeComponent();
            try
            {
                string sti = Convert.ToString(tbSti);
                Uri imageUri = new Uri(sti, UriKind.Relative);
                BitmapImage imageBitmap = new BitmapImage(imageUri);
                picture.Source = imageBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Der skete en fejl under billedets uploading: " + ex.Message);
            }

        }

        private void bOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(tbName.Text)) ||
                    string.IsNullOrEmpty(Convert.ToString(tbSti.Text)) ||
                    string.IsNullOrEmpty(Convert.ToString(tbProductionTime.Text)) ||
                    string.IsNullOrEmpty(Convert.ToString(tbHalfLife.Text)) ||
                    string.IsNullOrEmpty(Convert.ToString(tbSize.Text)))
                {
                    MessageBox.Show("Alle felter skal være udfyldt");
                }
                else
                {
                   
                    this.DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here, e.g. display an error message to the user
                MessageBox.Show("Der opstod en fejl: " + ex.Message);
            }
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {
            bCancel.IsCancel = true;
        }
    }
}
