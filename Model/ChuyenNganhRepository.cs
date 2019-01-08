using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DSSProject.Model
{
    public class ChuyenNganhRepository
    {
        public List<ChuyenNganh> chuyenNganhRepository { get; set; }

        public ChuyenNganhRepository()
        {
            chuyenNganhRepository = GetChuyenNganhRepo();
        }

        public List<ChuyenNganh> GetChuyenNganhRepo()
        {
            List<ChuyenNganh> listOfCNDT = new List<ChuyenNganh>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn_tuyensinh"].ConnectionString))
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
                    ChuyenNganh chuyenNganh = new ChuyenNganh
                    {
                        MaNganh = row["MaNganh"].ToString(),
                        TenChuyenNganh = row["TenChuyenNganh"].ToString(),
                    };

                    listOfCNDT.Add(chuyenNganh);
                }

                return listOfCNDT;
            }
        }

        public List<ChuyenNganh> SearchRecord(string[] arrQuery)
        {
            List<ChuyenNganh> listOfCNDT = new List<ChuyenNganh>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn_tuyensinh"].ConnectionString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in App.config");
                }

                string queryString = "SELECT * FROM chuyennganhdaotao WHERE ";
                for (int i = 0; i < arrQuery.Length; i++)
                {
                    arrQuery[i] = string.Format("(MaNganh LIKE '%{0}%' OR TenChuyenNganh LIKE '%{0}%')", arrQuery[i]);
                }

                queryString += string.Join(" AND ", arrQuery);

                SqlCommand query = new SqlCommand(queryString, conn);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    ChuyenNganh chuyenNganh = new ChuyenNganh
                    {
                        MaNganh = row["MaNganh"].ToString(),
                        TenChuyenNganh = row["TenChuyenNganh"].ToString(),
                    };

                    listOfCNDT.Add(chuyenNganh);
                }

                return listOfCNDT;
            }
        }

        public bool DelRecord(string maNganh)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn_tuyensinh"].ConnectionString))
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
                catch (SqlException)
                {
                    return false;
                }
            }
            return true;
        }

        public bool addNewRecord(ChuyenNganh chuyenNganhDaoTao)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn_tuyensinh"].ConnectionString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in App.config");
                }
                else if (chuyenNganhDaoTao == null)
                    throw new Exception("The passed argument 'chuyenNganhDaoTao' is null");

                string queryString = string.Format("INSERT INTO chuyennganhdaotao (MaNganh, TenChuyenNganh) VALUES ('{0}', '{1}')", chuyenNganhDaoTao.MaNganh, chuyenNganhDaoTao.TenChuyenNganh);

                SqlCommand query = new SqlCommand(queryString, conn);
                conn.Open();

                try
                {
                    query.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    return false;
                }
            }
            return true;
        }

        public bool UpdateRecord(ChuyenNganh chuyenNganhDaoTao)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn_tuyensinh"].ConnectionString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in App.config");
                }
                else if (chuyenNganhDaoTao == null)
                    throw new Exception("The passed argument 'chuyenNganhDaoTao' is null");

                string queryString = string.Format("UPDATE chuyennganhdaotao SET MaNganh = '{0}', NhomNganh = {1} WHERE MaNganh = '{0}'", chuyenNganhDaoTao.MaNganh, chuyenNganhDaoTao.TenChuyenNganh);

                SqlCommand query = new SqlCommand(queryString, conn);
                conn.Open();

                try
                {
                    query.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
