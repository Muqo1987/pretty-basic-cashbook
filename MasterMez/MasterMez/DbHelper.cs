using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace MasterMez
{
    internal class DbHelper
    {
       public static SQLiteConnection con = new SQLiteConnection("Data Source=.\\Master.db;Version=3");
       static DataTable dt;
        public DataTable Listele(String sql)
        {
            dt = new DataTable();
            SQLiteDataAdapter ad = new SQLiteDataAdapter(sql,con);
            ad.Fill(dt);
            return dt;
        }
        public void Ekle(String sql)
        {
            SQLiteCommand com = new SQLiteCommand(sql,con);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
        }
        public void yazdir(string sql, string ay, SaveFileDialog sa)
        {
            int toplamM = 0;
            int toplamS = 0;
            int toplamK = 0;
            StreamWriter yaz = new StreamWriter(sa.FileName,true);
            yaz.WriteLine(ay);
            yaz.WriteLine("");
            yaz.WriteLine("");
            yaz.WriteLine("Marka | Maliyet | Satış | Kâr");
            yaz.WriteLine("");
            yaz.WriteLine("-----------------------------");
            yaz.WriteLine("");
            SQLiteCommand com = new SQLiteCommand(sql,con);
            con.Open();
            SQLiteDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                string marka = dr["Marka"].ToString();
                string maliyet = dr["Maliyet"].ToString();
                string satış = dr["Satış"].ToString();
                string kar = dr["Kâr"].ToString();
                toplamM += Convert.ToInt32(maliyet);
                toplamS += Convert.ToInt32(satış);
                toplamK += Convert.ToInt32(kar);
                yaz.WriteLine($"{marka} | {maliyet} | {satış} | {kar}");
            }
            dr.Close();
            con.Close();
            yaz.WriteLine("");
            yaz.WriteLine("-----------------------------");
            yaz.WriteLine("");
            yaz.WriteLine($"Toplam maliyet : {toplamM}");
            yaz.WriteLine($"Toplam satış : {toplamS}");
            yaz.WriteLine($"Toplam kâr : {toplamK}");
            yaz.Close();
        }
    }
}
