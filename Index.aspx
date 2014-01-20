<%@ Page Language="C#" 
            MasterPageFile="~/MasterPage.master" 
            AutoEventWireup="true" 
            CodeFile="Index.aspx.cs" 
            Inherits="Index" Title="Welcome Page" 
            EnableEventValidation="false"

%>


<%@ MasterType VirtualPath="~/MasterPage.master" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<% Response.CacheControl = "private"; %>
<% Response.AddHeader ("Pragma", "no-cache") ;%>
<% Response.Expires = 0; %>
<% Session.Timeout = 20; %>

      
       <table  cellspacing=0 cellpadding=0>
           <tr>
               <td style="width: 15px">
               </td>
               <td colspan="4" style="vertical-align: middle; text-align: left; width: 664px;">
                   <asp:Label ID="lblWelcome" runat="server" Style="font-size: 14px; color: green; font-family: Times New Roman, Calibri, Arial"
                       Text="Welcome to the CG PD Search Site."
                       Width="577px"></asp:Label></td>
           </tr>
           <tr>
               <td style="width: 15px">
               </td>
               <td colspan="4" style="vertical-align: middle; text-align: left; width: 664px;">
                   &nbsp;</td>
           </tr>
           <tr>
               <td style="width: 15px">
               </td>
               <td colspan="4" style="vertical-align: middle; text-align: left; width: 664px;">
                   <asp:Label ID="Label1" runat="server" Style="font-size: 14px; color: green; font-family: Times New Roman, Calibri, Arial"
                       Text="Application Instructions: " Width="577px"></asp:Label></td>
           </tr>
           <tr>
               <td style="width: 15px">
               </td>
               <td colspan="4" style="vertical-align: middle; text-align: left; width: 664px;">
                   &nbsp;</td>
           </tr>
           <tr>
               <td style="width: 15px">&nbsp;
               </td>
               <td colspan="4" style="vertical-align: middle; text-align: left; width: 664px;">
                   <asp:Label ID="Label6" runat="server" Style="font-size: 14px; color: green; font-family: Times New Roman, Calibri, Arial"
                       Text="1.) From the Application menu you have the option to improve your accessibility by increasing or decreasing text size. This application is best viewed at resolution 1024 by 768 pixels."
                       Width="577px"></asp:Label></td>
           </tr>
           <tr>
               <td style="width: 15px">&nbsp;
               </td>
               <td colspan="4" style="vertical-align: middle; text-align: left; width: 664px;">
                   &nbsp;</td>
           </tr>
           <tr>
               <td style="width: 15px">&nbsp;
               </td>
               <td colspan="4" style="vertical-align: middle; text-align: left; width: 664px;">
                   <asp:Label ID="Label2" runat="server" Style="font-size: 14px; color: green; font-family: Times New Roman, Calibri, Arial"
                       Text="2.)  You must register first, a two step process of first validating your email, and selected user name and password, then returning from a delivered email link to complete required user details.  Thereafter, you will have complete access to this application.  During that use please refrain from using the 'Back' option on your Internet Browser."
                       Width="577px"></asp:Label></td>
           </tr>
           <tr>
               <td style="width: 15px">&nbsp;
               </td>
               <td colspan="4" style="vertical-align: middle; text-align: left; width: 664px;">
                   &nbsp;</td>
           </tr>
           <tr>
               <td style="width: 15px; height: 17px;">&nbsp;
               </td>
               <td colspan="4" style="vertical-align: middle; text-align: left; height: 17px; width: 664px;">
                   <asp:Label ID="Label3" runat="server" Style="font-size: 14px; color: green; font-family: Times New Roman, Calibri, Arial"
                       Text="3.) From the application's Product Search option, you will be able to build a listing of Declarations, quite similiar to the notion of a 'shopping cart'."
                       Width="577px"></asp:Label></td>
           </tr>
           <tr>
               <td style="width: 15px">&nbsp;
               </td>
               <td colspan="4" style="vertical-align: middle; text-align: left; width: 664px;">
                   &nbsp;</td>
           </tr>
           <tr>
               <td style="width: 15px; height: 19px">&nbsp;
               </td>
               <td colspan="4" style="vertical-align: middle; height: 19px; text-align: left; width: 664px;">
                   <asp:Label ID="Label4" runat="server" Style="font-size: 14px; color: green; font-family: Times New Roman, Calibri, Arial"
                       Text="4.) The most recent version of Adobe Reader is required by this application. Please follow the link below to download it unto your desktop."
                       Width="577px"></asp:Label></td>
           </tr>
           <tr>
               <td style="width: 15px; height: 19px;">&nbsp;
               </td>
               <td colspan="4" style="vertical-align: middle; height: 19px; text-align: left; width: 664px;">
                   &nbsp;</td>
           </tr>
      </table>
          
            
            
            
            <table  cellspacing=0 cellpadding=0 width=90% border=0>
                <tr>
                    <td style="width: 15px">&nbsp;
                    </td>
                    <td style="font-size: 14px; vertical-align: middle; width: 182px; color: blue;
                        text-align: left; height: 19px;">
                        &nbsp; &nbsp; &nbsp; &nbsp;
                        <asp:LinkButton CssClass=ClickForMore ID="btnEnterApplication" runat="server" 
                              OnClick="btnEnterApplication_Click"
                            Text="Enter CG PDs " 
                            style="text-align:center; border-right: gray thin solid; border-top: gray thin solid; font-size: 14px; border-left: gray thin solid; color: black; border-bottom: gray thin solid; font-family: Times New Roman, Calibri, Arial; background-color: white;" Width="273px"       />
                            <td colspan="3" style="vertical-align: middle; text-align: left; height: 19px;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 15px;">&nbsp;
                    </td>
                    <td style="font-size: 14px; vertical-align: middle; width: 182px; color: blue; height: 19px;
                        text-align: left">
                        &nbsp;
                    </td>
                    <td colspan="3" style="vertical-align: middle; height: 19px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td style="width: 15px">
                    </td>
                    <td colspan="4" style="vertical-align: middle; text-align: left">
           <asp:Label runat=server id="lblAdobe"
           Text="Download ADOBE Reader here:"
           style="font-size: 14px; color: blue; font-family: Times New Roman, Calibri, Arial; text-align: left; vertical-align: middle;"
           ></asp:Label>&nbsp;
                    <asp:HyperLink ID="hyperAdobe" runat="server" 
                       ImageUrl="~/Docs/get_adobe_reader.gif" Target="_self" style="vertical-align: middle; text-align: left">
                        </asp:HyperLink></td>
                </tr>
                <tr>
                    <td style="width: 15px">
                        &nbsp;
                    </td>
                    <td style="width: 182px; vertical-align: middle; text-align: right; font-size: 14px; color: blue;">
           </td>
                    <td colspan="3" style="vertical-align: middle; text-align: left;">
                        &nbsp;
                    </td>
                </tr>
        </table> 
      
        
</asp:Content>

