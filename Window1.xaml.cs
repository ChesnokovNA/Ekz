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
using System.Windows.Shapes;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;

namespace Ekz
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void btnOKDate_Click(object sender, RoutedEventArgs e)
        {
            //using (DataContext db = new DataContext(Properties.Settings.Default.srConnectionString))
            //{
            //    btnKl
            //    string date = dgklient.Text;
            //    Table<klients> uslugis = db.GetTable<klients>();
            //    Table<klients> klients = db.GetTable<klients>();
            //    var query = from a in zakazs
            //                join b in klients on a.id_klzak equals b.id_kl
            //                join c in uslugis on a.id_uslzak equals c.id_uslugi
            //                where a.date_zak == Convert.ToDateTime(date)
            //                select new { a.id_zakaz, a.date_zak, a.status_zak, b.id_kl, c.name_uslugi, c.cena_uslugi };
            //    dgklient.ItemsSource = query;
            //}
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }
        public void Update()
        {
            using (DataContext db = new DataContext(Properties.Settings.Default.srConnectionString))
            {

                Table<klients> klients = db.GetTable<klients>();
                dgklient.ItemsSource = klients;

            }
        }

        private void btnNewKl_Click(object sender, RoutedEventArgs e)
        {
            tbFIONew.IsEnabled = true;
            btnCanKl.IsEnabled = true;
            btnSaveNewKl.Visibility = Visibility.Visible;
            
        }

        private void btnSaveNewKl_Click(object sender, RoutedEventArgs e)
        {
            DataClasses2DataContext db = new DataClasses2DataContext();
            klients klients = new klients();
            klients.name_kl = tbFIONew.Text;
            db.GetTable<klients>().InsertOnSubmit(klients);
            db.SubmitChanges();
            Update();
            tbFIONew.Text = "";
            tbFIONew.IsEnabled = false;
            btnCanKl.IsEnabled = false;
        }

        private void btnRedKl_Click(object sender, RoutedEventArgs e)
        {
            tbFIONew.IsEnabled = true;
            btnCanKl.IsEnabled = true;
            btnSaveNewKl.Visibility = Visibility.Hidden;
            btnSaveRedKl.Visibility = Visibility.Visible;
            DataClasses2DataContext db = new DataClasses2DataContext();
            object item = dgKlient.SelectedItem;
            long vb = Convert.ToInt64((dgKlient.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
            tbFIONew.Text = (from u in db.klients
                             where u.id_kl == vb
                             select u.name_kl).FirstOrDefault();
        }

        private void btnCanKl_Click(object sender, RoutedEventArgs e)
        {
            tbFIONew.Text = "";
        }

        private void btnKl_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                using (DataContext db = new DataContext(Properties.Settings.Default.srConnectionString))
                {
                    Table<klients> q = db.GetTable<klients>();
                    var srcTable = q.Where(p => p.name_kl.Contains(btnKl.Text));
                    dgklient.ItemsSource = srcTable;
                }

            }
            catch (Exception)
            {
            }
        }

        private void btnSaveRedKl_Click(object sender, RoutedEventArgs e)
        {
            DataClasses2DataContext db = new DataClasses2DataContext();
            klients klients = new klients();
            klients.name_kl = tbFIONew.Text;
            db.GetTable<klients>().InsertOnSubmit(klients);
            db.SubmitChanges();
            Update();

            tbFIONew.Text = "";
            tbFIONew.IsEnabled = false;
            btnCanKl.IsEnabled = false;
        }
    }
}
