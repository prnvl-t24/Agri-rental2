<%@ Page Title="" Language="C#" MasterPageFile="~/Home/HomeMasterPage.master" AutoEventWireup="true" CodeFile="FarmerRegistration.aspx.cs" Inherits="Home_FarmerRegistration" %>

<asp:Content  ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />

    <style>
   body {
    background: linear-gradient(to right, #dfffd6, #ffffff);
    font-family: 'Segoe UI', sans-serif;
}

.registration-container {
    background-color: #ffffff;
    padding: 30px;
    margin-top: 40px;
    border-radius: 20px;
    box-shadow: 0 5px 20px rgba(0, 0, 0, 0.1);
}

h2 {
    color: #2d6a4f;
}

.btn-primary {
    background-color: #2d6a4f;
    border: none;
}

.btn-primary:hover {
    background-color: #40916c;
}

.text-danger {
    color: #dc3545 !important;
}

#popupMessage {
    display: none;
    position: fixed;
    top: 20px;
    right: 20px;
    background-color: #28a745;
    color: white;
    padding: 15px;
    border-radius: 8px;
    z-index: 9999;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
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
<div class="container registration-container">
    <center><h2>Farmer Registration</h2></center>
<div class="row">
    <asp:Image ID="imgPreview" runat="server" Width="100px" Height="100px" CssClass="mb-2" />
                    <asp:FileUpload ID="fuphoto" runat="server" CssClass="form-control" onchange="previewImage(event)" />
    <div class="col-md-4 mb-3">
        <label for="txtname" class="form-label">Full Name</label>
        <asp:TextBox ID="txtname" runat="server" CssClass="form-control" placeholder="Enter full name" />
        <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtname" ErrorMessage="Full Name is required." CssClass="text-danger" Display="Dynamic" />
    </div>
    
    <div class="col-md-4 mb-3">
        <label for="txtphone" class="form-label">Mobile Number</label>
        <asp:TextBox ID="txtphone" runat="server" CssClass="form-control" MaxLength="10" placeholder="Enter mobile number" />
        <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtphone" ErrorMessage="Mobile Number is required." CssClass="text-danger" Display="Dynamic" />
        <asp:RegularExpressionValidator ID="revPhone" runat="server" ControlToValidate="txtphone" ValidationExpression="^\d{10}$" ErrorMessage="Enter a valid 10-digit number." CssClass="text-danger" Display="Dynamic" />
    </div>

    <div class="col-md-4 mb-3">
        <label for="txtemail" class="form-label">Email</label>
        <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" TextMode="Email" placeholder="Enter email (optional)" />
        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtemail" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$" ErrorMessage="Invalid email format." CssClass="text-danger" Display="Dynamic" />
       <asp:RequiredFieldValidator ID="revEmhail" runat="server" ControlToValidate="txtemail" ErrorMessage="Email is required." CssClass="text-danger" Display="Dynamic" />

    </div>

    <div class="col-md-4 mb-3">
        <label for="txtaddress" class="form-label">Address</label>
        <asp:TextBox ID="txtaddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" placeholder="Enter address" />
        <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtaddress" ErrorMessage="Address is required." CssClass="text-danger" Display="Dynamic" />
    </div>

    <div class="col-md-4 mb-3">
        <label for="txtlandarea" class="form-label">Land Area (in acres)</label>
        <asp:TextBox ID="txtlandarea" runat="server" CssClass="form-control" placeholder="Enter land area (optional)" />
        <asp:RegularExpressionValidator ID="revLandArea" runat="server" ControlToValidate="txtlandarea" ValidationExpression="^\d+(\.\d{1,2})?$" ErrorMessage="Enter a valid numeric land area." CssClass="text-danger" Display="Dynamic" />
    </div>

    <div class="col-md-4 mb-3">
        <label for="txtcroptype" class="form-label">Crop Type</label>
        <asp:TextBox ID="txtcroptype" runat="server" CssClass="form-control" placeholder="e.g. Wheat, Rice, Maize" />
    </div>

  

    <div class="col-md-4 mb-3">
        <label for="txtpassword" class="form-label">Password</label>
        <asp:TextBox ID="txtpassword" runat="server" CssClass="form-control" TextMode="Password" />
        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtpassword" ErrorMessage="Password is required." CssClass="text-danger" Display="Dynamic" />
    </div>

    <div class="col-md-4 mb-3">
        <label for="txtconfirmPassword" class="form-label">Confirm Password</label>
        <asp:TextBox ID="txtconfirmPassword" runat="server" CssClass="form-control" TextMode="Password" />
        <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txtconfirmPassword" ErrorMessage="Confirm Password is required." CssClass="text-danger" Display="Dynamic" />
        <asp:CompareValidator ID="cvPassword" runat="server" ControlToCompare="txtpassword" ControlToValidate="txtconfirmPassword" ErrorMessage="Passwords do not match." CssClass="text-danger" Display="Dynamic" />
    </div>
      <%--<div class="col-md-4 mb-3">
        <label for="fuphoto" class="form-label">Profile Photo</label>
        <asp:FileUpload ID="fuphoto" runat="server" CssClass="form-control" />
    </div>--%>
    <div class="col-md-4 mb-3">
        <label for="txtColor" class="form-label">What is your Nick Name ?</label>
        <asp:TextBox ID="txtColor" runat="server" CssClass="form-control" placeholder="Enter your favourite color" />
        <asp:RequiredFieldValidator ID="rfvColor" runat="server" ControlToValidate="txtColor" ErrorMessage="Please enter your favourite color." CssClass="text-danger" Display="Dynamic" />
    </div>
</div>

<div class="text-center">
    <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-primary px-4 mt-3" OnClick="btnRegister_Click" />
</div>
</div>

    <div id="popupMessage" style="display:none; position:fixed; top:20px; right:20px; background-color:#28a745; color:white; padding:15px; border-radius:8px; z-index:9999; box-shadow:0 0 10px rgba(0,0,0,0.2);">
    Farmer registered successfully!
</div>
    <div class="modal fade" id="successModal" tabindex="-1" aria-labelledby="successModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content border-success">
      <div class="modal-header bg-success text-white">
        <h5 class="modal-title" id="successModalLabel">Success</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body text-success">
        Farmer registered successfully!
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-success" data-bs-dismiss="modal">OK</button>
      </div>
    </div>
  </div>
</div>
    <div class="modal fade" id="errorModal" tabindex="-1" aria-labelledby="errorModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header bg-danger text-white">
        <h5 class="modal-title" id="errorModalLabel">Error</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        Something went wrong. Please try again!
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
      </div>
    </div>
  </div>
</div>
    <script type="text/javascript">
    function showPopup() {
        var popup = document.getElementById('popupMessage');
        popup.style.display = 'block';
        setTimeout(function () {
            popup.style.display = 'none';
        }, 3000); // hide after 3 seconds
    }
    </script>
</asp:Content>

