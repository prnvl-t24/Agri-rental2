<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMasterPage.master" AutoEventWireup="true" CodeFile="AddCategory.aspx.cs" Inherits="User_AddCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <style>
        .search-box {
    width: 100% !important; /* Ensures it takes full width */
    max-width: 300px; /* Adjust width as needed */
    border: 2px solid blue;
    border-radius: 5px 0px 0px 5px; /* Rounded corners only on the left */
    padding: 5px 10px;
    font-weight: bold;
    text-align: center;
    outline: none;
    transition: all 0.3s ease-in-out;
}

.input-group {
    display: flex;
    width: 100%;
    max-width: 350px; /* Ensure it has enough space */
    justify-content: center;
    align-items: center;
}




.search-box:focus {
    border-color: blue;
    box-shadow: 0px 0px 10px blue;
    background-color: #f0fff0;
}
    .form-container {
        width: 70%;
        margin: 30px auto;
        padding: 30px;
        border: 1px solid #ddd;
        background-color: #ffffff;
        border-radius: 10px;
        box-shadow: 0 0 15px rgba(0, 0, 0, 0.1);
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    }

    .form-container h2, .form-container h3 {
        text-align: center;
        color: #2d6a4f;
        margin-bottom: 20px;
    }

    label {
        display: block;
        margin-top: 15px;
        font-weight: 600;
        color: #333;
    }

    input[type="text"], textarea, .aspNetDisabled {
        width: 100%;
        padding: 10px;
        margin-top: 8px;
        border: 1px solid #ccc;
        border-radius: 5px;
        box-sizing: border-box;
        transition: border-color 0.3s ease;
    }

    input[type="text"]:focus, textarea:focus {
        border-color: #40916c;
        outline: none;
    }

    .form-container .aspNetDisabled {
        background-color: #f0f0f0;
    }

    /*asp\:button, input[type="submit"], input[type="button"] {
        background-color: #40916c;
        color: white;
        border: none;
        padding: 10px 20px;
        margin-top: 20px;
        border-radius: 5px;
        cursor: pointer;
        font-weight: bold;
        transition: background-color 0.3s ease;
    }

    asp\:button:hover, input[type="submit"]:hover, input[type="button"]:hover {
        background-color: #2d6a4f;
    }*/

    .table {
        width: 100%;
        border-collapse: collapse;
        margin-top: 30px;
        font-size: 14px;
    }

    .table th, .table td {
        padding: 12px;
        border: 1px solid #ddd;
        text-align: center;
    }
      .gridview-container {
        overflow-x: auto;
    }
    .table th {
        background-color: #2d6a4f;
        color: white;
        font-weight: bold;
    }

    .table tr:nth-child(even) {
        background-color: #f9f9f9;
    }

    .table tr:hover {
        background-color: #eefae2;
    }

    .table .btn {
        padding: 5px 12px;
        font-size: 13px;
    }
    .modal-body-scroll {
    max-height: 200px;
    overflow-y: scroll;
    background: #f9f9f9; /* Just for visibility */
}

</style>


   <div class="form-container">
            <h2>Add Category</h2>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label><br /><br />
            <label>Category Name:</label>
            <asp:TextBox ID="txtCategoryName" runat="server" /><br /><br />
            <label>Description:</label>
            <asp:TextBox ID="txtDescription" runat="server" /><br /><br />
            <asp:Button ID="btnAdd" runat="server" Text="Add Category" CssClass="btn btn-primary" OnClick="btnAddCategory_Click" /><br /><br />
            <asp:Button ID="btnViewAll" runat="server" Text="View All Categories"
    CssClass="btn btn-danger" OnClientClick="showCategoryModal(); return false;" />

            <%--<h3>Category List</h3>
         <asp:GridView ID="gvCategory" runat="server" AutoGenerateColumns="False"
    CssClass="table table-bordered table-striped text-center"
    HeaderStyle-CssClass="table-dark"
    OnRowDeleting="gvCategory_RowDeleting"
    DataKeyNames="CategoryID">
                <Columns>
                    <asp:BoundField DataField="CategoryID" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="CategoryName" HeaderText="Category Name" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:CommandField ShowDeleteButton="True" DeleteText="Delete" />
                </Columns>
            </asp:GridView>--%>
        </div>
    <!-- Modal -->
 
