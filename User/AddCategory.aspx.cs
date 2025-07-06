using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class User_AddCategory : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategoryGrid();
            lblMessage.Visible = false;
            }
        }

    protected void btnAddCategory_Click(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            // Vendor not logged in, redirect or show message
            Response.Redirect("VendorLogin.aspx");
            return;
        }

        int vendorId = Convert.ToInt32(Session["UserID"]);
        string categoryName = txtCategoryName.Text.Trim();
        string description = txtDescription.Text.Trim();

        if (string.IsNullOrEmpty(categoryName))
        {
            lblMessage.Text = "Please enter category name.";
            return;
        }

        string connStr = ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connStr))
        {
            string query = "INSERT INTO Equipment_Category (CategoryName, Description, VendorID) VALUES (@CategoryName, @Description, @VendorID)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@VendorID", vendorId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                BindCategoryGrid();
                ScriptManager.RegisterStartupScript(this, GetType(), "Insert Message", "alert('Category Added successfully.');", true);
                txtDescription.Text = "";
                txtCategoryName.Text = "";
            }
        }
    }
    //protected void gvCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    if (Session["UserID"] == null)
    //    {
    //        Response.Redirect("../Home/VendorLogin.aspx");
    //        return;
    //    }

    //    int vendorId = Convert.ToInt32(Session["UserID"]);
    //    int categoryId = Convert.ToInt32(gvCategory.DataKeys[e.RowIndex].Value);

    //    using (SqlConnection con = new SqlConnection(connectionString))
    //    {
    //        con.Open();
    //        SqlTransaction tran = con.BeginTransaction();

    //        try
    //        {
    //            string deleteProductsQuery = "DELETE FROM Product WHERE CategoryID = @CategoryID AND VendorID = @VendorID";
    //            SqlCommand cmdDeleteProducts = new SqlCommand(deleteProductsQuery, con, tran);
    //            cmdDeleteProducts.Parameters.AddWithValue("@CategoryID", categoryId);
    //            cmdDeleteProducts.Parameters.AddWithValue("@VendorID", vendorId);
    //            cmdDeleteProducts.ExecuteNonQuery();

    //            string deleteCategoryQuery = "DELETE FROM Equipment_Category WHERE CategoryID = @CategoryID AND VendorID = @VendorID";
    //            SqlCommand cmdDeleteCategory = new SqlCommand(deleteCategoryQuery, con, tran);
    //            cmdDeleteCategory.Parameters.AddWithValue("@CategoryID", categoryId);
    //            cmdDeleteCategory.Parameters.AddWithValue("@VendorID", vendorId);
    //            cmdDeleteCategory.ExecuteNonQuery();

    //            tran.Commit();

    //            ScriptManager.RegisterStartupScript(this, GetType(), "DeleteMessage", "alert('Category and its products deleted successfully.');", true);
    //        }
    //        catch (Exception ex)
    //        {
    //            tran.Rollback();
    //            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", $"alert('Error occurred: {ex.Message}');", true);
    //        }
    //    }

    //    BindCategoryGrid();
    //}


    protected void gvCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("../Home/VendorLogin.aspx");
            return;
        }

        int vendorId = Convert.ToInt32(Session["UserID"]);
        int categoryId = Convert.ToInt32(gvCategory.DataKeys[e.RowIndex].Value);

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            SqlTransaction tran = con.BeginTransaction();

            try
            {
                string deleteProductsQuery = "DELETE FROM Product WHERE CategoryID = @CategoryID AND VendorID = @VendorID";
                SqlCommand cmdDeleteProducts = new SqlCommand(deleteProductsQuery, con, tran);
                cmdDeleteProducts.Parameters.AddWithValue("@CategoryID", categoryId);
                cmdDeleteProducts.Parameters.AddWithValue("@VendorID", vendorId);
                cmdDeleteProducts.ExecuteNonQuery();

                string deleteCategoryQuery = "DELETE FROM Equipment_Category WHERE CategoryID = @CategoryID AND VendorID = @VendorID";
                SqlCommand cmdDeleteCategory = new SqlCommand(deleteCategoryQuery, con, tran);
                cmdDeleteCategory.Parameters.AddWithValue("@CategoryID", categoryId);
                cmdDeleteCategory.Parameters.AddWithValue("@VendorID", vendorId);
                cmdDeleteCategory.ExecuteNonQuery();

                tran.Commit();

                // Show SweetAlert success popup
                ScriptManager.RegisterStartupScript(this, GetType(), "Deleted", "Swal.fire('Deleted!', 'Category and related products were deleted successfully.', 'success');", true);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", $"Swal.fire('Error', '{ex.Message}', 'error');", true);
            }
        }

        BindCategoryGrid();
    }

    public static bool DeleteCategory(int categoryId)
    {
        try
        {
            int vendorId = Convert.ToInt32(System.Web.HttpContext.Current.Session["UserID"]);

            string connStr = ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();

                string deleteProductsQuery = "DELETE FROM Product WHERE CategoryID = @CategoryID AND VendorID = @VendorID";
                SqlCommand cmdDeleteProducts = new SqlCommand(deleteProductsQuery, con, tran);
                cmdDeleteProducts.Parameters.AddWithValue("@CategoryID", categoryId);
                cmdDeleteProducts.Parameters.AddWithValue("@VendorID", vendorId);
                cmdDeleteProducts.ExecuteNonQuery();

                string deleteCategoryQuery = "DELETE FROM Equipment_Category WHERE CategoryID = @CategoryID AND VendorID = @VendorID";
                SqlCommand cmdDeleteCategory = new SqlCommand(deleteCategoryQuery, con, tran);
                cmdDeleteCategory.Parameters.AddWithValue("@CategoryID", categoryId);
                cmdDeleteCategory.Parameters.AddWithValue("@VendorID", vendorId);
                cmdDeleteCategory.ExecuteNonQuery();

                tran.Commit();
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    private void BindCategoryGrid()
    {
        //if (Session["UserID"] == null)
        //{
        //    Response.Redirect("../Home/VendorLogin.aspx");
        //    return;
        //}

        int vendorId = Convert.ToInt32(Session["UserID"]);

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT CategoryID, CategoryName, Description, VendorID FROM Equipment_Category WHERE VendorID = @VendorID ORDER BY CategoryID DESC", con);
            da.SelectCommand.Parameters.AddWithValue("@VendorID", vendorId);

            DataTable dt = new DataTable();
            da.Fill(dt);
            gvCategory.DataSource = dt;
            gvCategory.DataBind();
        }
    }
    protected void gvCategory_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvCategory.EditIndex = e.NewEditIndex;
        BindCategoryGrid();
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal", "showCategoryModal();", true); // Keep modal open
    }

    protected void gvCategory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvCategory.EditIndex = -1;
        BindCategoryGrid();
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal", "showCategoryModal();", true);
    }

    protected void gvCategory_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gvCategory.Rows[e.RowIndex];
        int categoryId = Convert.ToInt32(gvCategory.DataKeys[e.RowIndex].Value);

        TextBox txtDescription = (TextBox)row.FindControl("txtDescription");
        string newDescription = txtDescription.Text;

        string query = "UPDATE Equipment_Category SET Description = @Description WHERE CategoryID = @CategoryID";

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Description", newDescription);
                cmd.Parameters.AddWithValue("@CategoryID", categoryId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        gvCategory.EditIndex = -1;
        BindCategoryGrid(); // method to refresh/rebind the grid
    }

}

