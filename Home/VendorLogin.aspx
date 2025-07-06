<%@ Page Title="" Language="C#" MasterPageFile="~/Home/HomeMasterPage.master" AutoEventWireup="true" CodeFile="VendorLogin.aspx.cs" Inherits="Home_UserLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <style>
        body {
            background: linear-gradient(to right, #74ebd5, #acb6e5);
            font-family: 'Segoe UI', sans-serif;
        }

        .login-container {
            margin-top: 100px;
        }

        .login-card {
            background: white;
            border-radius: 15px;
            padding: 30px;
            box-shadow: 0 8px 16px rgba(0, 0, 0, 0.25);
        }

        .login-card h2 {
            color: #2d6a4f;
            margin-bottom: 20px;
        }

        .btn-custom {
            background-color: #2d6a4f;
            color: white;
            font-weight: bold;
        }

        .btn-custom:hover {
            background-color: #40916c;
            color: white;
        }

        .form-control:focus {
            border-color: #95d5b2;
            box-shadow: 0 0 0 0.2rem rgba(149, 213, 178, 0.25);
        }

        .form-label {
            color: #1b4332;
        }
    </style>
       <div class="container login-container">
            <div class="row justify-content-center">
                <div class="col-md-5">
                    <div class="login-card">
                        <h2 class="text-center">Vendor Login</h2>

                        <div class="mb-3">
                            <label for="txtEmail" class="form-label">Email</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter your email"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <label for="txtPassword" class="form-label">Password</label>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter your password"></asp:TextBox>
                        </div>
<!-- Add inside form1 -->
<div class="modal fade" id="successModal" tabindex="-1" aria-labelledby="successModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content text-center">
      <div class="modal-header bg-success text-white">
        <h5 class="modal-title w-100" id="successModalLabel">Login Successful</h5>
      </div>
      <div class="modal-body">
        Welcome back, Vendor! Redirecting to your dashboard...
      </div>
    </div>
  </div>
</div>
                        <div class="d-grid">
                            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-custom" OnClick="btnLogin_Click"  />
                        </div>

                        <div class="text-center mt-3">
                            <a href="FarmerRegistration.aspx" style="color:#2d6a4f; font-weight:bold;">New Farmer? Register Here</a><br />
                            <a href="VendorForgotPassword.aspx" style="color:#2d6a4f; font-weight:bold;">Forgot Password? Click Here</a>

                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>

