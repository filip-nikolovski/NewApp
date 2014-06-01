<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Download.aspx.cs" Inherits="Diplomska.Download" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gvDownload" runat="server" DataKeyNames="id_version" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                        <%# Container.DataItemIndex +1+"." %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="id_version" HeaderText="id version" />
                <asp:BoundField DataField="version_name" HeaderText="version_name" />

                <asp:TemplateField>
                    <ItemTemplate>
                        <a href="<%# Eval("file_path")%>"><%# Eval("version_name")%> </a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <asp:Label ID="lblException" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
