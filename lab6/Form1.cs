using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab6
{
    public partial class Form1 : Form
    {
        public Form1() => InitializeComponent();

        Image fota;
        Bitmap bitFota;
        int szerokosc, wysokosc;
        public static Image obraz(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }
     
       
        private int ciemniej(int color, int delta)
        {
            if (color < 127 + delta) return (127 / czyKolor(127 + delta)) * color;
            else if (color > 127 - delta) return ((127 * color) + (255 * delta)) / czyKolor(127 + delta);
            else return 127;
        }
        public int czyKolor(int value)
        {
            if (value >= 255) return 254;
            if (value <= 0) return 1;
            else return value;
        }


        private void Form1_Load_1(object sender, EventArgs e)
        {
            fota = pictureBox1.Image;
            fota = obraz(fota, new Size(pictureBox1.Size.Width, pictureBox1.Size.Height));
            bitFota = new Bitmap(fota);
            szerokosc = bitFota.Width;
            wysokosc = bitFota.Height;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Bitmap ustal = new Bitmap(fota);
            for (int y = 0; y < wysokosc; y++)
            {
                for (int x = 0; x < szerokosc; x++)
                {
                    Color tukanPixel = bitFota.GetPixel(x, y);

                    int a = tukanPixel.A;
                    int r = czyKolor((127 / czyKolor(127 - trackBar1.Value)) * (tukanPixel.R - trackBar1.Value));
                    int g = czyKolor((127 / czyKolor(127 - trackBar1.Value)) * (tukanPixel.G - trackBar1.Value));
                    int b = czyKolor((127 / czyKolor(127 - trackBar1.Value)) * (tukanPixel.B - trackBar1.Value));
                    ustal.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                }
            }
            kolHis(ustal);
            pictureBox2.Image = ustal;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Bitmap ustal = new Bitmap(fota);
            for (int y = 0; y < wysokosc; y++)
            {
                for (int x = 0; x < szerokosc; x++)
                {
                    Color tukanPixel = bitFota.GetPixel(x, y);

                    int a = tukanPixel.A;
                    int r = ciemniej(tukanPixel.R, trackBar2.Value);
                    int g = ciemniej(tukanPixel.G, trackBar2.Value);
                    int b = ciemniej(tukanPixel.B, trackBar2.Value);
                    ustal.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                }
            }
            kolHis(ustal);
            pictureBox2.Image = ustal;
        }

        private void kolHis(Bitmap temp)
        {
            int[] czerw = new int[256];
            int[] ziel = new int[256];
            int[] nieb = new int[256];
            for (int x = 0; x < szerokosc; x++)
            {
                for (int y = 0; y < wysokosc; y++)
                {
                    Color pixel = temp.GetPixel(x, y);
                    czerw[pixel.R]++;
                    ziel[pixel.G]++;
                    nieb[pixel.B]++;
                }
            }

            
            chart1.Series["red"].Points.Clear();
            chart1.Series["green"].Points.Clear();
            chart1.Series["blue"].Points.Clear();
            for (int i = 0; i < 256; i++)
            {
                chart1.Series["red"].Points.AddXY(i, czerw[i]);
                chart1.Series["green"].Points.AddXY(i, ziel[i]);
                chart1.Series["blue"].Points.AddXY(i, nieb[i]);
            }
            chart1.Invalidate();
        }
    }

}