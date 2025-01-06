<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employe.aspx.cs" Inherits="Interview_Testt.Employe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/bootstrap.min.js"></script>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        
        <div class="container mt-5">
            <div class="row">
                <div class="col-md-4 offset-4">
                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="form-control"> 
</asp:DropDownList>
                    <br />
                    <br />
                    <asp:Label ID="SelectedEmployeeLabel" runat="server" Text="Select an employee above." CssClass="form-label"></asp:Label>
                </div>
            </div>
        </div>
        
    </form>
</body>
</html>
