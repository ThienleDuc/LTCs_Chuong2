using Microsoft.Data.SqlClient;
using QuanLySinhVien_Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace QuanLySinhVien_Web.Models
{
    public class GiaoVien
    {
        public GiaoVien()
        {
        }

        [Key]
        [StringLength(20)]
        public string MaGV { get; set; }

        [StringLength(100)]
        public string HoTen { get; set; }

        [DataType(DataType.Date)]
        public DateTime? NgaySinh { get; set; }

        [StringLength(10)]
        public string GioiTinh { get; set; }

        [StringLength(200)]
        public string DiaChi { get; set; }

        [StringLength(10)]
        public string Mobile { get; set; }

        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(50)]
        public string HocVi { get; set; }

        [StringLength(50)]
        public string ChucDanh { get; set; }

        [StringLength(50)]
        public string ChucVu { get; set; }

        [StringLength(10)]
        public string PWord { get; set; }

        [StringLength(20)]
        public string StatusGV { get; set; }

        public GiaoVien(string maGV, string hoTen, DateTime? ngaySinh,
            string gioiTinh, string diaChi, string mobile, string email,
            string hocVi, string chucDanh, string chucVu, string pWord, string statusGV)
        {
            MaGV = maGV;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            DiaChi = diaChi;
            Mobile = mobile;
            Email = email;
            HocVi = hocVi;
            ChucDanh = chucDanh;
            ChucVu = chucVu;
            PWord = pWord;
            StatusGV = statusGV;
        }
    }

    public class GiaoVienRepos
    {
        private ConnectionDatabase _connectionDatabase;

        public GiaoVienRepos(ConnectionDatabase connectionDatabase)
        {
            _connectionDatabase = connectionDatabase;
        }

        public List<GiaoVien> GetAllGiaoVien()
        {
            List<GiaoVien> list = new List<GiaoVien>();

            using (SqlConnection conn = _connectionDatabase.GetConnection())
            {
                string query = @"
                SELECT MaGV, HoTen, NgaySinh, GioiTinh, DiaChi, Mobile, Email, 
                       HocVi, ChucDanh, ChucVu, StatusGV 
                FROM GiaoVien";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new GiaoVien
                        {
                            MaGV = reader["MaGV"].ToString(),
                            HoTen = reader["HoTen"].ToString(),
                            NgaySinh = reader["NgaySinh"] == DBNull.Value ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("NgaySinh")),
                            GioiTinh = reader["GioiTinh"].ToString(),
                            DiaChi = reader["DiaChi"].ToString(),
                            Mobile = reader["Mobile"].ToString(),
                            Email = reader["Email"].ToString(),
                            HocVi = reader["HocVi"].ToString(),
                            ChucDanh = reader["ChucDanh"].ToString(),
                            ChucVu = reader["ChucVu"].ToString(),
                            StatusGV = reader["StatusGV"].ToString()
                        });
                    }
                }
            }

            return list;
        }

        public bool AddGiaoVien(GiaoVien giaoVien)
        {
            using (SqlConnection conn = _connectionDatabase.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("AddGiaoVien", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@HoTen", giaoVien.HoTen ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NgaySinh", giaoVien.NgaySinh ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@GioiTinh", giaoVien.GioiTinh ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DiaChi", giaoVien.DiaChi ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Mobile", giaoVien.Mobile ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", giaoVien.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@HocVi", giaoVien.HocVi ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ChucDanh", giaoVien.ChucDanh ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ChucVu", giaoVien.ChucVu ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PWord", giaoVien.PWord ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@StatusGV", giaoVien.StatusGV ?? (object)DBNull.Value);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0; 
            }
        }
    }

}

