using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasterMez
{
    public partial class frmMarkaEkle : Form
    {
        public frmMarkaEkle()
        {
            InitializeComponent();
        }
        public int id = 0;
        DbHelper db = new DbHelper();
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void button3_Click(object sender, EventArgs e)
        {
            db.Ekle($"Insert Into Markalar(Marka) Values('{txtMarka.Text}')");
            dataGridView1.DataSource = db.Listele("Select * From Markalar");
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void frmMarkaEkle_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Listele("Select * From Markalar");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            db.Ekle($"Delete From Markalar Where Id={id}");
            dataGridView1.DataSource = db.Listele("Select * From Markalar");
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
