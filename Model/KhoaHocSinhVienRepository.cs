using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DSSProject.Model
{
    public class KhoaHocSinhVienRepository
    {
        public List<KhoaHocSinhVien> khoaHocRepository { get; set; }

        public KhoaHocSinhVienRepository()
        {
            khoaHocRepository = GetKhoaHocRepo();
        }

        public List<KhoaHocSinhVien> GetKhoaHocRepo()
        {
            List<KhoaHocSinhVien> listOfKH = new List<KhoaHocSinhVien>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn_nguon_nhan_luc"].ConnectionString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in App.config");
                }

                SqlCommand query = new SqlCommand("SELECT * FROM khoahocsinhvien", conn);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    KhoaHocSinhVien khoaHoc = new KhoaHocSinhVien
                    {
                        MaKhoaHoc = row["ma_khoa_hoc"].ToString(),
                        NamVao = (int)row["nam_vao"],
                        NamRa = (int)row["nam_ra"],
                    };

                    listOfKH.Add(khoaHoc);
                }

                return listOfKH;
            }
        }
    }
}
