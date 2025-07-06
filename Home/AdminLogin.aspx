<%@ Page Title="" Language="C#" MasterPageFile="~/Home/HomeMasterPage.master" AutoEventWireup="true" CodeFile="AdminLogin.aspx.cs" Inherits="Home_AdminLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <style>
        body {
            background: linear-gradient(to right, #d4fc79, #96e6a1);
            font-family: 'Segoe UI', sans-serif;
        }

        .login-container {
            max-width: 450px;
            margin: 80px auto;
            background-color: #ffffff;
            padding: 40px;
            border-radius: 15px;
            box-shadow: 0 4px 15px rgba(0, 0, 0, 0.2);
        }

        .login-container h3 {
            text-align: center;
            color: #2d6a4f;
            margin-bottom: 30px;
        }

        .btn-success {
            background-color: #2d6a4f;
            border: none;
        }

        .btn-success:hover {
            background-color: #1b4332;
        }
    </style>
        <div class="login-container">
            <h3>Admin Login</h3>

            <div class="mb-3">
                <label for="txtUsername" class="form-label">Username</label>
                <asp:TextBox ID="txtusername" runat="server" CssClass="form-control" placeholder="Enter username"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtPassword" class="form-label">Password</label>
                <asp:TextBox ID="txtpassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter password"></asp:TextBox>
            </div>

            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-success w-100" OnClick="btnLogin_Click"  />

            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger d-block mt-3 text-center"></asp:Label>
        </div>
    <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
   


</asp:Content>

