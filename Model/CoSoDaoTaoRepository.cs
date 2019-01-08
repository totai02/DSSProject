

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DSSProject.Model
{
    public class CoSoDaoTaoRepository
    {
        public List<CoSoDaoTao> coSoRepository { get; set; }

        public CoSoDaoTaoRepository()
        {
            coSoRepository = GetCoSoRepo();
        }

        public List<CoSoDaoTao> GetCoSoRepo()
        {
            List<CoSoDaoTao> listOfCS = new List<CoSoDaoTao>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn_nguon_nhan_luc"].ConnectionString))
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
                    CoSoDaoTao coSo = new CoSoDaoTao
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
    }
}
