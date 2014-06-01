<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewEvent.aspx.cs" Inherits="Diplomska.ViewEvent" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <link href="style/style.css" rel="stylesheet" />
    <link href="style/bootstrap.css" rel="stylesheet" />
    <link href="style/justified-nav.css" rel="stylesheet" />
     <script type="text/javascript" src="scripts/jquery.js"></script>


    <script>

        function display(id) {

            var traget = document.getElementById(id);

            traget.style.display = "block";
            //window.location = "#addConference ";
            $('html,body').animate({
                scrollTop: $("#addWork").offset().top
            }, 1000);
        }

        function hide(id) {
            var target = document.getElementById(id);
            //window.location = "#gv";
            target.style.display = "none";

       
           
        }

    </script>
   

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
    <div class="container">





        
        <div id="acount">
             <a href="Index.aspx" style="float:left">
                    <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/ukim-logo-9.png" /><asp:Image ID="imgLogo1" runat="server" ImageUrl="~/Images/finki-logo-9.png" /></a>
               
            <asp:Label ID="lblLogedAs" runat="server" Text=""></asp:Label><a href="UserProfile.aspx"><asp:Label ID="lblLogedAs1" runat="server" Text=""></asp:Label></a>
            &nbsp;
            <asp:Button ID="btnLoguot" runat="server" OnClick="Button1_Click" Text="Loguot" Width="70px" />
        </div>

            <div class="masthead">
                <ul class="nav nav-justified">
                    <li ><a href="Index.aspx">Научен Труд</a></li>
                    <li><a href="Conference.aspx">Конференција</a></li>
                    <li class="active"><a href="Events.aspx">Настани</a></li>
                </ul>
                <a href="Events.aspx" style="margin-bottom:-15px;">Настани> </a><asp:Label ID="lblNav" runat="server" Text=""></asp:Label>
            </div>
          
      
              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            
        <div class="main-content"  >     
                   <div class="main-content1" style="padding-left:4%; padding-top:4%;padding-bottom:4%">
            <h3 class="text-muted" style="padding-bottom:1.5%">
                <asp:Label ID="lblSWTitle" runat="server" Text=""></asp:Label></h3>
           
                <h4>
                    <asp:Label ID="lblConfTitle" runat="server" Text=""></asp:Label>
                </h4>
            <b><asp:Label ID="lblEvent" runat="server" Text=""></asp:Label></b>
            <br />
            <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lblPlace" runat="server" Text=""></asp:Label>
            <div id="butt" style="float:right;padding-right:10%;margin-bottom:5%">
                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" class="btn btn-primary" />
                <asp:Button ID="btnModify" runat="server" Text="modify" OnClick="btnModify_Click" OnClientClick="display('addWork')" class="scroll btn btn-primary" />

               
            </div>
            <asp:Label ID="lblErr" runat="server" Text="" Visible="true"></asp:Label>

        </div>

            

             <asp:Panel  runat="server" ID="addWork" class="add" Style="display: block; padding-left:4%;">
                <input type="button" value="" class="button_add" onclick="hide('addWork')"  />
                 <h3 class="text-muted" >
                    <asp:Label ID="lblTitle1" runat="server" Text="Измени Настан"></asp:Label></h3>

                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Конференција"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSW" runat="server" DataTextField="fname" DataValueField="id" AppendDataBoundItems="true" AutoPostBack="false" class="form-control" >
                                    <asp:ListItem Value="0">=Select=</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="ddlSW" ValidationGroup="group1" InitialValue="0"></asp:RequiredFieldValidator>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                 <asp:Label ID="lblHoliday" runat="server" Text="Име на настан"></asp:Label>
           
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlEvent" runat="server" AutoPostBack="false" class="form-control" >
                                    <asp:ListItem Value="0">=Select=</asp:ListItem>
                                    <asp:ListItem Value="date_abstract" Text="Date abstract"></asp:ListItem>
                                    <asp:ListItem Value="date_full_paper" Text="Date full paper"></asp:ListItem>
                                    <asp:ListItem Value="date_izvestuvajne" Text="Date izvestuvajne"></asp:ListItem>
                                    <asp:ListItem Value="date_camera_redy" Text="Date camera redy"></asp:ListItem>
                                    <asp:ListItem Value="date_konferencija" Text="Date konferencija"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="ddlEvent" ValidationGroup="group1" InitialValue="0"></asp:RequiredFieldValidator>


                            </td>
                        </tr>
                         <tr>
                            <td>
                                <asp:Label ID="lblEventDate" runat="server" Text="Избери дата"></asp:Label>
                
                            </td>
                            <td>
                                <asp:TextBox ID="txtEventDate" runat="server" class="form-control" ></asp:TextBox>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/icon.jpg" AlternateText="Calendar" Height="20px" Width="20px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="txtEventDate" ValidationGroup="group1"></asp:RequiredFieldValidator>
            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEventDate" PopupButtonID="ImageButton1" Format="yyyy-MM-dd" PopupPosition="TopRight"></asp:CalendarExtender>

                            </td>
                        </tr>
                    
                    </table>
                 <div class="btnDown" style="margin-right:40%">
                                <input type="button" onclick="hide('addWork')" value=" cancel " class="scroll btn btn-primary" />

                     <asp:Button ID="btnSave" runat="server" Text="Update Event" OnClick="btnSave_Click" OnClientClick="display('addWork')" ValidationGroup="group1"  class="btn btn-primary"  />
                               
                                
                 </div>

             
                
           
            </asp:Panel>

                  
        

         </div>
                             </ContentTemplate>
                  <Triggers>
                      
                      
                    
                  </Triggers>
                    </asp:UpdatePanel>
            <div class="aside">

               <section>
                    
                    <div id="inTouch">
                        
                            
                        <div class="asideCalendar">
                            <asp:Calendar ID="Calendar1" runat="server" OnDayRender="Calendar1_DayRender" OnVisibleMonthChanged="Calendar1_VisibleMonthChanged" OnSelectionChanged="Calendar1_SelectionChanged" Style="width: 100%">
                                <TodayDayStyle BackColor="#D1DDF1" ForeColor="White" />
                                <OtherMonthDayStyle ForeColor="#CC9966" />
                                <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                 <TitleStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="9pt" 
                ForeColor="White" />
                            </asp:Calendar>
                        </div>
                            <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />
                            <ajaxToolkit:ModalPopupExtender runat="server" ID="programmaticModalPopup" BehaviorID="programmaticModalPopupBehavior"
                                TargetControlID="hiddenTargetControlForModalPopup" PopupControlID="programmaticPopup"
                               DropShadow="True" RepositionMode="RepositionOnWindowScroll">
                            </ajaxToolkit:ModalPopupExtender>
                             <asp:Panel runat="server" CssClass="modalPopupCalendar" ID="programmaticPopup" style="display:none">
                            <div class="calTitile" >
                                <asp:Label ID="lblDTitle" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="calBody">
                                <a href="ViewEvent.aspx">
                                    <asp:Label ID="lblDBody" runat="server" Text="Label"></asp:Label></a><br />
                            </div>


                        </asp:Panel>
                      </div>
                    
                </section>
                 <h4>Настани кои следуваат</h4>
           <div runat="server"  id="pnl" class="asideReminder">
                        <asp:GridView ID="gvReminder" runat="server" DataKeyNames="id" AutoGenerateColumns="False" Style="width: 100%" OnRowCreated="gvReminder_RowCreated" OnSelectedIndexChanged="gvReminder_SelectedIndexChanged" ShowHeader="False" CellPadding="7" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                               <asp:BoundField DataField="hol" HeaderText="Event" />
                                <asp:BoundField DataField="HolidayDate"  />
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
               

            </div>

            <script type="text/javascript">
               
                function popupModal(value,v) {

                    $get('<%=lblDTitle.ClientID %>').innerText = v;
                $get('<%=lblDBody.ClientID %>').innerText = value;
                var pop = $find('programmaticModalPopupBehavior');
             
                pop.set_Y(event.clientY);
                pop.show("slow");

            }


            $('#programmaticPopup').on('mouseleave', function () {
                 
                setTimeout(function () {
                    $find("programmaticModalPopupBehavior").hide();
                }, 500);                  
            });


                $(document).ready(function () {

                    //var tar = document.getElementById('newWork');
                    // tar.style.display = "none";

                    document.getElementById('<%=addWork.ClientID%>').style.display = "none";

                    });

</script>

    </form>
</body>
</html>
