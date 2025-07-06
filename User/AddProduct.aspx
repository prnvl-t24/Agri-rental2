<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMasterPage.master" AutoEventWireup="true" CodeFile="AddProduct.aspx.cs" Inherits="User_AddProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style>
    body {
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        background-color: #f4f7f9;
        margin: 0;
        padding: 0;
    }
    .input-group {
    display: flex;
    width: 100%;
    max-width: 350px; /* Ensure it has enough space */
    justify-content: center;
    align-items: center;
}

    .form-container {
        background-color: #fff;
        border-radius: 15px;
        padding: 30px;
        max-width: 800px;
        margin: 50px auto;
        box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
    }

    .form-container h2, .form-container h3 {
        color: #2c3e50;
        margin-bottom: 20px;
        text-align: center;
    }

    label {
        font-weight: 600;
        display: block;
        margin-top: 15px;
        color: #34495e;
    }
    .form-grid {
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    gap: 20px;
}

.form-grid .form-group {
    display: flex;
    flex-direction: column;
}

@media (max-width: 992px) {
    .form-grid {
        grid-template-columns: repeat(2, 1fr);
    }
}

@media (max-width: 600px) {
    .form-grid {
        grid-template-columns: 1fr;
    }
}

    .form-control {
        width: 100%;
        padding: 10px 15px;
        margin-top: 5px;
        border-radius: 8px;
        border: 1px solid #ccc;
        box-sizing: border-box;
        font-size: 15px;
        transition: border-color 0.3s ease;
    }

    .form-control:focus {
        border-color: #007bff;
        outline: none;
    }

    .btn {
        padding: 10px 25px;
        border-radius: 8px;
        font-size: 16px;
        font-weight: 600;
        border: none;
        cursor: pointer;
        margin-right: 10px;
    }

   /* .btn-primary {
        background-color: #007bff;
        color: white;
    }

    .btn-primary:hover {
        background-color: #0056b3;
    }

    .btn-secondary {
        background-color: #6c757d;
        color: white;
    }

    .btn-secondary:hover {
        background-color: #5a6268;
    }*/

    .table {
        width: 100%;
        border-collapse: collapse;
        margin-top: 30px;
    }

    .table th, .table td {
        padding: 12px 15px;
        border: 1px solid #dee2e6;
        text-align: center;
    }

    .table th {
        background-color: #007bff;
        color: white;
    }

    .table tr:nth-child(even) {
        background-color: #f2f2f2;
    }

    .table tr:hover {
        background-color: #e9ecef;
    }

    .aspNetDisabled {
        opacity: 0.6;
        pointer-events: none;
    }
    .modal-body-scroll {
    max-height: 400px;
    overflow-y: auto;
}
</style>
   
    <script>
        function previewImage(event) {
            var reader = new FileReader();
            reader.onload = function () {
                var img = document.getElementById('<%= imgPreview.ClientID %>');
                img.src = reader.result;
            };
            reader.readAsDataURL(event.target.files[0]);
        }
    </script>
 <div class="form-container">
        <h2>Product Management</h2>

        <asp:Label ID="lblMessage" runat="server" ForeColor="Green" /><br /><br />

        <asp:HiddenField ID="hfProductID" runat="server" />

        <div class="form-grid">
    <div class="form-group">
        <label>Category:</label>
        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <label>Product Name:</label>
        <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" required />
    </div>

    <div class="form-group">
        <label>Price:</label>
        <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" required />
    </div>

    <div class="form-group">
        <label>Description:</label>
        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" required />
    </div>

   <%-- <div class="form-group">
        <label>Quantity:</label>
        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" required />
    </div>--%>

    <div class="form-group">
        <label>Image:</label>
<asp:FileUpload ID="fuphoto" runat="server" CssClass="form-control" onchange="previewImage(event)" />
    </div>
            <div class="form-group">
 <asp:Panel ID="pnlImagePreview" runat="server">
    <asp:Image ID="imgPreview" runat="server" Width="150px" Height="150px" Visible="false" />
</asp:Panel>


</div>


        <asp:Button ID="btnSave" runat="server" Text="Add Product" CssClass="btn btn-primary" OnClick="btnSave_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" Visible="false" />
        <br /><br />
     <asp:Button ID="btnViewAll" runat="server" Text="View All Products" CssClass="btn btn-danger" OnClientClick="$('#productModal').modal('show'); return false;" />


     <!-- Bootstrap Modal -->
<div class="modal fade" id="productModal" tabindex="-1" role="dialog" aria-labelledby="productModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="productModalLabel">All Products</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="$('#productModal').modal('hide');">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
           <div class="d-flex justify-content-center mb-3">
    <div class="input-group" >
<asp:TextBox ID="txtSearch" runat="server" CssClass="form-control mb-3" placeholder="Search category..." />
        <span class="input-group-text bg-primary text-white">
            <i class="fa fa-search"></i>
        </span>
    </div>
</div>
   <div class="modal-body-scroll">
    <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="False"
        CssClass="table table-bordered"
        OnRowCommand="gvProducts_RowCommand" DataKeyNames="ProductID">
        <Columns>
            <asp:BoundField DataField="ProductID" HeaderText="ID" />
            <asp:BoundField DataField="ProductName" HeaderText="Name" />
            <asp:BoundField DataField="CategoryName" HeaderText="Category" />
            <asp:BoundField DataField="Price" HeaderText="Price" />
            <asp:BoundField DataField="Description" HeaderText="Description" />
            <asp:ButtonField CommandName="EditRow" Text="Edit" ControlStyle-CssClass="btn btn-success" ButtonType="Button" />
            <asp:ButtonField CommandName="DeleteRow" Text="Delete" ControlStyle-CssClass="btn btn-danger" ButtonType="Button" />
        </Columns>
    </asp:GridView>
</div>
      </div>
    </div>
  </div>
</div>
        <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

<%--<script type="text/javascript">
    $(document).ready(function () {
        $("#<%= txtSearch.ClientID %>").on("keyup", function () {
            var value = $(this).val().toLowerCase();

            // Target GridView rows EXCEPT the first one (which is the header)
            $("#<%= gvProducts.ClientID %> tr").each(function (index) {
                if (index === 0) return; // Skip header row

                var rowText = $(this).text().toLowerCase();
                $(this).toggle(rowText.indexOf(value) !== -1);
            });
        });
    });
</script>--%>
<script>
    $(document).ready(function () {
        $("#txtSearch").on("keyup", function () {
            var value = $(this).val().toLowerCase();

            $("#gvCategory tbody tr").each(function () {
                var rowText = $(this).text().toLowerCase();
                $(this).toggle(rowText.indexOf(value) !== -1);
            });
        });
    });
</script>

<link href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css" rel="stylesheet" />
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/js/bootstrap.bundle.min.js"></script>

       
    </div></asp:Content>

