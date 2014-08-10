<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true"
    CodeBehind="MyAccount.aspx.cs" Inherits="UrbanSchedulerProject.App.Pages.MyAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cMain" runat="server">
    <ul>
        <li>
            <h1>
                My Account</h1>
        </li>
        <li>
            <h3>
                Pending Reservations</h3>
        </li>
        <li>
            <telerik:RadTabStrip ID="_rTabReservations" runat="server" AutoPostBack="True" OnTabClick="_rTabReservations_TabClick"
                SelectedIndex="0">
                <Tabs>
                    <telerik:RadTab Text="Open" Value="Open" Selected="True">
                    </telerik:RadTab>
                    <telerik:RadTab Text="Closed" Value="Closed">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadGrid ID="_rgReservations" runat="server" AllowPaging="True" AllowSorting="True"
                AllowFilteringByColumn="true" OnItemCreated="SharedFunction_ItemCreated" OnNeedDataSource="_rgReservations_NeedDataSource"
                OnItemCommand="_rgReservations_ItemCommand" AutoGenerateColumns="False" Width="965px"
                ShowStatusBar="True" GridLines="None">
                <ExportSettings IgnorePaging="true" OpenInNewWindow="true" FileName="Room Reservations">
                    <Pdf PageHeight="210mm" PageWidth="297mm" PageTitle="Room Reservations" DefaultFontFamily="Arial Unicode MS"
                        PageBottomMargin="20mm" PageTopMargin="20mm" PageLeftMargin="20mm" PageRightMargin="20mm" />
                </ExportSettings>
                <PagerStyle Mode="NumericPages" AlwaysVisible="true" />
                <MasterTableView Width="100%" CommandItemDisplay="Top">
                    <CommandItemSettings ShowExportToPdfButton="false" ShowAddNewRecordButton="false"
                        ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowExportToWordButton="true" />
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="ViewDetails" HeaderText="View" ItemStyle-HorizontalAlign="Center"
                            AllowFiltering="false">
                            <ItemTemplate>
                                <asp:ImageButton ID="_imgView" runat="server" ToolTip="View Details" CommandName="ViewDetails"
                                    CommandArgument='<%#Eval("id")%>' ImageUrl="~/Theme/Images/Icons/page_white_magnify.png" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="id" HeaderText="id" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RequestedDate" HeaderText="Requested Date">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Room.Number" HeaderText="Room Number">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Room.Building.Name" HeaderText="Room Number">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Room.RoomType.Name" HeaderText="Room Type">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="Approved" HeaderText="Approved" DataField="Approved">
                            <ItemTemplate>
                                <asp:Label ID="_lblThing" runat="server" Text='<%# ProcessMyDataItem(Eval("Approved")) %>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Approved" HeaderText="Approved" Visible="false">
                        </telerik:GridBoundColumn>
                    </Columns>
                    <PagerStyle AlwaysVisible="True" />
                    <EditFormSettings>
                        <EditColumn ButtonType="ImageButton" />
                    </EditFormSettings>
                </MasterTableView>
            </telerik:RadGrid>
        </li>
        <li>
            <h3>
                Up Comming Reserved Rooms</h3>
        </li>
        <li>
            <telerik:RadScheduler runat="server" ID="_rsReservations" Width="965px" DayStartTime="08:00:00"
                OnNavigationComplete="_rsReservations_OnNavigationComplete" ReadOnly="true" Height="600px"
                DayEndTime="18:00:00" OnAppointmentDataBound="_rsReservations_AppointmentDataBound"
                SelectedView="MonthView" DataKeyField="ID" DataSubjectField="Subject" DataStartField="Start"
                DataEndField="End">
                <AdvancedForm Modal="true" />
                <ExportSettings FileName="MyAppScheduler" OpenInNewWindow="True">
                    <Pdf AllowCopy="True" Author="MyApp" Creator="MyApp" PageHeight="297mm" PageWidth="210mm"
                        PaperSize="A4" Producer="MyApp" Subject="Scheduler" Title="Scheduler" />
                </ExportSettings>
                <TimelineView UserSelectable="false" />
                <TimeSlotContextMenuSettings EnableDefault="true" />
                <AppointmentContextMenuSettings EnableDefault="true" />
            </telerik:RadScheduler>
        </li>
        <li>
            <h3>
                My Rooms</h3>
        </li>
        <li id="MyRoom">
            <telerik:RadGrid ID="_rgMyRooms" runat="server" OnNeedDataSource="_rgMyRooms_NeedDataSource"
                OnItemCreated="_rgMyRooms_ItemCreated" OnItemCommand="_rgMyRooms_ItemCommand"
                AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                Width="965px" ShowStatusBar="True" GridLines="None">
                <ExportSettings IgnorePaging="true" OpenInNewWindow="true" FileName="My Rooms">
                    <Pdf PageHeight="210mm" PageWidth="297mm" PageTitle="My Rooms" DefaultFontFamily="Arial Unicode MS"
                        PageBottomMargin="20mm" PageTopMargin="20mm" PageLeftMargin="20mm" PageRightMargin="20mm" />
                </ExportSettings>
                <PagerStyle Mode="NumericPages" AlwaysVisible="true" />
                <MasterTableView Width="100%" CommandItemDisplay="Top">
                    <CommandItemSettings ShowExportToPdfButton="false" AddNewRecordText="Add New Room"
                        ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowExportToWordButton="true" />
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="ViewDetails" HeaderText="View" ItemStyle-HorizontalAlign="Center"
                            AllowFiltering="false">
                            <ItemTemplate>
                                <asp:ImageButton ID="_imgView" runat="server" ToolTip="View Details" CommandName="ViewDetails"
                                    CommandArgument='<%#Eval("id")%>' ImageUrl="~/Theme/Images/Icons/page_white_magnify.png" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="EditDetails" HeaderText="Edit" ItemStyle-HorizontalAlign="Center"
                            AllowFiltering="false">
                            <ItemTemplate>
                                <asp:ImageButton ID="_imgEdit" runat="server" ToolTip="Edit Details" CommandName="EditDetails"
                                    CommandArgument='<%#Eval("id")%>' ImageUrl="~/Theme/Images/Icons/table_edit.png" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="id" HeaderText="id" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Title" HeaderText="Title">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Number" HeaderText="Room Number">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Description" HeaderText="Description">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MaxOccupancy" HeaderText="Max Occupancy">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RoomType.Name" HeaderText="Room Type">
                        </telerik:GridBoundColumn>
                    </Columns>
                    <PagerStyle AlwaysVisible="True" />
                    <EditFormSettings>
                        <EditColumn ButtonType="ImageButton" />
                    </EditFormSettings>
                </MasterTableView>
            </telerik:RadGrid>
        </li>
        <li id="BuildingsHeader">
            <h3>
                My Buildings</h3>
        </li>
        <li id="BuildingsGrid">
            <telerik:RadGrid ID="_rgMyBuildings" runat="server" OnNeedDataSource="_rgMyBuildings_NeedDataSource"
                OnItemCreated="SharedFunction_ItemCreated" OnItemCommand="_rgMyBuildings_ItemCommand"
                AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                Width="965px" ShowStatusBar="True" GridLines="None">
                <ExportSettings IgnorePaging="true" OpenInNewWindow="true" FileName="My Buildings">
                    <Pdf PageHeight="210mm" PageWidth="297mm" PageTitle="My Buildings" DefaultFontFamily="Arial Unicode MS"
                        PageBottomMargin="20mm" PageTopMargin="20mm" PageLeftMargin="20mm" PageRightMargin="20mm" />
                </ExportSettings>
                <PagerStyle Mode="NumericPages" AlwaysVisible="true" />
                <MasterTableView Width="100%" CommandItemDisplay="Top">
                    <CommandItemSettings ShowExportToPdfButton="false" AddNewRecordText="Add New Building"
                        ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowExportToWordButton="true" />
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="ViewDetails" HeaderText="View" ItemStyle-HorizontalAlign="Center"
                            AllowFiltering="false">
                            <ItemTemplate>
                                <asp:ImageButton ID="_imgEdit" runat="server" ToolTip="View Details" CommandName="ViewDetails"
                                    CommandArgument='<%#Eval("id")%>' ImageUrl="~/Theme/Images/Icons/page_white_magnify.png" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="id" HeaderText="id" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Name" HeaderText="Name">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PrimaryAddress" HeaderText="Primary Address">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="SecondaryAddress" HeaderText="Secondary Address">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="City" HeaderText="City">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="State" HeaderText="State">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Zip" HeaderText="Zip">
                        </telerik:GridBoundColumn>
                    </Columns>
                    <PagerStyle AlwaysVisible="True" />
                    <EditFormSettings>
                        <EditColumn ButtonType="ImageButton" />
                    </EditFormSettings>
                </MasterTableView>
            </telerik:RadGrid>
        </li>
        <li>
            <h3>
                Messages</h3>
        </li>
        <li>
            <telerik:RadGrid ID="_rgMessages" runat="server" OnNeedDataSource="_rgMessages_NeedDataSource"
                OnItemCreated="SharedFunction_ItemCreated" OnItemCommand="_rgMessages_ItemCommand"
                AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                Width="965px" ShowStatusBar="True" GridLines="None">
                <ExportSettings IgnorePaging="true" OpenInNewWindow="true" FileName="My Messages">
                    <Pdf PageHeight="210mm" PageWidth="297mm" PageTitle="My Messages" DefaultFontFamily="Arial Unicode MS"
                        PageBottomMargin="20mm" PageTopMargin="20mm" PageLeftMargin="20mm" PageRightMargin="20mm" />
                </ExportSettings>
                <PagerStyle Mode="NumericPages" AlwaysVisible="true" />
                <MasterTableView Width="100%" CommandItemDisplay="Top">
                    <CommandItemSettings ShowExportToPdfButton="false" ShowAddNewRecordButton="false"
                        ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowExportToWordButton="true" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="Subject" HeaderText="Subject">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DatePosted" HeaderText="Date Posted">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Message" HeaderText="Message">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="building.Title" HeaderText="Building">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="room.Number" HeaderText="Room">
                        </telerik:GridBoundColumn>
                    </Columns>
                    <PagerStyle AlwaysVisible="True" />
                    <EditFormSettings>
                        <EditColumn ButtonType="ImageButton" />
                    </EditFormSettings>
                </MasterTableView>
            </telerik:RadGrid>
        </li>
    </ul>
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

        function printScheduler()
        {
            PopupCenter("/App/Popup/SchedulePrint.aspx?type=myrooms", 500, 920);
        }
        function pageLoad()
        {
            var $ = $telerik.$;
            $(" <li><a onclick='printScheduler()'><span>Print</span></a></li>").appendTo($('.rsHeader ul'));
        }

    </script>
</asp:Content>
