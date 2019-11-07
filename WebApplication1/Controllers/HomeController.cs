using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Process;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult FAQ()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserAccount user)
        {
            if (ModelState.IsValid)
            {
                /*string password = user.Password;
            PBKDF2Hash PwdHash = new PBKDF2Hash(password);
            string passwordhash = PwdHash.HashedPassword;*/
            bool enabled = true;

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStrMrtTicketing"].ConnectionString);
            string sql = @"INSERT INTO UserAccounts VALUES(@EmailAddress, @PasswordHash, @Role, @Enabled)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@EmailAddress",user.EmailAddress);
            cmd.Parameters.AddWithValue("@PasswordHash", user.Password);
            cmd.Parameters.AddWithValue("@Role", "user");
            cmd.Parameters.AddWithValue("@Enabled", enabled);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                ViewBag.Text = "Status: Data successfully saved.";
            }
            catch
            {
                ViewBag.Text= "Email address already existed. Please register with another email address";
                return View("RegisterFail", user);
            }
            finally
            {
                conn.Close();
            }

                return View("RegisterScc", user);
            }
            else
            {
                return View();
            }
        }

        public ActionResult RegisterScc()
        {
            return View();
        }


        public ActionResult RegisterFail()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLogin user)
        {
            if (ModelState.IsValid)
            {
            string sql = "SELECT * FROM UserAccounts WHERE EmailAddress=@EmailAddress AND PasswordHash=@Password";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStrMrtTicketing"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@EmailAddress", user.EmailAddress);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    Object objrole = dt.Rows[0]["Role"];
                    Object objenabled = dt.Rows[0]["Enabled"];
                    bool enabled = Convert.ToBoolean(objenabled);

                    if (enabled == true)
                    {
                        System.Web.HttpContext.Current.Session["username"]= user.EmailAddress;
                        System.Web.HttpContext.Current.Session["role"] = objrole;

                        if (System.Web.HttpContext.Current.Session["role"].ToString() == "admin")
                        {
                            return RedirectToAction("Index", "Admin");

                        }
                        else if (System.Web.HttpContext.Current.Session["role"].ToString() == "user") { 
                            return RedirectToAction("Index", "Customer");
                        }
                        else { 
                      
                        return View();
                        }
                    }

                }
                else
                {
                    ViewBag.Text = "You're email address is not registered yet. Please proceed to account registration";
                    return View();
                }
            }
            ViewBag.Text = "You're email address or/and password is incorrect";
            return View();
        }

          
       
        //end of controller
    }
}
