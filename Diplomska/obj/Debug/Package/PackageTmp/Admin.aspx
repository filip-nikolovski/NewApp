<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Diplomska.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
 
     
     <link href="style/style.css" rel="stylesheet" />
    <link href="style/bootstrap.css" rel="stylesheet" />
    <link href="style/justified-nav.css" rel="stylesheet" />
   
</head>
<body>
    <form  runat="server">
      
        <div  class="container">
            <div id="acount">
                 <a href="Index.aspx" style="float:left">
                    <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/ukim-logo-9.png" /><asp:Image ID="imgLogo1" runat="server" ImageUrl="~/Images/finki-logo-9.png" /></a>
               
                 <asp:Label ID="lblLogedAs" runat="server" Text=""></asp:Label>
            &nbsp;
        <asp:Button ID="btnLoguot" runat="server" OnClick="Button1_Click" Text="Loguot" Width="70px" />
            </div>
           

        

         <div class="masthead">
            
             <ul class="nav nav-justified">
          <li class="active"><a href="Admin.aspx">Корисници</a></li>
          <li ><a href="ListingSW.aspx">Научен труд</a></li>
         
          
        </ul>
        </div>

        <div class="main-content" style="width:63%">
            <div class="main-content1">
            <div class="topMain">


                <asp:Label ID="lblUsers" runat="server" Text="Листај корисници по:"></asp:Label>
                <asp:DropDownList ID="ddlUsers" runat="server" class="form-control" style="width:85%">
                    <asp:ListItem Value="0">За прифаќање </asp:ListItem>
                    <asp:ListItem Value="1">Прифатени</asp:ListItem>
                    <asp:ListItem Value="2">Сите корисници</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnGO" runat="server" Text="Листај" OnClick="btnGO_Click" class="scroll btn btn-default"/>


            </div>
            <asp:GridView ID="gvListUsers" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gvListUsers_SelectedIndexChanged" DataKeyNames="id" OnRowCommand="gvListUsers_OnRowCommand" OnRowCreated="gvListUsers_RowCreated" CellPadding="4" ForeColor="#333333" GridLines="None" style="width:100%">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="name" HeaderText="Name" />
                    <asp:BoundField DataField="surname" HeaderText="Surname" />
                    <asp:BoundField DataField="email" HeaderText="Email" />
                    <asp:BoundField DataField="labs_id" HeaderText="labratory" />
                    <asp:BoundField DataField="accsepted" HeaderText="accsepted" />
                    <asp:TemplateField >
                        <ItemTemplate>
                            <asp:RadioButton runat="server" GroupName="venc" ID="rd1" Text="add"/>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:RadioButton runat="server" GroupName="venc" ID="rd2"  Text="remove" Visible="true"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                    
                   
                    <asp:ButtonField CommandName="Select" Text="aplay" HeaderText="Confirm" ButtonType="Button" />
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
            <asp:Label ID="lblEx" runat="server" Text="" ForeColor="Red"></asp:Label>

        </div>
            </div>
            <div class="aside" style="width:35%;padding-left:0%">
                <section>
                    <h3 style="padding-left:5%">Рестартирај лозинка</h3>
                </section>
                <asp:GridView ID="gvResserPass" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowCreated="gvResserPass_RowCreated" CellPadding="6" ForeColor="#333333" GridLines="None" style="width:100%">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="User" HeaderText="User" />
                  
                         <asp:TemplateField >
                        <ItemTemplate>
                            <asp:Button ID="btnResset" runat="server" Text="Resset" RowIndex='<%# Eval("id") %>'
                                    OnClick="button_click" class="scroll btn btn-default"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:TemplateField >
                        <ItemTemplate>
                            <asp:Button ID="btnDelete" runat="server" Text="delete user" RowIndex='<%# Eval("id") %>'
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
            </div>

            </div>
    </form>
       <script src="scripts/jquery.js"></script>
    <script src="scripts/bootstrap.min.js"></script>

</body>
</html>
