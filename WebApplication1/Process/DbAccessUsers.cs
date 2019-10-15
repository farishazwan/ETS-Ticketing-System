using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Process
{
    public class DbAccessUsers
    {
        public IEnumerable<User> GetDbUser()
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStrMrtTicketing"].ConnectionString);

            IList<User> dbUser = new List<User>();

            conn.Open();

            SqlCommand cmd2 = new SqlCommand("SELECT Id from UserAccounts WHERE UserAccounts.EmailAddress='" + System.Web.HttpContext.Current.Session["username"] + "'  ", conn);
            // string userID=ConvertCommandParamatersToLiteralValues(cmd2);
            Int32 ticketUses = ((Int32?)cmd2.ExecuteScalar()) ?? 0;

            string sql = @"SELECT * FROM Users WHERE Users.UserId='" + ticketUses + "'";
            SqlCommand cmd1 = new SqlCommand(sql, conn);


            SqlDataReader reader = cmd1.ExecuteReader();

            while (reader.Read())
            {
                dbUser.Add(new User()
                {
                    ProfileId = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    ICNum = reader.GetString(3),
                    PhoneNum = reader.GetString(4),
                    Address = reader.GetString(5),
                    profileImg = reader.GetString(6),
                    UserId = reader.GetInt32(7)

                });
            }

            conn.Close();
            return dbUser;
        }
    }
}