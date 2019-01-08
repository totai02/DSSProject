using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DSSProject.Model
{
    public class ChuyenNganhDaoTaoRepository
    {
        public List<ChuyenNganhDaoTao> chuyenNganhRepository { get; set; }

        public ChuyenNganhDaoTaoRepository()
        {
            chuyenNganhRepository = GetChuyenNganhRepo();
        }

        public List<ChuyenNganhDaoTao> GetChuyenNganhRepo()
        {
            List<ChuyenNganhDaoTao> listOfCNDT = new List<ChuyenNganhDaoTao>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn_nguon_nhan_luc"].ConnectionString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in App.config");
                }

                SqlCommand query = new SqlCommand("SELECT * FROM chuyennganhdaotao", conn);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    ChuyenNganhDaoTao chuyenNganh = new ChuyenNganhDaoTao
                    {
                        MaNganh = row["MaNganh"].ToString(),
                        NhomNganh = row["NhomNganh"].ToString(),
                        TenChuyenNganh = row["TenChuyenNganh"].ToString(),
                    };

                    listOfCNDT.Add(chuyenNganh);
                }

                return listOfCNDT;
            }
        }
    }
}
