<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucLogin.ascx.cs" Inherits="UrbanSchedulerProject.App.UserControl.ucLogin" %>
<script type="text/javascript">
    function openCreate(sender, args) {

        $.fn.colorbox({
            href: "/App/Popup/pUserCreation.aspx",
            iframe: true,
            preloading: true,
            width: "573px",
            height: "650px",
            fastIframe: true,
            scrolling: false
        });
    }
</script>
<div id="stylizedUserLogin" style="float: left; width: 300px" class="userLoginForm">
    <asp:Panel ID="_pnlLoggedIn" Visible="false" runat="server">
        <asp:Label ID="_lblEmail" runat="server" Enabled="false" Visible="false"></asp:Label>
        <telerik:RadButton ID="_btnLogout" UseSubmitBehavior="false" TabIndex="-1" runat="server" Text="Logout" OnClick="_btnLogout_Click">
        </telerik:RadButton>
    </asp:Panel>
    <asp:Panel ID="_pnlUserLoggedIn" runat="server" DefaultButton="_btnLogin">
        <label for="_txtEmal">
            Email</label>
        <telerik:RadTextBox ID="_txtEmail" CssClass="inputStyle" Width="160px" runat="server">
        </telerik:RadTextBox>
        <label for="_txtPassword">
            Password</label>
        <telerik:RadTextBox ID="_txtPassword" CssClass="inputStyle" Width="160px" runat="server"
            TextMode="Password">
        </telerik:RadTextBox>
        <br />
        <telerik:RadButton ID="_btnLogin" Style="margin-left: 80px" runat="server" Text="Login"
            OnClick="_btnLogin_Click">
        </telerik:RadButton>
        <telerik:RadButton ID="_btnCreateAccount" runat="server" AutoPostBack="false" Text="Create Account"
            OnClientClicked="openCreate">
        </telerik:RadButton>
    </asp:Panel>
</div>
