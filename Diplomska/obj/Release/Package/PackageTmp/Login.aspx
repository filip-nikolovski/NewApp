<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Diplomska.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="shortcut icon" href="../Images/favicon.ico" />
    <link href="style/loginStile.css" rel="stylesheet" />
    <link href="style/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
    <form class="form-signin" runat="server">
    
        <h2 class="form-signin-heading">Please sign in</h2>
        <asp:TextBox ID="TextBox1" runat="server" type="text" class="form-control" style="width:100%" placeholder="Email address" autofocus></asp:TextBox>
        <asp:TextBox ID="TextBox2" runat="server" type="password" class="form-control" style="width:100%" placeholder="Password"></asp:TextBox>
       
        <asp:CheckBox ID="chkRemember" runat="server" /> Задржи ме најавен
       
        <asp:Button ID="Button1" runat="server" onclick="Button2_Click" Text="Sign in" 
             class="btn btn-lg btn-primary btn-block" />
     


         <a href="Register.aspx" style="float:right">Register</a><b><asp:Label runat="server" ID="lbl" Text="|" style="float:right;margin-left:2%;margin-right:2%;"></asp:Label></b>
       <a href="RessetPassword.aspx" style="float:right">forgot password</a>  <br />
    <asp:Label ID="Label3" runat="server" Text="" Font-Size="12px" ForeColor="Red" Visible="false"></asp:Label>
     
</form>
     </div>
    <script src="scripts/jquery.js"></script>
    <script src="scripts/bootstrap.min.js"></script>

    </body>
</html>
