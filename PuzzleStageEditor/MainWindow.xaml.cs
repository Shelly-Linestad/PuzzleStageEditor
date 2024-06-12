using Microsoft.Win32;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Forms;

namespace PuzzleStageEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int colorsLength = 4;
        int[,] blocks = new int[,]
        {
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0}
        };
        string[] ccodes = { "#ffff935e", "#ff7eff50", "#ff6891fd", "#fffd68e2" };
        string[] lines;
        System.Windows.Controls.Button[,] buttons = new System.Windows.Controls.Button[8,8];
        ColorDialog cDialog = new ColorDialog();

        public int nextcolor(int colornum)
        {
            int rcolor = (colornum + 1) % colorsLength;
            return rcolor;
        }
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    System.Windows.Controls.Button button = new System.Windows.Controls.Button();
                    button.Name = ("box"+i.ToString()+j.ToString());
                    button.Content = "0";
                    button.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    button.Margin = new Thickness((1 + i) * 32, (1 + j) * 32, 0, 0);
                    button.VerticalAlignment= VerticalAlignment.Top;
                    button.Background = System.Windows.Media.Brushes.Red;
                    button.Width= 32;
                    button.Height = 32;
                    button.Click += new RoutedEventHandler(this.box_Click);
                    buttons[j,i] = button;
                    grid_main.Children.Add(button);
                }
            }
            var clr = FromHex(ccodes[0]);
            var color = new System.Windows.Media.SolidColorBrush(clr);
            clr = FromHex(ccodes[1]);
            Cbutton1.Background = new System.Windows.Media.SolidColorBrush(clr);
            clr = FromHex(ccodes[2]);
            Cbutton2.Background = new System.Windows.Media.SolidColorBrush(clr);
            clr = FromHex(ccodes[3]);
            Cbutton3.Background = new System.Windows.Media.SolidColorBrush(clr);
            label07.Content = ccodes[0] + ccodes[1] + ccodes[2] + ccodes[3];
        }

        private void box_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button bt  = (System.Windows.Controls.Button)sender;
            int posX = ((int)bt.Margin.Left / 32) - 1;
            int posY = ((int)bt.Margin.Top / 32) - 1;
            label3.Content = posX.ToString();
            label4.Content = posY.ToString();

            switch (bt.Content.ToString())
            {
                case "0":
                    bt.Content = "1";
                    bt.Background = System.Windows.Media.Brushes.Yellow;
                    break;
                case "1":
                    bt.Content = "2";
                    bt.Background = System.Windows.Media.Brushes.GreenYellow;
                    break;
                case "2":
                    bt.Content = "3";
                    bt.Background = System.Windows.Media.Brushes.Violet;
                    break;
                case "3":
                    bt.Content = "0";
                    bt.Background = System.Windows.Media.Brushes.Red;
                    break;
            }
            //blocks[posX, posY] = (int)bt.Content;
            /*
            int posX = ((int)bt.Margin.Left / 32) - 1;
            int posY = ((int)bt.Margin.Top / 32) - 1;
            */
            string intvalue = bt.Content.ToString();
            blocks[posX, posY] = int.Parse(intvalue);
        }

        private void openStageFile(object sender, RoutedEventArgs e)
        {
            string[] stages;
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "All File (*.*)|*.*|text (*.txt)|*.txt";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                stages = File.ReadAllLines(openFileDialog.FileName);

                for (int i = 0; i < 4; i++)
                {
                    ccodes[i] = stages[i];

                }
                for (int i = 4; i < 12; i++)
                {
                    lines = stages[i].Split(" ");
                    //여기부터(lines의 i-4 index의 Blocks에 값 적용, 각 box별 contents와 Background에 적용)
                    for (int j = 0; j < 8; j++)
                    {
                        blocks[i-4,j] = int.Parse(lines[j]);
                        buttons[i - 4, j].Content = lines[j];
                        switch (buttons[i - 4, j].Content.ToString())
                        {
                            case "0":
                                buttons[i - 4, j].Background = System.Windows.Media.Brushes.Red;
                                break;
                            case "1":
                                buttons[i - 4, j].Background = System.Windows.Media.Brushes.Yellow;
                                break;
                            case "2":
                                buttons[i - 4, j].Background = System.Windows.Media.Brushes.GreenYellow;
                                break;
                            case "3":
                                buttons[i - 4, j].Background = System.Windows.Media.Brushes.Violet;
                                break;
                        }
                    }
                }
            }
            Debug.WriteLine("loaded");

            var clr0 = FromHex(ccodes[0]);
            var color = new System.Windows.Media.SolidColorBrush(clr0);
            Cbutton0.Background = color;
            var clr1 = FromHex(ccodes[1]);
            Cbutton1.Background = new System.Windows.Media.SolidColorBrush(clr1);
            var clr2 = FromHex(ccodes[2]);
            Cbutton2.Background = new System.Windows.Media.SolidColorBrush(clr2);
            var clr3 = FromHex(ccodes[3]);
            Cbutton3.Background = new System.Windows.Media.SolidColorBrush(clr3);
            label07.Content = ccodes[0] + ccodes[1] + ccodes[2] + ccodes[3];

        }

        private void openColorpicker(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button bt = (System.Windows.Controls.Button)sender;
            cDialog.AllowFullOpen = true;
            cDialog.ShowHelp = false;
            SolidColorBrush scbrush = (SolidColorBrush)bt.Background;
            System.Drawing.Color clrs = new System.Drawing.Color();
            clrs = System.Drawing.Color.FromArgb(scbrush.Color.A, scbrush.Color.R, scbrush.Color.G, scbrush.Color.B);


            if (cDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                var brushColor = System.Windows.Media.Color.FromArgb(cDialog.Color.A, cDialog.Color.R, cDialog.Color.G, cDialog.Color.B);
                var brush = new System.Windows.Media.SolidColorBrush(brushColor);
                bt.Background = brush;
                if (sender == Cbutton0)
                    ccodes[0] = HexConverter(cDialog.Color);
                if (sender == Cbutton1)
                    ccodes[1] = HexConverter(cDialog.Color);
                if (sender == Cbutton2)
                    ccodes[2] = HexConverter(cDialog.Color);
                if (sender == Cbutton3)
                    ccodes[3] = HexConverter(cDialog.Color);
                label07.Content = ccodes[0] + ccodes[1] + ccodes[2] + ccodes[3];
            }
        }
        private static String HexConverter(System.Drawing.Color c)
        {
            String rtn = String.Empty;
            try
            {
                rtn = "#FF" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
            }
            catch (Exception ex)
            {
                //doing nothing
            }

            return rtn;
        }

        private System.Windows.Media.Color FromHex(string hex)
        {
            string colorcode = hex;
            int argb = Int32.Parse(colorcode.Replace("#", ""), System.Globalization.NumberStyles.HexNumber);
            return System.Windows.Media.Color.FromArgb((byte)((argb & -16777216) >> 0x18),
                                  (byte)((argb & 0xff0000) >> 0x10),
                                  (byte)((argb & 0xff00) >> 8),
                                  (byte)(argb & 0xff));


        }

        private void Savetext(object sender, RoutedEventArgs e)
        {
            string[] cont = new string[12];
            for (int i = 0; i < 4; i++)
            {
                cont[i] = ccodes[i];
            }
            for (int i = 0; i < 8; i++)
            {
                string linetemp = "";
                for (int j = 0; j < 8; j++)
                {
                    linetemp = linetemp + blocks[j, i];
                    if (j!=7)
                    {
                        linetemp += " ";
                    }
                }
                cont[i+4] = linetemp;
            }
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.Filter = "All File (*.*)|*.*|text (*.txt)|*.txt";
            sfd.RestoreDirectory = true;
            if(sfd.ShowDialog()==true)
            {
                File.WriteAllLines(sfd.FileName, cont);
            }
        }

    }


}