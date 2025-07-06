using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;



public partial class Farmer_UpdateProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

        if (!IsPostBack)
        {
            if (!IsPostBack)
            {
                string photo = ViewState["photo"] != null ? ViewState["photo"].ToString() : string.Empty;

                if (string.IsNullOrEmpty(photo))
                {
                    photo = "img.jpg"; // Default image
                }

                if (string.IsNullOrEmpty(imgPreview.ImageUrl) || imgPreview.ImageUrl == "~/Images/")
                {
                    imgPreview.ImageUrl = ResolveUrl("~/Images/img.jpg");
                }


            }
            if (Session["FarmerID"] == null)
            {
                Response.Redirect("../Home/FarmerLogin.aspx");
                return;
            }
            LoadUserData();
         
        }
    }

    private void LoadUserData()
    {
        string farmerId = Session["FarmerID"].ToString();
        string connStr = ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connStr))
        {
            string query = "SELECT * FROM Farmer_Registration WHERE FarmerID = @FarmerID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@FarmerID", farmerId);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtname.Text = dr["FullName"].ToString();
                txtemail.Text = dr["Email"].ToString();
                txtphone.Text = dr["MobileNumber"].ToString();
                txtaddress.Text = dr["Address"].ToString();
                txtpin.Text = dr["LandArea"].ToString();
                txtconfirmPassword.Text = dr["PasswordHash"].ToString();

                string photo = dr["ProfilePhotoPath"].ToString();
                if (!string.IsNullOrEmpty(photo))
                    imgPreview.ImageUrl = ResolveUrl("~/Images/" + photo);
                else
                    imgPreview.ImageUrl = ResolveUrl("~/Images/img.jpg");

            }
        }
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        if (Session["FarmerID"] == null)
        {
            Response.Redirect("Login.aspx");
            return;
        }

        string farmerId = Session["FarmerID"].ToString();
        string connStr = ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connStr))
        {
            con.Open();

            string photoFileName = null;
            if (fuPhoto.HasFile)
            {
                photoFileName = Guid.NewGuid() + "_" + fuPhoto.FileName;
                string savePath = Server.MapPath("~/Images/") + photoFileName;
                fuPhoto.SaveAs(savePath);
            }

            string query = @"
    UPDATE Farmer_Registration 
    SET FullName = @FullName,
        Email = @Email,
        MobileNumber = @MobileNumber,
        Address = @Address,
        LandArea = @LandArea,
        PasswordHash = @PasswordHash" +
          (photoFileName != null ? ", ProfilePhotoPath = @ProfilePhotoPath" : "") +
      " WHERE FarmerID = @FarmerID";  // ← Added space here


            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@FullName", txtname.Text.Trim());
            cmd.Parameters.AddWithValue("@Email", txtemail.Text.Trim());
            cmd.Parameters.AddWithValue("@MobileNumber", txtphone.Text.Trim());
            cmd.Parameters.AddWithValue("@Address", txtaddress.Text.Trim());
            cmd.Parameters.AddWithValue("@LandArea", txtpin.Text.Trim());
            cmd.Parameters.AddWithValue("@PasswordHash", txtconfirmPassword.Text.Trim());
            cmd.Parameters.AddWithValue("@FarmerID", farmerId);

            if (photoFileName != null)
            {
                cmd.Parameters.AddWithValue("@ProfilePhotoPath", photoFileName);
                imgPreview.ImageUrl = "~/Images/" + photoFileName;
            }

            cmd.ExecuteNonQuery();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Profile updated successfully!');", true);
        }
    }
}

