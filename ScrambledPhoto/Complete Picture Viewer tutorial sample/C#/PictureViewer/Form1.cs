using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            // No code needed here for this sample.
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            // Show the Open File dialog. If the user chooses OK, load the 
            // picture that the user chose.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
            }

        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            // Clear the picture.
            pictureBox1.Image = null;
        }

        private void backgroundButton_Click(object sender, EventArgs e)
        {
            Alg();

            //// Show the color picker dialog box. If the user chooses OK, change the 
            //// PictureBox control's background to the color the user chose. 
            //if (colorDialog1.ShowDialog() == DialogResult.OK)
            //    pictureBox1.BackColor = colorDialog1.Color;
        }

        private void Alg()
        {
            var aaa = pictureBox1.Image;

            Bitmap bitmap = (Bitmap)aaa;
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 1; j < bitmap.Width; j+=2)
                {
                    var pixel1 = bitmap.GetPixel(j, i);
                    var pixel2 = bitmap.GetPixel(j-1, i );
                    var A = pixel1.A + pixel2.A;
                    A = A < 256 ? A : 255;

                    var R = pixel1.R + pixel2.R;
                    R = R < 256 ? R : 255;

                    var G = pixel1.G + pixel2.G;
                    G = G < 256 ? G : 255;

                    var B = pixel1.B + pixel2.B;
                    B = B < 256 ? B : 255;


                    var pixel3 = Color.FromArgb(R, G, B);
                    bitmap.SetPixel(j, i, pixel3);
                }
            }
            pictureBox1.Refresh();
        }
        bool half = false;
        private void Alg2()
        {
            var aaa = pictureBox1.Image;
            
            Bitmap bitmap = (Bitmap)aaa;
            for (int i = bitmap.Height/2; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    var pixel1 = bitmap.GetPixel(j, i);
                    var pixel2 = bitmap.GetPixel(j, i - bitmap.Height / 2);
                    var A = pixel1.A + pixel2.A;
                    A = A < 256 ? A : 255;

                    var R = pixel1.R + pixel2.R;
                    R = R < 256 ? R : 255;

                    var G = pixel1.G + pixel2.G;
                    G = G < 256 ? G : 255;

                    var B = pixel1.B + pixel2.B;
                    B = B < 256 ? B : 255;


                    var pixel3 = Color.FromArgb( R, G, B);
                    bitmap.SetPixel(j, i, pixel3);
                }
            }
            pictureBox1.Refresh();
        }

        private void Alg22()
        {
            var aaa = pictureBox1.Image;

            Bitmap bitmap = (Bitmap)aaa;
            for (int i = 0; i < bitmap.Size.Height; i++)
            {
                for (int j = 0; j < bitmap.Size.Width - 1; j += 2)
                {
                    Color first = bitmap.GetPixel(j, i);
                    Color second = bitmap.GetPixel(j + 1, i);
                    bitmap.SetPixel(j, i, second);
                    bitmap.SetPixel(j + 1, i, first);

                }
            }

            pictureBox1.Refresh();
        }

        private void Alg1()
        {
            var aaa = pictureBox1.Image;

            Bitmap bitmap = (Bitmap)aaa;
            for (int i = 0; i < bitmap.Size.Height; i++)
            {
                for (int j = 0; j < bitmap.Size.Width; j++)
                {
                    bitmap.SetPixel(j, i, Color.AliceBlue);
                }
            }

            pictureBox1.Refresh();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            // Close the form. 
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // If the user selects the Stretch check box,  
            // change the PictureBox's SizeMode property to "Stretch". 
            // If the user clears the check box, change it to "Normal".
            // Choosing Stretch shows the entire image in the available space.          
            if (checkBox1.Checked)
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            else
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
