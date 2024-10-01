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
    public partial class frmKasa : Form
    {
        public frmKasa()
        {
            InitializeComponent();
        }
        public int id = 0;
            [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
            private extern static void ReleaseCapture();
            [DllImport("user32.DLL", EntryPoint = "SendMessage")]
            private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        DbHelper db = new DbHelper();
        private void frmKasa_Load(object sender, EventArgs e)
        {
            cmbAylar.DisplayMember = "Ay";
            cmbAylar.ValueMember = "Ay";
            cmbAylar.DataSource = db.Listele("Select Ay From Aylar");
            cmbMarka.DisplayMember = "Marka";
            cmbMarka.ValueMember = "Marka";
            cmbMarka.DataSource = db.Listele("Select Marka From Markalar");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (cmbMarka.Text != "" && txtMaliyet.Text != "" && txtSatis.Text != "" && cmbAylar.Text != "")
            {
                double kar = Convert.ToDouble(txtSatis.Text) - Convert.ToDouble(txtMaliyet.Text);
                db.Ekle($"Insert Into Kasa(Marka,Maliyet,Satış,Kâr,Ay) Values('{cmbMarka.Text}','{txtMaliyet.Text}','{txtSatis.Text}','{kar}','{cmbAylar.Text}')");
                dataGridView1.DataSource = db.Listele($"Select Id,Marka,Maliyet,Satış,Kâr From Kasa Where Ay='{cmbAylar.Text}'");
            }
            else
            {
                MessageBox.Show("Lütfen verileri eksiksiz giriniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btnAyEkle_Click(object sender, EventArgs e)
        {
            frmAyEkle frm = new frmAyEkle();
            frm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            db.Ekle($"Delete From Kasa Where Id={id}");
            dataGridView1.DataSource = db.Listele($"Select Id,Marka,Maliyet,Satış,Kâr From Kasa Where Ay='{cmbAylar.Text}'");
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
             
            id=Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmMarkaEkle frm = new frmMarkaEkle();
            frm.Show();
        }

        private void cmbAylar_Click(object sender, EventArgs e)
        {
            cmbAylar.DisplayMember = "Ay";
            cmbAylar.ValueMember = "Ay";
            cmbAylar.DataSource = db.Listele("Select Ay From Aylar");
        }

        private void cmbMarka_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbMarka_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void cmbAylar_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Listele($"Select Id,Marka,Maliyet,Satış,Kâr From Kasa Where Ay='{cmbAylar.Text}'");
        }

        private void cmbMarka_Click(object sender, EventArgs e)
        {
            cmbMarka.DisplayMember = "Marka";
            cmbMarka.ValueMember = "Marka";
            cmbMarka.DataSource = db.Listele("Select Marka From Markalar");
        }

        private void frmKasa_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmbMarka.Text != "" && txtMaliyet.Text != "" && txtSatis.Text != "" && cmbAylar.Text != "")
                {
                    double kar = Convert.ToDouble(txtSatis.Text) - Convert.ToDouble(txtMaliyet.Text);
                    db.Ekle($"Insert Into Kasa(Marka,Maliyet,Satış,Kâr,Ay) Values('{cmbMarka.Text}','{txtMaliyet.Text}','{txtSatis.Text}','{kar}','{cmbAylar.Text}')");
                    dataGridView1.DataSource = db.Listele($"Select Id,Marka,Maliyet,Satış,Kâr From Kasa Where Ay='{cmbAylar.Text}'");
                }
                else
                {
                    MessageBox.Show("Lütfen verileri eksiksiz giriniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }
    }
}
