<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  
         AutoEventWireup="true" CodeFile="ShowLetter.aspx.cs" 
         Inherits="ShowLetter" Title="Declaration Content" 
         EnableEventValidation="false"
         
         %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Import Namespace="System.Text" %>




<asp:Content EnableViewState=false ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"  >

<% Response.CacheControl = "private"; %>
<% Response.AddHeader ("Pragma", "no-cache") ;%>
<% Response.Expires = 0; %>

 
 
 <table width=100% cellpadding=0 cellspacing=0 >
     <tr bgcolor=white>
         <td width="100%">
             <table cellpadding=0 cellspacing=0 border=0 width=100%>
                 <tr >
                     <td style="width: 9px">
                     </td>
                     <td colspan="2" style="font-size: 12px; vertical-align: middle; color: black; text-align: left">
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td style="width: 9px; height: 37px;">
                     </td>
                     <td colspan="2" style="font-size: 12px; vertical-align: middle; color: black; text-align: left; height: 37px;">
                <asp:Label ID="Label1" runat="server" Style="font-size: 12px; color: blue" Text='NOTE: If in the rare event your Declaration Letter should present a poorly placed page break or improperly placed signature, please return to "Go to Declarations" and select fewer declarations. Also, if you cannot see the PDF toolbar, please key: ALT-F8.'
                    Width="619px"></asp:Label></td>
                     <td colspan="1" style="font-size: 12px; vertical-align: middle; width: 44px; color: black;
                         text-align: left; height: 37px;">
                     </td>
                 </tr>
                 </table>
             &nbsp;</tr>
      
    </table> 
                 
         <asp:Panel runat=server ID=PDFPanel width='800' height='1200'>
              
             <iframe width='800' height='1200' runat=server   id='embedTemplate1'  unselectable=off  scrolling=yes  />
               
               </asp:Panel>

            

       
 
</asp:Content>

