<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Popup.Master" AutoEventWireup="true" CodeBehind="SchedulePrint.aspx.cs" Inherits="UrbanSchedulerProject.App.Popup.SchedulePrint" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cMain" runat="server">
<a href="javascript:window.print()"><img src="../../Theme/Images/Icons/Print.gif"></a>
         <telerik:RadScheduler runat="server" ID="_rsReservations" Width="900px" Height="800px" DayStartTime="08:00:00"
                DayEndTime="18:00:00" OnAppointmentDataBound="_rsReservations_AppointmentDataBound"
                OnNavigationComplete="_rsReservations_OnNavigationComplete" SelectedView="MonthView"
                DataKeyField="ID" DataSubjectField="Subject" DataStartField="Start" DataEndField="End">
                <TimelineView UserSelectable="false" />
                <TimeSlotContextMenuSettings EnableDefault="true" />
                <AppointmentContextMenuSettings EnableDefault="true" />
            </telerik:RadScheduler>
</asp:Content>
