<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMasterPage.master" AutoEventWireup="true" CodeFile="UpdateProfile.aspx.cs" Inherits="User_UpdateProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <style>
    .update-profile-form {
        background: #ffffff;
        padding: 30px;
        border-radius: 15px;
        box-shadow: 0 8px 20px rgba(0, 0, 0, 0.08);
        max-width: 900px;
        margin: 40px auto;
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    }

    .update-profile-form h2 {
        color: #2c3e50;
        font-weight: 600;
        margin-bottom: 30px;
    }

    .update-profile-form label {
        font-weight: 600;
        color: #34495e;
        margin-bottom: 5px;
        display: inline-block;
    }

    .update-profile-form .form-control {
        border-radius: 8px;
        padding: 10px 15px;
        font-size: 15px;
        border: 1px solid #ccc;
        transition: border-color 0.3s ease;
    }

    .update-profile-form .form-control:focus {
        border-color: #007bff;
        outline: none;
        box-shadow: 0 0 0 0.15rem rgba(0, 123, 255, 0.25);
    }

    .update-profile-form .btn-primary {
        padding: 12px 30px;
        font-size: 16px;
        font-weight: 600;
        background-color: #007bff;
        border: none;
        border-radius: 8px;
        transition: background-color 0.3s ease;
    }

    .update-profile-form .btn-primary:hover {
        background-color: #0056b3;
    }

    .update-profile-form .text-success {
        font-weight: 500;
    }

    .update-profile-form .mb-3, 
    .update-profile-form .mb-4 {
        margin-bottom: 1.5rem !important;
    }

    .update-profile-form .mt-4 {
        margin-top: 2rem !important;
    }

    .update-profile-form .d-block {
        display: block;
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
<div class="container mt-4 update-profile-form">
            <h2 class="text-center mb-4">Update Vendor Profile</h2>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>
       <asp:Image ID="imgPreview" runat="server" Width="100px" Height="100px" />
<asp:FileUpload ID="fuphoto" runat="server" onchange="previewImage(event)" />
    <div class="col-md-6">
                    <label>Password</label>
                    <asp:TextBox ID="txtpass" runat="server" TextMode="SingleLine" CssClass="form-control" />
                </div>
            <div class="row mb-3">
                <div class="col-md-6">
                    <label>Full Name / Business Name</label>
                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-6">
                    <label>Mobile Number</label>
                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" MaxLength="10" />
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <label>Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                </div>
                <div class="col-md-6">
                    <label>Full Address</label>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" />
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-4">
                    <label>City</label>
                    <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-4">
                    <label>State</label>
                    <asp:TextBox ID="txtState" runat="server" CssClass="form-control" />
                </div>
           <%--     <div class="col-md-4">
                    <label>Equipment Type</label>
                    <asp:TextBox ID="txtEquipmentType" runat="server" CssClass="form-control" />
                </div>--%>
            </div>

         <%--   <div class="row mb-3">
                <div class="col-md-6">
                    <label>Equipment Details</label>
                    <asp:TextBox ID="txtEquipmentDetails" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" />
                </div>
                <div class="col-md-6">
                    <label>Base Location</label>
                    <asp:TextBox ID="txtBaseLocation" runat="server" CssClass="form-control" />
                </div>
            </div>--%>

          

          <center>  <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Update Profile" OnClick="btnUpdate_Click" /></center>
        </div>
</asp:Content>

