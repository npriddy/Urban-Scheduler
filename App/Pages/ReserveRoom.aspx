<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true"
    CodeBehind="ReserveRoom.aspx.cs" Inherits="UrbanSchedulerProject.App.Pages.RoomReservationPage" %>
<%@ Register Src="~/App/UserControl/ucFeedback.ascx" TagName="ucFeedback" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cMain" runat="server">
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
                Reserve Room:
                <asp:Label ID="_lblRoomNumber" runat="server"></asp:Label></h1>
        </li>
        <li>
            <h3>
                Step 1: Request Room Times</h3>
        </li>
        <li>
            <telerik:RadScheduler runat="server" ID="_rsReservations" Width="970px" DayStartTime="08:00:00"
                OnNavigationComplete="_rsReservations_OnNavigationComplete" ReadOnly="true" DayEndTime="18:00:00"
                OnAppointmentInsert="_rsReservations_AppointmentInsert" OnAppointmentDataBound="_rsReservations_AppointmentDataBound"
                OnAppointmentUpdate="_rsReservations_AppointmentUpdate" OnAppointmentDelete="_rsReservations_AppointmentDelete"
                SelectedView="MonthView" DataKeyField="ID" DataSubjectField="Subject" DataStartField="Start"
                DataEndField="End">
                <AdvancedForm Modal="true" />
                <ExportSettings FileName="Schedule">
                </ExportSettings>
                <TimelineView UserSelectable="false" />
                <TimeSlotContextMenuSettings EnableDefault="true" />
                <AppointmentContextMenuSettings EnableDefault="true" />
            </telerik:RadScheduler>
        </li>
        <li>
            <h3>
                <strong>2. Room Times Requesting</strong>
            </h3>
        </li>
        <li>
            <uc1:ucfeedback id="ucDateFeedBack" runat="server" />
        </li>
        <li>
            <table width="950px" id="styleized" class="tableForStyle">
                <tr>
                    <td>
                        <label for="_rdpDate">
                            Date</label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="_rdpDate" runat="server" CssClass="inputStyle">
                            <DateInput CssClass="inputStyle">
                            </DateInput>
                        </telerik:RadDatePicker>
                    </td>
                    <td>
                        <label for="_rtpStartTime">
                            Start Time</label>
                    </td>
                    <td>
                        <telerik:RadTimePicker ID="_rtpStartTime" runat="server" CssClass="inputStyle">
                        </telerik:RadTimePicker>
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <label for="_rtpEndTime">
                            End Time</label>
                    </td>
                    <td>
                        <telerik:RadTimePicker ID="_rtpEndTime" runat="server" CssClass="inputStyle">
                        </telerik:RadTimePicker>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                    <td colspan="2">
                        <telerik:RadButton ID="_btnAddDates" runat="server" Text="Add Date To Reserve" OnClick="_btnAddDates_Click"  Width="150px">
                        </telerik:RadButton>
                    </td>
                </tr>
            </table>
        </li>
        <li>
            <telerik:RadGrid ID="_rgAvailableTimes" runat="server" OnItemDataBound="_rgAvailableTimes_OnItemDataBound"  OnItemCommand="_rgAvailableTimes_ItemCommand"
                OnNeedDataSource="_rgAvailableTimes_NeedDataSource" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" Width="950px" ShowStatusBar="True" GridLines="None">
                <PagerStyle Mode="NumericPages" AlwaysVisible="true" />
                <MasterTableView Width="100%" CommandItemDisplay="Top">
                    <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToPdfButton="false" />
                    <Columns>
                    <telerik:GridBoundColumn DataField="ID" Visible="false" UniqueName="Id"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Date" UniqueName="Date" DataField="Date" DataFormatString="{0:MM/dd/yy}">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Start" UniqueName="Start" DataField="Start">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="End" UniqueName="End" DataField="End">
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn Text="Delete" CommandName="Delete" Visible="true" ButtonType="ImageButton">
                        <HeaderStyle Width="2%" />
                    </telerik:GridButtonColumn>
                    </Columns>
                    <PagerStyle AlwaysVisible="True" />
                    <EditFormSettings>
                        <EditColumn ButtonType="ImageButton" />
                    </EditFormSettings>
                </MasterTableView>
            </telerik:RadGrid>
        </li>
        <li>
            <h3>
                Step 3: Comments</h3>
        </li>
        <li>
            <telerik:RadEditor ID="_rContent" Width="950px" runat="server" Height="200px" SkinID="MinimalSetOfTools"
                ToolsFile="~/App_Data/BasicTools.xml" EditModes="Design">
                <CssFiles>
                    <telerik:EditorCssFile Value="~/Theme/EditorContentArea.css" />
                </CssFiles>
            </telerik:RadEditor>
        </li>
        <li>
            <h3>
                Step 4: Finalize</h3>
        </li>
        <li>
            <div class='DivStyled'>
                <telerik:RadButton ID="_btnSubmitReservationRequest" Text="Submit Reservation Request"  Width="150px"
                    OnClick="_btnSubmitReservationRequest_Click" runat="server">
                </telerik:RadButton>
            </div>
        </li>
    </ul>
       <telerik:RadAjaxManagerProxy ID="_raProxy">
    
    </telerik:RadAjaxManagerProxy>
</asp:Content>
