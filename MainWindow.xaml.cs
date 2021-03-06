using System;
using System.Windows;
using System.Windows.Controls;
using LibMas;
using Microsoft.Win32;

namespace PracticalWork_13
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private int[,] matrix;


        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            if (matrix == null)
            {
                MessageBox.Show("Сначала создайте таблицу", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int[,] sortMatrix = new int[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    sortMatrix = matrix;
                }
            }
            int numberIdentical = 0;
            for (int i = 0; i < sortMatrix.GetLength(1); i++)
            {
                int coincidence = 0;
                for (int d = 0; d < sortMatrix.GetLength(1) - 1; d++)
                {
                   int MinRow = d;
                    for (int j = d + 1; j < sortMatrix.GetLength(0); j++)
                    {
                        if (sortMatrix[j, i] > sortMatrix[MinRow, i])
                        {
                            MinRow = j;
                            int storage = sortMatrix[d, i];
                            sortMatrix[d, i] = sortMatrix[MinRow, i];
                            sortMatrix[MinRow, i] = storage;
                        }
                    }
                    if (sortMatrix[d, i] == matrix[d, i])
                    {
                        coincidence++;
                    }
                }
                if (matrix.GetLength(0) == coincidence) numberIdentical++;
            }
            rezultOut.Text = Convert.ToString(numberIdentical);
        }

        private void FillMatrix_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(rowText.Text, out int row) && int.TryParse(columnText.Text, out int column) && row > 0 && column > 0)
            {
                matrix = new int[row, column];
                MatrixLogic.FillRandomValues(matrix);
                tabelMatrix.ItemsSource = VisualArray.ToDataTable(matrix).DefaultView;
                rezultOut.Clear();
                size.Text = string.Format("Размер таблицы: {0}х{1}", row, column);
            }
            else
            {
                MessageBox.Show("Введены неверные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            rowText.Clear();
            columnText.Clear();
            tabelMatrix.ItemsSource = null;
            rezultOut.Clear();
            if (matrix != null)
            {
                MatrixLogic.ClearMatrix(matrix);
            }

            size.Text = string.Format("Размер таблицы: 0х0");
            selectedText.Text = string.Format("Выбранная ячейка: 0х0");
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (matrix == null)
            {
                MessageBox.Show("Сначала создайте таблицу", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = ".txt";
            save.Filter = "Все файлы (*.*) | *.* | Текстовые файлы (*.txt*) | *.txt*";
            save.FilterIndex = 2;
            save.Title = "Сохранить Таблицы";
            if (save.ShowDialog() == true)
            {
                MatrixLogic.SaveMatrix(save.FileName, matrix);
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Все файлы (*.*)|*.*|Текстовые файлы|*.txt";
            open.FilterIndex = 2;
            open.Title = "Открытие таблицы";
            if (open.ShowDialog() == true)
            {
                if (open.FileName != string.Empty)
                {
                    MatrixLogic.OpenMatrix(open.FileName, out matrix);
                    rowText.Text = matrix.GetLength(0).ToString();
                    columnText.Text = matrix.GetLength(1).ToString();
                    tabelMatrix.ItemsSource = VisualArray.ToDataTable(matrix).DefaultView;
                    size.Text = string.Format("Размер таблицы: {0}х{1}", matrix.GetLength(0), matrix.GetLength(1));
                    selectedText.Text = string.Format("Выбранная ячейка: 0х0");
                }
            }
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Самсаков Андрей Александрович ИСП-31\nВариант 1\nПрактическая работа №13\nДана матрица размера M * N. Найти количество ее столбцов, элементы которых упорядочены по убыванию.", "Информация о программе", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Exit_Cick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            tabelMatrix.ItemsSource = null;
            rezultOut.Clear();
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            int indexColumn = e.Column.DisplayIndex;
            int indexRow = e.Row.GetIndex();
            if (!int.TryParse(((TextBox)e.EditingElement).Text.Replace('.', ','), out int value))
            {
                MessageBox.Show("Введены неверные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            matrix[indexRow, indexColumn] = value;
            rezultOut.Clear();
        }
        private void TabelMatrix_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (tabelMatrix.CurrentColumn == null) return;
            selectedText.Text = string.Format("Выбранная ячейка: {0}х{1}", tabelMatrix.Items.IndexOf(tabelMatrix.CurrentItem) + 1, tabelMatrix.CurrentColumn.DisplayIndex + 1);
        }
    }
}

