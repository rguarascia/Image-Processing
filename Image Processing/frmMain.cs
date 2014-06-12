using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Imaging;

//By: Ryan Guarascia.
//Grade 11 - 2013-2014
//A simple hue changer but with more than the default pixel changer.This program is meant for performance
//and has a save function to save the pixels to a text document that can then be re-read and re-edited.
namespace Image_Processing
{
    public partial class frmMain : Form
    {

        string bmpPath;

        bool Invert = false;

        ImageProcessor Render = new ImageProcessor();

        public frmMain()
        {
            InitializeComponent();

            toolTip1.ToolTipIcon = ToolTipIcon.Info;

            toolTip1.SetToolTip(this.ckbSave, "This function will save the rendered image as a text file. Caution - slows down performance.");

        }

        private void btnGetPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog getImage = new OpenFileDialog();
            if (getImage.ShowDialog() == DialogResult.OK)
            {
                bmpPath = getImage.FileName;
            }
            btnRender.Enabled = true;
        }

        private void btnRender_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Render.Rendering(bmpPath, (byte)nudAlpha.Value, (byte)nudRed.Value, (byte)nudGreen.Value, (byte)nudBlue.Value, ckbSave.Checked);
            btnSaveImage.Enabled = true;
        }

        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            SaveFileDialog ImageOut = new SaveFileDialog();
            ImageOut.Filter = "PNG Files(*.PNG)|*.PNG";
            if (ImageOut.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(ImageOut.FileName, ImageFormat.Jpeg);
            }
        }

        private void btnInvert_Click(object sender, EventArgs e)
        {
            Invert = true;
        }

        public bool InvertImg()
        {
            return Invert;
        }
    }
}
