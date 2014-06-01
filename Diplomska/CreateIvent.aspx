<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateIvent.aspx.cs" Inherits="Diplomska.CreateIvent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
                <asp:Label ID="lblLogedAs" runat="server" Text=""></asp:Label>

                &nbsp;

        <asp:Button ID="btnLoguot" runat="server" OnClick="Button1_Click" Text="Loguot" Width="70px" />
            </div>
            <div class="masthead">

                <ul class="nav nav-justified">
                    <li ><a href="Index.aspx">Index</a></li>
                    <li><a href="Conference.aspx">Create Conference</a></li>
                    <li class="active"><a href="Events.aspx">Events</a></li>

                </ul>
            </div>
            <div class="main-content">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>

            <asp:Label ID="lblSW" runat="server" Text="Izberi Trud"></asp:Label>
            <asp:DropDownList ID="ddlSW" runat="server" DataTextField="full_name" DataValueField="id" AppendDataBoundItems="true" AutoPostBack="true">
                <asp:ListItem Value="0">=Select=</asp:ListItem>
            </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="ddlSW" ValidationGroup="group1" InitialValue="0"></asp:RequiredFieldValidator>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEvent" runat="server" Text="Chose Event"></asp:Label>
            <asp:DropDownList ID="ddlEvent" runat="server" AutoPostBack="true">
                <asp:ListItem Value="0">=Select=</asp:ListItem>
                <asp:ListItem Value="date_abstract">Date abstract</asp:ListItem>
                <asp:ListItem Value="date_full_paper">Date full paper </asp:ListItem>
                <asp:ListItem Value="date_izvestuvajne">Date izvestuvajne</asp:ListItem>
                <asp:ListItem Value="date_camera_redy">Date camera redy</asp:ListItem>
                <asp:ListItem Value="date_konferencija">Date konferencija</asp:ListItem>
            </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="ddlEvent" ValidationGroup="group1" InitialValue="0"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="lblEventDate" runat="server" Text="Izberi data"></asp:Label>
            <asp:TextBox ID="txtEventDate" runat="server"></asp:TextBox>
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/icon.jpg" AlternateText="Calendar" Height="20px" Width="20px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="txtEventDate" ValidationGroup="group1"></asp:RequiredFieldValidator>

            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEventDate" PopupButtonID="ImageButton1" Format="yyyy-MM-dd"></asp:CalendarExtender>

            <br />
            <br />
            <asp:Button ID="btnSave" runat="server" Text="Save Event" OnClick="btnSave_Click" ValidationGroup="group1" />

            <br />
            <br />
            <asp:Label ID="lblException" runat="server" Text=""></asp:Label>
                </div>
              <div class="aside">
                <section>
                    <h1>Info</h1>
                    <ul id="inTouch">
                        <li>Country: <a class="sideLinks" href="http://en.wikipedia.org/wiki/Cambodia">Cambodia</a><br>
                            State: <a class="sideLinks" href="http://en.wikipedia.org/wiki/Siem_Reap">Siem Reap</a><br>
                            Location: 13&deg; 24' 44" N - 103&deg; 52' 0" E<br>
                            Era: <a class="sideLinks" href="http://en.wikipedia.org/wiki/Khmer_Empire">Khmer Empire</a><br>
                            Culture:<a class="sideLinks" href="http://en.wikipedia.org/wiki/Culture_of_Cambodia"> Khmer</a><br>
                        </li>
                    </ul>
                </section>
                <nav>
                    <h1>Надворешни линкови</h1>
                    <ul class="links">
                        <li><a href="http://www.angkorwhat.net" />Official web page</a></li>
                        <li><a href="http://whc.unesco.org/en/list/668">Angkor Wat UNESCO</a></li>
                        <li><a href="http://en.wikipedia.org/wiki/Angkor_Wat">Angkor Wat Wikipedia</a></li>
                        <li><a href="http://video.nationalgeographic.com/video/specials/ancient-mysteries/angkor-wat-temples" />Angkor Wat nationalgeographic</a></li>
                    </ul>
                </nav>
            </div>
        </div>
    </form>
</body>
</html>
