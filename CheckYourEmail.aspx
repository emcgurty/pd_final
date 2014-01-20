<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" 
        AutoEventWireup="true" CodeFile="CheckYourEmail.aspx.cs" 
        Inherits="CheckYourEmail" Title="Check Your Email" 
        EnableEventValidation="false"

%>
<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id=divCheckYouremail runat=server>
        <table cellpadding=0 cellspacing=0>
            <tr>
                <td style="width: 100px">
                </td>
                <td colspan="3">
                    &nbsp;</td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                </td>
                <td colspan="3">
                <asp:Label ID="lblUser" runat="server" Text="" Width="353px" style="font-size: 16px; vertical-align: middle; font-family: Times New Roman, Times New Roman, Calibri, Arial; text-align: center"></asp:Label>
                </td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 27px">
                </td>
                <td colspan="3" style="height: 27px">
                </td>
                <td style="width: 100px; height: 27px">
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 27px;">
                </td>
                <td colspan="3" style="height: 27px">
                <asp:Label ID="lblAcknowledgeNewRegistration" runat="server" Text="Thank you for registering" Width="357px" style="font-size: 16px; vertical-align: middle; color: black; font-family: Times New Roman, Times New Roman, Calibri, Arial; text-align: center">
                </asp:Label>
                </td>
                <td style="width: 100px; height: 27px;">
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 28px;">
                </td>
                <td colspan="3" style="height: 28px">
                    <asp:Label ID="lblCheckYourEmail" runat="server" Text="Please check your email." Width="353px" style="font-size: 16px; vertical-align: middle; font-family: Times New Roman, Times New Roman, Calibri, Arial; text-align: center"></asp:Label></td>
                <td style="width: 100px; height: 28px;">
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 28px">
                </td>
                <td colspan="3" style="height: 28px">
                    &nbsp;</td>
                <td style="width: 100px; height: 28px">
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px; vertical-align: middle; text-align: center;">
                    <asp:LinkButton CssClass=ClickForMore ID="btnLogout" runat="server" Text="Logout" style="border-right: gray thin solid; border-top: gray thin solid; font-size: 16px; border-left: gray thin solid; color: black; border-bottom: gray thin solid; font-family: Times New Roman, Times New Roman, Calibri, Arial; background-color: transparent" Width="127px" OnClick="cmdLogoutUser_Click" />
                    
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
        </table>
  </div>  

</asp:Content>

