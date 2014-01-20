<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    EnableEventValidation="false"
    CodeFile="Forgot.aspx.cs" Inherits="Forgot" Title="Forgot" 
%>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div >
  
       <table style="width: 86%">
           <tr>
               <td style="width: 2%; height: 59px;">
               </td>
               <td colspan="3" style="font-size: 16px; vertical-align: top; color: green; font-family: Times New Roman, Calibri, Arial; height: 59px; text-align: left">
                   <asp:Label ID="lblInformation" runat="server" Text="Please enter the following details, which you provided at registration.  If they are found on the CG database, your user name and a randomly generated password will be sent to your email address. This process will involve an end of your current browser session." style="vertical-align: top; text-align: left" Width="626px"></asp:Label></td>
           </tr>
            <tr>
                <td style="width: 2%">
                    &nbsp;</td>
                <td style="width: 6%">
                    &nbsp;</td>
                <td style="width: 7%">
                    &nbsp;</td>
                <td style="width: 3%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 2%; height: 1px;">
                </td>
                <td style="width: 6%; height: 1px; vertical-align: middle; text-align: right;">
                    
        <asp:label id="lblFirstName" runat="server" text="Your first name:" Height="14px" Width="184px" Font-Size="XX-Small" style="font-size: 16px; font-family: Times New Roman, Calibri, Arial; vertical-align: middle; text-align: right; position: static;"></asp:label>
        </td>
                <td style="width: 7%; height: 1px;">
                    <asp:TextBox ID="txtFirstName" runat="server" Width="213px" style="vertical-align: middle; text-align: left" MaxLength="50"></asp:TextBox></td>
                <td style="width: 3%; height: 1px;">
                </td>
            </tr>
           <tr>
               <td style="width: 2%; height: 1px">
               </td>
               <td style="width: 6%; height: 1px; vertical-align: middle; text-align: right;">
                   <asp:Label ID="lblLastName" runat="server" Font-Size="XX-Small" Height="14px" Style="font-size: 16px;
                       vertical-align: middle; font-family: Times New Roman, Calibri, Arial; text-align: right" Text="Your last name:"
                       Width="183px"></asp:Label></td>
               <td style="width: 7%; height: 1px">
                    <asp:TextBox ID="txtLastName" runat="server" Width="213px" MaxLength="50" ></asp:TextBox></td>
               <td style="width: 3%; height: 1px">
               </td>
           </tr>
           <tr>
               <td style="width: 2%; height: 1px">
               </td>
               <td style="width: 6%; height: 1px; vertical-align: middle; text-align: right;">
                   <asp:Label ID="lblEmail" runat="server" Font-Size="XX-Small" Height="14px" Style="font-size: 16px;
                       vertical-align: middle; font-family: Times New Roman, Calibri, Arial; text-align: right" Text="Your email address:"
                       Width="184px"></asp:Label></td>
               <td colspan="2" style="height: 1px">
                   <asp:TextBox ID="txtemail" runat="server" Style="vertical-align: middle;
                       text-align: left" Width="455px" MaxLength="100"></asp:TextBox></td>
           </tr>
            
            <tr>
                <td style="width: 2%">
                    &nbsp;</td>
                <td style="width: 6%">
                    &nbsp;</td>
                <td style="width: 7%">
                    &nbsp;<asp:Label ID="lblResponse" runat="server" Style="font-size: 14px; color: red;
                        font-family: Times New Roman, Calibri, Arial" Text="Your name and email were not found on the database, please try again."
                        Visible="False" Width="416px"></asp:Label></td>
                <td style="width: 3%">
                    &nbsp;</td>
            </tr>
           <tr>
               <td style="width: 2%">
               </td>
               <td style="width: 6%">
                   &nbsp;</td>
               <td style="width: 7%">
                   &nbsp;</td>
               <td style="width: 3%">
               </td>
           </tr>
            <tr>
                <td style="width: 2%; height: 21px">
                </td>
                <td style="width: 6%; height: 21px">
                </td>
                <td style="width: 7%; height: 21px">
        <asp:LinkButton CssClass=ClickForMore id="cmdForgot" runat="server" text="Submit" OnClick="cmdSubmit_Click" Width="81px" Font-Names="Arial" Font-Size="XX-Small" BackColor="#FFFFFF" ForeColor="#CC0033" style="text-align:center; border-right: gray thin solid; border-top: gray thin solid; border-left: gray thin solid; border-bottom: gray thin solid; background-color: white; color: black; font-size: 16px; font-family: Times New Roman, Calibri, Arial;" /></td>
                <td style="width: 3%; height: 21px">
                </td>
            </tr>
            <tr>
                <td style="width: 2%; height: 21px">
                </td>
                <td style="width: 6%; height: 21px">&nbsp;
                </td>
                <td style="width: 7%; height: 21px">&nbsp;
                   
                         </td>
                <td style="width: 3%; height: 21px">&nbsp;
                </td>
            </tr>
        </table> 
        </div>
  
      


</asp:Content>

