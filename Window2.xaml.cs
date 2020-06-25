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
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void btZareg_Click(object sender, RoutedEventArgs e)
        {
            string s = tbPassReg.Text;
            char[] array = s.ToCharArray();
            int d = s.Length;
            int k = 0;
            int u = 0;
            int b = 0;
            char p = '$';
            char j = '!';
            char f = '@';
            char h = '%';
            char z = '^';
            char x = '#';
            for (int i = 0; i < d; i++)
            {
                if (char.IsUpper(array[i]))
                    k++;

            }
            for (int i = 0; i < d; i++)
            {
                if (char.IsNumber(array[i]))
                    u++;

            }
            for (int i = 0; i < d; i++)
            {
                if (Convert.ToChar(p) == (array[i]) || Convert.ToChar(j) ==
                (array[i]) || Convert.ToChar(f) == (array[i]) || Convert.ToChar(h) == (array[i]) ||
                Convert.ToChar(z) == (array[i]) || Convert.ToChar(x) == (array[i]))
                    b++;
            }
            if ((k >= 1) && (tbPassReg.Text.Length >= 6) && (u >= 1) && (b >= 1) && tbFIOReg.Text.Length != 0)
            {
                using (DataContext db = new DataContext(Properties.Settings.Default.srConnectionString))
                {
                    string fio = tbFIOReg.Text;
                    string log = tbLogReg.Text;
                    string pas = tbPassReg.Text;
                    string rol = tbRolReg.Text;
                    users user = new users();
                    user.fio_user = fio;
                    user.lod_user = log;
                    user.pas_user = pas;
                    user.rol_user = rol;
                    db.GetTable<users>().InsertOnSubmit(user);
                    db.SubmitChanges();
                    MessageBox.Show("Пользователь добавлен");
                    btnBack.Visibility = Visibility;
                    btZareg.Visibility = Visibility.Hidden;
                }
                
            }
            else
            {
                MessageBox.Show("Пароль должен содержать специальные символы ($ ! @ # ^ %), одну цифру и одну заглавную букву, а так же должен состоять минимум из 6 символов");
            }
        }
    
            private void btnBack_Click(object sender, RoutedEventArgs e)
            {
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Hide();
            }


        
}   }
