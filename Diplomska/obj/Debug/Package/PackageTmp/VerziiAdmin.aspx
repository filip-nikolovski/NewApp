<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerziiAdmin.aspx.cs" Inherits="Diplomska.VerziiAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
 
     <link href="style/style.css" rel="stylesheet" />
    <link href="style/bootstrap.css" rel="stylesheet" />
    <link href="style/justified-nav.css" rel="stylesheet" />
   
</head>
<body>
    <form id="Form1"  runat="server">
      
        <div  class="container">

        

         <div class="masthead">
                  <div id="acount">
                 <a href="Index.aspx" style="float:left">
                    <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/ukim-logo-9.png" /><asp:Image ID="imgLogo1" runat="server" ImageUrl="~/Images/finki-logo-9.png" /></a>
               
                <asp:Label ID="lblLogedAs" runat="server" Text=""></asp:Label>

                &nbsp;

        <asp:Button ID="btnLoguot" runat="server" Text="Loguot" Width="70px" OnClick="btnLoguot_Click" />


            </div>
             <ul class="nav nav-justified">
          <li ><a href="Admin.aspx">Корисници</a></li>
          <li class="active"><a href="ListingSW.aspx">Научен труд</a></li>
          
          
        </ul>
             <a href="ListingSW.aspx" style="margin-bottom:-15px;">Научен труд> </a>Верзии
        </div>

        <div class="main-content">
            <div class="main-content1">
            <div class="topMain">
                <h3 class="text-muted"><asp:Label ID="lblTitle" runat="server" Text="Избраниот труд нема креирано верзии."></asp:Label></h3>
            </div>
            <asp:GridView ID="gvVerziiAdmin" runat="server" DataKeyNames="id_version" OnRowCreated="gvVerziiAdmin_RowCreated"  AutoGenerateColumns="False"  Style="width: 100%" CellPadding="4" ForeColor="#333333" GridLines="None" >
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex +1+"." %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="version_name" HeaderText="Version Name" />
                    <asp:BoundField DataField="uploader" HeaderText="Uploaded by" />
                    <asp:BoundField DataField="date_upload" HeaderText="Date Uplod" />

                    <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="ibtn1" runat="server" Text="delete" RowIndex='<%# Eval("id_version") %>'
                                    OnClick="button_click" class="btn btn-default"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>

            <br />
            <asp:Label ID="lblEx" runat="server" Text=""></asp:Label>

           
                </div>
        </div>
            </div>
    </form>
       <script src="scripts/jquery.js"></script>
    <script src="scripts/bootstrap.min.js"></script>

</body>
</html>
