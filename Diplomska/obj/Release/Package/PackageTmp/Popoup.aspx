<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Popoup.aspx.cs" Inherits="Diplomska.Popoup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link href="style/style.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">
        function passValues() {
            //   window.opener.document.getElementById('lblAutors').innerText = "haha";
            //window.opener.document.getElementById('lblAutors').innerText = document.getElementById('lblHidd').value;
            // var txt =
            window.opener.document.getElementById('lblAutors').innerText = document.getElementById("lblHidd").textContent;
            window.opener.document.getElementById('lblEmail').innerText = document.getElementById("lblEmail").textContent;
            window.opener.document.getElementById('TextBox1').value = document.getElementById("lblEmail").textContent;
            window.close();
        }
    </script>
  
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:CheckBoxList ID="cblCorispondingAutors" runat="server" DataTextField="fullName" DataValueField="id">
            </asp:CheckBoxList>
            <br />
            <br />
            <asp:LinkButton ID="lkCloseWindow" runat="server" Text="Close window" OnClientClick="window.close();"></asp:LinkButton>
            &nbsp;<asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
            <br />
            <asp:Label ID="lblHidd" runat="server" Text="" Visible="true"></asp:Label>

            <br />
            <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>

        </div>
    </form>
</body>
</html>
