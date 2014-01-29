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

namespace Mosaïque
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Application.Current.MainWindow.WindowState = WindowState.Maximized;

            PaintMeMyMosaic(5);
        }

        private void PaintMeMyMosaic(UInt32 nbTile)
        {
            Palette.Children.Clear();
            SolidColorBrush init = new SolidColorBrush(Colors.White);
            Random rand = new Random();

            int tileWidth = Convert.ToInt32(SystemParameters.FullPrimaryScreenWidth / nbTile);
            int tileHeight = Convert.ToInt32(SystemParameters.FullPrimaryScreenHeight / nbTile);

            for (int y = 0; y < nbTile; y++)
            {
                StackPanel stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;
                for (int x = 0; x < nbTile; x++)
                {
                    int red = rand.Next(256);
                    int green = rand.Next(256);
                    int blue = rand.Next(256);
                    Tile tile = DrawMeMyMosaic(init.Color, red, green, blue);
                    init = tile.background;
                    TextBlock Name = new TextBlock();
                    Grid grid = new Grid();
                    Name.Foreground = new SolidColorBrush(Colors.White);
                    Name.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    Name.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    Name.TextAlignment = TextAlignment.Center;
                    Name.Text = tile.hexa + Environment.NewLine + tile.rgb;
                    grid.Background = tile.background;
                    grid.Width = tileWidth;
                    grid.Height = tileHeight;
                    grid.Children.Add(Name);
                    stack.Children.Add(grid);
                }
                Palette.Children.Add(stack);
            }
        }

        private Tile DrawMeMyMosaic(Color mix, int red, int green, int blue)
        {
            if (mix != null)
            {
                red = (red + Convert.ToInt32(mix.R)) / 2;
                green = (green + Convert.ToInt32(mix.G)) / 2;
                blue = (blue + Convert.ToInt32(mix.B)) / 2;
            }
            SolidColorBrush color = new SolidColorBrush(Color.FromRgb((byte)red, (byte)green, (byte)blue));
            return new Tile() { background = color, hexa = HexConverter(color.Color), rgb = RGBConverter(color.Color) };
        }

        #region converter
        // http://stackoverflow.com/questions/2395438/convert-system-drawing-color-to-rgb-and-hex-value
        private static string HexConverter(Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        private static string RGBConverter(Color c)
        {
            return "rgb (" + c.R.ToString() + "," + c.G.ToString() + "," + c.B.ToString() + ")";
        }
        #endregion

        #region model
        private class Tile
        {
            public SolidColorBrush background;
            public string hexa;
            public string rgb;
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UInt32 u;
            if (UInt32.TryParse(value.Text, out u))
                if (u > 0 && u < 21)
                    PaintMeMyMosaic(u);
                else
                    MessageBox.Show("set a value between 1 and 20");
        }
    }
}
