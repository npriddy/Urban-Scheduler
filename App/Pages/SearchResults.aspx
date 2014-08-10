<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="SearchResults.aspx.cs"   Inherits="UrbanSchedulerProject.App.SearchResults" %>
<asp:Content ContentPlaceHolderID="cMain" runat="server" ID="Content1">
    <telerik:RadAjaxPanel ID="_rjp1" runat="server">
        <telerik:RadTextBox ID="_txtSearchText" runat="server">
        </telerik:RadTextBox>
        <telerik:RadButton ID="_btnSearch" runat="server" Text="Search" OnClick="_btnSearch_Click">
        </telerik:RadButton>
        <asp:Label ID="_lblCords" runat="server" Text="Search"></asp:Label>
        <div id="map" style="width: 550px; height: 450px">
        </div>
    </telerik:RadAjaxPanel>
    <telerik:RadCodeBlock ID="_rcb1" runat="server">
        <script type="text/javascript">
    //<![CDATA[
            var ajaxmanager;

            $(document).ready(function () {
                if (ajaxmanager === null) {
                    ajaxmanager = $find("<%=RadAjaxManager.GetCurrent(Page).ClientID%>");
                }
                GUnload();
                loadGoogleMap();

            });



            function myUserControlClickHandler(addressInput) {
                //console.log(addressInput);
                latlng = searchLocations(addressInput);
                //window.location.href = window.location.href + "?latlong=" + latlng;



            }

            function finishClick(latlang) {
                // if (latlang !== null) {
                // var str = latlang.split(",");
                // console.log(str[0]);
                //console.log(str[1]);
                //}

                $find('ctl00_RadAjaxManager1').ajaxRequest(latlang);
            }
            var map;
            var geocoder;
            function searchLocations(addressInput) {
                //console.log("search");
                geocoder.getLatLng(addressInput, function (latlng) {
                    //alert(latlng);
                    if (!latlng) {
                        console.log("not found");
                        finishClick('not found');
                    } else {
                        console.log(latlng);
                        finishClick(latlng);  //searchLocationsNear(latlng);
                    }
                });
            }
            function loadGoogleMap() {
                if (GBrowserIsCompatible()) {
                    geocoder = new GClientGeocoder();
                    // A function to create the marker and set up the event window
                    // Dont try to unroll this function. It has to be here for the function closure
                    // Each instance of the function preserves the contends of a different instance
                    // of the "marker" and "html" variables which will be needed later when the event triggers.    
                    function createMarker(point, html) {
                        var marker = new GMarker(point);
                        GEvent.addListener(marker, "click", function () {
                            marker.openInfoWindowHtml(html);
                        });
                        return marker;
                    }

                    // Display the map, with some controls and set the initial location 
                    map = new GMap2(document.getElementById("map"));
                    map.addControl(new GLargeMapControl());
                    map.addControl(new GMapTypeControl());
                    map.setCenter(new GLatLng(43.907787, -79.359741), 8);

                    // Set up three markers with info windows 

                    var point = new GLatLng(43.65654, -79.90138);
                    var marker = createMarker(point, '<div style="width:240px">Some stuff to display in the First Info Window. With a <a href="http://www.econym.demon.co.uk">Link<\/a> to my home page<\/div>')
                    map.addOverlay(marker);

                    var point = new GLatLng(43.91892, -78.89231);
                    var marker = createMarker(point, 'Some stuff to display in the<br>Second Info Window')
                    map.addOverlay(marker);

                    var point = new GLatLng(43.82589, -79.10040);
                    var marker = createMarker(point, 'Some stuff to display in the<br>Third Info Window')
                    map.addOverlay(marker);

                }

                // display a warning if the browser was not compatible
                else {
                    alert("Sorry, the Google Maps API is not compatible with this browser");
                }
            }



            // This Javascript is based on code provided by the
            // Community Church Javascript Team
            // http://www.bisphamchurch.org.uk/   
            // http://econym.org.uk/gmap/

    //]]>
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="AjaxManagerProxy1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="_lblCords" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="_lblCords" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
</asp:Content>
