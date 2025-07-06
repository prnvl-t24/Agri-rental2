<%@ Page Title="" Language="C#" MasterPageFile="~/Home/HomeMasterPage.master" AutoEventWireup="true" CodeFile="FarmerForgotPassword.aspx.cs" Inherits="Home_FarmerForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />

 <style>
        .forgot-container {
            max-width: 400px;
            margin: 100px auto;
            background: #fff;
            padding: 30px;
            box-shadow: 0 0 15px rgba(0,0,0,0.2);
            border-radius: 10px;
        }
    </style>
      <div class="container">
            <div class="forgot-container">
                <h4 class="text-center mb-4">Farmer Forgot Password</h4>

                <div class="form-group">
                    <label for="txtMobile">Enter Registered Mobile Number</label>
                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" MaxLength="10" placeholder="e.g., 9876543210"></asp:TextBox>
                </div>
                <div class="mb-3">
    <label for="txtAnswer" class="form-label">Security Answer (e.g. Your Nick Name)</label>
    <asp:TextBox ID="txtAnswer" runat="server" CssClass="form-control" placeholder="Enter your answer" />
    <asp:RequiredFieldValidator ID="rfvAnswer" runat="server"
        ControlToValidate="txtAnswer" ErrorMessage="Answer is required." 
        CssClass="text-danger" Display="Dynamic" />
</div>


                <asp:Button ID="btnFetchPassword" runat="server" CssClass="btn btn-primary btn-block" Text="Get Password" OnClick="btnFetchPassword_Click" />

                <div class="text-center mt-3">
                    <a href="FarmerLogin.aspx">Back to Login</a>
                </div>

                <asp:Label ID="lblResult" runat="server" CssClass="text-danger mt-2 d-block text-center"></asp:Label>
            </div>
        </div>
</asp:Content>

