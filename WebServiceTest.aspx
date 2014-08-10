<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="WebServiceTest.aspx.cs" Inherits="UrbanSchedulerProject.App.WebServiceTest" %>

<asp:Content ContentPlaceHolderID="cMain" ID="Content1" runat="server">

<script type="text/javascript">
    function run() {
        $.post("GoogleMapsService.asmx/ListUsers", function (data) {
            alert("Data Loaded: " + data);
        });
    }
</script>

<telerik:RadButton OnClientClicked="run"  AutoPostBack="false" ID="_btnTest" runat="server"></telerik:RadButton>
</asp:Content>