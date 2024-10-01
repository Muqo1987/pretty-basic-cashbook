using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasterMez
{
    public partial class frmAyEkle : Form
    {
        public frmAyEkle()
        {
            InitializeComponent();
        }
        public int id = 0;
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        DbHelper db = new DbHelper();
        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void frmAyEkle_Load(object sender, EventArgs e)
        {
           dataGridView1.DataSource = db.Listele("Select Id,Ay From Aylar");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            db.Ekle($"Insert Into Aylar(Ay) Values('{txtAy.Text}')");
            dataGridView1.DataSource = db.Listele("Select Id,Ay From Aylar");
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            db.Ekle($"Delete From Aylar Where Id={id}");
            dataGridView1.DataSource = db.Listele("Select Id,Ay From Aylar");
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           id= Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value.ToString());
        }
    }
}
