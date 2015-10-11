using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;


namespace FamilyFinance
{
    class DBHelper
    {

        /// <summary>
        /// 连接数据库字段
        /// </summary>
        public static string connectionString = @"Data Source=dell-PC\SQLEXPRESS;Initial Catalog=Family;User ID=sa;Password=mwq1992414";
        public static SqlConnection connection = new SqlConnection(connectionString);

    }
}
