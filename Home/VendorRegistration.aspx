<%@ Page Title="" Language="C#" MasterPageFile="~/Home/HomeMasterPage.master" AutoEventWireup="true" CodeFile="VendorRegistration.aspx.cs" Inherits="Home_VendorRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <style>
    .container {
        max-width: 1000px;
    }

    .card {
        border-radius: 10px;
        box-shadow: 0 0 12px rgba(0, 0, 0, 0.1);
    }

    .card-header {
        font-size: 1.5rem;
        font-weight: 600;
        letter-spacing: 1px;
    }

    label {
        font-weight: 600;
    }

    .form-control:focus {
        border-color: #28a745;
        box-shadow: 0 0 6px #28a74588;
    }

    .form-group {
        margin-bottom: 1rem;
    }

    .btn-success {
        font-weight: 600;
        border-radius: 50px;
    }

    .form-check input {
        margin-right: 10px;
    }

    @media (max-width: 768px) {
        .col-md-4 {
            margin-bottom: 20px;
        }
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

<div class="card container mt-4">
    <div class="card-header bg-success text-white text-center">
        Vendor Registration
    </div>
    <div class="card-body">

        <div class="row">
            <!-- Left Column -->
            <div class="col-md-4">
                <div class="form-group text-center">
                    <asp:Image ID="imgPreview" runat="server" Width="100px" Height="100px" CssClass="mb-2" />
                    <asp:FileUpload ID="fuphoto" runat="server" CssClass="form-control" onchange="previewImage(event)" />
                </div>
                <div class="form-group">
                    <label>Full Name / Business Name <span class="text-danger">*</span></label>
                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvFullName" runat="server" ControlToValidate="txtFullName" ErrorMessage="Required" ForeColor="Red" Display="Dynamic" />
                </div>
              
              
               <%-- <div class="form-group">
                    <label>Equipment Base Location <span class="text-danger">*</span></label>
                    <asp:TextBox ID="txtBaseLocation" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvBaseLocation" runat="server" ControlToValidate="txtBaseLocation" ErrorMessage="Required" ForeColor="Red" Display="Dynamic" />
                </div>--%>
            </div>

            <!-- Middle Column -->
            <div class="col-md-4">
                <div class="form-group">
                    <label>City <span class="text-danger">*</span></label>
                    <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCity" ErrorMessage="Required" ForeColor="Red" Display="Dynamic" />
                </div>
                <div class="form-group">
                    <label>State <span class="text-danger">*</span></label>
                    <asp:TextBox ID="txtState" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="txtState" ErrorMessage="Required" ForeColor="Red" Display="Dynamic" />
                </div>
                <div class="form-group">
                    <label>Password <span class="text-danger">*</span></label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Required" ForeColor="Red" Display="Dynamic" />
                </div>
            </div>

            <!-- Right Column -->
            <div class="col-md-4">
                <div class="form-group">
                    <label>Full Address <span class="text-danger">*</span></label>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                    <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddress" ErrorMessage="Required" ForeColor="Red" Display="Dynamic" />
                </div>
                  <div class="form-group">
                    <label>Mobile Number <span class="text-danger">*</span></label>
                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" MaxLength="10" />
                    <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ControlToValidate="txtMobile" ErrorMessage="Required" ForeColor="Red" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="revMobile" runat="server" ControlToValidate="txtMobile" ValidationExpression="^\d{10}$" ErrorMessage="Enter valid 10-digit mobile number" ForeColor="Red" Display="Dynamic" />
                </div>
                  <div class="form-group">
                    <label>Email <span class="text-danger">*</span></label>
                    <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtemail" ErrorMessage="Required" ForeColor="Red" Display="Dynamic" />
                </div>
               <%-- <div class="form-group">
                    <label>Equipment Type</label>
                    <asp:CheckBoxList ID="chkEquipment" runat="server" CssClass="form-check">
                        <asp:ListItem Text="Tractor" />
                        <asp:ListItem Text="Plow" />
                        <asp:ListItem Text="Harvester" />
                        <asp:ListItem Text="Seed Drill" />
                    </asp:CheckBoxList>
                </div>--%>
                <%--<div class="form-group">
                    <label>Equipment Details</label>
                    <asp:TextBox ID="txtEquipmentDetails" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                </div>--%>
            </div>
        </div>
        <div class="mb-3">
    <label for="txtAnswer" class="form-label">Security Answer (e.g.Your Nick Name )</label>
    <asp:TextBox ID="txtAnswer" runat="server" CssClass="form-control" placeholder="Enter your answer" />
    <asp:RequiredFieldValidator ID="rfvAnswer" runat="server"
        ControlToValidate="txtAnswer" ErrorMessage="Answer is required." 
        CssClass="text-danger" Display="Dynamic" />
</div>
        <!-- Submit Button -->
        <div class="text-center mt-4">
            <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-success px-5" OnClick="btnRegister_Click" />
            <asp:Label ID="lblMessage" runat="server" CssClass="d-block mt-3 font-weight-bold text-success"></asp:Label>
        </div>
    </div>
</div>
   

</asp:Content>



