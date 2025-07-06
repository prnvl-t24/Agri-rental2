using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;


public partial class Home_VendorForgotPassword : System.Web.UI.Page
{
    string connStr = ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string mobile = txtMobile.Text.Trim();
        string answer = txtAnswer.Text.Trim();
        string connectionString = ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "SELECT PasswordHash FROM Vendor_Registration WHERE Answer=@ans and  MobileNumber = @MobileNumber";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MobileNumber", mobile);
            cmd.Parameters.AddWithValue("@ans", answer);

            conn.Open();
            object result = cmd.ExecuteScalar();

            if (result != null)
            {
                lblResult.CssClass = "text-success";
                lblResult.Text = "Your password is: <strong>" + result.ToString() + "</strong>";
            }
            else
            {
                lblResult.CssClass = "text-danger";
                lblResult.Text = "Mobile number not found. Please try again.";
            }
        }
    }
}