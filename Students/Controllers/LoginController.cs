using Students.Context;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Students.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        //db_testEntities dbobj = new db_testEntities();
        
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public JsonResult LoginS(string username, string password)
        {
            // Check if credentials are valid
            if (isValid(username, password))
            {
                // Create session and store user details
                Session["User"] = username;

                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public JsonResult Logout()
        {
            // Destroy session
            Session.Abandon();

            return Json(new { success = true });
        }

        private bool isValid(string username, string password)
        {
            bool isValidUser = false;
            using (SqlConnection con = new SqlConnection("Data Source=AR_RAHMAN;Initial Catalog=db_test;User ID=sa;Password=sql123@"))
            {
                // Create a SQL command to select the user with the given username and password
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Admin WHERE Email=@Username AND Password=@Password", con);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                // Open the database connection
                con.Open();

                // Execute the SQL command and get the count of matching rows
                int count = (int)cmd.ExecuteScalar();

                // Check if there is a row that matches the given username and password
                if (count > 0)
                {
                    isValidUser = true;
                }

                // Close the database connection
                con.Close();
            }

            return isValidUser;
        }
    }
}