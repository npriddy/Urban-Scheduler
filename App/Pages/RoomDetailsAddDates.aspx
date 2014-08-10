<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.Master"
    CodeBehind="RoomDetailsAddDates.aspx.cs" Inherits="UrbanSchedulerProject.App.Pages.RoomDetailsAddDates" %>

<asp:Content ContentPlaceHolderID="cMain" runat="server" ID="_content1">
    <style>
        .DivStyled label
        {
            width: 120px !important;
        }
    </style>
    <ul>
        <li>
            <div class="DivStyled">
                <telerik:RadButton ID="_btnBack" runat="server" Text="Back" OnClick="_btnBack_Click"  Width="150px">
                </telerik:RadButton>
            </div>
        </li>
        <li>
            <h1>
                <strong>Room Availability</strong>
            </h1>
        </li>
        <li>
            <div class="DivStyled">
                <%--<fieldset style="border: 0px;">--%>
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
                    </ul>
              <%--  </fieldset>--%>
            </div>
        </li>
        <li>
            <asp:Panel ID="_pnlDates" runat="server">
                <table width="950px" id="styleized" class="tableForStyle">
                    <tr>
                        <td>
                            <label for="_rdpStartDate">
                                Start Date</label>
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="_rdpStartDate" runat="server" CssClass="inputStyle">
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
                        <td>
                            <label for="_rdpEndDate">
                                End Date</label>
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="_rdpEndDate" runat="server" CssClass="inputStyle">
                            </telerik:RadDatePicker>
                        </td>
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
                        <td>
                            <label for="_rcbDays">
                                Days</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="_rcbDays" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true"
                                CssClass="inputStyle">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Sunday" Value="sun" />
                                    <telerik:RadComboBoxItem Text="Monday" Value="mon" />
                                    <telerik:RadComboBoxItem Text="Teusday" Value="tue" />
                                    <telerik:RadComboBoxItem Text="Wednesday" Value="wed" />
                                    <telerik:RadComboBoxItem Text="Thursday" Value="thur" />
                                    <telerik:RadComboBoxItem Text="Friday" Value="fri" />
                                    <telerik:RadComboBoxItem Text="Saturday" Value="sat" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            <telerik:RadButton ID="_btnAddDates" runat="server" Text="Add Date" OnClick="_btnAddDates_Click"  Width="150px">
                            </telerik:RadButton>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </li>
        <li>
            <telerik:RadGrid ID="_rgAvailableTimes" runat="server" OnItemDataBound="_rgAvailableTimes_OnItemDataBound" 
                Width="950px" OnItemCommand="_rgAvailableTimes_ItemCommand" OnNeedDataSource="_rgAvailableTimes_NeedDataSource">
                <MasterTableView AutoGenerateColumns="false">
                    <Columns>
                        <telerik:GridBoundColumn HeaderText="ID" UniqueName="ID" DataField="ID" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="StartDate" UniqueName="StartDate" DataFormatString="{0:d}"
                            DataField="StartDate">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="StartTime" UniqueName="StartTime" DataFormatString="{0:hh}"
                            DataField="StartTime">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="EndDate" UniqueName="EndDate" DataFormatString="{0:d}"
                            DataField="EndDate">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="EndTime" UniqueName="EndTime" DataFormatString="{0:hh}"
                            DataField="EndTime">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Days" UniqueName="Days" DataField="Days">
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn Text="Delete" CommandName="Delete" Visible="true" ButtonType="ImageButton">
                            <HeaderStyle Width="2%" />
                        </telerik:GridButtonColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </li>
        <li>
            <telerik:RadScheduler runat="server" ID="_rsReservations" DayStartTime="08:00:00" ReadOnly="true"
                Width="950px" DayEndTime="18:00:00" OnAppointmentInsert="RadScheduler1_AppointmentInsert" OnNavigationComplete="_rsReservations_OnNavigationComplete"
                OnAppointmentDataBound="_rsReservations_AppointmentDataBound" OnAppointmentUpdate="RadScheduler1_AppointmentUpdate"
                OnAppointmentDelete="RadScheduler1_AppointmentDelete" SelectedView="MonthView"
                DataKeyField="ID" DataSubjectField="Subject" DataStartField="Start" DataEndField="End">
                <AdvancedForm Modal="true" EnableResourceEditing="false" />
                <ExportSettings FileName="Schedule">
                </ExportSettings>
                <TimelineView UserSelectable="false" />
                <TimeSlotContextMenuSettings EnableDefault="true" />
                <AppointmentContextMenuSettings EnableDefault="true" />
            </telerik:RadScheduler>
        </li>
    </ul>
       <telerik:RadAjaxManagerProxy ID="_raProxy">
    <AjaxSettings>
    <telerik:AjaxSetting AjaxControlID="_rsReservations">
    <UpdatedControls>
    
    <telerik:AjaxUpdatedControl ControlID="_rsReservations" />
    </UpdatedControls>
    </telerik:AjaxSetting>
    
    </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
</asp:Content>
