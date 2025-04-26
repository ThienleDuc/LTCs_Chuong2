using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LeDucThien_138_BaiTapWeb.Models
{
    public class LopSinhHoat
    {
        [Key]
        [StringLength(20)]
        public string MaLSH { get; set; }

        [StringLength(100)]
        public string TenLSH { get; set; }

        [StringLength(20)]
        public string MaNganh { get; set; }

        public int? SiSo { get; set; }

        [StringLength(20)]
        public string MaKhoa { get; set; }

        [StringLength(20)]
        public string MaGVCN { get; set; }

        [StringLength(100)]
        public string HoTen { get; set; }

        public int? KhoaHoc { get; set; }

        [StringLength(255)]
        public string GhiChu { get; set; }

        public LopSinhHoat(string maLSH, string tenLSH, string maNganh, int? siSo, string maKhoa,
            string maGVCN, string hoTen, int? khoaHoc, string ghiChu)
        {
            MaLSH = maLSH;
            TenLSH = tenLSH;
            MaNganh = maNganh;
            SiSo = siSo;
            MaKhoa = maKhoa;
            MaGVCN = maGVCN;
            HoTen = hoTen;
            KhoaHoc = khoaHoc;
            GhiChu = ghiChu;
        }

        public LopSinhHoat()
        {
        }
    }
    public class LopSinhHoatRepos
    {
        private readonly ConnectionDatabase _connectionDb;

        public LopSinhHoatRepos(ConnectionDatabase connectionDb)
        {
            _connectionDb = connectionDb;
        }

        public List<LopSinhHoat> GetAllLopSinhHoat()
        {
            List<LopSinhHoat> list = new List<LopSinhHoat>();

            using (SqlConnection conn = _connectionDb.GetConnection())
            {
                string query = @"
                SELECT 
                    L.MaLSH, 
                    L.TenLSH, 
                    L.MaNganh, 
                    L.SiSo, 
                    L.MaKhoa, 
                    L.MaGVCN, 
                    G.HoTen,
                    L.KhoaHoc, 
                    L.GhiChu
                FROM 
                    LopSinhHoat L
                JOIN 
                    GiaoVien G ON L.MaGVCN = G.MaGV";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new LopSinhHoat
                        {
                            MaLSH = reader["MaLSH"].ToString(),
                            TenLSH = reader["TenLSH"].ToString(),
                            MaNganh = reader["MaNganh"].ToString(),
                            SiSo = reader["SiSo"] != DBNull.Value ? Convert.ToInt32(reader["SiSo"]) : (int?)null,
                            MaKhoa = reader["MaKhoa"].ToString(),
                            MaGVCN = reader["MaGVCN"].ToString(),
                            HoTen = reader["HoTen"].ToString(),
                            KhoaHoc = reader["KhoaHoc"] != DBNull.Value ? Convert.ToInt32(reader["KhoaHoc"]) : (int?)null,
                            GhiChu = reader["GhiChu"].ToString()
                        });
                    }
                }
            }

            return list;
        }
    }
}
