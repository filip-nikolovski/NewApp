<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Diplomska.Index" enableEventValidation="false"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Научен труд</title>

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
                scrollTop: $("#addWork").offset().top
            }, 750);

            document.getElementById('txtTitle').value = '';
            document.getElementById('txtPrice').value = '';
            document.getElementById('txtDate').value = '';
            document.getElementById('txtDescription').value = '';
            document.getElementById('TextBox1').value = '';


        }
        
        function hide(id) {
            var target = document.getElementById(id);
            //window.location = "#gv";
            
         
            target.style.display = "none";

        }


    </script>
    
</head>
<body>
    <form id="form1" runat="server" method="post" enctype="multipart/form-data">
        <div class="container">
            <div id="acount">
                <a href="http://www.ukim.edu.mk/" style="float: left">
                    <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/ukim-logo-9.png" /></a><a href="http://www.finki.ukim.mk/mk/home" style="float: left"> <asp:Image ID="imgLogo1" runat="server" ImageUrl="~/Images/finki-logo-9.png" /></a><a href="http://liit.finki.ukim.mk/" style="float: left"><asp:Image ID="ImageliitLogo" runat="server" ImageUrl="~/Images/liit1 logo.png"  Style="height:100%;width:70%;float:left;margin-bottom:-1px"/></a>
                <asp:Label ID="lblLogedAs" runat="server" Text=""></asp:Label><a href="UserProfile.aspx"><asp:Label ID="lblLogedAs1" runat="server" Text=""></asp:Label></a>

                &nbsp;

        <asp:Button ID="btnLoguot" runat="server" OnClick="Button1_Click" Text="Loguot" Width="70px" />


            </div>

            <div class="masthead">

                <ul class="nav nav-justified">
                    <li class="active"><a href="Index.aspx">Научен Труд</a></li>
                    <li><a href="Conference.aspx">Конференција</a></li>
                    <li><a href="Events.aspx">Настани</a></li>

                </ul>
            </div>
             <asp:ScriptManager ID="ScriptManager2" runat="server">
                        </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="main-content">
                        <div class="main-content1">
                             <div class="topMain">
                                <h3 class="text-muted">
                                    <asp:Label ID="lblTitleEmpty" runat="server" Text=""></asp:Label></h3>
                            </div>
                            <div id="newWork">
                                <asp:Label ID="lblNew" runat="server" Text="Додади научен труд"></asp:Label> &nbsp;
                                <asp:Button ID="btnAdd" runat="server" Text="add" OnClientClick="display('addWork')" OnClick="btnAdd_Click" class="scroll btn btn-default"  />
                            </div>
                            <asp:GridView ID="gvList" runat="server" DataKeyNames="id" AutoGenerateColumns="False"  OnRowCreated="gvList_RowCreated" Style="width: 1000%" CellPadding="10" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex +1+"." %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="title" HeaderText="Title" />
                                    <asp:BoundField DataField="autores" HeaderText="autors" />
                                    
                                    <asp:BoundField DataField="date" HeaderText="Created" />
                                    
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnDetails" runat="server" Text="details" RowIndex='<%# Eval("id") %>'
                                              OnClick="btnDetails_click"   class="btn btn-default" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnVersions" runat="server" Text="versions" RowIndex='<%# Eval("id") %>'
                                                OnClick="btnVersions_clisk" class="btn btn-default" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                      <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnEdit" runat="server" Text="edit" RowIndex='<%# Eval("id") %>'
                                                OnClick="btnEdit_click" OnClientClick="display('addWork')" class="scroll btn btn-default" />
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

                            <asp:Label ID="lblEx" runat="server" Text=""></asp:Label>
                        </div>

                        <asp:Panel runat="server" ID="addWork" class="add" Style="display: block">
                            <input type="button" value="" class="button_add" onclick="hide('addWork')" />
                            <h3 class="text-muted">
                                <asp:Label ID="lblTitle1" runat="server" Text="Креирај Труд"></asp:Label></h3>


                            <br />
                            <table >
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTitle" runat="server" Text="Наслов: "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTitle" runat="server" class="form-control" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="txtTitle" ValidationGroup="group1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDescription" runat="server" Text="Опис: "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDescription" runat="server" class="form-control" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="txtDescription" ValidationGroup="group1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPrice" runat="server" Text="Цена на котизација: "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPrice" runat="server" class="form-control" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Задолжително поле" Display="Dynamic" ControlToValidate="txtPrice" ValidationGroup="group1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
      
                                <tr>
                                    <td>
                                        <asp:Label ID="lblAccesepted" runat="server" Text="Прифатен: "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rblAcc" runat="server">
                                            <asp:ListItem Value="0" Text="Не"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Да"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>

                                       
                                         <asp:Button ID="btnShow" runat="server" Text="Избери Автори" OnClientClick="return ShowModalPopup(); hide('addWork')" class="btn btn-default" />
                                    </td>
                                    <td>


                                        <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Columns="10" Rows="5" class="form-control" disabled ></asp:TextBox>
                                    </td>
                                </tr>
                         
                 

                            </table>

                            <div class="btnDown">
                                <input type="button" onclick="hide('addWork')" value="Cancel" class="scroll btn btn-primary" />

                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" Visible="false" ValidationGroup="group1" class="btn btn-primary" />

                                        <asp:Button ID="Button2" runat="server"
                                            Text="Save" OnClick="Button2_Click" ValidationGroup="group1" class="btn btn-primary" />
                            </div>
                            <br />
                            <asp:Label ID="Label1" runat="server"></asp:Label>
                            <br />
                            <br />
                            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                            <br />

                        </asp:Panel>

                       
	
    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
        PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
	
    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none"  >
        <div class="header">
           
            Избери кориспондинг автори <input type="button" value="" class="button_add" onclick="HideModalPopup()" />
        </div>
        <div class="body">
           <div runat="server"  id="pnl"  style="overflow-y:scroll;height:250px;">

               <asp:CheckBoxList ID="cblCorispondingAutors" runat="server" DataTextField="fullName" DataValueField="id">
                            </asp:CheckBoxList>
                            
                            </div>
                           
                            
                            
            <div class="btnDown" style="margin-right:29%">
                 <input type="button" id="Button1" onclick="HideModalPopup()" value=" Cancel " class="btn btn-primary" />
                            <asp:Button ID="Button3" runat="server" Text="Избери" OnClick="Button11_Click" OnClientClick="HideModalPopup()"  class="btn btn-primary" />
            </div>
            <asp:Label ID="Label5" runat="server" Text="" Visible="true"></asp:Label>
        </div>
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

                               <b> <asp:Label ID="Label4" runat="server" Text="Наслов: "></asp:Label></b>
                                <asp:Label ID="lblPopTitle" runat="server" Text=""></asp:Label><br />

                               <b> <asp:Label ID="Label6" runat="server" Text="Креирал: "></asp:Label></b>
                                <asp:Label ID="lblPopAutors" runat="server" Text=""></asp:Label><br />

                               <b> <asp:Label ID="Label7" runat="server" Text="Кореспондинг автори: "></asp:Label></b>
                                <asp:Label ID="lblPopCorrAutors" runat="server" Text=""></asp:Label><br />

                               <b> <asp:Label ID="Label8" runat="server" Text="Цена: "></asp:Label></b>
                                <asp:Label ID="lblPopPrice" runat="server" Text=""></asp:Label><br />

                               <b> <asp:Label ID="Label9" runat="server" Text="Датум: "></asp:Label></b>
                                <asp:Label ID="lblPopDate" runat="server" Text=""></asp:Label><br />

                              <b>  <asp:Label ID="Label10" runat="server" Text="Прифатен: "></asp:Label></b>
                                <asp:Label ID="lblPopAcc" runat="server" Text=""></asp:Label><br />

                              <b>  <asp:Label ID="Label11" runat="server" Text="Опис: "></asp:Label></b>
                                <asp:Label ID="lblPopDescription" runat="server" Text=""></asp:Label><br />

                                <div class="btnDown" style="margin-right:2%;margin-top:20%">

                                    <input type="button" onclick=" HideModalPopup1(); return hide('addWork')" value="cancel" class="btn btn-primary" />

                                </div>
                                
                                <br />
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
           <div runat="server"  id="Div1" class="asideReminder">

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

        
        <script>
            function popupModal(value, v) {
                $get('<%=lblDTitle.ClientID %>').innerText = v;
                $get('<%=lblDBody.ClientID %>').innerText = value;
                var pop = $find('programmaticModalPopupBehavior');
                pop.set_Y(event.clientY);
                pop.show();
            }

            $('#programmaticPopup').on('mouseleave', function () {
                setTimeout(function () {
                    $find("programmaticModalPopupBehavior").hide();
                }, 500);
            });


            $(document).ready(function () {
                document.getElementById('<%=addWork.ClientID%>').style.display = "none";
            });

         
                function ShowModalPopup() {
                    $find("mpe").show();
                    return false;
                }
                function HideModalPopup() {
                    $find("mpe").hide();

                    $('html,body').animate({
                        scrollTop: $("#addWorkf").offset().top
                    }, 1000);
                    return false;
                }


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
