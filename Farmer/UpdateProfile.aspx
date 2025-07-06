<%@ Page Title="" Language="C#" MasterPageFile="~/Farmer/FarmerMasterPage.master" AutoEventWireup="true" CodeFile="UpdateProfile.aspx.cs" Inherits="Farmer_UpdateProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />--%>

    <style>
      /*  body {
            background: linear-gradient(to right, #dfffd6, #ffffff);
            font-family: 'Segoe UI', sans-serif;
        }
*/
        .update-container {
            background-color: #ffffff;
            padding: 30px;
            margin-top: 40px;
            border-radius: 20px;
            box-shadow: 0 5px 20px rgba(0,0,0,0.1);
            max-width: 900px;
            margin-left: auto;
            margin-right: auto;
        }

        h2 {
            color: #2d6a4f;
            margin-bottom: 20px;
            text-align: center;
            font-weight: 700;
        }

        .btn-update {
            background-color: #2d6a4f;
            border: none;
            font-weight: 600;
            width: 100%;
        }

        .btn-update:hover {
            background-color: #40916c;
        }

        .img-preview {
            width: 120px;
            height: 120px;
            object-fit: cover;
            border-radius: 50%;
            border: 2px solid #2d6a4f;
            margin-bottom: 10px;
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

  <div class="update-container">
        <h2>Update Farmer Profile</h2>

        <asp:Label ID="lblMessage" runat="server" ForeColor="red" CssClass="mb-3 d-block text-center"></asp:Label>

        <div class="mb-3 text-center">
            <asp:Image ID="imgPreview" runat="server" CssClass="img-preview" />
            <asp:FileUpload ID="fuPhoto" runat="server" CssClass="form-control mt-2" onchange="previewImage(event)" />
        </div>

        <!-- 3 column grid for form inputs -->
        <div class="row g-3">
            <div class="col-md-4">
                <label for="txtname" class="form-label">Full Name</label>
                <asp:TextBox ID="txtname" runat="server" CssClass="form-control" placeholder="Enter full name" />
                <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtname" ForeColor="red" ErrorMessage="Full Name is required" />
            </div>

            <div class="col-md-4">
                <label for="txtphone" class="form-label">Mobile Number</label>
                <asp:TextBox ID="txtphone" runat="server" CssClass="form-control" MaxLength="10" placeholder="Enter mobile number" />
                <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtphone" ForeColor="red" ErrorMessage="Mobile Number is required" />
            </div>

            <div class="col-md-4">
                <label for="txtemail" class="form-label">Email</label>
                <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" TextMode="Email" placeholder="Enter email (optional)" />
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtemail" ForeColor="red" ErrorMessage="Invalid email format" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" />
            </div>

            <div class="col-md-4">
                <label for="txtaddress" class="form-label">Address</label>
                <asp:TextBox ID="txtaddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" placeholder="Enter address" />
                <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtaddress" ForeColor="red" ErrorMessage="Address is required" />
            </div>

            <div class="col-md-4">
                <label for="txtpin" class="form-label">Land Area (in acre)</label>
                <asp:TextBox ID="txtpin" runat="server" CssClass="form-control" placeholder="Enter land area" />
                <asp:RequiredFieldValidator ID="rfvPin" runat="server" ControlToValidate="txtpin" ForeColor="red" ErrorMessage="Land area is required" />
            </div>

            <div class="col-md-4">
                <label for="txtconfirmPassword" class="form-label">Password</label>
                <asp:TextBox ID="txtconfirmPassword" runat="server" CssClass="form-control" TextMode="SingleLine" placeholder="Password" />
            </div>
        </div>

        <div class="mt-4">
            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-update" Text="Update Profile" OnClick="btnupdate_Click" />
        </div>
    </div>
    

</asp:Content>

