<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VersionSource.aspx.cs" Inherits="Diplomska.VersionSource" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    
    <link href="style/style.css" rel="stylesheet" />
    <link href="style/bootstrap.css" rel="stylesheet" />
    <link href="style/justified-nav.css" rel="stylesheet" />
    <script type="text/javascript" src="scripts/jquery.js"></script>

    
    <script type="text/javascript" src="scripts/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="scripts/jquery.uploadify.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="container">


            <div id="acount">
                <a href="Index.aspx" style="float: left">
                    <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/ukim-logo-9.png" /><asp:Image ID="imgLogo1" runat="server" ImageUrl="~/Images/finki-logo-9.png" /></a>

                <asp:Label ID="lblLogedAs" runat="server" Text=""></asp:Label><a href="UserProfile.aspx"><asp:Label ID="lblLogedAs1" runat="server" Text=""></asp:Label></a>
                &nbsp;
            <asp:Button ID="btnLoguot" runat="server" OnClick="Button1_Click" Text="Loguot" Width="70px" />
            </div>

            <div class="masthead">
                <ul class="nav nav-justified">
                    <li class="active"><a href="Index.aspx">Научен Труд</a></li>
                    <li><a href="Conference.aspx">Конференција</a></li>
                    <li><a href="Events.aspx">Настани</a></li>
                </ul>
                <a href="Index.aspx" style="margin-bottom: -15px;">Научен труд> </a><a href="Versions.aspx">Верзии> </a>
                <asp:Label ID="lblNav" runat="server" Text=""></asp:Label>
            </div>



            <div class="main-content">
                <div class="main-content1" style="padding-left: 4%; padding-top: 2%; padding-bottom: 4%">
                    <h3 class="text-muted" style="padding-bottom: 1.5%">
                        <asp:Label ID="lblSWTitle" runat="server" Text=""></asp:Label></h3>
                    <div style="float: right;margin-bottom:1%">
                        <asp:Button ID="btnDownload" runat="server" Text="Download selected" OnClick="DownloadFiles" class="btn btn-default" />
                        <asp:Button ID="btnDownloadAll" runat="server" Text="Download all files" OnClick="btnDownloadAll_Click" class="btn btn-default" />
                    </div>


                    <asp:GridView ID="GridView1" CssClass="myDataGridClass" runat="server" AutoGenerateColumns="false" EmptyDataText="No files available" CellPadding="7" ForeColor="#333333" GridLines="None" OnRowCreated="GridView1_RowCreated" Style="width: 100%">
                        <Columns>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                    <asp:Label ID="lblFilePath" runat="server" Text='<%# Eval("Value") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="File Name" ControlStyle-CssClass="imag">
                                <ItemTemplate>
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/images.png" CssClass="imm" />
                                    <asp:Label ID="lblFilename" runat="server" Text='<%# Eval("Text") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Image ID="Image11" runat="server" ImageUrl="~/Images/download.png" CssClass="imm" />

                                    <asp:LinkButton ID="lnkDownload" Text="Download" CommandArgument='<%# Eval("Value") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Image ID="Image111" runat="server" ImageUrl="~/Images/delete.png" CssClass="imm" />

                                    <asp:LinkButton ID="lnkDelete" Text="Delete" CommandArgument='<%# Eval("Value") %>' runat="server" OnClick="DeleteFile" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />

                    </asp:GridView>
                    <br />
                    <asp:Label ID="lblerr" runat="server" Text=""></asp:Label>


                </div>
            </div>


            <div class="aside">

                <asp:FileUpload ID="FileUpload1" runat="server" /><br />
                <a href="javascript:$('#<%=FileUpload1.ClientID%>').fileUploadStart()">Start Upload</a>
                <a href="javascript:$('#<%=FileUpload1.ClientID%>').fileUploadClearQueue()">Clear</a>
                <asp:Label ID="lblPath" runat="server" Text="" Visible="true"></asp:Label>
            </div>

        </div>


    </form>
</body>
</html>

<script type = "text/javascript">
    $(window).load(
        function () {
            $("#<%=FileUpload1.ClientID %>").fileUpload({
                'uploader': 'scripts/uploader.swf',
                'cancelImg': 'images/cancel.png',
                'buttonText': 'Upload Files',
                'script': 'Upload.ashx',
                'fileDesc': 'Image Files',
                'multi': true,
                'auto': false
            });
            return false;
        }
);
</script> 

