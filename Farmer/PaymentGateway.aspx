<%@ Page Title="" Language="C#" MasterPageFile="~/Farmer/FarmerMasterPage.master" AutoEventWireup="true" CodeFile="PaymentGateway.aspx.cs" Inherits="Farmer_PaymentGateway" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style>
        body {
            background-color: #f7f7f7;
        }
        .payment-form {
            max-width: 500px;
            margin: 50px auto;
            padding: 30px;
            background: white;
            border-radius: 12px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }
        .payment-form .form-control {
            border-radius: 10px;
        }
        .payment-form .btn-primary {
            width: 100%;
            border-radius: 10px;
        }
        .payment-title {
            font-weight: 600;
            font-size: 1.5rem;
            margin-bottom: 20px;
        }
    </style>
    <div class="payment-form">
    <div class="payment-title">Payment Gateway</div>
        <div class="mb-3">
            <label for="cardHolder" class="form-label">Card Holder Name</label>
            <input type="text" class="form-control" id="cardHolder" placeholder="Card Holder Name" required>
        </div>
        <div class="mb-3">
            <label for="cardNumber" class="form-label">Card Number</label>
            <input type="text" class="form-control" id="cardNumber" placeholder="1234 5678 9012 3456" required maxlength="16">
        </div>
        <div class="row">
            <div class="col-md-6 mb-3">
                <label for="expiry" class="form-label">Expiry Date</label>
                <input type="text" class="form-control" id="expiry" placeholder="MM/YY" required maxlength="5">
            </div>
            <div class="col-md-6 mb-3">
                <label for="cvc" class="form-label">CVC</label>
                <input type="password" class="form-control" id="cvc" placeholder="123" required maxlength="4">
            </div>
        </div>
          <asp:Button ID="btnPayNow" runat="server" Text="Pay Now" CssClass="btn btn-primary w-100" OnClick="btnPayNow_Click" />
        <asp:Button ID="Delete" CssClass="btn btn-danger" runat="server" Text="Cancel Payment" OnClick="Delete_Click" />
</div>

<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>

</asp:Content>

