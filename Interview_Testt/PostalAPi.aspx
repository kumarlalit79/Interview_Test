<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PostalAPi.aspx.cs" Inherits="Interview_Testt.PostalAPi" Async="true"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Find City by PIN Code</h2>

            <label for="txtPinCode">Enter PIN Code:</label>
            <asp:TextBox ID="txtPinCode" runat="server" MaxLength="6"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txtPinCode" ErrorMessage="PIN Code is required." ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegExValidator" runat="server" 
                ControlToValidate="txtPinCode" 
                ValidationExpression="^\d{6}$" 
                ErrorMessage="Please enter a valid 6-digit PIN code." 
                ForeColor="Red"></asp:RegularExpressionValidator>
            
            <br /><br />
            
            <asp:Button ID="btnSubmit" runat="server" Text="Find City" OnClick="btnSubmit_Click" />
            
            <br /><br />
            
            <asp:Label ID="lblResult" runat="server" Text="" ForeColor="Green"></asp:Label>
            <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
        </div>
    </form>
</body>
</html>
