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
            int kolCol = 0;
            for (int NumCol = 0; NumCol < sortMatrix.GetLength(1); NumCol++)
            {
                int coincidence = 0;
                for (int NumRow = 0; NumRow < sortMatrix.GetLength(1) - 1; NumRow++)
                {
                   int MinRow = NumRow;
                    for (int j = NumRow + 1; j < sortMatrix.GetLength(0); j++)
                    {
                        if (sortMatrix[j, NumCol] > sortMatrix[MinRow, NumCol])
                        {
                            MinRow = j;
                            int Temp = sortMatrix[NumRow, NumCol];
                            sortMatrix[NumRow, NumCol] = sortMatrix[MinRow, NumCol];
                            sortMatrix[MinRow, NumCol] = Temp;
                        }
                    }
                    if (sortMatrix[NumRow, NumCol] == matrix[NumRow, NumCol])
                    {
                        coincidence++;
                    }
                }
                if (matrix.GetLength(0) == coincidence) kolCol++;
            }
            rezultOut.Text = Convert.ToString(kolCol);
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(rowText.Text, out int row) && int.TryParse(columnText.Text, out int column) && row > 0 && column > 0)
            {
                matrix = new int[row, column];
                MatrixLogic.FillRandomValues(matrix);
                initialTable.ItemsSource = VisualArray.ToDataTable(matrix).DefaultView;
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
            initialTable.ItemsSource = null;
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
                    initialTable.ItemsSource = VisualArray.ToDataTable(matrix).DefaultView;
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
            initialTable.ItemsSource = null;
            rezultOut.Clear();
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            //Опряделяем номер столбца
            int indexColumn = e.Column.DisplayIndex;
            //Определяем номер строки
            int indexRow = e.Row.GetIndex();
            //Проверяем правильное значение ввел пользователь
            if (!int.TryParse(((TextBox)e.EditingElement).Text.Replace('.', ','), out int value))
            {
                MessageBox.Show("Введены неверные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //Заносим введенное значение в матрицу
            matrix[indexRow, indexColumn] = value;
            rezultOut.Clear();
        }
        private void InitialTable_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (initialTable.CurrentColumn == null) return;
            selectedText.Text = string.Format("Выбранная ячейка: {0}х{1}", initialTable.Items.IndexOf(initialTable.CurrentItem) + 1, initialTable.CurrentColumn.DisplayIndex + 1);
        }
    }
}

