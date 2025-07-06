using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;


public partial class Home_UserLogin : System.Web.UI.Page
{
    string conStr = ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            string query = "SELECT UserID FROM Vendor_Registration WHERE Email = @Email AND PasswordHash = @Password";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());

            con.Open();
            object result = cmd.ExecuteScalar();
          

            if (result != null)
            {
                // ✅ Using Session.Add() instead of Session["key"] = value
                Session.Add("UserID", result.ToString());
                Session.Add("UserEmail", txtEmail.Text.Trim());

                // Show success modal and redirect
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", @"
                    var myModal = new bootstrap.Modal(document.getElementById('successModal'));
                    myModal.show();
                    setTimeout(function() {
                        window.location.href = '../User/UserDefault.aspx';
                    }, 2000);
                ", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invalid credentials');", true);
            }
            con.Close();
        }
    }
  
}
