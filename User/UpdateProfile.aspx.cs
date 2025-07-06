using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

public partial class User_UpdateProfile : System.Web.UI.Page
{
    string conStr = ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            
                string photo = ViewState["photo"] != null ? ViewState["photo"].ToString() : string.Empty; if (string.IsNullOrEmpty(photo))
                {
                    photo = "img.jpg"; // Default dummy image
                }

                imgPreview.ImageUrl = ResolveUrl(photo);
            


            LoadVendorProfile();
        }
    }
    private void LoadVendorProfile()
    {
        
        string userId = Session["UserID"].ToString(); // or from QueryString
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Vendor_Registration WHERE UserID=@UserID", con);
            cmd.Parameters.AddWithValue("@UserID", userId);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtFullName.Text = dr["FullNameOrBusinessName"].ToString();
                txtMobile.Text = dr["MobileNumber"].ToString();
                txtEmail.Text = dr["Email"].ToString();
                txtAddress.Text = dr["FullAddress"].ToString();
                txtpass.Text = dr["PasswordHash"].ToString();
                txtCity.Text = dr["City"].ToString();
                txtState.Text = dr["State"].ToString();
                
            
             
                if (!string.IsNullOrEmpty(dr["photo"].ToString()))
                {
                    imgPreview.ImageUrl = ResolveUrl("~/User/Vendorimg/" + dr["photo"].ToString());
                }
                else
                {
                    imgPreview.ImageUrl = ResolveUrl("~/User/Vendorimg/img.jpg");
                }
            }
            con.Close();
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string userId = Session["UserID"].ToString();
        int UserID = Convert.ToInt32(userId);
        string logoFileName = "";

        // Retain current photo from ImageUrl (only the filename)
        string existingPhoto = Path.GetFileName(imgPreview.ImageUrl);

        if (fuphoto.HasFile)
        {
            logoFileName = UserID + "_" + fuphoto.FileName;
            string filePath = Server.MapPath("~/User/Vendorimg/") + logoFileName;
            fuphoto.SaveAs(filePath);
            // ✅ Update image preview immediately
            imgPreview.ImageUrl = "~/User/Vendorimg/" + logoFileName;
        }
      
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand(@"UPDATE Vendor_Registration SET 
            FullNameOrBusinessName=@FullName,
            MobileNumber=@Mobile,
            Email=@Email,
            FullAddress=@Address,
            City=@City,
            State=@State,
         
            PasswordHash=@pass,
            Photo=@photo
            WHERE UserID=@UserID", con);

            cmd.Parameters.AddWithValue("@FullName", txtFullName.Text.Trim());
            cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
            cmd.Parameters.AddWithValue("@City", txtCity.Text.Trim());
            cmd.Parameters.AddWithValue("@State", txtState.Text.Trim());
           
         
            cmd.Parameters.AddWithValue("@pass", txtpass.Text.Trim());
            cmd.Parameters.AddWithValue("@photo", logoFileName);
            cmd.Parameters.AddWithValue("@UserID", userId);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Profile updated successfully!');", true);
        }
    }
}