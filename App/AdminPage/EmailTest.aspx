<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="EmailTest.aspx.cs" Inherits="UrbanSchedulerProject.App.AdminPage.EmailTest" %>
<asp:Content ContentPlaceHolderID="cMain" runat="server" ID="content1">

<telerik:RadButton ID="_btnEmailTest" runat="server" Text="Email Test" 
        onclick="_btnEmailTest_Click"></telerik:RadButton>
</asp:Content>