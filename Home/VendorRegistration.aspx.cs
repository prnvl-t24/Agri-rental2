using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;


public partial class Home_VendorRegistration : System.Web.UI.Page
{
    string conStr = ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
    }



    protected void btnRegister_Click(object sender, EventArgs e)
    {
        
        if(fuphoto.HasFile)
        {
           string ppath= Server.MapPath("/User/Vendorimg/"+fuphoto.FileName);
            fuphoto.SaveAs(ppath);
        }
        using (SqlConnection con = new SqlConnection(conStr))
        {
            con.Open();

            // Check if mobile exists
            string checkMobileQuery = "SELECT COUNT(*) FROM Vendor_Registration WHERE MobileNumber = @Mobile";
            SqlCommand checkMobileCmd = new SqlCommand(checkMobileQuery, con);
            checkMobileCmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());

            int mobileExists = (int)checkMobileCmd.ExecuteScalar();

            if (mobileExists > 0)
            {
                ShowPopup("This mobile number is already registered. Please use a different one.");
                return;
            }

            // Check if email exists
            string checkEmailQuery = "SELECT COUNT(*) FROM Vendor_Registration WHERE Email = @Email";
            SqlCommand checkEmailCmd = new SqlCommand(checkEmailQuery, con);
            checkEmailCmd.Parameters.AddWithValue("@Email", txtemail.Text.Trim());

            int emailExists = (int)checkEmailCmd.ExecuteScalar();

            if (emailExists > 0)
            {
                ShowPopup("This email is already registered. Please use a different one.");
                return;
            }
            string query = @"INSERT INTO Vendor_Registration 
            (FullNameOrBusinessName, MobileNumber,Email,  FullAddress, City, State, PasswordHash,Photo,Answer) 
            VALUES 
            (@FullNameOrBusinessName, @MobileNumber,@email,  @FullAddress, @City, @State, 
           @PasswordHash,@photo,@ans)";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@FullNameOrBusinessName", txtFullName.Text.Trim());
            cmd.Parameters.AddWithValue("@MobileNumber", txtMobile.Text.Trim());
            cmd.Parameters.AddWithValue("@email", txtemail.Text.Trim());
    
            cmd.Parameters.AddWithValue("@FullAddress", txtAddress.Text.Trim());
            cmd.Parameters.AddWithValue("@City", txtCity.Text.Trim());
            cmd.Parameters.AddWithValue("@State", txtState.Text.Trim());
            cmd.Parameters.AddWithValue("@ans", txtAnswer.Text.Trim());

            // Dummy values — update later with actual location logic

         
        
            cmd.Parameters.AddWithValue("@photo", fuphoto.FileName); // Optional

            // Replace this with proper hashing
            cmd.Parameters.AddWithValue("@PasswordHash", txtPassword.Text.Trim());

            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i > 0)
            {
                ShowPopup("Registration successful!");
                ClearForm();
            }
            else
            {
                ShowPopup("Something went wrong. Please try again.");
            }
        }
    }
    private void ShowPopup(string message)
    {
        string script = "alert('" + message.Replace("'", "\\'") + "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", script, true);
    }

    private void ClearForm()
    {
        txtFullName.Text = "";
        txtMobile.Text = "";
        txtCity.Text = "";
        txtState.Text = "";
        txtAddress.Text = "";
        txtPassword.Text = "";
        txtAnswer.Text = "";
        txtemail.Text = "";
            
    }
}
