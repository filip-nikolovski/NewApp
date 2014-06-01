<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Conference.aspx.cs" Inherits="Diplomska.Conference" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
 
    <link href="style/style.css" rel="stylesheet" />
    <link href="style/bootstrap.css" rel="stylesheet" />
    <link href="style/justified-nav.css" rel="stylesheet" />
    <script src="scripts/jquery.js" type="text/javascript"></script>

    <script>

        function display(id) {
            var traget = document.getElementById(id);
            traget.style.display = "block";
            //window.location = "#addConference ";
            $('html,body').animate({
                scrollTop: $("#addConference").offset().top
            }, 750);
        }

        function hide(id) {
            var target = document.getElementById(id);
            //window.location = "#gv";

    

            target.style.display = "none";

            document.getElementById('txtPlace').value = '';
            document.getElementById('txtState').value = '';
            document.getElementById('txtFullName').value = '';
            document.getElementById('txtShortName').value = '';
            document.getElementById('txtYear').value = '';
            document.getElementById('txtRedenBr').value = '';
            document.getElementById('txtDateAbstract').value = '';
            document.getElementById('txtDateFullPaper').value = '';
            document.getElementById('txtDateIzvestuvanje').value = '';
            document.getElementById('txtDateCameraRedy').value = '';
            document.getElementById('txtDateConferencija').value = '';
            document.getElementById('txtCenaNaKotizacija').value = '';
            document.getElementById('ddlNaucenTrud').value = '0';
        }

    </script>
    
   

