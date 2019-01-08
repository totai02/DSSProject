using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSSProject.Model
{
    public class CoSoTheoNganhRepository
    {
        public List<CoSoTheoNganh> coSoRepository { get; set; }

        public CoSoTheoNganhRepository()
        {
            coSoRepository = GetCoSoRepo();
        }

        public List<CoSoTheoNganh> GetCoSoRepo()
        {
            List<CoSoTheoNganh> listOfCSTN = new List<CoSoTheoNganh>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn_nguon_nhan_luc"].ConnectionString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in App.config");
                }

                SqlCommand query = new SqlCommand("SELECT * FROM cosotheonganh", conn);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    CoSoTheoNganh soSo = new CoSoTheoNganh
                    {
                        MaTruong = row["MaTruong"].ToString(),
                        MaNganh = row["MaNganh"].ToString(),
                        NamDT = (int)row["NamDT"],
                        SoCB = (int)row["SoCB"],
                        DiemChuan = (float)row["DiemChuan"],
                        ChiTieu = (int)row["ChiTieu"],
                        SlDaTuyen = (int)row["SLDaTuyen"],
                    };

                    listOfCSTN.Add(soSo);
                }

                return listOfCSTN;
            }
        }
    }
}
