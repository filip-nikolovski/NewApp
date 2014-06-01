<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Diplomska.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Регистрација</title>
     <link href="style/style.css" rel="stylesheet" />
     <link rel="shortcut icon" href="../Images/favicon.ico" />
    <link href="style/loginStile.css" rel="stylesheet" />
    <link href="style/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
             <div id="acount" style="margin-bottom:7%">
                 <a href="Index.aspx" style="float:left">
                    <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/ukim-logo-9.png" /><asp:Image ID="imgLogo1" runat="server" ImageUrl="~/Images/finki-logo-9.png" /></a>
               
               

            </div>
    <div class="main-content" style="padding-left:3%">
        <div class="main-content1 " style="height:495px">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

               
     <h3 class="text-muted" >
                    <asp:Label ID="lblTitle1" runat="server" Text="Регистрација"></asp:Label></h3>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblName" runat="server" Text="Име: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server"  class="form-control" placeholder="Име"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="txtName" ValidationGroup="group1"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblSurname" runat="server" Text="Презиме: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSurname" runat="server" class="form-control" placeholder="Презиме"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="txtSurname" ValidationGroup="group1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUniversity" runat="server" Text="Унивиерзитет: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlUniversity" runat="server" DataValueField="id" class="form-control" DataTextField="name" AutoPostBack="true" AppendDataBoundItems="true"  OnSelectedIndexChanged="ddlUniversity_SelectedIndexChanged">
                        <asp:ListItem Value="0">-- Select university --</asp:ListItem>

                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="ddlUniversity" ValidationGroup="group1" InitialValue="0"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                     <asp:Label ID="lblFaculty" runat="server" Text="Факултет:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlFaculty" runat="server" DataValueField="id" class="form-control" DataTextField="name" AutoPostBack="true" AppendDataBoundItems="true"  OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged">
                        <asp:ListItem Value="0">-- Select faculty --</asp:ListItem>

                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="ddlFaculty" ValidationGroup="group1" InitialValue="0"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                     <asp:Label ID="lblInstitute" runat="server" Text="Институт:"></asp:Label>
                </td>
                <td>
                        <asp:DropDownList ID="ddlInstitute" runat="server" DataValueField="id" class="form-control" DataTextField="name" AutoPostBack="true" AppendDataBoundItems="true"  OnSelectedIndexChanged="ddlInstitute_SelectedIndexChanged">
                        <asp:ListItem Value="0">-- Select institute --</asp:ListItem>

                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="ddlInstitute" ValidationGroup="group1" InitialValue="0"></asp:RequiredFieldValidator>
                </td>
            </tr>
              <tr>
                <td>
                    <asp:Label ID="lblLab" runat="server" Text="Лабораторија: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlLab" runat="server" DataValueField="id" class="form-control" DataTextField="Name" AutoPostBack="false" AppendDataBoundItems="true" >
                        <asp:ListItem Value="0">-- Select labaratory --</asp:ListItem>

                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="ddlLab" ValidationGroup="group1" InitialValue="0"></asp:RequiredFieldValidator>
                </td>
            </tr>




        <tr>
                <td>
                    <asp:Label ID="lblEmail" runat="server" Text="Email: "></asp:Label>
                </td>
                <td>

                    <asp:TextBox ID="txtEmail" runat="server"  class="form-control" placeholder="емаил адреса"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"  ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="txtEmail" ValidationGroup="group1"></asp:RequiredFieldValidator>
                   
                     <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail" ErrorMessage="*Неправилен емаил формат" Display="Dynamic" ValidationGroup="group1"></asp:RegularExpressionValidator><br />

                </td>
            </tr>
        <tr>
                <td>
                    <asp:Label ID="lblPassword" runat="server" Text="Лозинка: "  ></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" TextMode="Password"  runat="server"  class="form-control" placeholder="лозинка"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).*$" ControlToValidate="txtPassword" ErrorMessage="*Неправилен формат на лозинка" Display="Dynamic" ValidationGroup="group1"></asp:RegularExpressionValidator><br />
               
                     </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblPasswordRe"  runat="server" Text="Повтори лозинка: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPasswordRE" TextMode="Password" runat="server"  class="form-control" placeholder="Повтори лозинка"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="txtPasswordRE" ValidationGroup="group1"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="*Внеси иста лозинка" ControlToValidate="txtPasswordRE" ControlToCompare="txtPassword" ValidationGroup="group1" Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
             
    
        </table>
                    <div class="btnDown">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel"  class="btn btn-primary" OnClick="btnCancel_Click"/>
                    <asp:Button ID="Button1" runat="server" Text="Save" OnClick="Button1_Click" ValidationGroup="group1" class="btn btn-primary"/>
                    </div>
        <br />
            <asp:Label ID="lblErr" runat="server" Text="" ForeColor="Red"></asp:Label>
        <br />
                            <hr style="width:65%;float:left;padding:0%;"/><br />

                    <div class="description" style="margin-top:0%;float:left">
                        *Лозинката мора да содржи минимум 8 карактери од кои мора да има <br />барем еден број и голема буква
                    </div>

                        </ContentTemplate>
            </asp:UpdatePanel>
            </div>
                 
    </div>
            </div>
    </form>
</body>
</html>
