<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GridView.aspx.cs" Inherits="Interview_Testt.GridView" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.7.0.min.js"></script>
    <script type="text/javascript">
    function confirmDelete() {
        return confirm("Are you sure you want to delete this item?");
    }
</script>
</head>
<body>
    <form id="form1"  class="container mt-5" runat="server">
        <div class="card">
            <div class="card-header text-center  ">
                <h3>User Details</h3>
                <div class="row">
                    <div class="col-4"></div>
                     <div class="col-4"></div>
                     <div style="align-items:end;" class="col-4"><asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-success text-end" NavigateUrl="~/Crud.aspx">Add User</asp:HyperLink></div>
                </div>
                <br>

                <table class="w-100">
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>Search Type</td>
                        <td>
                            <asp:DropDownList ID="ddlsearchtype" CssClass="form-control" runat="server" Width="226px">
                                <asp:ListItem Selected="True">Select</asp:ListItem>
                                <asp:ListItem>AadharNo</asp:ListItem>
                                <asp:ListItem>MobileNo</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox CssClass="form-control" ID="txtsearchnumber" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnserach" runat="server" OnClick="btnserach_Click" Text="Search" />
&nbsp;<asp:Button ID="btnsearchall" runat="server" OnClick="btnsearchall_Click" Text="Show All" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>

<asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
    <Columns>
        <asp:TemplateField HeaderText="SRNo.">
            
            <ItemTemplate>
                <%#Container.DataItemIndex+1 %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:BoundField DataField="Address" HeaderText="Address" />
        <asp:BoundField DataField="City" HeaderText="City" />
        <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
        <asp:BoundField DataField="Email" HeaderText="Email" />
        <asp:BoundField DataField="WhatsAppNumber" HeaderText="WhatsApp Number" />
        <asp:BoundField DataField="Aadhaar_Card_Number" HeaderText="Aadhaar Card Number" />
        <asp:TemplateField HeaderText="Aadhaar Card Image">
            <ItemTemplate>
                <asp:HyperLink ID="hlAadhaarCard" runat="server"
                    NavigateUrl='<%# Eval("Aadhaar_Card_Pic") %>'
                    Text="View Image"
                    Target="_blank"></asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>
       
        <asp:TemplateField HeaderText="Passport_Size_Pic">
    <ItemTemplate>
        <asp:HyperLink ID="hlAadhaarCard" runat="server"
            NavigateUrl='<%# Eval("Passport_Size_Pic") %>'
            Text="View Image"
            Target="_blank"></asp:HyperLink>
    </ItemTemplate>
</asp:TemplateField>
        <asp:TemplateField HeaderText="Qualifications">
            <ItemTemplate>
                <asp:Label ID="lblQualifications" runat="server" Text='<%# Bind("Qualifications") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Action">
     <ItemTemplate>
         <asp:Button ID="Btn_Edit" runat="server" Text="Edit"  CommandName='<%# Bind("Id") %>' OnClick="Btn_Edit_Click"/>
         &nbsp;
         <asp:Button ID="Btn_delete" runat="server" Text="Delete" CommandName='<%# Bind("Id") %>' OnClientClick="return confirmDelete();"  OnClick="Btn_delete_Click"  />
     </ItemTemplate>
 </asp:TemplateField>
    </Columns>
</asp:GridView>
            </div>

            
        </div>
    </form>
</body>
</html>