</head>
<body>

    <form id="form1" runat="server">

        <div class="container">
            <div id="acount">
                <a href="Index.aspx" style="float: left">
                    <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/ukim-logo-9.png" /><asp:Image ID="imgLogo1" runat="server" ImageUrl="~/Images/finki-logo-9.png" /></a>

                <asp:Label ID="lblLogedAs" runat="server" Text=""></asp:Label><a href="UserProfile.aspx"><asp:Label ID="lblLogedAs1" runat="server" Text=""></asp:Label></a>

                &nbsp;

        <asp:Button ID="btnLoguot" runat="server" OnClick="Button1_Click" Text="Loguot" Width="70px" />


            </div>

            <div class="masthead">

                <ul class="nav nav-justified">
                    <li><a href="Index.aspx">Научен Труд</a></li>
                    <li class="active"><a href="Conference.aspx">Конференција</a></li>
                    <li><a href="Events.aspx">Настани</a></li>

                </ul>
            </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <div class="main-content">
                        <div id="gv" class="main-content1">
                            <div class="topMain">
                                <h3 class="text-muted">
                                    <asp:Label ID="lblTitleEmpty" runat="server" Text=""></asp:Label></h3>
                            </div>
                            <div id="newWork">
                                <asp:Label ID="lblErr" runat="server" Text="Додади конференција"></asp:Label>


                                <asp:Button ID="btnAdd" runat="server" Text="add" OnClientClick="display('addConference')" OnClick="btnAdd_Click" class="scroll btn btn-default" />


                            </div>




                            <asp:GridView ID="gvConference" runat="server" DataKeyNames="id" AutoGenerateColumns="False" Style="width: 100%" OnRowCreated="gvConference_RowCreated" CellPadding="6" ForeColor="#333333" GridLines="None" OnRowDataBound="OrderGrid_RowDataBound">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex +1+"." %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="full_name" HeaderText="full name" />
                                    <asp:BoundField DataField="place" HeaderText="place" />
                                    <asp:BoundField DataField="state" HeaderText="state" />
                                    <asp:BoundField DataField="year" HeaderText="year" />
                                    <asp:BoundField DataField="cena_na_kotizacija" HeaderText="cena na kotizacija" />

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnDetails" runat="server" Text="details" RowIndex='<%# Eval("id") %>'
                                                OnClick="btnDetails_Click" class="btn btn-default" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnEdit" runat="server" Text="edit" RowIndex='<%# Eval("id") %>'
                                                OnClick="btnEdit_click" CommandName="update-something" OnClientClick="display('addConference')" class="scroll btn btn-default" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="ibtn1" runat="server" Text="delete" RowIndex='<%# Eval("id") %>'
                                                OnClick="button_click" class="btn btn-default" />
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

                        <asp:Panel runat="server" ID="addConference" Style="display: block" class="add">
                            <input type="button" value="" class="button_add" onclick="hide('addConference')" />
                            <h3 class="text-muted">
                                <asp:Label ID="lblTitle" runat="server" Text="Креирај Конференција"></asp:Label></h3>
                            <br />


                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNaucenTrud" runat="server" Text="Научен труд"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlNaucenTrud" runat="server" class="form-control" DataTextField="title" DataValueField="id" AppendDataBoundItems="true" AutoPostBack="true">
                                            <asp:ListItem Value="0">=Select=</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="ddlNaucenTrud" ValidationGroup="group1" InitialValue="0"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPlace" runat="server" Text="Место"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPlace" runat="server" class="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="txtPlace" ValidationGroup="group1"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblState" runat="server" Text="Држава"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtState" runat="server" class="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="txtState" ValidationGroup="group1"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblFullName" runat="server" Text="Цело име"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFullName" runat="server" class="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="txtFullName" ValidationGroup="group1"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblShortName" runat="server" Text="Скратено име"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtShortName" runat="server" class="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="txtShortName" ValidationGroup="group1"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblYear" runat="server" Text="Година"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtYear" runat="server" class="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="txtYear" ValidationGroup="group1"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblRedenBr" runat="server" Text="Реден број"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRedenBr" runat="server" class="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="txtRedenBr" ValidationGroup="group1"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDateAbstract" runat="server" Text="Датум abstract"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDateAbstract" runat="server" class="form-control"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/icon.jpg" AlternateText="Calendar" Height="20px" Width="20px" />
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateAbstract" PopupButtonID="ImageButton1" Format="yyyy-MM-dd" PopupPosition="BottomRight"></asp:CalendarExtender>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDateFullPaper" runat="server" Text="Датум full paper"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtDateFullPaper" runat="server" class="form-control"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/icon.jpg" AlternateText="Calendar" Height="20px" Width="20px" />
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateFullPaper" PopupButtonID="ImageButton2" Format="yyyy-MM-dd" PopupPosition="BottomRight"></asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDateIzvestuvanje" runat="server" Text="Датум на известување"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtDateIzvestuvanje" runat="server" class="form-control"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/icon.jpg" AlternateText="Calendar" Height="20px" Width="20px" />
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDateIzvestuvanje" PopupButtonID="ImageButton3" Format="yyyy-MM-dd" PopupPosition="BottomRight"></asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDateCameraRedy" runat="server" Text="Датум camera redy"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtDateCameraRedy" runat="server" class="form-control"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/icon.jpg" AlternateText="Calendar" Height="20px" Width="20px" />
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtDateCameraRedy" PopupButtonID="ImageButton4" Format="yyyy-MM-dd" PopupPosition="TopRight"></asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDateConferencija" runat="server" Text="Датум на конференција"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtDateConferencija" runat="server" class="form-control"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/icon.jpg" AlternateText="Calendar" Height="20px" Width="20px" />
                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtDateConferencija" PopupButtonID="ImageButton5" Format="yyyy-MM-dd" PopupPosition="TopRight"></asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCenaNaKotizacija" runat="server" Text="Цена на котизација"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtCenaNaKotizacija" runat="server" class="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="txtCenaNaKotizacija" ValidationGroup="group1"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>

                            </table>
                            <div class="btnDown" style="margin-right: 34%">
                                <input type="button" onclick="hide('addConference')" value="cancel" class="scroll btn btn-primary" />

                                <asp:Button ID="btnSave" runat="server" Text="save" OnClick="btnSave_Click" ValidationGroup="group1" class="btn btn-primary" Visible="true" />
                                <asp:Button ID="btnUpdate" runat="server" Text="update" OnClick="btnUpdate_Click" ValidationGroup="group1" class="btn btn-primary" Visible="false" />


                            </div>
                            <asp:Label ID="lblException" runat="server" Text=""></asp:Label>


                        </asp:Panel>

                        <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
                        <asp:ModalPopupExtender ID="ModalPopupExtender2" BehaviorID="mpe1" runat="server"
                            PopupControlID="pnlPopupInfo" TargetControlID="LinkButton1" BackgroundCssClass="modalBackground">
                        </asp:ModalPopupExtender>

                        <asp:Panel ID="pnlPopupInfo" runat="server" CssClass="modalPopup1" Style="display: none">
                            <div class="header">

                                <asp:Label ID="lblPopUpSWDetails" runat="server" Text="Label"></asp:Label>
                                <input type="button" value="" class="button_add" onclick="HideModalPopup1()" />
                            </div>
                            <div class="body">

                                <b>
                                    <asp:Label ID="Label4" runat="server" Text="Цело име: "></asp:Label></b>
                                <asp:Label ID="lblPopTitle" runat="server" Text=""></asp:Label><br />

                                <b>
                                    <asp:Label ID="Label6" runat="server" Text="Кратко име: "></asp:Label></b>
                                <asp:Label ID="lblPopAutors" runat="server" Text=""></asp:Label><br />

                                <b>
                                    <asp:Label ID="Label7" runat="server" Text="Држава: "></asp:Label></b>
                                <asp:Label ID="lblPopCorrAutors" runat="server" Text=""></asp:Label><br />

                                <b>
                                    <asp:Label ID="Label8" runat="server" Text="Место: "></asp:Label></b>
                                <asp:Label ID="lblPopPrice" runat="server" Text=""></asp:Label><br />

                                <b>
                                    <asp:Label ID="Label9" runat="server" Text="Година: "></asp:Label></b>
                                <asp:Label ID="lblPopDate" runat="server" Text=""></asp:Label><br />

                                <b>
                                    <asp:Label ID="Label10" runat="server" Text="Реден број: "></asp:Label></b>
                                <asp:Label ID="lblPopAcc" runat="server" Text=""></asp:Label><br />

                                <b>
                                    <asp:Label ID="Label11" runat="server" Text="Датум abstract: "></asp:Label></b>
                                <asp:Label ID="lblPopDescription" runat="server" Text=""></asp:Label><br />

                                <b>
                                    <asp:Label ID="Label12" runat="server" Text="Датум full paper: "></asp:Label></b>
                                <asp:Label ID="lblPopDFP" runat="server" Text=""></asp:Label><br />

                                <b>
                                    <asp:Label ID="Label13" runat="server" Text="Датум известување: "></asp:Label></b>
                                <asp:Label ID="lblPopDI" runat="server" Text=""></asp:Label><br />

                                <b>
                                    <asp:Label ID="Label14" runat="server" Text="Датум camera redy: "></asp:Label></b>
                                <asp:Label ID="lblPopDCR" runat="server" Text=""></asp:Label><br />

                                <b>
                                    <asp:Label ID="Label15" runat="server" Text="Датум конференција: "></asp:Label></b>
                                <asp:Label ID="lblPopDK" runat="server" Text=""></asp:Label><br />

                                <b>
                                    <asp:Label ID="Label16" runat="server" Text="Цена на котизација: "></asp:Label></b>
                                <asp:Label ID="lblPopCenaK" runat="server" Text=""></asp:Label>

                                <div class="btnDown" style="margin-right: 2%; margin-top: 4%">
                                    <input type="button" onclick=" HideModalPopup1(); return hide('addConference')" value="cancel" class="btn btn-primary" />

                                </div>


                                <asp:Label ID="Label3" runat="server" Text="" Visible="true"></asp:Label>

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
                        <asp:Panel runat="server" CssClass="modalPopupCalendar" ID="programmaticPopup" Style="display: none">
                            <div class="calTitile">
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
                <div runat="server" id="pnl" class="asideReminder">

                    <asp:GridView ID="gvReminder" runat="server" DataKeyNames="id" AutoGenerateColumns="False" Style="width: 100%" OnRowCreated="gvReminder_RowCreated" OnSelectedIndexChanged="gvReminder_SelectedIndexChanged" ShowHeader="False" CellPadding="7" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="hol" HeaderText="Event" />
                            <asp:BoundField DataField="HolidayDate" />
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
        <script>
            function popupModal(value, v) {

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
                    document.getElementById('<%=addConference.ClientID%>').style.display = "none";
                });


                function ShowModalPopup1() {
                    $find("mpe1").show();
                    return false;
                }

                function HideModalPopup1() {
                    $find("mpe1").hide();
                    return false;
                }

        </script>
    </form>
</body>
</html>
