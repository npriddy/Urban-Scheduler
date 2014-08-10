<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Admin.aspx.cs" Inherits="UrbanSchedulerProject.App.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cMain" runat="server">
 <div id="stylized" style="float: left; width:910px !important;" class="myform">
 <h2>Admin Page</h2>
 <p><h1>Users</h1></p>
    <telerik:RadGrid ID="RadGrid1" runat="server" Skin="Web20" Width="700px" OnNeedDataSource="RadGrid1_NeedDataSource">
    <MasterTableView AutoGenerateColumns="false">
    <Columns>
    <telerik:GridBoundColumn DataField="Id" HeaderText="Id"></telerik:GridBoundColumn>
    </Columns>
    </MasterTableView>
    </telerik:RadGrid>
    <p><h1>Schedule Test</h1></p>
    <telerik:RadScheduler runat="server" ID="RadScheduler1" Width="750px" DayStartTime="08:00:00"
        DayEndTime="18:00:00" TimeZoneOffset="03:00:00" DataKeyField="ID" DataSubjectField="Subject"
        DataStartField="Start" DataEndField="End" DataRecurrenceField="RecurrenceRule"
        DataRecurrenceParentKeyField="RecurrenceParentId" DataReminderField="Reminder">
        <AdvancedForm Modal="true" />
        <TimelineView UserSelectable="false" />
        <TimeSlotContextMenuSettings EnableDefault="true" />
        <AppointmentContextMenuSettings EnableDefault="true" />
        <Reminders Enabled="true" />
    </telerik:RadScheduler>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cSecondary" runat="server">
</asp:Content>
