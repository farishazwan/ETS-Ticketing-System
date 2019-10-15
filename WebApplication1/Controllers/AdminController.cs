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
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.Session["username"] != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ViewPurchase(Ticket ticket)
        {
            if (System.Web.HttpContext.Current.Session["username"] != null)
            {
                DBAccessAdmin db = new DBAccessAdmin();
                IEnumerable<Ticket> dbItem = db.GetDbTickets();
                return View(dbItem);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult ViewUser(User user)
        {

            if (System.Web.HttpContext.Current.Session["username"] != null)
            {
                DbAccessUserList db = new DbAccessUserList();
                IEnumerable<UserAccount> dbItem = db.GetDbUserList();
                return View(dbItem);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult ViewProfile()
        {
            if (System.Web.HttpContext.Current.Session["username"] != null)
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStrMrtTicketing"].ConnectionString);

                IList<User> dbUser = new List<User>();

                conn.Open();

                SqlCommand cmd2 = new SqlCommand("SELECT Id from UserAccounts WHERE UserAccounts.EmailAddress='" + System.Web.HttpContext.Current.Session["username"] + "'  ", conn);
                // string userID=ConvertCommandParamatersToLiteralValues(cmd2);
                Int32 ticketUses = ((Int32?)cmd2.ExecuteScalar()) ?? 0;

                string sql = @"SELECT * FROM Users WHERE Users.UserId='" + ticketUses + "'";
                SqlCommand cmd1 = new SqlCommand(sql, conn);

                Int32 result = ((Int32?)cmd1.ExecuteScalar()) ?? 0;

                if (result == 0)
                {
                    return RedirectToAction("CreateProfile", "Admin");
                }
                else
                {
                    DbAccessUsers db = new DbAccessUsers();
                    IEnumerable<User> dbItem = db.GetDbUser();

                    return View(dbItem);
                }

            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult CreateProfile()
        {
            if (System.Web.HttpContext.Current.Session["username"] != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public ActionResult CreateProfile(User user)
        {
            if (ModelState.IsValid)
            {

                if (HttpContext.Request.Files.AllKeys.Any())
                {
                    // Get the uploaded image from the Files collection
                    var httpPostedFile = HttpContext.Request.Files[0];

                    if (httpPostedFile != null)
                    {
                        // Validate the uploaded image(optional)

                        // Get the complete file path
                        var fileSavePath = (HttpContext.Server.MapPath("~/ProfileImg") + httpPostedFile.FileName.Substring(httpPostedFile.FileName.LastIndexOf(@"\")));

                        // Save the uploaded file to "UploadedFiles" folder
                        httpPostedFile.SaveAs(fileSavePath);

                        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStrMrtTicketing"].ConnectionString);
                        SqlCommand cmd = new SqlCommand("spInsertUserDetail", conn);

                        SqlCommand cmd2 = new SqlCommand("SELECT Id from UserAccounts WHERE UserAccounts.EmailAddress='" + System.Web.HttpContext.Current.Session["username"] + "'  ", conn);
                        // string userID=ConvertCommandParamatersToLiteralValues(cmd2);
                        conn.Open();
                        Int32 ticketUses = ((Int32?)cmd2.ExecuteScalar()) ?? 0;

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", user.LastName);
                        cmd.Parameters.AddWithValue("@ICNum", user.ICNum);
                        cmd.Parameters.AddWithValue("@PhoneNum", user.PhoneNum);
                        cmd.Parameters.AddWithValue("@Address", user.Address);
                        cmd.Parameters.AddWithValue("@profileImg", httpPostedFile.FileName.Substring(httpPostedFile.FileName.LastIndexOf(@"\")));
                        cmd.Parameters.AddWithValue("@UserId", ticketUses);
                        conn.Close();

                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            ViewBag.MessageError(ex.Message.ToString(), "Error Message");
                            return View();
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
                return RedirectToAction("ViewProfile", "Admin");
            }

            //Model state
            else
            {
                return View();
            }
        }


        public ActionResult EditProfile()
        {
            if (System.Web.HttpContext.Current.Session["username"] != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditProfile(int id, User user)
        {

            if (ModelState.IsValid)
            {

                /* if (imageName != null)
                 {
                     string path = Path.Combine(Server.MapPath("~/ProfileImg"), Path.GetFileName(imageName.FileName));
                     imageName.SaveAs(path);

                 }*/

                if (HttpContext.Request.Files.AllKeys.Any())
                {
                    // Get the uploaded image from the Files collection
                    var httpPostedFile = HttpContext.Request.Files[0];

                    if (httpPostedFile != null)
                    {
                        // Validate the uploaded image(optional)

                        // Get the complete file path
                        var fileSavePath = (HttpContext.Server.MapPath("~/ProfileImg") + httpPostedFile.FileName.Substring(httpPostedFile.FileName.LastIndexOf(@"\")));

                        // Save the uploaded file to "UploadedFiles" folder
                        httpPostedFile.SaveAs(fileSavePath);

                        SqlConnection conn = new SqlConnection(ConfigurationManager.
                 ConnectionStrings["connStrMrtTicketing"].ConnectionString);
                        SqlCommand cmd = new SqlCommand("spUpdateUserDetail", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ProfileId", id);
                        cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", user.LastName);
                        cmd.Parameters.AddWithValue("@ICNum", user.ICNum);
                        cmd.Parameters.AddWithValue("@PhoneNum", user.PhoneNum);
                        cmd.Parameters.AddWithValue("@Address", user.Address);
                        cmd.Parameters.AddWithValue("@profileImg", httpPostedFile.FileName.Substring(httpPostedFile.FileName.LastIndexOf(@"\")));

                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            ViewBag.MessageError(ex.Message.ToString(), "Error Message");
                            return View();
                        }
                        finally
                        {
                            conn.Close();
                        }

                    }
                }
                return RedirectToAction("ViewProfile", "Admin");
            }
            else
            {
                return View();
            }

        }



        //end of controller
    }
}