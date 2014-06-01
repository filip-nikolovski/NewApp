<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListingSW.aspx.cs" Inherits="Diplomska.ListingSW" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="style/style.css" rel="stylesheet" />
    <link href="style/bootstrap.css" rel="stylesheet" />
    <link href="style/justified-nav.css" rel="stylesheet" />
    
    
</head>
<body>
    <form id="form1" runat="server">
        
    <div class="container">
        <div id="acount">
                 <a href="Index.aspx" style="float:left">
                    <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/ukim-logo-9.png" /><asp:Image ID="imgLogo1" runat="server" ImageUrl="~/Images/finki-logo-9.png" /></a>
               
                <asp:Label ID="lblLogedAs" runat="server" Text=""></asp:Label>

                &nbsp;

        <asp:Button ID="btnLoguot" runat="server" Text="Loguot" Width="70px" OnClick="btnLoguot_Click" />


            </div>
       
        <div class="masthead">
           
             <ul class="nav nav-justified">
          <li ><a href="Admin.aspx">Корисници</a></li>
          <li class="active"><a href="ListingSW.aspx">Научен труд</a></li>
          
              
            
        </ul>
        </div >
    <div class="main-content">
        <div class="main-content1">
        <div class="topMain">
        <asp:Label ID="lblChoseUser" runat="server" Text="Листај трудови по корисник"></asp:Label>
            &nbsp;
        <asp:DropDownList ID="ddlCorrAut" runat="server" DataTextField="fullName" DataValueField="email" AppendDataBoundItems="true" AutoPostBack="true" class="form-control" style="width:85%">
            <asp:ListItem Value="0">--ALL--</asp:ListItem>
        </asp:DropDownList>
&nbsp;
        <asp:Button ID="Button1" runat="server" Text="Листај" OnClick="Button1_Click" class="scroll btn btn-default"/>
       
        </div>
        <asp:GridView ID="gvScienceWork" runat="server" DataKeyNames="id" OnRowCreated="gvScienceWork_RowCreated" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" style="width:100%">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                        <%# Container.DataItemIndex +1+"." %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="title" HeaderText="Title" />
                <asp:BoundField DataField="autors" HeaderText="Authors" />
               
                <asp:BoundField DataField="price" HeaderText="Price" />
                <asp:BoundField DataField="date" HeaderText="Date" />
               

                   <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="ibtn1" runat="server" Text="delete" RowIndex='<%# Eval("id") %>'
                                    OnClick="button_click" class="scroll btn btn-default"/>
                            </ItemTemplate>
                        </asp:TemplateField>

                    <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="ibtn11" runat="server" Text="Верзии" RowIndex='<%# Eval("id") %>'
                                    OnClick="button11_click" class="scroll btn btn-default"/>
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
        <asp:Label ID="lblErr" runat="server" Text="" Visible="true"></asp:Label>
   
        </div>
        </div>
        <div>
        </div>
    </div>
    </form>
</body>
</html>
