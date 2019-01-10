

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DSSProject.Model
{
    public class CoSoRepository
    {
        public List<CoSo> coSoRepository { get; set; }

        public CoSoRepository()
        {
            coSoRepository = GetCoSoRepo();
        }

        public List<CoSo> GetCoSoRepo()
        {
            List<CoSo> listOfCS = new List<CoSo>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn_tuyensinh"].ConnectionString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in App.config");
                }

                SqlCommand query = new SqlCommand("SELECT * FROM cosodaotao", conn);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    CoSo coSo = new CoSo
                    {
                        MaTruong = row["MaTruong"].ToString(),
                        TenTruong = row["TenTruong"].ToString(),
                        DiaChi = row["DiaChi"].ToString(),
                        Website = row["Website"].ToString(),
                        TinhThanh = row["TinhThanh"].ToString(),
                        DVChuQuan = row["DVChuQuan"].ToString(),
                    };

                    listOfCS.Add(coSo);
                }

                return listOfCS;
            }
        }

        public List<CoSo> SearchRecord(string[] arrQuery)
        {
            List<CoSo> listOfCNDT = new List<CoSo>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn_tuyensinh"].ConnectionString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in App.config");
                }

                string queryString = "SELECT * FROM cosodaotao WHERE ";
                for (int i = 0; i < arrQuery.Length; i++)
                {
                    arrQuery[i] = string.Format("(MaTruong LIKE '%{0}%' OR TenTruong LIKE '%{0}%' OR DiaChi LIKE '%{0}%' OR Website LIKE '%{0}%' OR TinhThanh LIKE '%{0}%' OR DVChuQuan LIKE '%{0}%')", arrQuery[i]);
                }

                queryString += string.Join(" AND ", arrQuery);

                SqlCommand query = new SqlCommand(queryString, conn);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    CoSo coSo = new CoSo
                    {
                        MaTruong = row["MaTruong"].ToString(),
                        TenTruong = row["TenTruong"].ToString(),
                        DiaChi = row["DiaChi"].ToString(),
                        Website = row["Website"].ToString(),
                        TinhThanh = row["TinhThanh"].ToString(),
                        DVChuQuan = row["DVChuQuan"].ToString(),
                    };

                    listOfCNDT.Add(coSo);
                }

                return listOfCNDT;
            }
        }

        public bool DelRecord(string maTruong)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn_tuyensinh"].ConnectionString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in App.config");
                }

                string queryString = string.Format("DELETE FROM tuyensinh WHERE MaTruong='{0}';", maTruong);
                queryString += string.Format("DELETE FROM cosodaotao WHERE MaTruong='{0}'", maTruong);

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

        public bool addNewRecord(CoSo coSoDaoTao)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn_tuyensinh"].ConnectionString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in App.config");
                }
                else if (coSoDaoTao == null)
                    throw new Exception("The passed argument 'coSoDaoTao' is null");

                string queryString = string.Format("INSERT INTO cosodaotao (MaTruong, TenTruong, DiaChi, Website, TinhThanh, DVChuQuan) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", coSoDaoTao.MaTruong, coSoDaoTao.TenTruong, coSoDaoTao.DiaChi, coSoDaoTao.Website, coSoDaoTao.TinhThanh, coSoDaoTao.DVChuQuan);

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

        public bool UpdateRecord(CoSo coSoDaoTao)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn_tuyensinh"].ConnectionString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in App.config");
                }
                else if (coSoDaoTao == null)
                    throw new Exception("The passed argument 'coSoDaoTao' is null");

                string queryString = string.Format("UPDATE cosodaotao SET MaTruong='{0}', TenTruong='{1}', DiaChi='{2}', Website='{3}', TinhThanh='{4}', DVChuQuan='{5}' WHERE MaTruong = '{0}'", coSoDaoTao.MaTruong, coSoDaoTao.TenTruong, coSoDaoTao.DiaChi, coSoDaoTao.Website, coSoDaoTao.TinhThanh, coSoDaoTao.DVChuQuan);

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
