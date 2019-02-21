using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    public partial class Form1 : Form
    {
        private OpenFileDialog openFileDialog1;

        public Form1()
        {
            InitializeComponent();
            openFileDialog1 = new OpenFileDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
                
            }
        }
        int i = 1;
        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = (Bitmap)pictureBox1.Image;
            Bitmap bitmap1 = new Bitmap(bitmap.Width, bitmap.Height);
            Copy(bitmap, bitmap1);

            pictureBox2.Image = bitmap1;

            ShiftBy(bitmap1, i);

            pictureBox2.Refresh();
            i++;
        }

        private void ShiftBy(Bitmap bitmap1, int Delta)
        {
            for (int i = 0; i < bitmap1.Height; i++)
            {
                int currentDelta = Delta;
                for (int j = 0; j < bitmap1.Width- 2 * currentDelta; j+=2*currentDelta, currentDelta++)
                {   
                    Color[] first = new Color[currentDelta];
                    Color[] second = new Color[currentDelta];
                    for (int k = j; k < j + currentDelta; k++)
                    {
                        first[k - j] = bitmap1.GetPixel(k, i);
                    }

                    for (int k = j+currentDelta; k < j + 2*currentDelta; k++)
                    {
                        second[k - (j + currentDelta)] = bitmap1.GetPixel(k, i);
                    }

                    for (int k = j; k < j + currentDelta; k++)
                    {
                        bitmap1.SetPixel(k, i, second[k - j]);
                    }

                    for (int k = j + currentDelta; k < j + 2 * currentDelta; k++)
                    {
                        bitmap1.SetPixel(k, i, first[k - (j + currentDelta)]);
                    }
                    
                }
            }

        }

        public void Alg33(Bitmap bitmap)
        {
            for (int i = bitmap.Height / 4; i < 3 * bitmap.Height / 4; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    bitmap.SetPixel(j, i, Color.Black);
                }
            }
        }

        private void Copy(Bitmap bitmap1, Bitmap bitmap2)
        {
            for (int i = 0; i < bitmap2.Height; i++)
            {
                for (int j = 0; j < bitmap2.Width; j++)
                {
                    var color = bitmap1.GetPixel(j, i);
                    bitmap2.SetPixel(j, i, color);
                }
            }
        }

        private void Alg333(Bitmap bitmap)
        {
            for (int i = bitmap.Height/4; i < 3*bitmap.Height/4; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    bitmap.SetPixel(j, i, Color.Black);
                }
            }
        }

        private void Alg2(Bitmap bitmap)
        {
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    bitmap.SetPixel(j, i, Color.Black);
                }
            }
        }
    }
}
