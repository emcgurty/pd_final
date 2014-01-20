<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" 
         AutoEventWireup="true" CodeFile="ErrorMessage.aspx.cs" 
         Inherits="ErrorMessage" Title="Error Message" 
         EnableEventValidation="false"
         %>
         
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 556px">
        <tr>
            <td style="width: 2%">
            </td>
            <td style="width: 80%">
            </td>
            <td style="width: 262px">
            </td>
        </tr>
        <tr>
            <td style="width: 2%">
            </td>
            <td style="width: 80%; color: black; font-family: Times New Roman, Calibri, Arial; text-align: center">
                The following event has occurred within this Application</td>
            <td style="width: 262px">
            </td>
        </tr>
        <tr>
            <td style="width: 2%">
            </td>
            <td style="width: 80%">
            </td>
            <td style="width: 262px">
            </td>
        </tr>
        <tr>
            <td style="width: 2%">
            </td>
            <td style="width: 80%">
                <asp:Label ID="lblErrorMessage" runat="server" Height="234px" Width="437px" style="font-size: 16px; vertical-align: middle; color: blue; font-family: Times New Roman, Calibri, Arial; text-align: center"></asp:Label></td>
            <td style="width: 262px">
            </td>
        </tr>
        <tr>
            <td style="width: 2%">
            </td>
            <td style="width: 80%">
            </td>
            <td style="width: 262px">
            </td>
        </tr>
        <tr>
            <td style="width: 2%">
            </td>
            <td style="width: 80%; text-align: center">
                Sorry for the inconvenience.</td>
            <td style="width: 262px">
            </td>
        </tr>
        
    </table>

</asp:Content>

