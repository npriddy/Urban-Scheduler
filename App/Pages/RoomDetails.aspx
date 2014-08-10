<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true"
    CodeBehind="RoomDetails.aspx.cs" Inherits="UrbanSchedulerProject.App.Pages.RoomDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cMain" runat="server">
    <style>
        ul
        {
            list-style: none;
        }
        ul li
        {
            padding-bottom: 5px;
        }
    </style>
    <ul>
        <li>
            <div class="DivStyled">
                <telerik:RadButton ID="_btnBack" runat="server" Text="Back" OnClick="_btnBack_Click"
                    Width="150px">
                </telerik:RadButton>
                <telerik:RadButton ID="_btnViewLocation" runat='server' Text="View Location" Style="margin-bottom: 5px;"
                    Width="150px" OnClick="_btnViewLocation_Click">
                </telerik:RadButton>
                <telerik:RadButton ID="_btnReserve" runat="server" Text="Reserve Room" OnClick="_btnReserve_Click"
                    Width="150px">
                </telerik:RadButton>
            </div>
        </li>
        <li>
            <h1>
                Room Details</h1>
        </li>
        <li>
            <h3>
                <strong>1. Address Information</strong>
            </h3>
        </li>
        <li>
            <div id="stylized" style="float: left; width: 950px !important;" class="userLoginForm">
                <div class="spacer">
                </div>
                <!-- Address Information -->
                <div style="float: left; width: 400px;">
                    <fieldset>
                        <legend>Room Details</legend>
                        <ul>
                            <li>
                                <label for="_txtRoomNumber">
                                    Room Number
                                </label>
                                <telerik:RadTextBox ID="_txtRoomNumber" CssClass="inputStyle" runat="server" Width="205px"
                                    ReadOnly="true">
                                </telerik:RadTextBox>
                            </li>
                            <li>
                                <label for="_txtRoomTitle">
                                    Title
                                </label>
                                <telerik:RadTextBox ID="_txtRoomTitle" CssClass="inputStyle" runat="server" Width="205px"
                                    ReadOnly="true">
                                </telerik:RadTextBox>
                            </li>
                            <li>
                                <label for="_txtMaxOccupancy">
                                    Max Occupancy
                                </label>
                                <telerik:RadNumericTextBox ID="_txtMaxOccupancy" runat="server" Type="Number" Width="205px"
                                    ReadOnly="true" CssClass="inputStyle">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </li>
                            <li>
                                <label for="_txtRoomType">
                                    Type</label>
                                <telerik:RadTextBox ID="_txtRoomType" runat="server" CssClass="inputStyle" ReadOnly="true"
                                    Width="205px">
                                </telerik:RadTextBox>
                            </li>
                            <li>
                                <label for="">
                                    Rating</label>
                                <telerik:RadRating ID="_rRating" runat="server" ItemCount="5" SelectionMode="Continuous"
                                    OnClientRated="OnClientRated" OnRate="RadRating1_Rate" Precision="Exact" Orientation="Horizontal" />
                                <telerik:RadToolTip ID="RadToolTip1" runat="server" ShowEvent="FromCode" HideEvent="FromCode"
                                    TargetControlID="_rRating" RelativeTo="Element" Skin="Default" Position="BottomLeft"
                                    OffsetX="-15" Animation="Slide">
                                    <br />
                                    <asp:Label ID="lblInstructions" runat="server" Text="Your feedback:"></asp:Label><br />
                                    <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Rows="5" Columns="30"></asp:TextBox><br />
                                    <asp:Button ID="btnPostComment" UseSubmitBehavior="false" runat="server" Style="margin-top: 5px;
                                        color: #3e570a" Text="Post" OnClick="btnPostComment_Click" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtComments"
                                        Display="Dynamic" ErrorMessage="Please use plain text only" ValidationExpression="[^<]+"></asp:RegularExpressionValidator>
                                </telerik:RadToolTip>
                            </li>
                        </ul>
                    </fieldset>
                </div>
                <!-- End Address Information -->
                <!-- Building Information -->
                <div style="float: right; width: 400px;">
                    <fieldset>
                        <legend>Building Information</legend>
                        <ul>
                            <li></li>
                            <li>
                                <!-- Start Primary -->
                                <label for="_txtBuildingName">
                                    Building Name</label>
                                <telerik:RadTextBox ID="_txtBuildingName" CssClass="inputStyle" runat="server" Width="205px"
                                    ReadOnly="true">
                                </telerik:RadTextBox>
                            </li>
                            <li>
                                <!-- Start Primary -->
                                <label for="_txtPrimaryAddress">
                                    Primary Address</label>
                                <telerik:RadTextBox ID="_txtPrimaryAddress" CssClass="inputStyle" runat="server"
                                    ReadOnly="true" Width="205px">
                                </telerik:RadTextBox>
                            </li>
                            <li>
                                <!-- End Primary -->
                                <!-- Start Secondary -->
                                <label for="_txtSecondaryAddress">
                                    Secondary Address</label>
                                <telerik:RadTextBox ID="_txtSecondaryAddress" CssClass="inputStyle" runat="server"
                                    ReadOnly="true" Width="205px">
                                </telerik:RadTextBox>
                            </li>
                            <li>
                                <!-- End Secondary -->
                                <!-- Start City -->
                                <label for="_txtCity">
                                    City</label>
                                <telerik:RadTextBox ID="_txtCity" CssClass="inputStyle" runat="server" Width="205px"
                                    ReadOnly="true">
                                </telerik:RadTextBox>
                            </li>
                            <li>
                                <!-- End City -->
                                <!-- Start Zip -->
                                <label for="_txtZip">
                                    Zip</label>
                                <telerik:RadTextBox ID="_txtZip" CssClass="inputStyle" runat="server" Width="205px"
                                    ReadOnly="true">
                                </telerik:RadTextBox>
                            </li>
                            <li>
                                <!-- End Zip -->
                                <!-- Start State -->
                                <label for="_txtState">
                                    State</label>
                                <telerik:RadTextBox ID="_txtState" runat="server" CssClass="inputStyle" ReadOnly="true"
                                    Width="205px">
                                </telerik:RadTextBox>
                            </li>
                        </ul>
                    </fieldset>
                    <!-- End State -->
                </div>
                <!-- End Building Information -->
                <div class="spacer">
                </div>
            </div>
        </li>
        <li>
            <h3>
                2. Room Description</h3>
        </li>
        <li>
            <div class="DivStyled">
                <asp:Panel ID="_pnlDescription" runat="server">
                    <asp:Literal ID="_litDescription" runat="server"></asp:Literal>
                </asp:Panel>
            </div>
        </li>
        <li>
            <h3>
                3. Images</h3> (click to view larger image)
        </li>
        <li id="imageGrid">
            <div class="DivStyled">
                <center>
                    <asp:Literal ID="_litPrettyPhoto" runat="server"></asp:Literal>
                </center>
            </div>
        </li>
        <li>
            <h3>
                4. Room Availability </h3><h5><span class="small">*all times are in PST</span></h5>
        </li>
        <li>
            <telerik:RadButton ID="_btnExportSchedule" runat="server" Visible="false" Text="Export to PDF"  Width="150px"
                OnClick="_btnExportSchedule_Click">
            </telerik:RadButton>
            <telerik:RadScheduler runat="server" ID="_rsReservations" Width="950px" DayStartTime="08:00:00"
                DayEndTime="18:00:00" OnAppointmentDataBound="_rsReservations_AppointmentDataBound"
                OnNavigationComplete="_rsReservations_OnNavigationComplete" SelectedView="MonthView"
                DataKeyField="ID" DataSubjectField="Subject" DataStartField="Start" DataEndField="End">
                <TimelineView UserSelectable="false" />
                <TimeSlotContextMenuSettings EnableDefault="true" />
                <AppointmentContextMenuSettings EnableDefault="true" />
            </telerik:RadScheduler>
        </li>
    </ul>
    <ul>
        <li>
            <h3>
                5. Comments</h3>
        </li>
        <li>
            <div class="DivStyled">
                <asp:Literal ID="_litComments" runat="server"></asp:Literal>
            </div>
        </li>
    </ul>
    <telerik:RadCodeBlock ID="_rCode" runat="server">
        <script type="text/javascript">
            function ShowAddressLocation(address)
            {
                PopupCenter("Map.htm?MarkerOpen=true&address=" + address, "Address Information", 975, 850);
                return false;
            }

            function OnClientRated(controlRating, args)
            {
                var tooltip = $find("<%=RadToolTip1.ClientID %>");
                tooltip.show();
            }

            function CloseToolTip1()
            {
                var tooltip = Telerik.Web.UI.RadToolTip.getCurrent();
                if (tooltip)
                {
                    tooltip.hide();
                }
            }

            //Put your JavaScript code here.
            function printScheduler()
            {
                PopupCenter("/App/Popup/SchedulePrint.aspx?type=roomdetails&roomId=" + getQuerystring("roomId"), 500, 920);
            }
            function pageLoad()
            {
                var $ = $telerik.$;
                $(" <li><a onclick='printScheduler()'><span>Print</span></a></li>").appendTo($('.rsHeader ul'));
            }

            $(document).ready(function ()
            {
                $("a[rel^='prettyPhoto']").prettyPhoto({
                    animationSpeed: 'normal', /* fast/slow/normal */
                    opacity: 0.80, /* Value between 0 and 1 */
                    showTitle: true, /* true/false */
                    social_tools: false
                });
            });

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="_raProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="_rsReservations">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="_rsReservations" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cSecondary" runat="server">
</asp:Content>
