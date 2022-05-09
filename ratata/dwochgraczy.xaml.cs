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

namespace ratata
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[,] mapa =
        {
            {2,2,2,2,2,2,2,2,2,2,2,2,2 },
            {2,1,1,1,1,1,1,1,1,1,1,1,2 },
            {2,1,1,1,1,1,1,1,1,1,1,1,2 },
            {2,1,1,1,1,1,1,1,1,1,1,1,2 },
            {2,1,1,1,1,1,1,1,1,1,1,1,2 },
            {2,1,1,1,1,1,1,1,1,1,1,1,2 },
            {2,1,1,1,1,1,1,1,1,1,1,1,2 },
            {2,1,1,1,1,1,1,1,1,1,1,1,2 },
            {2,1,1,1,1,1,1,1,1,1,1,1,2 },
            {2,1,1,1,1,1,1,1,1,1,1,1,2 },
            {2,1,1,1,1,1,1,1,1,1,1,1,2 },
            {2,1,1,1,1,1,1,1,1,1,1,1,2 },
            {2,2,2,2,2,2,2,2,2,2,2,2,2 }
        };
        int ratRow = 6, ratCol = 6;
        bool kolej = true;

        Image szczuras = new Image();
        public MainWindow()
        {
            InitializeComponent();
            LosoweWylaczonePola();
            Init();
        }

        void LosoweWylaczonePola()
        {
            Random rng = new Random();
            for(int i = 0; i < 13; i++)
            {
                mapa[rng.Next(2, 11), rng.Next(2, 11)] = 0;
            }
            mapa[6, 6] = 1;
        }

        void Init()
        {
            for(int i = 0; i < 13; i++)
            {
                for(int j = 0; j < 13; j++)
                {
                    if(mapa[i,j] == 2)
                    {
                        Image ser = new Image();
                        ser.BeginInit();
                        ser.Source = new BitmapImage(new Uri("/images/ser.png", UriKind.Relative));
                        Siatka.Children.Add(ser);
                        ser.SetValue(Grid.ColumnSpanProperty, 2);
                        if(i%2 == 0)
                            ser.SetValue(Grid.ColumnProperty, j * 2 + 1);
                        else
                            ser.SetValue(Grid.ColumnProperty, j * 2);
                        ser.SetValue(Grid.RowProperty, i);
                        ser.Height = 40;
                        ser.EndInit();
                    }
                    else
                    {
                        Button przycisk = new Button()
                        {
                            Tag = string.Format($"{i}_{j}"),
                            IsEnabled = CzyWlaczone(i, j)
                        };
                        przycisk.Click += new RoutedEventHandler(PrzyciskClick);
                        Siatka.Children.Add(przycisk);
                        przycisk.SetValue(Grid.ColumnSpanProperty, 2);
                        if (i % 2 == 0)
                            przycisk.SetValue(Grid.ColumnProperty, j * 2 + 1);
                        else
                            przycisk.SetValue(Grid.ColumnProperty, j * 2);
                        przycisk.SetValue(Grid.RowProperty, i);
                    }
                }
            }
            szczuras.BeginInit();
            szczuras.Source = new BitmapImage(new Uri("/images/szczur.png", UriKind.Relative));
            Siatka.Children.Add(szczuras);
            szczuras.SetValue(Grid.ColumnSpanProperty, 2);
            szczuras.Width = 60;
            szczuras.EndInit();
            SzczurUpdate();
        }

        void SzczurUpdate()
        {
            szczuras.SetValue(Grid.RowProperty, ratRow);
            if(ratRow%2 == 0)
                szczuras.SetValue(Grid.ColumnProperty, ratCol * 2 + 1);
            else
                szczuras.SetValue(Grid.ColumnProperty, ratCol * 2);
        }

        void PrzyciskClick(object sender, RoutedEventArgs e)
        {

            Button b = (Button)sender;
            string tag = b.Tag.ToString();
            int row = int.Parse(tag.Split('_')[0]);
            int col = int.Parse(tag.Split('_')[1]);
            InfoBlock.Text = string.Format($"TAG: {tag} ROW: {row} COLUMN: {col} ratRow: {ratRow} ratCol: {ratCol}");
            if (!kolej || row == ratRow && col == ratCol)
                return;
            mapa[row, col] = 0;
            b.IsEnabled = false;

            kolej = false;
            CzyjaKolej.Text = "Kolej Szczura";

            //ratRow++;
            //SzczurUpdate();
        }

        void CzyWygralSzczurolap()
        {

        }
        private void RuchClick(object sender, RoutedEventArgs e)
        {
            if (kolej)
                return;
            int dir = int.Parse(((Button)sender).Tag.ToString());
            int tempRow = ratRow, tempCol = ratCol;
            if(dir == 1)
            {
                if(ratRow%2 == 0)
                {
                    tempRow--;
                    tempCol++;
                }
                else
                {
                    tempRow--;
                }
            }
            if(dir == 2)
            {
                tempCol++;
            }
            if(dir == 3)
            {
                if(ratRow%2 == 0)
                {
                    tempRow++;
                    tempCol++;
                }
                else
                {
                    tempRow++;
                }
            }
            if(dir == 4)
            {
                if(ratRow%2 == 0)
                {
                    tempRow++;
                }
                else
                {
                    tempCol--;
                    tempRow++;
                }
            }
            if(dir == 5)
            {
                tempCol--;
            }

            if(dir == 6)
            {
                if(ratRow%2 == 0)
                {
                    tempRow--;
                }
                else
                {
                    tempCol--;
                    tempRow--;
                }
            }

            if (mapa[tempRow, tempCol] == 0)
                return;

            ratCol = tempCol;
            ratRow = tempRow;
            SzczurUpdate();
            if(mapa[ratRow,ratCol] == 2)
            {
                MessageBox.Show("wygral szczuras");
            }

            kolej = true;
            CzyjaKolej.Text = "Kolej Szczurołapa";
        }

        bool CzyWlaczone(int i, int j)
        {
            if (mapa[i, j] == 1)
                return true;
            else
                return false;
        }
    }
}