<div class="modal fade" id="categoryModal" tabindex="-1" aria-labelledby="categoryModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-xl modal-dialog-centered">
    <div class="modal-content">
      <div class="modal-header bg-success text-white">
        <h5 class="modal-title" id="categoryModalLabel">All Categories</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
           <div class="d-flex justify-content-center mb-3">
    <div class="input-group" style="max-width: 400px;">
<asp:TextBox ID="txtSearch" runat="server" CssClass="form-control mb-3" placeholder="Search category..." />
        <span class="input-group-text bg-primary text-white">
            <i class="fa fa-search"></i>
        </span>
    </div>
</div>
<div class="modal-body p-3" style="max-height: 70vh; overflow-y: auto;">
 <asp:GridView ID="gvCategory" runat="server" AutoGenerateColumns="False"
    CssClass="table table-bordered table-striped text-center"
    DataKeyNames="CategoryID"
    OnRowEditing="gvCategory_RowEditing"
    OnRowCancelingEdit="gvCategory_RowCancelingEdit"
    OnRowUpdating="gvCategory_RowUpdating"
    OnRowDeleting="gvCategory_RowDeleting">

    <Columns>
        <asp:BoundField DataField="CategoryID" HeaderText="ID" ReadOnly="True" />
        <asp:BoundField DataField="CategoryName" HeaderText="Category Name" />
   <asp:TemplateField HeaderText="Description">
    <ItemTemplate>
        <%# Eval("Description") %>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("Description") %>' 
                     TextMode="MultiLine" Width="400px" Rows="4" CssClass="form-control"></asp:TextBox>
    </EditItemTemplate>
</asp:TemplateField>

        <asp:BoundField DataField="VendorID" HeaderText="Vendor ID" />
   <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                
                <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="✏️" ToolTip="Edit"
                    style="color: orange; font-size:18px; text-decoration:none; border:none; background:none;"></asp:LinkButton>
<asp:LinkButton ID="btnDelete" runat="server"
    Text="🗑️" ToolTip="Delete"
    CommandName="Delete"
   OnClientClick="return confirm('Are you sure you want to delete this category? Deleting this category will also remove all related products !  ');"

    CausesValidation="false"
    style="color: red; font-size:18px; border: none; background: none;">
</asp:LinkButton>




            </ItemTemplate>
            <EditItemTemplate>
                <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Update" Text="✔️" ToolTip="Update"
                    style="color: green; font-size:18px; text-decoration:none; border:none; background:none;"></asp:LinkButton>

                <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" Text="❌" ToolTip="Cancel"
                    style="color: gray; font-size:18px; text-decoration:none; border:none; background:none; margin-left:10px;"></asp:LinkButton>
            </EditItemTemplate>
        </asp:TemplateField>
                    </Columns>
            
                    </asp:GridView>


    </div>
      </div>
    </div>
  </div>
</div>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>


<script type="text/javascript">
    function confirmCategoryDelete(categoryId) {
        Swal.fire({
            title: 'Are you sure?',
            html: 'Deleting this category will also remove all related products.',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'No, cancel',
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6'
        }).then((result) => {
            if (result.isConfirmed) {
                // Call server-side deletion via AJAX/WebMethod
                deleteCategoryFromServer(categoryId);
            } else {
                Swal.fire('Cancelled', 'Category not deleted.', 'info');
            }
        });

        return false; // Prevent normal postback
    }

    function deleteCategoryFromServer(categoryId) {
        $.ajax({
            type: "POST",
            url: "AddCategory.aspx/DeleteCategory",
            data: JSON.stringify({ categoryId: categoryId }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d === true) {
                    Swal.fire('Deleted!', 'Category and related products deleted successfully.', 'success')
                        .then(() => location.reload());
                } else {
                    Swal.fire('Error', 'Deletion failed.', 'error');
                }
            },
            error: function (xhr, status, error) {
                Swal.fire('Error', 'An unexpected error occurred.', 'error');
            }
        });
    }
</script>

   <script>
    function showCategoryModal() {
        var myModal = new bootstrap.Modal(document.getElementById('categoryModal'));
        myModal.show();
    }
    </script>
            <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

<script type="text/javascript">
    $(document).ready(function () {
        $("#<%= txtSearch.ClientID %>").on("keyup", function () {
            var value = $(this).val().toLowerCase();

            // Target GridView rows EXCEPT the first one (which is the header)
            $("#<%= gvCategory.ClientID %> tr").each(function (index) {
                if (index === 0) return; // Skip header row

                var rowText = $(this).text().toLowerCase();
                $(this).toggle(rowText.indexOf(value) !== -1);
            });
        });
    });
</script>


</asp:Content>

