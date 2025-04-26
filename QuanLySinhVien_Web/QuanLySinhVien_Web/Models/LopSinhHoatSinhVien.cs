using Microsoft.Data.SqlClient;
using QuanLySinhVien_Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySinhVien_Web.Models
{
    public class LopSinhHoatSinhVien
    {
        public LopSinhHoatSinhVien()
        {
        }

        [Key, Column(Order = 0)]
        [StringLength(20)]
        public string MaLSH { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(20)]
        public string MaSV { get; set; }

        [StringLength(100)]
        public string HoTen { get; set; }

        public int? KiHoc { get; set; }

        public int? TichLuy { get; set; }

        public int? GOP { get; set; }

        public int? EOP { get; set; }

        [StringLength(20)]
        public string StatusLSHSV { get; set; }

        [StringLength(255)]
        public string GhiChu { get; set; }

        public LopSinhHoatSinhVien(string maLSH, string maSV, string hoTen, int? kiHoc,
            int? tichLuy, int? gOP, int? eOP, string statusLSHSV, string ghiChu)
        {
            MaLSH = maLSH;
            MaSV = maSV;
            HoTen = hoTen;
            KiHoc = kiHoc;
            TichLuy = tichLuy;
            GOP = gOP;
            EOP = eOP;
            StatusLSHSV = statusLSHSV;
            GhiChu = ghiChu;
        }

    }

    public class LopSinhHoatSinhVienRepos
    {
        private readonly ConnectionDatabase _connectionDb;

        public LopSinhHoatSinhVienRepos(ConnectionDatabase connectionDb)
        {
            _connectionDb = connectionDb;
        }

        public List<LopSinhHoatSinhVien> GetAllLopSinhHoatSinhVien()
        {
            List<LopSinhHoatSinhVien> list = new List<LopSinhHoatSinhVien>();

            using (SqlConnection conn = _connectionDb.GetConnection())
            {
                string query = @"
                SELECT 
                    L.MaLSH, 
                    L.MaSV, 
                    S.Ho + ' ' + S.Ten AS HoTen, 
                    L.KiHoc, 
                    L.TichLuy, 
                    L.GOP, 
                    L.EOP, 
                    L.StatusLSHSV, 
                    L.GhiChu
                FROM 
                    LopSinhHoatSinhVien L
                JOIN 
                    SinhVien S ON L.MaSV = S.MaSV";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new LopSinhHoatSinhVien
                        {
                            MaLSH = reader["MaLSH"].ToString(),
                            MaSV = reader["MaSV"].ToString(),
                            HoTen = reader["HoTen"].ToString(),
                            KiHoc = reader["KiHoc"] != DBNull.Value ? Convert.ToInt32(reader["KiHoc"]) : (int?)null,
                            TichLuy = reader["TichLuy"] != DBNull.Value ? Convert.ToInt32(reader["TichLuy"]) : (int?)null,
                            GOP = reader["GOP"] != DBNull.Value ? Convert.ToInt32(reader["GOP"]) : (int?)null,
                            EOP = reader["EOP"] != DBNull.Value ? Convert.ToInt32(reader["EOP"]) : (int?)null,
                            StatusLSHSV = reader["StatusLSHSV"].ToString(),
                            GhiChu = reader["GhiChu"].ToString()
                        });
                    }
                }
            }

            return list;
        }
    }

}