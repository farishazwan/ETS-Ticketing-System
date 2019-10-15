using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Process
{
    public class DbAccessUserList
    {
        public IEnumerable<UserAccount> GetDbUserList()
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStrMrtTicketing"].ConnectionString);

            IList<UserAccount> dbUserList = new List<UserAccount>();

            conn.Open();

            string sql = @"SELECT * FROM UserAccounts";
            SqlCommand cmd1 = new SqlCommand(sql, conn);

            SqlDataReader reader = cmd1.ExecuteReader();

            while (reader.Read())
            {
                dbUserList.Add(new UserAccount()
                {
                    Id = reader.GetInt32(0),
                    EmailAddress = reader.GetString(1),
                    Password = reader.GetString(2),
                    Role = reader.GetString(3),
                    Enabled = reader.GetBoolean(4)

                });
            }

            conn.Close();
            return dbUserList;
        }
    }
}