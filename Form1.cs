using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OkulProject
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = @"Data Source=GUNESBERKANT\SQLEXPRESS;Initial Catalog=OkulProjectDB;Integrated Security=True;Encrypt=False";
            Con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Con;
            cmd.CommandText = "select * from Ogrenci";

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgv.DataSource = dt;

            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            cmbOgr.Items.Clear();

            while (dr.Read())
            {
                cmbOgr.Items.Add(dr["ogrenciNo"].ToString());
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = @"Data Source=GUNESBERKANT\SQLEXPRESS;Initial Catalog=OkulProjectDB;Integrated Security=True;Encrypt=False";
            Con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Con;
            cmd.CommandText = "insert into Ogrenci(ogrenciNo,adSoyad,Adres) VALUES(@ogrenciNo,@adSoyad,@adres)";

            cmd.Parameters.AddWithValue("@ogrenciNo", txtOgr.Text);
            cmd.Parameters.AddWithValue("@adSoyad", txtAdSoyad.Text);
            cmd.Parameters.AddWithValue("@adres", txtAdres.Text);

            if(cmd.ExecuteNonQuery()>0)
            {
                MessageBox.Show("Eklendi");
            }

            Con.Close();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = @"Data Source=GUNESBERKANT\SQLEXPRESS;Initial Catalog=OkulProjectDB;Integrated Security=True;Encrypt=False";
            Con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Con;
            cmd.CommandText = "update Ogrenci set ogrenciNo = @ogrenciNo, adSoyad = @adSoyad, adres = @adres where ogrenciNo = @ogrenciNoGuncel";

            cmd.Parameters.AddWithValue("@ogrenciNo", txtOgr.Text);
            cmd.Parameters.AddWithValue("@adSoyad", txtAdSoyad.Text);
            cmd.Parameters.AddWithValue("@adres", txtAdres.Text);
            cmd.Parameters.AddWithValue("@ogrenciNoGuncel", txtOgr.Text);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Guncellendi");

            Con.Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = @"Data Source=GUNESBERKANT\SQLEXPRESS;Initial Catalog=OkulProjectDB;Integrated Security=True;Encrypt=False";
            Con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Con;
            cmd.CommandText = "delete from Ogrenci where ogrenciNo=@ogrenciNo";

            cmd.Parameters.AddWithValue("@ogrenciNo", cmbOgr.SelectedItem.ToString());

            cmd.ExecuteNonQuery();
            MessageBox.Show("Silindi");

            Con.Close();
        }

        private void cmbOgr_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = @"Data Source=GUNESBERKANT\SQLEXPRESS;Initial Catalog=OkulProjectDB;Integrated Security=True;Encrypt=False";
            Con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Con;
            cmd.CommandText = "select * from Ogrenci where ogrenciNo = @ogrenciNo";

            cmd.Parameters.AddWithValue("@ogrenciNo", cmbOgr.SelectedItem.ToString());

            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            while(dr.Read())
            {
                txtOgr.Text = dr["ogrenciNo"].ToString();
                txtAdSoyad.Text = dr["adSoyad"].ToString();
                txtAdres.Text = dr["adres"].ToString();
            }

            Con.Close();
        }
    }
}
