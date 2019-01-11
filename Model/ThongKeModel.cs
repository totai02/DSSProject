using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSSProject.Model
{
    public class ThongKeModel
    {
        public List<string> SoLuongTheoNganh(string nganh)
        {
            List<string> listOfSL = new List<string>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn_tuyensinh"].ConnectionString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in App.config");
                }

                string queryString = "SELECT SUM(ChiTieu) AS SoLuong FROM chuyennganhdaotao, tuyensinh WHERE chuyennganhdaotao.MaNganh = tuyensinh.MaNganh";
                queryString += string.Format(" AND chuyennganhdaotao.MaNganh = '{0}' GROUP BY NamDaoTao", nganh);
                SqlCommand query = new SqlCommand(queryString, conn);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    listOfSL.Add(row["SoLuong"].ToString());
                }

                return listOfSL;
            }
        }

    }
}
