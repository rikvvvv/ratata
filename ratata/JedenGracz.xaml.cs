using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ratata
{
    /// <summary>
    /// Logika interakcji dla klasy JedenGracz.xaml
    /// </summary>
    public partial class JedenGracz : Window
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

        int[,] trasa = new int[13,13];
        int ratRow = 6, ratCol = 6;

        Image szczuras = new Image();
        public JedenGracz()
        {
            InitializeComponent();
            LosoweWylaczonePola();
            TrasaUpdate();
            Init();
        }

        void TrasaUpdate()
        {
            for(int i = 1; i < 12; i++)
            {
                for(int j = 1; j < 12; j++)
                {
                    if (mapa[i, j] == 0)
                        trasa[i, j] = -1;
                    else
                        trasa[i, j] = 0;
                }
            }
        }

        void LosoweWylaczonePola()
        {
            Random rng = new Random();
            for (int i = 0; i < 13; i++)
            {
                mapa[rng.Next(2, 11), rng.Next(2, 11)] = 0;
            }
            mapa[6, 6] = 1;
        }

        void Init()
        {
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    if (mapa[i, j] == 2)
                    {
                        Image ser = new Image();
                        ser.BeginInit();
                        ser.Source = new BitmapImage(new Uri("/images/ser.png", UriKind.Relative));
                        Siatka.Children.Add(ser);
                        ser.SetValue(Grid.ColumnSpanProperty, 2);
                        if (i % 2 == 0)
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
            if (ratRow % 2 == 0)
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

            if (row == ratRow && col == ratCol)
                return;
            InfoBlock.Text = string.Format($"TAG: {tag} ROW: {row} COLUMN: {col} ratRow: {ratRow} ratCol: {ratCol}");
            mapa[row, col] = 0;
            TrasaUpdate();
            b.IsEnabled = false;

            //ratRow++;
            //SzczurUpdate();
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
