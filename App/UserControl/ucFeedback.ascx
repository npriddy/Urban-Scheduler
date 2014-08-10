<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="ucFeedback.ascx.cs"
    Inherits="UrbanSchedulerProject.App.UserControl.ucFeedback" %>
<div id="_ucFeedBackDiv">
    <asp:Panel ID="pnlFeedbackError" Visible="false" EnableViewState="false" runat="server">
        <div id='error' runat="server" class="error">
            <asp:Literal ID="litFeedbackError" runat="server" EnableViewState="false"></asp:Literal><br />
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlFeedbackSuccess" Visible="false" EnableViewState="false" runat="server"
        Style="font-family: Segoe UI,Arial,Helvetica,sans-serif !important; font-size: 12px !important;">
        <div id="success" runat="server" class="success">
            <asp:Literal ID="litFeedbackSuccess" runat="server" EnableViewState="false"></asp:Literal><br />
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlFeedbackWarning" Visible="false" EnableViewState="false" runat="server"
        Style="font-family: Segoe UI,Arial,Helvetica,sans-serif !important; font-size: 12px !important;">
        <div id="warning" runat="server" class="warning">
            <asp:Literal ID="litFeedbackWarning" runat="server" EnableViewState="false"></asp:Literal><br />
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlFeedbackInfo" Visible="false" EnableViewState="false" runat="server"
        Style="font-family: Segoe UI,Arial,Helvetica,sans-serif !important; font-size: 12px !important;">
        <div id="info" runat="server" class="info">
            <asp:Literal ID="litFeedbackInfo" runat="server" EnableViewState="false"></asp:Literal><br />
        </div>
    </asp:Panel>
</div>
<telerik:RadCodeBlock ID="_rcb" runat="server">
    <script type="text/javascript">

        function InjectError(error) {
            var litError = $find('#error');
            //$find("<%=litFeedbackError.ClientID%>");
            litError.html += error;
        }
    
    </script>
</telerik:RadCodeBlock>
