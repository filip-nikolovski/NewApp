<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pr.aspx.cs" Inherits="Diplomska.pr" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            width: 300px;
            border: 3px solid #0DA9D0;
            padding: 0;
        }
        .modalPopup .header
        {
            background-color: #2FBDF1;
            height: 30px;
            color: White;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
        }
        .modalPopup .body
        {
            min-height: 50px;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
	
    <asp:Button ID="btnShow" runat="server" Text="Show Modal Popup" OnClientClick="return ShowModalPopup()" />
	
    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
        PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
	
    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
        <div class="header">
            Modal Popup
        </div>
        <div class="body">
            This is a Modal Popup.
            <br />
            <asp:Button ID="btnHide" runat="server" Text="Hide Modal Popup" OnClientClick="return HideModalPopup()" />
        </div>
    </asp:Panel>
	
	
    <script type="text/javascript">
        function ShowModalPopup() {
            $find("mpe").show();
            return false;
        }
        function HideModalPopup() {
            $find("mpe").hide();
            return false;
        }
    </script>
    </form>
</body>
</html>
