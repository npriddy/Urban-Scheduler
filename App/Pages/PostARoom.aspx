<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true"
    CodeBehind="PostARoom.aspx.cs" Inherits="UrbanSchedulerProject.App.Pages.PostARoom" %>

<%@ Register Src="~/App/UserControl/ucFeedback.ascx" TagName="ucFeedback" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cMain" runat="server">
    <style>
        ul
        {
            list-style: none;
        }
    </style>
    <asp:Panel ID="_pnlPostARoom" Width="950px" runat="server">
        <h1>
            Post A Room</h1>
        <h3>
            <strong>1. Address Information</strong>
        </h3>
        <div id="stylized" style="float: left; width: 950px !important;" class="userLoginForm">
            <div class="spacer">
            </div>
            <!-- Address Information -->
            <div style="float: left; width: 400px;">
                <fieldset>
                    <legend>Room Details</legend>
                    <ul>
                        <li>
                            <label for="_txtRoomNumber">
                                Room Number <span class="small">(Required)</span></label>
                            <telerik:RadTextBox ID="_txtRoomNumber" CssClass="inputStyle" runat="server" Width="205px">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="_rfvRoomNumber" runat="server" ControlToValidate="_txtRoomNumber"
                                ErrorMessage="*" ValidationGroup="CreateRoom"></asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <label for="_txtRoomTitle">
                                Title <span class="small">(Required)</span></label>
                            <telerik:RadTextBox ID="_txtRoomTitle" CssClass="inputStyle" runat="server" Width="205px">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="_rfvRoomTtitle" runat="server" ControlToValidate="_txtRoomTitle"
                                ErrorMessage="*" ValidationGroup="CreateRoom"></asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <label for="_txtMaxOccupancy">
                                Max Occupancy <span class="small">(Required)</span></label>
                            <telerik:RadNumericTextBox ID="_txtMaxOccupancy" runat="server" Type="Number" Width="205px"
                                CssClass="inputStyle">
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                            </telerik:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="_rfvMaxOccupancy" runat="server" ControlToValidate="_txtMaxOccupancy"
                                ErrorMessage="*" ValidationGroup="CreateRoom"></asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <label for="_cbRoomType">
                                Type<span class="small">(Required)</span></label>
                            <telerik:RadComboBox ID="_cbRoomType" runat="server" CssClass="inputStyle" Height="200px"
                                Skin="Office2007" MarkFirstMatch="true" Width="205px">
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="_cbRoomType"
                                InitialValue="(None)" ErrorMessage="*" ValidationGroup="CreateRoom"></asp:RequiredFieldValidator>
                        </li>
                    </ul>
                </fieldset>
            </div>
            <!-- End Address Information -->
            <!-- Building Information -->
            <div style="float: right; width: 400px;">
                <fieldset>
                    <legend>Building Information</legend>
                    <ul>
                        <li>Building - Select a previous building to prefill address information. </li>
                        <li>
                            <label for="_cbState">
                                Building</label>
                            <telerik:RadComboBox ID="_cbBuilding" runat="server" CssClass="inputStyle" Height="200px"
                                AutoPostBack="true" Skin="Office2007" MarkFirstMatch="true" Width="205px" OnSelectedIndexChanged="_cbBuilding_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </li>
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
                <!-- End State -->
            </div>
            <!-- End Building Information -->
            <div class="spacer">
            </div>
        </div>
        <br />
        <h3>
            <strong>2. Room Description</strong>
        </h3>
        <telerik:RadEditor ID="_rtxtContent" Width="950px" runat="server" SkinID="MinimalSetOfTools"
            ToolsFile="~/App_Data/BasicTools.xml" runat="server" EditModes="Design">
            <CssFiles>
                <telerik:EditorCssFile Value="~/Theme/EditorContentArea.css" />
            </CssFiles>
        </telerik:RadEditor>
        <br />
        <h3>
            <strong>3. Room Availability</strong>
        </h3>
        <uc1:ucFeedback ID="ucDateFeedBack" runat="server" />
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
        <telerik:RadGrid ID="_rgAvailableTimes" runat="server" OnItemDataBound="_rgAvailableTimes_OnItemDataBound" OnItemCommand="_rgAvailableTimes_ItemCommand"
            OnNeedDataSource="_rgAvailableTimes_NeedDataSource" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" Width="950px" ShowStatusBar="True" GridLines="None">
            <PagerStyle Mode="NumericPages" AlwaysVisible="true" />
            <MasterTableView Width="100%" CommandItemDisplay="Top">
                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToPdfButton="false" />
                <Columns>
                    <telerik:GridBoundColumn HeaderText="ID" UniqueName="ID" DataField="ID" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="StartDate" UniqueName="StartDate" DataField="StartDate"
                        DataFormatString="{0:MM/dd/yy}">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="StartTime" UniqueName="StartTime" DataField="StartTime">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="EndDate" UniqueName="EndDate" DataField="EndDate"
                        DataFormatString="{0:MM/dd/yy}">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="EndTime" UniqueName="EndTime" DataField="EndTime">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Days" UniqueName="Days" DataField="Days">
                    </telerik:GridBoundColumn>

                         <telerik:GridButtonColumn Text="Delete" CommandName="Delete" Visible="true" ButtonType="ImageButton">
                        <HeaderStyle Width="2%" />
                    </telerik:GridButtonColumn>
                </Columns>
                <PagerStyle AlwaysVisible="True" />
                <EditFormSettings>
                    <EditColumn ButtonType="ImageButton" />
                </EditFormSettings>
            </MasterTableView>
        </telerik:RadGrid>
        <br />
        <h3>
            <strong>4. Upload Images</strong>
        </h3>
        <telerik:RadUpload ID="RadUpload1" InitialFileInputsCount="1" MaxFileInputsCount="1"
            AllowedFileExtensions=".jpg,.jpeg,.png,.bmp" runat="server" />
        <div class="DivStyled">
            <fieldset style="border: 0px;">
                <ul>
                    <li>
                        <label for="_txtName">
                            Name:
                        </label>
                        <telerik:RadTextBox ID='_txtName' CssClass="inputStyle" runat="server">
                        </telerik:RadTextBox></li><li>
                            <label for="_txtDescription">
                                Description:
                            </label>
                            <telerik:RadTextBox ID="_txtDescription" CssClass="inputStyle" runat="server">
                            </telerik:RadTextBox></li><li>
                                <div class="submitArea">
                                    <telerik:RadButton runat="server" ID="_btnUploadImage" Text="Upload files" OnClick="_btnUploadImage_Click"  Width="150px" />
                                </div>
                            </li>
                </ul>
            </fieldset>
        </div> 
        <telerik:RadGrid runat="server" ID="_rgImages" AllowPaging="True" AllowSorting="True"  OnItemCommand="_rgImages_ItemCommand"
            AutoGenerateColumns="false" Width="950px" ShowStatusBar="True" GridLines="None"
            PageSize="3" OnNeedDataSource="_rgImages_NeedDataSource">
            <PagerStyle Mode="NumericPages" AlwaysVisible="true" />
            <MasterTableView Width="100%" CommandItemDisplay="none">
                <Columns>
                <telerik:GridBoundColumn DataField="ID" UniqueName="ID" Visible="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Title" UniqueName="Title" HeaderText="Title">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Description" UniqueName="Description" HeaderText="Description">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ImageUrl" UniqueName="ImageUrl" HeaderText="ImageUrl"
                        Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn UniqueName="Image">
                        <ItemTemplate>
                            <asp:Image ID="_imgGrid" runat="server" Height="50px" ImageUrl='<%#  String.Format("~/FilesTemp/{0}", Eval("ImageUrl")) %>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridButtonColumn Text="Delete" CommandName="Delete" Visible="true" ButtonType="ImageButton">
                        <HeaderStyle Width="2%" />
                    </telerik:GridButtonColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
        <br />
        <h3>
            <strong>5. Finalize</strong>
        </h3>
        <div id="styleized" style="float: left; width: 950px !important;" class="userLoginForm">
            <div class="spacer">
            </div>
            <telerik:RadButton ID="_btnCreateRoom" runat="server" Text="Create Room" OnClick="_btnCreateRoom_Click"  Width="150px"
                ValidationGroup="CreateRoom">
            </telerik:RadButton>
        </div>
    </asp:Panel>
    <asp:Panel ID="_pnlCreateNoAccount" Visible="false" runat="server">
        <asp:Image ID="_pnlImageNoAccount" runat="server" ImageUrl="~/Theme/Images/Demo Image.png" />
    </asp:Panel>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="_raProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="_rgAvailableTimes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="_rgAvailableTimes" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="_btnAddDates">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="_rgAvailableTimes" />
                    <telerik:AjaxUpdatedControl ControlID="ucFeedbackBasePage" />
                    <telerik:AjaxUpdatedControl ControlID="ucDateFeedBack" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
</asp:Content>
