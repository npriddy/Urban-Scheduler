<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditRoomDetails.aspx.cs"
    MasterPageFile="~/MasterPages/Site.Master" Inherits="UrbanSchedulerProject.App.Pages.EditRoomDetails" %>

<%@ Register Src="~/App/UserControl/ucFeedback.ascx" TagName="ucFeedback" TagPrefix="uc1" %>
<asp:Content ID="_content1" ContentPlaceHolderID="cMain" runat="server">
    <div class="DivStyled">
        <telerik:RadButton ID="_btnBack" Width="150px" runat="server" Text="Back" OnClick="_btnBack_Click">
        </telerik:RadButton>
        <telerik:RadButton ID="_btnSave" Width="150px" runat="server" Text="Save Room" OnClick="_btnSaveRoom_Click">
        </telerik:RadButton>
    </div>
    <h1>
        Edit Room Details</h1>
    <h3>
        <strong>1. Room Location</strong>
    </h3>
    <br />
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
        <div style="float: right; width: 400px;">
            <fieldset>
                <legend>Building Information</legend>
                <ul>
                    <li>
                        <label for="_cbBuilding">
                            Building</label>
                        <telerik:RadComboBox ID="_cbBuilding" runat="server" CssClass="inputStyle" Height="200px"
                            AutoPostBack="true" Skin="Office2007" MarkFirstMatch="true" Width="205px">
                        </telerik:RadComboBox>
                    </li>
                </ul>
            </fieldset>
            <!-- End State -->
        </div>
    </div>
    <br />
    <h3>
        <strong>2. Room Description</strong>
    </h3>
    <telerik:RadEditor ID="_rContent" Width="950px" runat="server" Height="200px" SkinID="MinimalSetOfTools"
        ToolsFile="~/App_Data/BasicTools.xml" EditModes="Design">
        <CssFiles>
            <telerik:EditorCssFile Value="~/Theme/EditorContentArea.css" />
        </CssFiles>
    </telerik:RadEditor>
    <br />
    <h3>
        <strong>3. Room Availability</strong>
    </h3>
    <uc1:ucFeedback ID="ucDateFeedBack" runat="server" />
    <telerik:RadButton ID="_btnAddDates" runat="server" Text="Modify / Add Dates" OnClick="_btnAddDates_Click"
        Width="150px">
    </telerik:RadButton>
    <telerik:RadButton ID="_btnExportSchedule" runat="server" Visible="false" Text="Export to PDF" OnClick="_btnExportSchedule_Click"
        Width="150px">
    </telerik:RadButton>
    <asp:Panel ID="_pnlDates" Visible="false" runat="server">
        <telerik:RadGrid ID="_rgAvailableTimes" runat="server" OnItemDataBound="_rgAvailableTimes_OnItemDataBound"
            OnNeedDataSource="_rgAvailableTimes_NeedDataSource">
            <MasterTableView AutoGenerateColumns="false">
                <Columns>
                    <telerik:GridBoundColumn HeaderText="ID" UniqueName="ID" DataField="ID" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="StartDate" UniqueName="StartDate" DataFormatString="{0:d}"
                        DataField="StartDate">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="StartTime" UniqueName="StartTime" DataFormatString="{0:hh}"
                        DataField="StartTime">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="EndDate" UniqueName="EndDate" DataFormatString="{0:d}"
                        DataField="EndDate">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="EndTime" UniqueName="EndTime" DataFormatString="{0:hh}"
                        DataField="EndTime">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Days" UniqueName="Days" DataField="Days">
                    </telerik:GridBoundColumn>
                    <telerik:GridButtonColumn Text="Delete" CommandName="Delete" Visible="true" ButtonType="ImageButton">
                        <HeaderStyle Width="2%" />
                    </telerik:GridButtonColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </asp:Panel>
    <telerik:RadScheduler runat="server" ID="_rsReservations" Width="970px" DayStartTime="08:00:00"
        OnNavigationComplete="_rsReservations_OnNavigationComplete" ReadOnly="true" Height="600px"
        DayEndTime="18:00:00" OnAppointmentInsert="_rsReservations_AppointmentInsert"
        OnAppointmentDataBound="_rsReservations_AppointmentDataBound" OnAppointmentUpdate="_rsReservations_AppointmentUpdate"
        OnAppointmentDelete="_rsReservations_AppointmentDelete" SelectedView="MonthView"
        DataKeyField="ID" DataSubjectField="Subject" DataStartField="Start" DataEndField="End">
        <AdvancedForm Modal="true" />
        <ExportSettings FileName="MyAppScheduler" OpenInNewWindow="True">
            <Pdf AllowCopy="True" Author="MyApp" Creator="MyApp" PageHeight="297mm" PageWidth="210mm"
                PaperSize="A4" Producer="MyApp" Subject="Scheduler" Title="Scheduler" />
        </ExportSettings>
        <TimelineView UserSelectable="false" />
        <TimeSlotContextMenuSettings EnableDefault="true" />
        <AppointmentContextMenuSettings EnableDefault="true" />
    </telerik:RadScheduler>
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
                                <telerik:RadButton runat="server" ID="_btnUploadImage" Text="Upload files" OnClick="_btnUploadImage_Click"
                                    Width="150px" />
                            </div>
                        </li>
            </ul>
        </fieldset>
    </div>
    <telerik:RadGrid runat="server" ID="_rgImages" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="false" Width="960px" ShowStatusBar="True" GridLines="None"
        PageSize="3" OnNeedDataSource="_rgImages_NeedDataSource">
        <PagerStyle Mode="NumericPages" AlwaysVisible="true" />
        <MasterTableView Width="100%" CommandItemDisplay="none">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="ViewDetails" HeaderText="View">
                    <ItemTemplate>
                        <asp:ImageButton ID="_imgEdit" runat="server" ToolTip="View Details" CommandName="ViewDetails"
                            CommandArgument='<%#Eval("id")%>' ImageUrl="~/Theme/Images/Icons/page_white_magnify.png" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Title" UniqueName="Title" HeaderText="Title">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Description" UniqueName="Description" HeaderText="Description">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FileFiles.FilesName" UniqueName="ImageUrl" HeaderText="ImageUrl"
                    Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="Image">
                    <ItemTemplate>
                        <asp:Image ID="_imgGrid" runat="server" Height="50px" ImageUrl='<%#  String.Format("~/Files/{0}", Eval("FileFiles.ServerFileName")) %>' />
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
    <div class="DivStyled">
        <telerik:RadButton ID="_btnSaveRoom" runat="server" Text="Save Room" OnClick="_btnSaveRoom_Click"
            Width="150px" ValidationGroup="SaveRoom">
        </telerik:RadButton>
    </div>
    <telerik:RadAjaxManagerProxy ID="_raProxy">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="_rsReservations">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="_rsReservations" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <script type="text/javascript">
        function openCreateDates(roomId)
        {

            $.fn.colorbox({
                href: "/App/Popup/pAddScheduleDates.aspx?roomId=" + roomId,
                iframe: true,
                preloading: true,
                width: "1000px",
                height: "970px",
                fastIframe: true,
                scrolling: false
            });
        }
            //Put your JavaScript code here.
            function printScheduler()
            {
                PopupCenter("/App/Popup/SchedulePrint.aspx?type=roomdetails&roomId=" + getQuerystring("roomId"), 500, 920);
            }
            function pageLoad()
            {
                var $ = $telerik.$;
                $(" <li><a onclick='printScheduler()'><span>Print</span></a></li>").appendTo($('.rsHeader ul'));
            }

        
    </script>
    <style>
        ul
        {
            list-style: none;
        }
    </style>
</asp:Content>
