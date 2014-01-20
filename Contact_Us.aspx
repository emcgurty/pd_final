<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" 
         AutoEventWireup="true" CodeFile="Contact_Us.aspx.cs" 
         Inherits="Contact_Us" Title="Thanks!" 
         EnableEventValidation="false"
         %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table cellpadding=0 cellspacing=0>
    <tr>
        <td style="width: 6%; height: 21px">
        </td>
        <td style="width: 10%; height: 21px">
        </td>
        <td style="width: 10%; height: 21px">
        </td>
    </tr>
    <tr>
        <td colspan="3" style="vertical-align: middle; height: 21px; text-align: left; color: green;">
            &nbsp;
            CG welcomes your comments.&nbsp;</td>
    </tr>
    <tr>
        <td colspan="3" style="vertical-align: middle; height: 21px; text-align: left; color: green;">
            &nbsp;
            You will receive email confirmation of your contribution.</td>
    </tr>
    <tr>
        <td style="width: 6%; height: 21px">
        </td>
        <td style="width: 10%; height: 21px">
        </td>
        <td style="width: 10%; height: 21px">
        </td>
    </tr>
    <tr>
        <td style="width: 6%; height: 19px; vertical-align: middle; text-align: right;">
            <asp:Label ID="lblUserFirstNameLogout" runat="server" Text="Your full name:"></asp:Label>&nbsp;</td>
        <td colspan="2" style="height: 19px">
            <asp:Label ID="lblFullName1" runat="server" Text="lblFullName1" Width="362px"></asp:Label>&nbsp;</td>
    </tr>
    <tr>
        <td style="width: 6%; vertical-align: middle; text-align: right;">
            <asp:Label ID="lblLastName1" runat="server" Text="Your company:"></asp:Label>&nbsp;</td>
        <td colspan="2">
            <asp:Label ID="lblCompanyName1" runat="server" Text="lblCompanyName1" Width="364px"></asp:Label>&nbsp;</td>
    </tr>
    <tr>
        <td style="width: 6%; vertical-align: middle; text-align: right;">
            &nbsp;<asp:Label ID="Label1" runat="server" Text="Your email:"></asp:Label>&nbsp;</td>
        <td colspan="2">
            <asp:Label ID="lblUserEmail" runat="server" Width="362px"></asp:Label></td>
    </tr>
    <tr>
        <td style="vertical-align: middle; width: 6%; text-align: right">
        </td>
        <td style="width: 10%">
            &nbsp;</td>
        <td style="width: 10%">
        </td>
    </tr>
    <tr>
        <td style="width: 6%; vertical-align: middle; text-align: right;">
            <asp:Label ID="Label4" runat="server" Text="Nature of your question:" Width="164px" style="vertical-align: middle; text-align: right"></asp:Label>&nbsp;</td>
        <td style="width: 10%">
            <asp:DropDownList ID="cmbComments" runat="server" Width="230px">
                <asp:ListItem>Great Site</asp:ListItem>
                <asp:ListItem>Requestion Additional Information</asp:ListItem>
                <asp:ListItem Selected=True>Feedback</asp:ListItem>
                <asp:ListItem>Submit Correction</asp:ListItem>
            </asp:DropDownList></td>
        <td style="width: 10%">
        </td>
    </tr>
    <tr>
        <td style="width: 6%">
            &nbsp;</td>
        <td style="width: 10%">
            &nbsp;</td>
        <td style="width: 10%">
        </td>
    </tr>
    <tr>
        <td style="width: 6%; vertical-align: middle; text-align: right;" rowspan="4">
            <asp:Label ID="Label5" runat="server" Text="Comments:" style="vertical-align: top; text-align: right"></asp:Label>&nbsp;</td>
        <td colspan="2" rowspan="4">
            <asp:TextBox ID="txtUserComments" runat="server" Height="64px" Width="294px" TextMode="MultiLine"></asp:TextBox></td>
    </tr>
    <tr>
    </tr>
    <tr>
    </tr>
    <tr>
    </tr>
    <tr>
        <td style="width: 6%">
            &nbsp;</td>
        <td colspan="2">
            <asp:Label ID="lblProcessing" runat="server" Style="font-size: 14px; color: red;
                font-family: Times New Roman, Calibri, Arial; vertical-align: top; text-align: left;" Text="Label" Visible="False" Width="332px"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 6%; height: 36px">
            </td>
        <td colspan="2" style="height: 36px">
            <asp:LinkButton CssClass=ClickForMore ID="btnSubmitComments" runat="server" Text="Submit Comments" OnClick="btnSubmitComments_Click" style="vertical-align: middle; text-align: center" Height="19px" Width="147px" /></td>
    </tr>
    <tr>
        <td style="width: 6%; height: 26px">
        </td>
        <td colspan="2" style="height: 26px">
        </td>
    </tr>
</table>

</asp:Content>

