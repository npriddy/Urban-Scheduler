<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UrbanSchedulerProject.Default"
    MasterPageFile="~/MasterPages/Site.Master" %>

<asp:Content ContentPlaceHolderID="cMain" ID="Content1" runat="server">
    <script>

        if (isMobile())
        {
            $(mobileWarning).css("display", "inline");
        }

    </script>
    <div id="mobileWarning" style="display: none;">
        Popups may be automatically blocked on mobile devices please check these settings.
    </div>
    <div style="float: left; margin-right: 30px;">
        <h1>
            Making Posting and Finding Rooms Easier!</h1>
    </div>
    <br />
    <div style="float: left; width: 460px; margin-right: 30px;">
        <label>
            Post/Find/Reserve Rooms</label>
        <br />
        <br />
        <br />
        <h3>
            Rooms Available</h3>
        <h2>
            <asp:Label ID="_lblReservedRooms" runat='server'></asp:Label></h2>
        <br />
        <h3>
            Room Reservations Made</h3>
        <h2>
            <asp:Label ID="_lblRoomsAvail" runat='server'></asp:Label></h2>
    </div>
    <div style="float: left; width: 451px;">
        <p>
            <img src="/Theme/Images/GoogleMapExample.jpg" style="border: 1px solid;" alt="" />
        </p>
    </div>
    <br />
    <div style="padding-left: 50px">
        <a href="/App/Pages/FindARoom.aspx">
            <div style="float: left; margin-right: 30px;">
                <div class="HomePostRoom" style="width: 200px">
                    <h2 style="height: 80px; font-size: 1.5em;">
                        Find
                        <br />
                        Room<br />
                        Now</h2>
                </div>
            </div>
        </a>
    </div>
    <div style="float: left; text-align: left">
        <a href="/App/Pages/PostARoom.aspx">
            <div class="HomeFindRoom" style="width: 200px">
                <h2 style="height: 80px; font-size: 1.5em;">
                    Post
                    <br />
                    A<br />
                    Room</h2>
            </div>
        </a>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cSecondary" runat="server" ID="contentSecondary"
    Style="height: 100px">
    <br />
    <asp:Panel ID="_pnlContent" runat="server" Width="900px" BorderStyle="Solid" BorderWidth="1px"
        Style="padding: 0 0 0 0" BackColor="White">
        <div style="float: left; width: 150px; margin-right: 30px; margin-left: 200px">
            <h3>
                Find Rooms</h3>
            <p>
                Search for rooms by city / address / zip.
            </p>
        </div>
        <div style="float: left; width: 150px; margin-right: 30px;">
            <h3>
                Post Rooms</h3>
            <p>
                Create room information, post availability, upload floor plans.
            </p>
        </div>
        <div style="float: left; width: 150px; margin-right: 30px;">
            <h3>
                Reserve Rooms</h3>
            <p>
                Reserve rooms found for prefered times with our unique sechduling solutions.
            </p>
        </div>
    </asp:Panel>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
