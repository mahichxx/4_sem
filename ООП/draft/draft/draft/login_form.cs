using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace draft
{
    public partial class login_form : Form
    {
        public login_form()
        {
            InitializeComponent();
            this.passField.AutoSize = false;
            this.passField.Size = new Size(this.passField.Size.Width, 55);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void clouseButten_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void clouseButten_MouseEnter(object sender, EventArgs e)
        {
            clouseButten.ForeColor = Color.Orange;
        }

        private void clouseButten_MouseLeave(object sender, EventArgs e)
        {
            clouseButten.ForeColor= Color.White;
        }

        Point lastPoint;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;

            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
    }
}
