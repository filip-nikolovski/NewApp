<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="Diplomska.UserProfile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Лични податоци</title>

    <link href="style/style.css" rel="stylesheet" />
    <link href="style/bootstrap.css" rel="stylesheet" />
    <link href="style/justified-nav.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="container">

            <div id="acount">
                <a href="Index.aspx" style="float: left">
                    <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/ukim-logo-9.png" /><asp:Image ID="imgLogo1" runat="server" ImageUrl="~/Images/finki-logo-9.png" /></a>

                <asp:Label ID="lblLogedAs" runat="server" Text=""></asp:Label><a href="UserProfile.aspx"><asp:Label ID="lblLogedAs1" runat="server" Text=""></asp:Label></a>&nbsp;
            <asp:Button ID="btnLoguot" runat="server" OnClick="Button1_Click" Text="Loguot" Width="70px" />
            </div>

            <div class="masthead">
                <ul class="nav nav-justified">
                    <li><a href="Index.aspx">Научен Труд</a></li>
                    <li><a href="Conference.aspx">Конференција</a></li>
                    <li><a href="Events.aspx">Настани</a></li>
                </ul>
            </div>


            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <div class="main-content">
                        <div class="main-content1" style="padding-left: 4%; padding-top: 4%; padding-bottom: 4%">
                              <div id="newWork" style="padding-bottom:0%">
                               
                                <asp:Button ID="btnModify" runat="server" Text="Edit profile"  class="btn btn-default" OnClick="btnModify_Click"  />
                                <asp:Button ID="btnChangePass" runat="server" Text="Change password"  class="btn btn-default" OnClick="btnChangePass_Click" />
                                
                             </div>
                            <h3 class="text-muted" style="padding-bottom: 0%">
                                <asp:Label ID="lblSWTitle" runat="server" Text=""></asp:Label></h3>
                           
                         
                            <hr style="width:65%;float:left;padding:0%;"/>
                           <div runat="server" id="tblUsrInfo">
                            <table  style="padding-left:3%; width:80%;" >
                                <tr>
                                    <td style="text-align:left;width:20%">
                                        <asp:Label ID="lblName" runat="server" Text="Име "></asp:Label>
                                    </td>
                                    <td style="padding-left:2%;width:80%">
                                        <asp:Label ID="lblName1" runat="server" Text=""></asp:Label>
                                        <asp:TextBox ID="txtName" runat="server" Visible="false" class="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="txtName" ValidationGroup="group12"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left">
                                        <asp:Label ID="lblSurname" runat="server" Text="Презиме "></asp:Label>
                                    </td>
                                    <td style="padding-left:2%;">
                                        <asp:Label ID="lblSurname1" runat="server" Text=""></asp:Label>
                                        <asp:TextBox ID="txtSurname" runat="server" Visible="false" class="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="txtSurname" ValidationGroup="group12"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left">
                                        <asp:Label ID="lblEmail" runat="server" Text="email "></asp:Label>
                                    </td>
                                    <td style="padding-left:2%;">
                                        <asp:Label ID="lblEmail1" runat="server" Text=""></asp:Label>
                                         <asp:TextBox ID="txtEmail" runat="server" Visible="false" class="form-control"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail" ErrorMessage="*Неправилен емаил формат" Display="Dynamic" ValidationGroup="group12"></asp:RegularExpressionValidator>
                                            
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="txtEmail" ValidationGroup="group12"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left">
                                        <asp:Label ID="lblLab" runat="server" Text="лабораторија " ></asp:Label>
                                    </td>
                                    <td style="padding-left:2%;">
                                        <asp:Label ID="lblLab1" runat="server" Text=""></asp:Label>
                                        <asp:DropDownList ID="ddlLabs" runat="server" DataValueField="id" class="form-control" DataTextField="Name" AutoPostBack="false" AppendDataBoundItems="true" Visible="false">
                                        <asp:ListItem Value="0">-- Select labaratory --</asp:ListItem>
                                        </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="ddlLabs" InitialValue="0" ValidationGroup="group12"></asp:RequiredFieldValidator>
                                   
                                         </td>
                                </tr>
                         
                             
                            </table>
                               <div class="btnDown">
                                        <asp:Button ID="btnCancel1" runat="server" Text="Cancel" class="btn btn-primary" OnClick="btnCancel1_Click" Visible="false"/>

                                        <asp:Button ID="btnUpdate" runat="server" Text="Save" class="btn btn-primary" OnClick="btnUpdate_Click" Visible="false" ValidationGroup="group12"/>


                               </div>
                               <br />
                            </div>
                            <div runat="server" id="tblChangePass" visible="false">
                            <table style="width:80%">
                                <tr>
                                    <td style="text-align:right;width:250px">
                                        <asp:Label ID="lblPass" runat="server" Text="Внеси постоечка лозинка"></asp:Label>
                                    </td>
                                    <td style="padding-left:5%;">
                                        <asp:TextBox ID="txtPass" TextMode="Password" runat="server" class="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="txtPass" ValidationGroup="group1"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                 <tr>
                                    <td style="text-align:right">
                                        <asp:Label ID="lblRepeatPass" runat="server" Text="Повтори ја постоечката лозинка"></asp:Label>
                                    </td>
                                    <td style="padding-left:5%;">
                                        <asp:TextBox ID="txtRePass" TextMode="Password" runat="server" class="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="txtRePass" ValidationGroup="group1"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="*Внеси иста лозинка" ControlToValidate="txtRePass" ControlToCompare="txtPass" ValidationGroup="group1" Display="Dynamic"></asp:CompareValidator>
                
                                    </td>
                                </tr>
                                 <tr>
                                    <td style="text-align:right">
                                        <asp:Label ID="lblNewPass" runat="server" Text="Внеси ја новата лозинка"></asp:Label>
                                    </td>
                                    <td style="padding-left:5%;">
                                        <asp:TextBox ID="txtNewPass" TextMode="Password" runat="server" class="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="txtNewPass" ValidationGroup="group1"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                              

                            </table>
                                <div class="btnDown" style="margin-right:20%">
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"    class="btn btn-primary"/>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"  class="btn btn-primary" ValidationGroup="group1"/>


                                </div>
                                <br />
                                </div>
                             <asp:Label ID="lblErr" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>

                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
