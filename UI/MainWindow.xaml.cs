using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
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
using DTO;

namespace UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            comboBoxCountries.ItemsSource = GetCountryCodes();
        }


        private IEnumerable<string> GetCountryCodes()
        {
            var countries = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(x => new RegionInfo(x.LCID))
                .Select(x => x.ThreeLetterISORegionName).Where(x => !Char.IsNumber(x[0])).Distinct()
                .OrderBy(x => x);

            return countries;
        }
        private void Submit_OnClick(object sender, RoutedEventArgs e)
        {

            // Users dbo = new Users();
            //if (!string.IsNullOrEmpty(txtLogin.Text) && !string.IsNullOrEmpty(pass.Password))
            // {
            // TblUsers user = dbo.TblUsers.FirstOrDefault(t => t.Login == txtLogin.Text && t.Password == pass.Password);
            // if (user != null && comboBoxCountries.SelectedItem != null)
            //{
            CountryResearchRequest request = new CountryResearchRequest(1, comboBoxCountries.SelectedItem.ToString());

            MessageBus bus = new MessageBus();
            bus.PushMessageToExchange(request);
            MessageBox.Show("Message sent");



        }
    }
}
