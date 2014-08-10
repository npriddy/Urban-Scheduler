<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true"
    CodeBehind="FindARoom.aspx.cs" Inherits="UrbanSchedulerProject.App.Pages.FindARoom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cMain" runat="server">
    <ul>
        <li>
            <div class="DivStyled">
                <fieldset style="border: 0;">
                    <ul>
                        <li>
                            <label for="_txtZip">
                                Zip</label>
                            <telerik:RadTextBox ID="_txtZip" CssClass="inputStyle" runat="server" Width="205px">
                            </telerik:RadTextBox>
                        </li>
                        <li>
                            <label for="_cbState">
                                State</label>
                            <telerik:RadComboBox ID="_cbState" runat="server" CssClass="inputStyle" Height="200px" AutoPostBack="true"
                                Skin="Office2007" MarkFirstMatch="true" Width="205px" 
                                onselectedindexchanged="_cbState_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </li>
                        <li>
                            <label for="_cbCity">
                                City</label>
                            <telerik:RadComboBox ID="_cbCity" runat="server" MarkFirstMatch="true" DataValueField="value"
                                DataTextField="text" CssClass="inputStyle" Height="200px" Skin="Office2007" Width="205px">
                            </telerik:RadComboBox>
                            <telerik:RadButton ID="_btnViewAllCities" runat="server" Text="View City Locations"  Width="150px"
                                OnClick="_btnViewAllCities_Click">
                            </telerik:RadButton>
                        </li>
                        <li> <label for="_rdpStart">
                                Start Date</label>
                            <telerik:RadDatePicker ID='_rdpStart' runat="server">
                            <DateInput CssClass="inputStyle"></DateInput>
                            </telerik:RadDatePicker>
                        </li>
                        <li> <label for="_rdpEnd">
                                End Date</label>
                           <telerik:RadDatePicker ID='_rdpEnd' runat="server">
                            <DateInput CssClass="inputStyle"></DateInput>
                            </telerik:RadDatePicker>
                        </li>
                        <li>
                            <telerik:RadButton ID="_btnSearch" runat="server" Text="Search" OnClick="_btnSearch_Click"
                                Width="150px">
                            </telerik:RadButton>
                        </li>
                    </ul>
                </fieldset>
            </div>
        </li>
        <li>
            <table width="100%">
                <tr>
                    <td>
                        <h3>
                            Search Results
                        </h3>
                    </td>
                    <td>
                        <div style="float: right">
                            <telerik:RadButton ID="_btnViewResultsOnMap" runat="server" Text="View Results On Map"
                                Width="150px" OnClick="_viewResultsOnMap_Click">
                            </telerik:RadButton>
                        </div>
                    </td>
                </tr>
            </table>
        </li>
        <li>
            <telerik:RadGrid ID="_rgSearchResults" runat="server" OnNeedDataSource="_rgSearchResults_NeedDataSource"
                OnItemCreated="_rgSearchResults_ItemCreated" OnItemDataBound="_rgSearchResults_ItemDataBound"
                OnItemCommand="_rgSearchResults_ItemCommand" AllowPaging="True" AllowSorting="True"
                AllowFilteringByColumn="true" AutoGenerateColumns="False" Width="960px" ShowStatusBar="True"
                GridLines="None">
                <PagerStyle Mode="NumericPages" AlwaysVisible="true" />
                <ExportSettings IgnorePaging="true" OpenInNewWindow="true" FileName="Search Results">
                    <Pdf PageHeight="210mm" PageWidth="297mm" PageTitle="Room Results" DefaultFontFamily="Arial Unicode MS"
                        PageBottomMargin="20mm" PageTopMargin="20mm" PageLeftMargin="20mm" PageRightMargin="20mm" />
                </ExportSettings>
                <MasterTableView Width="100%" CommandItemDisplay="Top">
                    <CommandItemSettings ShowExportToPdfButton="false" ShowAddNewRecordButton="false"  ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowExportToWordButton="true" >
                    </CommandItemSettings>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="ViewDetails" AllowFiltering="false" HeaderText="View" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="_imgEdit" runat="server" ToolTip="View Details" CommandName="ViewDetails"
                                    CommandArgument='<%#Eval("id")%>' ImageUrl="~/Theme/Images/Icons/page_white_magnify.png" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="id" HeaderText="id" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Title" HeaderText="Title">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Building.PrimaryAddress" HeaderText="Address">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Building.City" HeaderText="City">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Building.State" HeaderText="State">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Building.Zip" HeaderText="Zip">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="ViewLocation" ItemStyle-Width="80px" AllowFiltering="false" HeaderText="View On Map"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="_imgLocation" runat="server" ToolTip="View Location" CommandName="Location"
                                    CommandArgument='<%#Eval("id")%>' ImageUrl="~/Theme/Images/Icons/map_go.png" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
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
            <telerik:AjaxSetting AjaxControlID="_cbState">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="_cbCity" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <style>
        ul
        {
            list-style: none;
        }
        ul li
        {
            padding-bottom: 5px;
        }
    </style>
    <script type="text/javascript">

        function ShowAddressLocation(address, centerOnUs)
        {
            PopupCenter("Map.htm?MarkerOpen=true&address=" + address + "&centerOnUs=" + centerOnUs, "Address Information", 975, 850);
            return false;
        }


        function ShowResults(address, centerOnUs)
        {
            PopupCenter("Map.htm?MarkerOpen=false&address=" + address + "&centerOnUs=" + centerOnUs, "Address Information", 975, 850);
            return false;
        }


    </script>
</asp:Content>
