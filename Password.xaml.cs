using System;
using System.Windows;

namespace PracticalWork_13
{
    public partial class Password : Window
    {
        public Password()
        {
            InitializeComponent();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            passwordEntered.Focus();
        }

        private void Authorization(object sender, RoutedEventArgs e)
        {
            if (passwordEntered.Password == "123") Close();
            else
            {
                MessageBox.Show("Введен неверный пароль, попробуйте еще раз или обратитесь в службу поддержки");
                passwordEntered.Focus();
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Owner.Close();
        }
    }
}
