using System;
using System.Windows;

namespace PracticalWork_13
{
    public partial class Setting : Window
    {
        public Setting()
        {
            InitializeComponent();
        }
        private void FillSetting_Click(object sender, RoutedEventArgs e)
        {
            int value;
            if (int.TryParse(rowText.Text, out value))
            {
                if (value > 0)
                    SettingValues.NumberRow = value;
                else
                {
                    MessageBox.Show("Ошибка в количестве строк");
                    rowText.Focus();
                    return;
                }
            }
            if (int.TryParse(columnText.Text, out value))
            {
                if (value > 0)
                    SettingValues.NumberColumn = value;
                else
                {
                    MessageBox.Show("Ошибка в количестве строк");
                    columnText.Focus();
                    return;
                }
            }
            Close();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            rowText.Focus();
            rowText.Text = SettingValues.NumberRow.ToString();
            columnText.Text = SettingValues.NumberColumn.ToString();
        }
    }
}
