using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;



public partial class Farmer_FarmerMasterPage : System.Web.UI.MasterPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FarmerID"] == null)
        {
            Response.Redirect("~/Home/FarmerLogin.aspx");
            return;
        }

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString))
        {
            string query = "SELECT FullName, ProfilePhotoPath FROM Farmer_Registration WHERE FarmerID = @fi";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@fi", Session["FarmerID"].ToString());
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                lblFarmerName.Text = dr["FullName"].ToString();

                if (!string.IsNullOrEmpty(dr["ProfilePhotoPath"].ToString()))
                {
                    imgProfile.ImageUrl = "~/User/PROFILEIMG/" + dr["ProfilePhotoPath"].ToString();
                }
                else
                {
                    imgProfile.ImageUrl = "~/Images/img.jpg";
                }
            }
            con.Close();
        }
    }
}
