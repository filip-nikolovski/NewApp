<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditConf.aspx.cs" Inherits="Diplomska.EditConf" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function openWindow() {
            window.open('Popoup.aspx', null, 'height=200,width=400,status=no,toolbar=no,menubar=no,location=no, scrollbars=no, titlebar=no,directories=no,location=no,screenX=400,screenY=400');
            return false;
        }


    </script>
  
    <script>


        function display(id) {

            var traget = document.getElementById(id);

            traget.style.display = "block";
            //window.location = "#addConference ";
            $('html,body').animate({
                scrollTop: $("#addWork").offset().top
            }, 1000);

            return false;
        }

        function hide(id) {
            var target = document.getElementById(id);
            //window.location = "#gv";
            target.style.display = "none";
            $('html,body').animate({
                scrollTop: $("#newWork").offset().top
            }, 2000);
            return false;
        }
    </script>

     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".scroll").click(function (event) {
                $('html,body').animate({ scrollTop: $(this.hash).offset().top }, 500);
            });
        });
    </script>

    <script>
        function display1(id) {
            var traget = document.getElementById(id);

            traget.style.display = "block";

        }

        function hide1(id) {
            var target = document.getElementById(id);
            target.style.display = "none";
        }

        function hide11(id) {
            var target = document.getElementById(id);
            target.style.display = "none";

            $('html,body').animate({
                scrollTop: $("#addWork").offset().top
            }, 1000);
        }


    </script>

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

        <asp:Button ID="btnLoguot" runat="server" OnClick="Button1_Click" Text="Loguot" Width="70px" />


            </div>

            <div class="masthead">

                <ul class="nav nav-justified">
                    <li ><a href="Index.aspx">Index</a></li>
                    <li class="active"><a href="Conference.aspx">Create Conference</a></li>
                    <li><a href="Events.aspx">Events</a></li>

                </ul>
            </div>

            <div class="main-content">
               

            </div>
            <div class="aside">
           <section>
                    <h1>Info</h1>
                    <div id="inTouch">
                        
                            

                            <asp:Calendar ID="Calendar1" runat="server" OnDayRender="Calendar1_DayRender" OnVisibleMonthChanged="Calendar1_VisibleMonthChanged" OnSelectionChanged="Calendar1_SelectionChanged" Style="width: 100%">
                                <TodayDayStyle BackColor="#D1DDF1" ForeColor="White" />
                                <OtherMonthDayStyle ForeColor="#CC9966" />
                                <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                 <TitleStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="9pt" 
                ForeColor="White" />
                            </asp:Calendar>

                            <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />
                            <ajaxToolkit:ModalPopupExtender runat="server" ID="programmaticModalPopup" BehaviorID="programmaticModalPopupBehavior"
                                TargetControlID="hiddenTargetControlForModalPopup" PopupControlID="programmaticPopup"
                                BackgroundCssClass="modalBackground" DropShadow="True" RepositionMode="RepositionOnWindowScroll">
                            </ajaxToolkit:ModalPopupExtender>
                            <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopup" Style="background-color: #FFFFCC; display: none; height: 125px; width: 225px; padding: 10px">

                                <a href="ViewEvent.aspx"><asp:Label ID="Label4" runat="server" Text="Label"></asp:Label></a><br />
                                <input type="button" onclick="popupModal1('hah')" value="cancel " />
                            </asp:Panel>
                      </div>
                    
                </section>
                <nav>
                    <h2>Настани</h2>
                   
                        <asp:GridView ID="gvReminder" runat="server" DataKeyNames="id" AutoGenerateColumns="False" Style="width: 100%" OnRowCreated="gvReminder_RowCreated" OnSelectedIndexChanged="gvReminder_SelectedIndexChanged" ShowHeader="False" CellPadding="7" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="Holiday" HeaderText="Event" />
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
                  
                </nav>
            </div>

           
               
            
            
        </div>

             
    </form>
</body>
</html>
