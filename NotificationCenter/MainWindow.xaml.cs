using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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

namespace NotificationCenter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            urav.Width = LeftGrid.ActualWidth*0.25;
            text.Content ="Проект Скофенко Кирилла";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Polosa.Visibility == Visibility.Collapsed)
            {
                Polosa.Visibility = Visibility.Visible;
            }
            else
            {
                Polosa.Visibility = Visibility.Collapsed;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window1 w = new Window1();
            w.Show();
            Close();
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            urav.Width = grid.ActualWidth*0.23;
            fon.Width = grid.ActualWidth * 0.75;
            fon.Height = grid.ActualHeight * 0.5;
        }
    }

    public class ThicknessMultiplyingConverter : IMultiValueConverter
        {
            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                return new Thickness() { Left = values.Cast<double>().Aggregate((x, y) => x * y) };
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                throw new NotSupportedException();
            }
        }
}