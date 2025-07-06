using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;



public partial class Farmer_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string conStr = ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString;
        using (SqlConnection con = new SqlConnection(conStr))
        {
            string query = "select FullName,ProfilePhotoPath from Farmer_Registration where FarmerID=@fi ";
            SqlCommand cmd = new SqlCommand(query, (con));
            cmd.Parameters.AddWithValue("@fi", Session["FarmerID"].ToString());
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                lblshow.Text = dr["FullName"].ToString();

                string rawPath = dr["ProfilePhotoPath"].ToString();

                if (!string.IsNullOrEmpty(rawPath))
                {
                    imgFarmer.ImageUrl = ResolveUrl("~/Images/"+rawPath);
                }
                else
                {
                    imgFarmer.ImageUrl = ResolveUrl("~/Images/img.jpg");
                }
            }


        }
    }
}