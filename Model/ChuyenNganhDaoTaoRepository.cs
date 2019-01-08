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

        public bool DelRecord(string maNganh)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn_nguon_nhan_luc"].ConnectionString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in App.config");
                }

                string queryString = string.Format("DELETE FROM chuyennganhdaotao WHERE MaNganh='{0}'", maNganh);

                SqlCommand query = new SqlCommand(queryString, conn);
                conn.Open();

                try
                {
                    query.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    return false;
                }
            }
            return true;
        }

        public bool addNewRecord(ChuyenNganhDaoTao chuyenNganhDaoTao)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn_nguon_nhan_luc"].ConnectionString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in App.config");
                }
                else if (chuyenNganhDaoTao == null)
                    throw new Exception("The passed argument 'chuyenNganhDaoTao' is null");

                string queryString = string.Format("INSERT INTO chuyennganhdaotao (MaNganh, NhomNganh, TenChuyenNganh) VALUES ('{0}', {1}, '{2}')", chuyenNganhDaoTao.MaNganh, chuyenNganhDaoTao.NhomNganh, chuyenNganhDaoTao.TenChuyenNganh);

                SqlCommand query = new SqlCommand(queryString, conn);
                conn.Open();

                try
                {
                    query.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    return false;
                }
            }
            return true;
        }

        public bool UpdateRecord(ChuyenNganhDaoTao chuyenNganhDaoTao)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn_nguon_nhan_luc"].ConnectionString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in App.config");
                }
                else if (chuyenNganhDaoTao == null)
                    throw new Exception("The passed argument 'chuyenNganhDaoTao' is null");

                string queryString = string.Format("UPDATE chuyennganhdaotao SET MaNganh = '{0}', NhomNganh = {1}, TenChuyenNganh = '{2}' WHERE MaNganh = '{0}'", chuyenNganhDaoTao.MaNganh, chuyenNganhDaoTao.NhomNganh, chuyenNganhDaoTao.TenChuyenNganh);

                SqlCommand query = new SqlCommand(queryString, conn);
                conn.Open();

                try
                {
                    query.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
