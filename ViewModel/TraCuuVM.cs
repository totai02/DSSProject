using DSSProject.Model;
using System;
using System.Collections.Generic;

namespace DSSProject.ViewModel
{
    class TraCuuVM
    {
        private TraCuuRepository traCuuRepo { get; set; }

        public TraCuuVM()
        {
            traCuuRepo = new TraCuuRepository();
        }

        public List<string> GetUniqueNamDaoTao()
        {
            return traCuuRepo.GetUniqueNamDaoTao();
        }

        public List<CoSo> TraCuu(List<string> maNganh, List<string> namDaoTao, int from = -1, int to = -1)
        {
            string queryString = "SELECT DISTINCT cosodaotao.MaTruong, TenTruong, DiaChi, Website, TinhThanh, DVChuQuan FROM chuyennganhdaotao, cosodaotao, tuyensinh";
            queryString += " WHERE cosodaotao.MaTruong = tuyensinh.MaTruong AND tuyensinh.MaNganh = chuyennganhdaotao.MaNganh";
            
            for (int i = 0; i < maNganh.Count; i++)
            {
                maNganh[i] = string.Format("EXISTS (SELECT 1 FROM tuyensinh WHERE tuyensinh.MaTruong = cosodaotao.MaTruong AND tuyensinh.MaNganh = {0})", maNganh[i]);
            }
            if (maNganh.Count > 0)
            {
                queryString += " AND " + string.Join(" AND ", maNganh);
            }

            for (int i = 0; i < namDaoTao.Count; i++)
            {
                namDaoTao[i] = string.Format("tuyensinh.NamDaoTao = '{0}'", namDaoTao[i]);
            }
            if (namDaoTao.Count > 0) queryString += string.Format(" AND ({0})", string.Join(" OR ", namDaoTao));

            if (from > 0)
            {
                queryString += string.Format(" AND tuyensinh.ChiTieu >= '{0}'", from);
            }

            if (to > 0)
            {
                queryString += string.Format(" AND tuyensinh.ChiTieu <= '{0}'", to);
            }

            return traCuuRepo.TraCuu(queryString);
        }

        public Tuple<CoSo, List<string>, List<List<KeyValuePair<string, string>>>> GetDetail(string maTruong)
        {
            return traCuuRepo.GetDetail(maTruong);
        }
    }
}
