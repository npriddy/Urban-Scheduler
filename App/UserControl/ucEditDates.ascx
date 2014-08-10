<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucEditDates.ascx.cs"
    Inherits="UrbanSchedulerProject.App.UserControl.ucEditDates" %>
<asp:Label ID="_lblId" runat="server"></asp:Label>
<div id="stylized" style="float: left; width: 600px !important;" class="userLoginForm">
    <div style="float: left; width: 600px">
        <label for="_rdpStartDate">
            Start Date</label>
        <telerik:RadDatePicker ID="_rdpStartDate" runat="server" CssClass="inputStyle">
        </telerik:RadDatePicker>
        <label for="_rtpStartTime">
            Start Time</label>
        <telerik:RadTimePicker ID="_rtpStartTime" runat="server" CssClass="inputStyle">
        </telerik:RadTimePicker>
        <br />
        <label for="_rdpEndDate">
            End Date</label>
        <telerik:RadDatePicker ID="_rdpEndDate" runat="server" CssClass="inputStyle">
        </telerik:RadDatePicker>
        <label for="_rtpEndTime">
            End Time</label>
        <telerik:RadTimePicker ID="_rtpEndTime" runat="server" CssClass="inputStyle">
        </telerik:RadTimePicker>
        <br />
        <label for="_rcbDays">
            Days</label>
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
    </div>
</div>
<br />
<telerik:RadButton ID="_btnUpdate" runat="server" Text="Update"  Width="150px">
</telerik:RadButton>
<telerik:RadButton ID="_btnCancel" runat="server" Text="Cancel"  Width="150px">
</telerik:RadButton>
