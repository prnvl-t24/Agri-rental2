using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home_AdminLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
        }
    }


    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string username = txtusername.Text.Trim();
        string password = txtpassword.Text; // In a real app, you'd hash the input password

        // --- THIS IS THE INSECURE PART ---
        // --- FOR DEMONSTRATION ONLY ---
        // --- REPLACE WITH DATABASE VALIDATION & PASSWORD HASHING ---
      

        if (txtusername.Text=="123" & txtpassword.Text=="123")
        {
            // Authentication successful
   
            Session.Add("AdminUser", username); // Store username in session using Add
            Session.Add("IsAdminLoggedIn", true); // Set a flag for logged-in state using Add


            // You could also use Forms Authentication for a more robust solution
            // FormsAuthentication.SetAuthCookie(username, false); // false for session cookie
            // Prepare the JavaScript for popup and redirection
            string script = "alert('Login Success!'); window.location.href = '../Admin/AdminDefault.aspx';";
            ClientScript.RegisterStartupScript(this.GetType(), "LoginSuccessRedirect", script, true);

        }
        else
        {
            // Authentication failed
            lblErrorMessage.Text = "Invalid username or password.";
            Session.Remove("AdminUser");
            Session.Remove("IsAdminLoggedIn");
        }
    }
}
    
