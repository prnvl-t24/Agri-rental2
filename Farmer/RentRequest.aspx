<%@ Page Title="" Language="C#" MasterPageFile="~/Farmer/FarmerMasterPage.master" AutoEventWireup="true" CodeFile="RentRequest.aspx.cs" Inherits="Farmer_RentRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  <style>
        .form-box {
            max-width: 600px;
            margin: 30px auto;
            background: #f9f9f9;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 0 15px rgba(0,0,0,0.1);
        }
    </style>

    <div class="form-box">
      <%--  <asp:Label ID="LBLALERT" runat="server" Text=""></asp:Label>--%>
        <h3 class="mb-4 text-center">Rent Product</h3>
        <asp:Label ID="lblProduct" runat="server" CssClass="form-label fw-bold"></asp:Label>
            <asp:HiddenField ID="hfProductID" runat="server" />
<asp:HiddenField ID="hfTotalAmount" runat="server" /> <!-- stores price per day -->
<asp:HiddenField ID="hfTotalCalculatedAmount" runat="server" /> <!-- stores total amount -->        <div class="mb-3">
            <label class="form-label">From Date:</label>
            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TextMode="Date" ></asp:TextBox>
        </div>

        <div class="mb-3">
            <label class="form-label">To Date:</label>
            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TextMode="Date" ></asp:TextBox>
        </div>

      <div class="mb-3">
    <label class="form-label">Total Amount (₹):</label>
    <asp:Label ID="lblAmount" runat="server" CssClass="form-control fw-bold"></asp:Label>
</div>


        <%--<div class="mb-3">
            <label class="form-label">Day(s) Description:</label>
            <asp:TextBox ID="txtDays" runat="server" CssClass="form-control" placeholder="e.g., Monday to Friday" ></asp:TextBox>
        </div>--%>
        <asp:Label ID="Label1" runat="server" CssClass="form-control fw-bold"></asp:Label>
<asp:HiddenField ID="HiddenField1" runat="server" />

        <asp:HiddenField ID="HiddenField2" runat="server" />

<asp:TextBox ID="TextBox1" runat="server" AutoPostBack="true" OnTextChanged="txtFromDate_TextChanged" Visible="false" />
<asp:TextBox ID="TextBox2" runat="server" AutoPostBack="true" OnTextChanged="txtToDate_TextChanged" Visible="false" />

<asp:Label ID="Label2" runat="server" ForeColor="Green" Font-Bold="true" />

<asp:HiddenField ID="HiddenField3" runat="server" />

        <asp:Button ID="btnSubmit" runat="server" Text="Submit Request" CssClass="btn btn-success w-100" OnClick="btnSubmit_Click" />
     <%--   <asp:Button ID="btnCancel" runat="server" Text="Cancel Request" CssClass="btn btn-danger w-100 mt-2" OnClick="btnCancel_Click" />--%>
    </div>
    <script type="text/javascript">
        function calculateTotalAmount() {
            var fromDate = document.getElementById('<%= txtFromDate.ClientID %>').value;
           var toDate = document.getElementById('<%= txtToDate.ClientID %>').value;
           var pricePerDay = parseFloat(document.getElementById('<%= hfTotalAmount.ClientID %>').value);
    var totalAmountLabel = document.getElementById('<%= lblAmount.ClientID %>');
           var hfTotalCalculatedAmount = document.getElementById('<%= hfTotalCalculatedAmount.ClientID %>');

           if (fromDate && toDate) {
               var date1 = new Date(fromDate);
               var date2 = new Date(toDate);

               if (date2 >= date1) {
                   var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                   var dayDiff = Math.ceil(timeDiff / (1000 * 3600 * 24)) + 1; // Including both days
                   var total = pricePerDay * dayDiff;
                   totalAmountLabel.innerText = total.toFixed(2);
                   hfTotalCalculatedAmount.value = total.toFixed(2);  // <-- update hidden field here
               } else {
                   totalAmountLabel.innerText = "0.00";
                   hfTotalCalculatedAmount.value = "0";
               }
           } else {
               totalAmountLabel.innerText = "0.00";
               hfTotalCalculatedAmount.value = "0";
           }
       }
    document.addEventListener("DOMContentLoaded", function () {
        document.getElementById('<%= txtFromDate.ClientID %>').addEventListener("change", calculateTotalAmount);
        document.getElementById('<%= txtToDate.ClientID %>').addEventListener("change", calculateTotalAmount);
    });
       
    </script>
<div id="successPopup" style="display: none; position: fixed; top: 35%; left: 50%; transform: translate(-50%, -50%);
    background-color: #e0ffe0; color: #155724; padding: 20px 30px; border: 2px solid #28a745;
    border-radius: 10px; z-index: 9999; font-size: 18px; text-align: center;">
    ✅ Request submitted successfully!<br /><br />
   
</div>
<script type="text/javascript">
    function showPopup() {
        var popup = document.getElementById("successPopup");
        popup.style.display = "block";

        setTimeout(function () {
            window.location.href = "RequsetPage.aspx";
        }, 1000); // 3 seconds delay before redirect
    }
</script>

</asp:Content>

