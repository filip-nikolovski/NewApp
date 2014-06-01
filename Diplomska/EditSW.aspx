<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditSW.aspx.cs" Inherits="Diplomska.EditSW" %>

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
                    <li class="active"><a href="Index.aspx">Index</a></li>
                    <li><a href="Conference.aspx">Create Conference</a></li>
                    <li><a href="Events.aspx">Events</a></li>

                </ul>
            </div>

            <div class="main-content">
               
                 <h3 class="text-muted" >
                    <asp:Label ID="lblTitle1" runat="server" Text="Креирај Труд"></asp:Label></h3>
                    <ContentTemplate>  
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>

                    <br />
                    <table style="width:70%">
                        <tr>
                            <td>
                                <asp:Label ID="lblTitle" runat="server" Text="Title: " ></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTitle" runat="server" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="txtTitle" ValidationGroup="group1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDescription" runat="server" Text="Description: "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescription" runat="server" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="txtDescription" ValidationGroup="group1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblPrice" runat="server" Text="Price of cohesion: "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPrice" runat="server" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="txtPrice" ValidationGroup="group1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDate" runat="server" Text="Date: "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDate" runat="server" class="form-control"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/icon.jpg" AlternateText="Calendar" Height="20px" Width="20px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="txtDate" ValidationGroup="group1"></asp:RequiredFieldValidator>

                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate" PopupButtonID="ImageButton1" Format="yyyy-MM-dd" PopupPosition="BottomRight"></asp:CalendarExtender>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAccesepted" runat="server" Text="Is Accesepted: "></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rblAcc" runat="server">
                                    <asp:ListItem Value="0" Text="no"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="yes"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                
                                <input type="button" onclick="display1('corrA')" value=" autors " class="btn btn-default"/>    
                            </td>
                            <td>


                                <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine"  Columns="10" Rows="5" class="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                </td>
                            <td>
                               <asp:FileUpload ID="FileUpload1" runat="server" class="form-control"/>

                            </td>
                           
                        </tr>
                         <tr>
                             <td>

                             </td>
                             <td style="float:right; margin-right:10%">
                                 <input type="button" onclick="hide('addWork')" value="Cancel" class="scroll btn btn-primary"/>
                                
                                <asp:Button ID="Button2" runat="server"
                                    Text="Save" OnClick="Button2_Click" ValidationGroup="group1" class="btn btn-primary"/>
                            
                             </td>
                              </tr>

                    </table>


                    <br />
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                    <br />

                         <asp:Panel id="corrA" runat="server" class="popupPanel">
                     
                            <input type="button" value="" class="button_add" onclick="hide1('corrA')" /> 
            <asp:CheckBoxList ID="cblCorispondingAutors" runat="server" DataTextField="fullName" DataValueField="id">
            </asp:CheckBoxList>
            <br />
            <br />
            <input type="button" id="btnClose" onclick="hide1('corrA')" value=" Cancel " class="btn btn-primary"/>
            &nbsp;<asp:Button ID="Button11" runat="server" Text="Button" OnClick="Button11_Click"  OnClientClick="return hide11('corrA')" class="btn btn-primary"/>
            <br />
            <asp:Label ID="lblHidd" runat="server" Text="" Visible="true"></asp:Label>

            <br />
            <asp:Label ID="Label3" runat="server" Text=""></asp:Label>

        </asp:Panel>

                        </ContentTemplate>
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

                <script src="http://code.jquery.com/jquery-2.0.0.js"> </script>
            <script>
                function popupModal(value) {


                    $get('<%=Label1.ClientID %>').innerText = value;
                    var pop = $find('programmaticModalPopupBehavior');
                    pop.set_X(event.clientX);
                    pop.set_Y(event.clientY);
                    pop.show();

                }



                function popupModal1(value) {

                    $get('<%=Label1.ClientID %>').innerText = value;

                $find('programmaticModalPopupBehavior').hide();

            }



        </script>
    </form>
</body>
</html>
