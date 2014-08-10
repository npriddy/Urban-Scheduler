<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true"
    CodeBehind="BuildingEdit.aspx.cs" Inherits="UrbanSchedulerProject.App.Pages.BuildingEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cMain" runat="server">
    <style>
        ul
        {
            list-style: none;
        }
    </style>
    <div class="DivStyled">
        <telerik:RadButton ID="_btnBack" runat="server" Text="Back" OnClick="_btnBack_Click"
            Width="150px">
        </telerik:RadButton>
        <telerik:RadButton ID='_btnSave' runat="server" Text="Save" OnClick="_btnSave_Click"
            Width="150px">
        </telerik:RadButton>
        <telerik:RadButton ID="_btnCancel" runat='server' Text="Cancel" OnClick="_btnCancel_Click"
            Visible="false" Width="150px">
        </telerik:RadButton>
    </div>
    <h1>
        Building Information</h1>
    <div id="stylized" style="float: left; width: 950px !important;" class="userLoginForm">
        <fieldset style="border: 0px">
            <ul>
                <li>
                    <!-- Start Primary -->
                    <label for="_txtBuildingName">
                        Building Name <span class="small">(Optional)</span></label>
                    <telerik:RadTextBox ID="_txtBuildingName" CssClass="inputStyle" runat="server" Width="205px">
                    </telerik:RadTextBox>
                </li>
                <li>
                    <!-- Start Primary -->
                    <label for="_txtPrimaryAddress">
                        Primary Address <span class="small">(Required)</span></label>
                    <telerik:RadTextBox ID="_txtPrimaryAddress" CssClass="inputStyle" runat="server"
                        Width="205px">
                    </telerik:RadTextBox>
                    <asp:RequiredFieldValidator ID="_rfvPrimaryAddress" runat="server" ControlToValidate="_txtPrimaryAddress"
                        ErrorMessage="*" ValidationGroup="CreateRoom"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <!-- End Primary -->
                    <!-- Start Secondary -->
                    <label for="_txtSecondaryAddress">
                        Secondary Address<span class="small">(Optional)</span></label>
                    <telerik:RadTextBox ID="_txtSecondaryAddress" CssClass="inputStyle" runat="server"
                        Width="205px">
                    </telerik:RadTextBox>
                    <asp:RequiredFieldValidator ID="_rfvPSecondaryAddress" runat="server" ControlToValidate="_txtSecondaryAddress"
                        Enabled="false" ErrorMessage="*" ValidationGroup="CreateRoom"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <!-- End Secondary -->
                    <!-- Start City -->
                    <label for="_txtCity">
                        City<span class="small">(Required)</span></label>
                    <telerik:RadTextBox ID="_txtCity" CssClass="inputStyle" runat="server" Width="205px">
                    </telerik:RadTextBox>
                    <asp:RequiredFieldValidator ID="_rfvCity" runat="server" ControlToValidate="_txtCity"
                        ErrorMessage="*" ValidationGroup="CreateRoom"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <!-- End City -->
                    <!-- Start Zip -->
                    <label for="_txtZip">
                        Zip<span class="small">(Required)</span></label>
                    <telerik:RadTextBox ID="_txtZip" CssClass="inputStyle" runat="server" Width="205px">
                    </telerik:RadTextBox>
                    <asp:RequiredFieldValidator ID="_rfvZip" runat="server" ControlToValidate="_txtZip"
                        ErrorMessage="*" ValidationGroup="CreateRoom"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <!-- End Zip -->
                    <!-- Start State -->
                    <label for="_cbState">
                        State<span class="small">(Required)</span></label>
                    <telerik:RadComboBox ID="_cbState" runat="server" CssClass="inputStyle" Height="200px"
                        Skin="Office2007" MarkFirstMatch="true" Width="205px">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="_rfvState" runat="server" ControlToValidate="_cbState"
                        InitialValue="(None)" ErrorMessage="*" ValidationGroup="CreateRoom"></asp:RequiredFieldValidator>
                </li>
            </ul>
        </fieldset>
    </div>
    <telerik:RadAjaxManagerProxy ID="_raProxy">
    
    </telerik:RadAjaxManagerProxy>
</asp:Content>
