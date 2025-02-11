using CulinaryBook.ApplicationData;
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

namespace CulinaryBook.Pages
{
    /// <summary>
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg : Page
    {
        public Reg()
        {
            InitializeComponent();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            ApplicationData.AppFrame.frmMain.GoBack();
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (pswPass1.Password != pswPass2.Password)
            {
                btnRegister.IsEnabled = false;
                pswPass2.Background = Brushes.LightCoral;
                pswPass2.BorderBrush = Brushes.Red;
            }
            else
            {
                btnRegister.IsEnabled = true;
                pswPass2.Background = Brushes.LightGreen;
                pswPass2.BorderBrush = Brushes.Green;
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (AppConnect.model01.Authors.Count(x => x.Login == txbLogin.Text) > 0)
            {
                MessageBox.Show("Пользователь с таким логином уже существует", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (
                String.IsNullOrEmpty(txbLogin.Text) ||
                String.IsNullOrWhiteSpace(txbLogin.Text) ||

                String.IsNullOrEmpty(txbName.Text) ||
                String.IsNullOrWhiteSpace(txbName.Text) ||

                String.IsNullOrEmpty(pswPass1.Password) ||
                String.IsNullOrWhiteSpace(pswPass1.Password) ||

                String.IsNullOrEmpty(txbStage.Text) ||
                String.IsNullOrWhiteSpace(txbStage.Text) ||

                String.IsNullOrEmpty(dpDate.Text) ||
                String.IsNullOrWhiteSpace(dpDate.Text) ||

                String.IsNullOrEmpty(txbEmail.Text) ||
                String.IsNullOrWhiteSpace(txbEmail.Text) ||

                String.IsNullOrEmpty(txbPhone.Text) ||
                String.IsNullOrWhiteSpace(txbPhone.Text)
            )
            {
                MessageBox.Show("Не заполнены все поля", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                Authors userObj = new Authors()
                {
                    Login = txbLogin.Text,
                    Password = pswPass1.Password,
                    AuthorName = txbName.Text,
                    Stage = float.Parse(txbStage.Text),
                    BirthDay = dpDate.SelectedDate.Value,
                    Phone = txbPhone.Text,
                    Email = txbEmail.Text
                };
                AppConnect.model01.Authors.Add(userObj);
                AppConnect.model01.SaveChanges();
                MessageBox.Show("Регистрация успешно закончилась", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                ApplicationData.AppFrame.frmMain.GoBack();
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
