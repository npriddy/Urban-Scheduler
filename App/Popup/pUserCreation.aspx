<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Popup.Master" CodeBehind="pUserCreation.aspx.cs"
    Inherits="UrbanSchedulerProject.App.Popup.pUserCreation" %>
<asp:Content ContentPlaceHolderID="cMain" runat="server" ID="content1">
    <script type="text/javascript">
        function Close() {
            parent.$.fn.colorbox.close();
        }
    </script>
    <style type="text/css">
        .rcCaptchaImage
        {
            float: left;
        }
        
        .rbDecorated
        {
            margin: 0 0 0 0 !imporant;
            border: 0 0 0 0 !imporant;
            padding: 4 2 4 2 !imporant;
        }
    </style>
    <telerik:RadAjaxPanel ID="_raPanel" runat="server">
        <div id="stylized" style="float: left" class="myform">
            <h1>
                Create Account</h1>
            <p>
                Required fields are Email, Password, and CAPTCHA.
            </p>
            <label>
                Email <span class="small">(Required)</span>
            </label>
            <telerik:RadTextBox ID="_txtEmail" CssClass="inputStyle" runat="server" ValidationGroup="AccountCreate"
                Width="180px">
            </telerik:RadTextBox>
            <div style="float: right; display: block; width: 150px; overflow: hidden">
                <asp:RequiredFieldValidator ID="_rfvEmail" runat="server" ValidationGroup="AccountCreate"
                    Display="Dynamic" Text="*" ControlToValidate="_txtEmail"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="emailValidator" ValidationGroup="AccountCreate"
                    Width="100px" runat="server" Display="Dynamic" ErrorMessage="Please enter a valid email address."
                    ValidationExpression="^[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]{1,})*(\.[a-zA-Z]{2,3}){1,2}$"
                    ControlToValidate="_txtEmail">
                </asp:RegularExpressionValidator>
            </div>
            <br />
            <label>
                First Name:</label>
            <telerik:RadTextBox ID="_txtFirstName" CssClass="inputStyle" runat="server" ValidationGroup="AccountCreate"
                Width="180px">
            </telerik:RadTextBox>
            <br />
            <label>
                Last Name:</label>
            <telerik:RadTextBox ID="_txtLastName" CssClass="inputStyle" runat="server" ValidationGroup="AccountCreate"
                Width="180px">
            </telerik:RadTextBox>
            <br />
            <label>
                Enter Password:<span class="small">(Required)</span></label>
            <telerik:RadTextBox ID="_txtPassword" CssClass="inputStyle" runat="server" TextMode="Password"
                ValidationGroup="AccountCreate" Width="180px">
            </telerik:RadTextBox>
            <div style="float: right; display: block; width: 150px; overflow: hidden">
                <asp:RequiredFieldValidator ID="_rfvPassword" runat="server" ValidationGroup="AccountCreate"
                    Display="Dynamic" Text="*" ControlToValidate="_txtPassword"></asp:RequiredFieldValidator>
            </div>
            <br />
            <label>
                Repeat Password:<span class="small">(Required)</span></label>
            <telerik:RadTextBox ID="_txtPasswordVerification" CssClass="inputStyle" TextMode="Password"
                runat="server" ValidationGroup="AccountCreate" Width="180px">
            </telerik:RadTextBox>
            <div style="float: right; display: block; width: 150px; overflow: hidden">
                <asp:RequiredFieldValidator ID="_rfvPasswordVerify" runat="server" ValidationGroup="AccountCreate"
                    Display="Dynamic" Text="*" ControlToValidate="_txtPasswordVerification"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="_cpPasswords" runat="server" ControlToCompare="_txtPassword"
                    ControlToValidate="_txtPasswordVerification" ErrorMessage="Passwords do not match."
                    Display="Dynamic" />
            </div>
            <br />
            <label>
                CAPTCHA <span class="small">(please enter text bellow)</span>
            </label>
            <div style="float: left; margin-left: 10px; margin-bottom: 10px">
                <telerik:RadCaptcha ID="_rCaptcha" runat="server" ErrorMessage="Page not valid. The code you entered is not valid."
                    ValidationGroup="AccountCreate" ValidatedTextBoxID="_txtCaptcha" Display="none">
                    <CaptchaImage EnableCaptchaAudio="false" RenderImageOnly="true" Width="180" ImageCssClass="rcCaptchaImage"
                        BackgroundColor="#609f0a" TextColor="White" BackgroundNoise="None" />
                </telerik:RadCaptcha>
            </div>
            <br />
            <br />
            <br />
            <br />
            <label>
                Verification <span class="small">(Required)</span></label>
            <telerik:RadTextBox ID="_txtCaptcha" CssClass="inputStyle" runat="server" ValidationGroup="AccountCreate"
                Width="180px">
            </telerik:RadTextBox>
            <div style="float: right; display: block; width: 150px; overflow: hidden">
                <asp:RequiredFieldValidator ID="_rfvCaptcha" runat="server" ValidationGroup="AccountCreate"
                    Display="Dynamic" Text="*" ControlToValidate="_txtCaptcha"></asp:RequiredFieldValidator>
            </div>
            <br />
            <telerik:RadButton ID="_btnCreateAccount" Style="float: left; margin-left: 150px"
                Width="180px" runat="server" ValidationGroup="AccountCreate" Text="Create Account"
                OnClick="_btnCreateAccount_Click">
            </telerik:RadButton>
        </div>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxManagerProxy ID="_proxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="_btnCreateAccount">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ucFeedBackBasePage" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
</asp:Content>
