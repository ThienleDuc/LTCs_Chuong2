﻿using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;

namespace LeDucThien_138_BaiTapWeb.Models
{
    public class ConnectionDatabase
    {
        private readonly string _databaseName;

        public ConnectionDatabase(IConfiguration configuration)
        {
            _databaseName = configuration.GetConnectionString("DefaultConnection");
        }

        public SqlConnection GetConnection()
        {
            try
            {
                // Tạo và trả về đối tượng SqlConnection
                SqlConnection connection = new SqlConnection(_databaseName);
                return connection;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và có thể ném lỗi hoặc trả về null
                Console.WriteLine("Lỗi khi tạo kết nối: " + ex.Message);
                return null;
            }
        }
    }
}
