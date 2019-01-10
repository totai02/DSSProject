using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DSSProject.Model
{
    class TraCuuRepository
    {
        public TraCuuRepository()
        {
        }

        public List<string> GetUniqueNamDaoTao()
        {
            List<string> listOfNDT = new List<string>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn_tuyensinh"].ConnectionString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in App.config");
                }

                SqlCommand query = new SqlCommand("SELECT DISTINCT NamDaoTao FROM tuyensinh", conn);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    listOfNDT.Add(row["NamDaoTao"].ToString());
                }

                return listOfNDT;
            }
        }

        public List<CoSo> TraCuu(string queryString)
        {
            List<CoSo> listOfCS = new List<CoSo>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn_tuyensinh"].ConnectionString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in App.config");
                }

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

                    listOfCS.Add(coSo);
                }

                return listOfCS;
            }
        }

        public Tuple<CoSo, List<string>, List<List<KeyValuePair<string, string>>>> GetDetail(string maTruong)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn_tuyensinh"].ConnectionString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in App.config");
                }

                string queryString = "SELECT cosodaotao.MaTruong, TenTruong, DiaChi, Website, TinhThanh, DVChuQuan, ChiTieu, NamDaoTao, TenChuyenNganh FROM chuyennganhdaotao, cosodaotao, tuyensinh";
                queryString += string.Format(" WHERE cosodaotao.MaTruong = tuyensinh.MaTruong AND tuyensinh.MaNganh = chuyennganhdaotao.MaNganh AND cosodaotao.MaTruong = '{0}'", maTruong);

                SqlCommand query = new SqlCommand(queryString, conn);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                CoSo coSo = null;
                List<string> listOfNameDT = new List<string>();
                List<List<KeyValuePair<string, string>>> listOfCN = new List<List<KeyValuePair<string, string>>>();

                foreach (DataRow row in dataTable.Rows)
                {
                    if (coSo == null)
                    {
                        coSo = new CoSo
                        {
                            MaTruong = row["MaTruong"].ToString(),
                            TenTruong = row["TenTruong"].ToString(),
                            DiaChi = row["DiaChi"].ToString(),
                            Website = row["Website"].ToString(),
                            TinhThanh = row["TinhThanh"].ToString(),
                            DVChuQuan = row["DVChuQuan"].ToString(),
                        };

                        listOfNameDT.Add(row["NamDaoTao"].ToString());

                        List<KeyValuePair<string, string>> newCN = new List<KeyValuePair<string, string>>();
                        newCN.Add(new KeyValuePair<string, string>(row["TenChuyenNganh"].ToString(), row["ChiTieu"].ToString()));
                        listOfCN.Add(newCN);
                    } 
                    else 
                    {
                        int idx = -1;
                        for (int i = 0; i < listOfNameDT.Count; i++)
                        {
                            if (string.Equals(listOfNameDT[i], row["NamDaoTao"].ToString()))
                            {
                                idx = i;
                                break;
                            }
                        }

                        if (idx == -1)
                        {
                            listOfNameDT.Add(row["NamDaoTao"].ToString());

                            List<KeyValuePair<string, string>> newCN = new List<KeyValuePair<string, string>>();
                            newCN.Add(new KeyValuePair<string, string>(row["TenChuyenNganh"].ToString(), row["ChiTieu"].ToString()));
                            listOfCN.Add(newCN);
                        }
                        else
                        {
                            listOfCN[idx].Add(new KeyValuePair<string, string>(row["TenChuyenNganh"].ToString(), row["ChiTieu"].ToString()));
                        }
                    }

                }

                return new Tuple<CoSo, List<string>, List<List<KeyValuePair<string, string>>>>(coSo, listOfNameDT, listOfCN);
            }
        }
    }
}
