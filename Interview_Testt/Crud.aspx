<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Crud.aspx.cs" Inherits="Interview_Testt.Crud" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.min.js"></script>
    <script>
        function validateForm() {
             
            if (document.getElementById("txtName").value == "") {
                document.getElementById("txtName").focus();
                alert("Name is required.");
                return false;
               
            }
            if (document.getElementById("txtAddress").value == "") {
                document.getElementById("txtAddress").focus();
                alert("Address is required.");
                return false;

            }
            if (document.getElementById("txtCity").value == "") {
                document.getElementById("txtCity").focus();
                alert("City is required.");
                return false;

            }
            if (document.getElementById("txtMobile").value == "") {
                document.getElementById("txtMobile").focus();
                alert("Mobile is required.");
                return false;
            }
            var mobile = document.getElementById("txtMobile").value;
            var mobilePattern = /^[0-9]{10}$/;
            if (!mobilePattern.test(mobile)) {
                alert("Please enter a valid 10-digit mobile number.");
                document.getElementById("mobile").focus();
                return false;
            }

            if (document.getElementById("txtWhatsApp").value == "") {
                document.getElementById("txtWhatsApp").focus();
                alert("Whatsapp mobile no. is required.");
                return false;

            }
            var mobile = document.getElementById("txtWhatsApp").value;
            var mobilePattern = /^[0-9]{10}$/;
            if (!mobilePattern.test(mobile)) {
                alert("Please enter a valid 10-digit mobile number.");
                document.getElementById("mobile").focus();
                return false;
            }

            if (document.getElementById("txtEmail").value == "") {
                document.getElementById("txtEmail").focus();
                alert("Email Id is required.");
                return false;

            }

            var email = document.getElementById("txtEmail").value;

    
            var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

            if (!emailPattern.test(email)) {
                alert("Please enter a valid email address.");
                document.getElementById("email").focus();
                return false;  
            }
            
            if (document.getElementById("txtAadhaar").value == "") {
                document.getElementById("txtAadhaar").focus();
                alert("aadhar is required.");
                return false;

            }
            var aadhar = document.getElementById("txtAadhaar").value;
            var mobilePattern = /^[0-9]{12}$/;
            if (!mobilePattern.test(aadhar)) {
                alert("Please enter a valid 12-digit aadhar number.");
                document.getElementById("aadhar").focus();
                return false;
            }


            if (document.getElementById("FileUpldAdhar").value == "") {
                document.getElementById("FileUpldAdhar").focus();
                alert("aadhar photo is required.");
                return false;

            }
            if (document.getElementById("FileUpldPassport").value == "") {
                document.getElementById("FileUpldPassport").focus();
                alert("passport photo is required.");
                return false;

            }
        
            var radios = document.getElementsByName("rblGender");
            var isChecked = false;

            for (var i = 0; i < radios.length; i++) {
                if (radios[i].checked) {
                    isChecked = true;
                    break;
                }
            }

            if (!isChecked) {
                alert("Please select a gender.");
                return false;
            }
            return false;  

            if (document.getElementById("chkQualifications").value == "") {
                document.getElementById("chkQualifications").focus();
                alert("select qualification is required.");
                return false;

            }


           
        }


    </script>

    <script>
        function validateRadio() {
            

            
        }
    </script>

    
</head>
<body>
    <form id="form1" runat="server" class="container mt-5">
        <div class="card">
            <div class="card-header text-center bg-primary text-white">
                <h3>User Details</h3>
            </div>

        <asp:HiddenField ID="hfUserId" runat="server" />

            <div class="card-body">
                <table class="table table-bordered">
                    <tbody>
                        <tr>
                            <td><label for="txtName">Name</label></td>
                            <td><asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td>
                                <label for="txtAddress">Address</label></td>
                            <td>
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td><label for="txtCity">City</label></td>
                            <td><asp:TextBox ID="txtCity" runat="server" CssClass="form-control"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td><label for="txtMobile">Mobile</label></td>
                            <td><asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" onkeypress="validateMobile();"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><label for="txtWhatsApp">WhatsApp Number</label></td>
                            <td><asp:TextBox ID="txtWhatsApp" runat="server" CssClass="form-control"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td><label for="txtEmail">Email</label></td>
                            <td><asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><label for="txtAadhaar">Aadhaar Card Number</label></td>
                            <td><asp:TextBox ID="txtAadhaar" runat="server" CssClass="form-control"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td><label for="AdharImg">Aadhaar Card Pic</label></td>
                            <td><asp:FileUpload ID="FileUpldAdhar" runat="server" CssClass="form-control" /></td>
                        </tr>
                        <tr>
                            <td><label for="PassportImg">Passport Size Pic</label></td>
                            <td><asp:FileUpload ID="FileUpldPassport" runat="server" CssClass="form-control" /></td>
                        </tr>

                        <tr>
                            <td><label>Gender</label></td>
                            <td>
                                <asp:RadioButtonList ID="rblGender" runat="server" CssClass="form-check-inline"  RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                    <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                                    <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>


                        <tr>
                            <td><label>Qualifications</label></td>
                            <td>
                                <asp:CheckBoxList ID="chkQualifications" runat="server" CssClass="form-check-inline" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="10th" Value="10th"></asp:ListItem>
                                    <asp:ListItem Text="12th" Value="12th"></asp:ListItem>
                                    <asp:ListItem Text="Graduate" Value="Graduate"></asp:ListItem>
                                    <asp:ListItem Text="Post Graduate" Value="Post Graduate"></asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2" class="text-center">
                                <asp:Button ID="btnAddUser" runat="server" Text="Save" CssClass="btn btn-primary" OnClientClick="validateForm();" OnClick="btnAddUser_Click1" />
                                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary" OnClick="btnBack_Click" />
                                <asp:HiddenField ID="hdnaadhar" runat="server" />
                                <asp:HiddenField ID="hdnpass" runat="server" />
                            </td>
                            
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>


       
    </form>

</body>
</html>
