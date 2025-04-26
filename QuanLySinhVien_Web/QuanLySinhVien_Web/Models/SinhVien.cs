using Microsoft.Data.SqlClient;
using QuanLySinhVien_Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace QuanLySinhVien_Web.Models
{
    public class SinhVien
    {
        public SinhVien()
        {
        }

        [Key]
        [StringLength(20)]
        public string MaSV { get; set; }

        [StringLength(50)]
        public string Ho { get; set; }

        [StringLength(50)]
        public string Ten { get; set; }

        [DataType(DataType.Date)]
        public DateTime? NgaySinh { get; set; }

        [StringLength(10)]
        public string GioiTinh { get; set; }

        [StringLength(100)]
        public string NoiSinh { get; set; }

        [StringLength(200)]
        public string DiaChi { get; set; }

        [StringLength(30)]
        public string DanToc { get; set; }

        [StringLength(10)]
        public string Mobile { get; set; }

        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(20)]
        public string StatusSV { get; set; }

        [StringLength(255)]
        public string GhiChu { get; set; }

        [StringLength(10)]
        public string PWord { get; set; }

        public SinhVien(string maSV, string ho, string ten, DateTime? ngaySinh, string gioiTinh,
            string noiSinh, string diaChi, string danToc,
            string mobile, string email, string statusSV, string ghiChu, string pWord)
        {
            MaSV = maSV;
            Ho = ho;
            Ten = ten;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            NoiSinh = noiSinh;
            DiaChi = diaChi;
            DanToc = danToc;
            Mobile = mobile;
            Email = email;
            StatusSV = statusSV;
            GhiChu = ghiChu;
            PWord = pWord;
        }
    }

    public class SinhVienRepos
    {
        private ConnectionDatabase _connectionDatabase;

        public SinhVienRepos(ConnectionDatabase connectionDatabase)
        {
            _connectionDatabase = connectionDatabase;
        }

        public List<SinhVien> GetAllSinhVien()
        {
            List<SinhVien> list = new List<SinhVien>();
            using (SqlConnection conn = _connectionDatabase.GetConnection())
            {
                string query = @"SELECT MaSV, Ho, Ten, NgaySinh, GioiTinh, NoiSinh, DiaChi, DanToc,
                Mobile, Email, StatusSV, GhiChu FROM SinhVien;";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new SinhVien
                        {
                            MaSV = reader["MaSV"].ToString(),
                            Ho = reader["Ho"].ToString(),
                            Ten = reader["Ten"].ToString(),
                            NgaySinh = reader["NgaySinh"] == DBNull.Value ? null : (DateTime?)reader.GetDateTime("NgaySinh"),
                            GioiTinh = reader["GioiTinh"].ToString(),
                            NoiSinh = reader["NoiSinh"].ToString(),
                            DiaChi = reader["DiaChi"].ToString(),
                            DanToc = reader["DanToc"].ToString(),
                            Mobile = reader["Mobile"].ToString(),
                            Email = reader["Email"].ToString(),
                            StatusSV = reader["StatusSV"].ToString(),
                            GhiChu = reader["GhiChu"].ToString()
                        });
                    }
                }

            }

            return list;
        }

        public List<SinhVien> TimKiemSinhVien(string keyword)
        {
            List<SinhVien> danhSach = new List<SinhVien>();

            using (SqlConnection conn = _connectionDatabase.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SearchSinhVien", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Keyword", keyword);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SinhVien sv = new SinhVien
                            {
                                MaSV = reader["MaSV"].ToString(),
                                Ho = reader["Ho"].ToString(),
                                Ten = reader["Ten"].ToString(),
                                NgaySinh = reader["NgaySinh"] as DateTime?,
                                GioiTinh = reader["GioiTinh"].ToString(),
                                NoiSinh = reader["NoiSinh"].ToString(),
                                DiaChi = reader["DiaChi"].ToString(),
                                DanToc = reader["DanToc"].ToString(),
                                Mobile = reader["Mobile"].ToString(),
                                Email = reader["Email"].ToString(),
                                StatusSV = reader["StatusSV"].ToString(),
                                GhiChu = reader["GhiChu"].ToString(),
                            };

                            danhSach.Add(sv);
                        }
                    }
                }
            }

            return danhSach;
        }

        public SinhVien GetSinhVienByMaSV(string maSinhVien)
        {
            SinhVien sinhVien = null;

            using (SqlConnection conn = _connectionDatabase.GetConnection())
            {
                string query = "SELECT * FROM SinhVien WHERE MaSV = @MaSV";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSV", maSinhVien);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    sinhVien = new SinhVien
                    {
                        MaSV = reader["MaSV"].ToString(),
                        Ho = reader["Ho"].ToString(),
                        Ten = reader["Ten"].ToString(),
                        NgaySinh = Convert.ToDateTime(reader["NgaySinh"]),
                        GioiTinh = reader["GioiTinh"].ToString(),
                        NoiSinh = reader["NoiSinh"].ToString(),
                        DiaChi = reader["DiaChi"].ToString(),
                        DanToc = reader["DanToc"].ToString(),
                        Mobile = reader["Mobile"].ToString(),
                        Email = reader["Email"].ToString(),
                        StatusSV = reader["StatusSV"].ToString(),
                        GhiChu = reader["GhiChu"].ToString(),
                        PWord = reader["PWord"].ToString()
                    };
                }
            }

            return sinhVien;
        }

        public bool AddSinhVien(SinhVien sinhVien)
        {
            using (SqlConnection conn = _connectionDatabase.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("AddSinhVien", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Ho", sinhVien.Ho ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Ten", sinhVien.Ten ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NgaySinh", sinhVien.NgaySinh ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@GioiTinh", sinhVien.GioiTinh ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NoiSinh", sinhVien.NoiSinh ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DiaChi", sinhVien.DiaChi ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DanToc", sinhVien.DanToc ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Mobile", sinhVien.Mobile ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", sinhVien.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@StatusSV", sinhVien.StatusSV ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@GhiChu", sinhVien.GhiChu ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PWord", sinhVien.PWord ?? (object)DBNull.Value);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0; 
            }
        }

        public bool UpdateSinhVien(SinhVien sinhVien)
        {
            using (SqlConnection conn = _connectionDatabase.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("UpdateSinhVien", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MaSV", sinhVien.MaSV ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Ho", sinhVien.Ho ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Ten", sinhVien.Ten ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NgaySinh", sinhVien.NgaySinh ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@GioiTinh", sinhVien.GioiTinh ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NoiSinh", sinhVien.NoiSinh ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DiaChi", sinhVien.DiaChi ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DanToc", sinhVien.DanToc ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Mobile", sinhVien.Mobile ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", sinhVien.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@StatusSV", sinhVien.StatusSV ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@GhiChu", sinhVien.GhiChu ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PWord", sinhVien.PWord ?? (object)DBNull.Value);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool DeleteSinhVien(string maSV)
        {
            using (SqlConnection conn = _connectionDatabase.GetConnection())
            {
                string query = "DELETE FROM SinhVien WHERE MaSV = @MaSV";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", maSV);

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }
    }
}