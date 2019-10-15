using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Process
{
    public class DBAccessAdmin
    {
        public IEnumerable<Ticket> GetDbTickets()
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStrMrtTicketing"].ConnectionString);

            IList<Ticket> dbTicket = new List<Ticket>();

            conn.Open();

            string sql = @"SELECT * FROM Tickets";
            SqlCommand cmd1 = new SqlCommand(sql, conn);



            SqlDataReader reader = cmd1.ExecuteReader();

            while (reader.Read())
            {
                dbTicket.Add(new Ticket()
                {
                    Name = reader.GetString(1),
                    ICNum = reader.GetString(2),
                    EmailAddress = reader.GetString(3),
                    FromDestination = reader.GetString(4),
                    ToDestination = reader.GetString(5),
                    TypeOfUser = reader.GetString(6),
                    NumOfTicket = reader.GetInt32(7),
                    NumOfWay = reader.GetString(8),
                    TotalPrice = reader.GetDouble(9),
                    DatePurchased = reader.GetDateTime(10)

                });
            }

            conn.Close();
            return dbTicket;
        }
    }
}