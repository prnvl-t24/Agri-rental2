  using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;



public partial class Home_FarmerLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string email = txtEmail.Text.Trim();
        string password = txtPassword.Text.Trim();

        string conStr = ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            string query = "SELECT FarmerID,FullName,ProfilePhotoPath FROM Farmer_Registration WHERE Email = @Email AND PasswordHash = @Password ";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {

                // Storing session values using Session.Add()
                Session.Add("FarmerID", dr["FarmerID"].ToString());
                Session.Add("FarmerEmail", email);
                Session.Add("FarmerName", dr["FullName"].ToString());

                // Show success modal and redirect
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", @"
                    var myModal = new bootstrap.Modal(document.getElementById('successModal'));
                    myModal.show();
                    setTimeout(function() {
                        window.location.href = '../Farmer/Default.aspx';
                    }, 2000);
                ", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invalid credentials.');", true);
            }

            con.Close();
        }
    }
}