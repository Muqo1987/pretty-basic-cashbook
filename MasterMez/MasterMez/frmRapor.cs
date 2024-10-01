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
using System.IO;

namespace MasterMez
{
    public partial class frmRapor : Form
    {
        public frmRapor()
        {
            InitializeComponent();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRapor_Load(object sender, EventArgs e)
        {
            DbHelper db = new DbHelper();
            cmbAylar.DisplayMember = "Ay";
            cmbAylar.ValueMember = "Ay";
            cmbAylar.DataSource = db.Listele("Select * From Aylar");
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAyEkle_Click(object sender, EventArgs e)
        {
            frmAyEkle frm = new frmAyEkle();
            frm.Show();
        }

        private void cmbAylar_SelectedIndexChanged(object sender, EventArgs e)
        {
            DbHelper db = new DbHelper();
            dataGridView1.DataSource = db.Listele($"Select Id,Marka,Maliyet,Satış,Kâr From Kasa Where Ay='{cmbAylar.Text}'");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog sv = new SaveFileDialog();
            sv.Filter = "text|*.txt";
            sv.InitialDirectory = "C://";
            if (sv.ShowDialog() == DialogResult.OK)
            {
                DbHelper db = new DbHelper();
                db.yazdir($"Select Marka,Maliyet,Satış,Kâr From Kasa Where Ay='{cmbAylar.Text}'",cmbAylar.Text,sv);
                MessageBox.Show("Dosya başarıyla kaydedildi.","Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Dosya Kaydedilemedi.","Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
