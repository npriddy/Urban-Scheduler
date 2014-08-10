<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true"
    CodeBehind="RoomReservationDetails.aspx.cs" Inherits="UrbanSchedulerProject.App.Pages.RoomReservationDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cMain" runat="server">
    <style>
        .DivStyled label
        {
            width: 120px !important;
        }
    </style>
    <ul>
        <li>
            <div class="DivStyled">
                <telerik:RadButton ID="_btnBack" runat="server" Text="Back" OnClick="_btnBack_Click"
                    Width="150px">
                </telerik:RadButton>
            </div>
        </li>
        <li>
            <h1>
                Room Reservation</h1>
        </li>
        <li>
            <h3>
                Room Information</h3>
        </li>
        <li>
            <div class="DivStyled">
                <fieldset style="border: 0px;">
                    <ul>
                        <li>
                            <label for="_txtRoomNumber">
                                Room Number:</label>
                            <telerik:RadTextBox ID="_txtRoomNumber" runat="server" ReadOnly="true" CssClass="inputStyle"
                                Width="360px">
                            </telerik:RadTextBox></li>
                        <li>
                            <label for="_txtBuildingName">
                                Building Name:</label>
                            <telerik:RadTextBox ID="_txtBuildingName" runat="server" ReadOnly="true" CssClass="inputStyle">
                            </telerik:RadTextBox></li>
                        <li>
                            <label for="_txtReservationStatus">
                                Reservation Stauts:</label>
                            <telerik:RadTextBox ID="_txtReservationStatus" runat="server" CssClass="inputStyle"
                                ReadOnly="true">
                            </telerik:RadTextBox></li>
                        <li>
                            <label for="_rdpRequestedDate">
                                Reservation Requested Date:</label>
                            <telerik:RadDatePicker ID="_rdpRequestedDate" runat="server" ReadOnly="true">
                                <DatePopupButton Enabled="false" Visible="false" />
                                <DateInput Enabled="false" CssClass="inputStyle">
                                </DateInput>
                            </telerik:RadDatePicker>
                        </li>
                    </ul>
                </fieldset>
            </div>
        </li>
        <li>
            <h3>
                1. Room Comments</h3>
        </li>
        <li>
            <div class="DivStyled">
                <asp:Literal ID="_litComments" runat="server"></asp:Literal>
            </div>
        </li>
        <li>
            <h3>
                2. Requested Dates</h3>
        </li>
        <li>
            <telerik:RadGrid ID="_rgRequestedTimes" runat="server" OnItemDataBound="_rgRequestedTimes_OnItemDataBound"
                OnItemCreated="_rgRequestedTimes_ItemCreated" OnNeedDataSource="_rgRequestedTimes_NeedDataSource"
                AllowPaging="True" AllowSorting="True" OnItemCommand="_rgRequestedTimes_ItemCommand"
                AutoGenerateColumns="False" Width="960px" ShowStatusBar="True" GridLines="None">
                <ExportSettings IgnorePaging="true" OpenInNewWindow="true" FileName="Room Reservation Times">
                    <Pdf PageHeight="210mm" PageWidth="297mm" PageTitle="Room Reservation Times" DefaultFontFamily="Arial Unicode MS"
                        PageBottomMargin="20mm" PageTopMargin="20mm" PageLeftMargin="20mm" PageRightMargin="20mm" />
                </ExportSettings>
                <PagerStyle Mode="NumericPages" AlwaysVisible="true" />
                <MasterTableView Width="100%" CommandItemDisplay="Top">
                    <CommandItemSettings ShowExportToPdfButton="false" ShowAddNewRecordButton="false"
                        ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowExportToWordButton="true">
                    </CommandItemSettings>
                    <Columns>
                        <telerik:GridBoundColumn HeaderText="ID" UniqueName="ID" DataField="ID" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="StartDate" UniqueName="StartDate" DataField="StartDate"
                            DataFormatString="{0:MM/dd/yy}">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Start Time" UniqueName="StartTime" DataField="StartDate">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="EndDate" UniqueName="EndDate" DataField="EndDate"
                            DataFormatString="{0:MM/dd/yy}">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="End Time" UniqueName="EndTime" DataField="EndDate">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Days" UniqueName="Days" DataField="Days" Visible="false">
                        </telerik:GridBoundColumn>
                    </Columns>
                    <PagerStyle AlwaysVisible="True" />
                    <EditFormSettings>
                        <EditColumn ButtonType="ImageButton" />
                    </EditFormSettings>
                </MasterTableView>
            </telerik:RadGrid>
            <telerik:RadScheduler runat="server" ID="_rsReservations" Width="970px" DayStartTime="08:00:00"
                OnNavigationComplete="_rsReservations_OnNavigationComplete" ReadOnly="true" DayEndTime="18:00:00"
                OnAppointmentDataBound="_rsReservations_AppointmentDataBound" SelectedView="MonthView"
                DataKeyField="ID" DataSubjectField="Subject" DataStartField="Start" DataEndField="End">
                <AdvancedForm Modal="true" />
                <ExportSettings FileName="Schedule">
                </ExportSettings>
                <TimelineView UserSelectable="false" />
                <TimeSlotContextMenuSettings EnableDefault="true" />
                <AppointmentContextMenuSettings EnableDefault="true" />
            </telerik:RadScheduler>
        </li>
        <li id="_roomCommentsHeader" runat="server">
            <h3>
                3. Room Comments To Send</h3>
        </li>
        <li id="_roomComments" runat="server">
            <telerik:RadEditor ID="_rContent" Width="960px" runat="server" Height="200px" SkinID="MinimalSetOfTools"
                ToolsFile="~/App_Data/BasicTools.xml" EditModes="Design">
                <CssFiles>
                    <telerik:EditorCssFile Value="~/Theme/EditorContentArea.css" />
                </CssFiles>
            </telerik:RadEditor>
        </li>
        <li id="_actionHeader" runat="server">
            <h3>
                4. Actions</h3>
        </li>
        <li id="_action" runat="server">
            <div class="DivStyled">
                <telerik:RadButton ID="_btnSubmitComments" runat="server" Text="Submit Comments"
                    Width="150px" OnClick="_btnSubmitComments_Click">
                </telerik:RadButton>
                <telerik:RadButton ID="_btnApprove" runat="server" Text="Approve Reservation" OnClick="_btnApprove_Click"
                    Width="150px">
                </telerik:RadButton>
                <telerik:RadButton ID="_btnDeny" runat="server" Text="Deny Reservation" OnClick="_btnDeny_Click"
                    Width="150px">
                </telerik:RadButton>
            </div>
        </li>
    </ul>
    <telerik:RadAjaxManagerProxy ID="_raProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="_rsReservations">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="_rsReservations" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    
<script type="text/javascript">
    function printScheduler()
    {
        PopupCenter("/App/Popup/SchedulePrint.aspx?type=myroomreservation&RoomReservationID=" + getQuerystring("RoomReservationID"), 500, 920);
    }
    function pageLoad()
    {
        var $ = $telerik.$;
        $(" <li><a onclick='printScheduler()'><span>Print</span></a></li>").appendTo($('.rsHeader ul'));
    }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cSecondary" runat="server">

</asp:Content>
