using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

public partial class Home_FarmerRegistration : System.Web.UI.Page
{
    string connStr = ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        string fullName = txtname.Text.Trim();
        string email = txtemail.Text.Trim();
        string phone = txtphone.Text.Trim();
        string address = txtaddress.Text.Trim();
        string password = txtpassword.Text.Trim();
        string confirmPassword = txtconfirmPassword.Text.Trim();
        string landArea = txtlandarea.Text.Trim();
        string cropType = txtcroptype.Text.Trim();

        // === Server-side validations ===
        if (string.IsNullOrWhiteSpace(fullName))
        {
            ShowAlert("Please enter full name.");
            return;
        }

        if (string.IsNullOrWhiteSpace(phone) || phone.Length != 10 || !phone.All(char.IsDigit))
        {
            ShowAlert("Please enter a valid 10-digit mobile number.");
            return;
        }

        if (!string.IsNullOrWhiteSpace(email) && !IsValidEmail(email))
        {
            ShowAlert("Please enter a valid email address.");
            return;
        }

        if (string.IsNullOrWhiteSpace(address))
        {
            ShowAlert("Please enter address.");
            return;
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            ShowAlert("Please enter password.");
            return;
        }

        if (password != confirmPassword)
        {
            ShowAlert("Passwords do not match.");
            return;
        }

        // === Photo Upload ===
        string profilePhotoFileName = "";
        if (fuphoto.HasFile)
        {
            profilePhotoFileName = fuphoto.FileName;
            string ppath = Server.MapPath("~/Images/" + profilePhotoFileName);
            fuphoto.SaveAs(ppath);
        }

        // === DB Insert ===
        using (SqlConnection con = new SqlConnection(connStr))
        {
            con.Open();

            // Check if mobile exists
            string checkMobileQuery = "SELECT COUNT(*) FROM Farmer_Registration WHERE MobileNumber = @Mobile";
            SqlCommand checkMobileCmd = new SqlCommand(checkMobileQuery, con);
            checkMobileCmd.Parameters.AddWithValue("@Mobile", txtphone.Text.Trim());

            int mobileExists = (int)checkMobileCmd.ExecuteScalar();

            if (mobileExists > 0)
            {
                ShowPopup("This mobile number is already registered. Please use a different one.");
                return;
            }

            // Check if email exists
            string checkEmailQuery = "SELECT COUNT(*) FROM Farmer_Registration WHERE Email = @Email";
            SqlCommand checkEmailCmd = new SqlCommand(checkEmailQuery, con);
            checkEmailCmd.Parameters.AddWithValue("@Email", txtemail.Text.Trim());

            int emailExists = (int)checkEmailCmd.ExecuteScalar();

            if (emailExists > 0)
            {
                ShowPopup("This email is already registered. Please use a different one.");
                return;
            }

            string query = @"INSERT INTO Farmer_Registration 
                (FullName, MobileNumber, Email, PasswordHash, Address, LandArea, ProfilePhotoPath, CropType, CreatedAt,Answer)
                VALUES 
                (@FullName, @MobileNumber, @Email, @PasswordHash, @Address, @LandArea, @ProfilePhotoPath, @CropType, @CreatedAt,@Ans)";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@FullName", fullName);
            cmd.Parameters.AddWithValue("@MobileNumber", phone);
            cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(email) ? DBNull.Value : (object)email);
            cmd.Parameters.AddWithValue("@PasswordHash", password); // Ideally hash this!
            cmd.Parameters.AddWithValue("@Address", address);
            cmd.Parameters.AddWithValue("@LandArea", string.IsNullOrEmpty(landArea) ? DBNull.Value : (object)landArea);
            cmd.Parameters.AddWithValue("@ProfilePhotoPath", profilePhotoFileName);
            cmd.Parameters.AddWithValue("@CropType", string.IsNullOrEmpty(cropType) ? DBNull.Value : (object)cropType);
            cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
            cmd.Parameters.AddWithValue("@Ans", txtColor.Text);


            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "successModal", "var myModal = new bootstrap.Modal(document.getElementById('successModal')); myModal.show();", true);
                clear();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorModal", "var myModal = new bootstrap.Modal(document.getElementById('errorModal')); myModal.show();", true);
            }
        }
    }
    private void ShowPopup(string message)
    {
        string script = "alert('" + message.Replace("'", "\\'") + "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", script, true);
    }
    // Utility method for alerts
    private void ShowAlert(string message)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('{message}');", true);
    }

    // Simple email validator
    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private void clear()
    {
        txtname.Text = "";
        txtemail.Text = "";
        txtphone.Text = "";
        txtaddress.Text = "";
        
        txtlandarea.Text = "";
        txtcroptype.Text = "";
        txtpassword.Text = "";
        txtconfirmPassword.Text = "";
        txtColor.Text = "";
    }

}
