<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DateRangeTest.aspx.cs"
    MasterPageFile="~/Site.Master" Inherits="UrbanSchedulerProject.App.AdminPage.DateRangeTest" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cMain">
<telerik:RadButton ID="_btnRun" Text="Run Dates" runat="server" 
        onclick="_btnRun_Click"></telerik:RadButton>Start <telerik:RadDatePicker ID="_rdpStart" runat="server"></telerik:RadDatePicker>End <telerik:RadDatePicker ID="_rdpEnd" runat="server"></telerik:RadDatePicker>
    <telerik:RadGrid ID="RadGrid1" runat="server" Skin="Web20" Width="700px" OnNeedDataSource="RadGrid1_NeedDataSource">
        <MasterTableView AutoGenerateColumns="true">
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
