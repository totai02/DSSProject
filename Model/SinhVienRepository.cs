using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DSSProject.Model
{
    public class SinhVienRepository
    {
        public List<SinhVien> sinhVienRepository { get; set; }

        public SinhVienRepository()
        {
            sinhVienRepository = GetSinhVienRepo();
        }

        public List<SinhVien> GetSinhVienRepo()
        {
            List<SinhVien> listOfSV = new List<SinhVien>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn_nguon_nhan_luc"].ConnectionString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in App.config");
                }

                SqlCommand query = new SqlCommand("SELECT * FROM sinhvien2014_2015", conn);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    SinhVien sinhVien = new SinhVien
                    {
                        MaNganh = row["MaNganh"].ToString(),
                        NhomNganh = row["NhomNganh"].ToString(),
                        TenChuyenNganh = row["TenChuyenNganh"].ToString(),
                        SoLuong = (int)row["soLuong"]
                    };

                    listOfSV.Add(sinhVien);
                }

                return listOfSV;
            }
        }
    }
}
