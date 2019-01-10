using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DSSProject.Model
{
    public class TuyenSinhRepository
    {
        public List<TuyenSinh> tuyenSinhRepository { get; set; }

        public TuyenSinhRepository()
        {
            tuyenSinhRepository = GetTuyenSinhRepo();
        }

        public List<TuyenSinh> GetTuyenSinhRepo()
        {
            List<TuyenSinh> listOfTS = new List<TuyenSinh>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn_tuyensinh"].ConnectionString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in App.config");
                }

                string queryString = "SELECT tuyensinh.MaTruong, TenTruong, tuyensinh.MaNganh, TenChuyenNganh, ChiTieu, NamDaoTao FROM tuyensinh";
                queryString += " JOIN cosodaotao ON tuyensinh.MaTruong = cosodaotao.MaTruong";
                queryString += " JOIN chuyennganhdaotao ON chuyennganhdaotao.MaNganh = tuyensinh.MaNganh";

                SqlCommand query = new SqlCommand(queryString, conn);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    TuyenSinh tuyenSinh = new TuyenSinh
                    {
                        MaTruong = row["MaTruong"].ToString(),
                        TenTruong = row["TenTruong"].ToString(),
                        MaNganh = row["MaNganh"].ToString(),
                        TenNganh = row["TenChuyenNganh"].ToString(),
                        ChiTieu = (int)row["ChiTieu"],
                        NamDaoTao = (int)row["NamDaoTao"]
                    };

                    listOfTS.Add(tuyenSinh);
                }

                return listOfTS;
            }
        }

        public List<TuyenSinh> SearchRecord(string[] arrQuery)
        {
            List<TuyenSinh> listOfCNDT = new List<TuyenSinh>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn_tuyensinh"].ConnectionString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in App.config");
                }

                string queryString = "SELECT * FROM tuyensinh WHERE ";
                for (int i = 0; i < arrQuery.Length; i++)
                {
                    arrQuery[i] = string.Format("(MaTruong LIKE '%{0}%' OR MaNganh LIKE '%{0}%' OR ChiTieu LIKE '%{0}%' OR NamDaoTao LIKE '%{0}%')", arrQuery[i]);
                }

                queryString += string.Join(" AND ", arrQuery);

                SqlCommand query = new SqlCommand(queryString, conn);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    TuyenSinh tuyenSinh = new TuyenSinh
                    {
                        MaTruong = row["MaTruong"].ToString(),
                        MaNganh = row["MaNganh"].ToString(),
                        ChiTieu = (int)row["ChiTieu"],
                        NamDaoTao = (int)row["NamDaoTao"]
                    };

                    listOfCNDT.Add(tuyenSinh);
                }

                return listOfCNDT;
            }
        }

        public bool DelRecord(string maTruong, string maNganh)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn_tuyensinh"].ConnectionString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in App.config");
                }

                string queryString = string.Format("DELETE FROM tuyensinh WHERE MaTruong='{0}' AND MaNganh='{1}'", maTruong, maNganh);

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

        public bool addNewRecord(TuyenSinh tuyenSinh)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn_tuyensinh"].ConnectionString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in App.config");
                }
                else if (tuyenSinh == null)
                    throw new Exception("The passed argument 'coSoDaoTao' is null");

                string queryString = string.Format("INSERT INTO tuyensinh (MaTruong, MaNganh, ChiTieu, NamDaoTao) VALUES ('{0}', '{1}', '{2}', '{3}')", tuyenSinh.MaTruong, tuyenSinh.MaNganh, tuyenSinh.ChiTieu, tuyenSinh.NamDaoTao);

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

        public bool UpdateRecord(TuyenSinh tuyenSinh)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn_tuyensinh"].ConnectionString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in App.config");
                }
                else if (tuyenSinh == null)
                    throw new Exception("The passed argument 'coSoDaoTao' is null");

                string queryString = string.Format("UPDATE tuyensinh SET MaTruong='{0}', MaNganh='{1}', ChiTieu='{2}', NamDaoTao='{3}' WHERE MaTruong='{0}' AND MaNganh='{1}'", tuyenSinh.MaTruong, tuyenSinh.MaNganh, tuyenSinh.ChiTieu, tuyenSinh.NamDaoTao);

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
