<%@ Page Language="C#" 
         MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
         CodeFile="ValidateNewUser.aspx.cs" Inherits="ValidateNewUser" 
         Title="Validate New User" EnableEventValidation="false"   %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script language="JavaScript">

  window.history.forward(1);

</script>
 
   <table cellpadding=0 cellspacing=0>
       <tr>
           <td style="width: 5px">&nbsp;
           </td>
           <td colspan="2">
               &nbsp;</td>
           <td style="width: 9%">
           </td>
           <td>
           </td>
       </tr>
       <tr>
           <td style="width: 5px">
           </td>
           <td colspan="2">
            <asp:Label ID="lblRequired" runat="server" Style="font-size: 16px; color: green; font-family: Times New Roman, Calibri, Arial"
                Text="New user registration is a two step process.  First your email, user name and password will be validated.  In the event of any invalid entry, your entered password will be cleared for mutual security purposes. Otherwise, on validation you will receive email from which you will be permitted to complete the registration process."
                ForeColor="#3300CC" Width="635px"></asp:Label></td>
           <td style="width: 9%">
           </td>
           <td>
           </td>
           
       </tr>
       <tr>
           <td style="width: 5px">
           </td>
           <td colspan="2">
               &nbsp;</td>
           <td style="width: 9%">
           </td>
           <td>
           </td>
       </tr>
       <tr>
           <td style="width: 5px">
           </td>
           <td colspan="2">
               <asp:Label ID="lblRequiredFields" runat="server" ForeColor="#3300CC" Style="font-size: 12px;
                   color: red; font-family: Times New Roman, Calibri, Arial" Text="All fields are required." Width="635px"></asp:Label></td>
           <td style="width: 9%">
           </td>
           <td>
           </td>
       </tr>
          <tr>
           <td style="width: 5px">&nbsp;
           </td>
           <td colspan="2">
               <asp:Label ID="Label1" runat="server" ForeColor="#3300CC" Style="font-size: 12px;
                   color: blue; font-family: Times New Roman, Calibri, Arial" Text="User name must have 6 - 20 characters, and password must have 6 - 12 characters."
                   Width="635px"></asp:Label></td>
           <td style="width: 9%">
           </td>
           <td>
           </td>
       </tr>
       <tr>
           <td style="width: 5px">
           </td>
           <td colspan="2">
               &nbsp;
           </td>
           <td style="width: 9%">
           </td>
           <td>
           </td>
       </tr>
       <tr>
           <td style="width: 5px">
           </td>
           <td colspan="2">
            <asp:TextBox ID="hiddenUser_Information_ID" runat="server" Visible="False" Width="14px">
            </asp:TextBox></td>
           <td style="width: 9%">
           </td>
           <td>
           </td>
       </tr>
       </table>
  
   
   <table cellpadding=0 cellspacing=0 width=100%>
       <tr>
           <td class="rj" style="width: 25%">
            <asp:Label ID="lblNewUserName" runat="server" Text="User Name:" style="font-size: 12px;color:red"></asp:Label>&nbsp;
            </td>
           <td style="width: 75%">
            <asp:TextBox ID="txtNewUserName" runat="server" TabIndex="1" 
              MaxLength="20" AutoPostBack="false"></asp:TextBox>
           </td>
           
       </tr>
       
       </table>
      
      
      <div id="divDuplicate" style="display:none" runat="server">
      <table cellpadding=0 cellspacing=0 width=100%>
           <tr>
               <td class="rj" style="width: 25%">
               </td>
               <td colspan="2" style="width: 75%">
               <asp:Label ID="lblDuplicate" runat="server" Style="font-size: 14px; vertical-align: middle;
                       color: red; text-align: left" Text="Label" Width="359px"></asp:Label></td>
           </tr>
       </table>
      </div>
           <table cellpadding=0 cellspacing=0 width=100%>
       <tr>
           <td class="rj" style="width: 25%">
               <asp:Label ID="lblNewUserPassword" runat="server" Text="Password:" style="font-size: 12px;color:red">
               </asp:Label>&nbsp;</td>
           <td colspan="2" style="width: 75%">
            <asp:TextBox ID="txtNewUserPassword" runat="server" TextMode="password" TabIndex="2" MaxLength=12 >
            </asp:TextBox>
            </td>
           
       </tr>
       <tr>
           <td class="rj" style="width: 25%">
            <asp:Label ID="lblReEnter" runat="server" Text="Confirm:" style="font-size: 12px;color:red"></asp:Label>&nbsp;</td>
           <td style="width: 75%">
            <asp:TextBox ID="txtReEnter" AutoPostBack="false" runat="server" TextMode="password" TabIndex="3" MaxLength="12" ></asp:TextBox>
            </td>
           
       </tr>
           <tr>
               <td class="rj" style="width: 25%; height: 19px;">
               </td>
               <td style="width: 75%; height: 19px;">
            <asp:Label ID="lblInstructions" runat="server" Style="font-size: 14px; color: purple;
                font-family: Times New Roman, Calibri, Arial" 
                Text="Password must contain one capital letter and one numeric value" Width="370px" ></asp:Label></td>
           </tr>
       </table>
       <div id=divPassword style="display:none" runat=Server>
       <table cellpadding=0 cellspacing=0 width=100%>
           <tr>
               <td class="rj" style="width: 25%">
               </td>
               <td style="width: 75%">
            </td>
           </tr>
               <tr>
                   <td class="rj" style="width: 25%">
                   </td>
                   <td style="width: 75%">
                      <asp:Label ID="lblPassword" runat="server" Style="font-size: 14px; vertical-align: middle;
                       color: red; text-align: left" Text="Label" Width="359px"></asp:Label>
                       </td>
               </tr>
       </table>
       </div>
       
       
           
       
    
    
 
    
    
    <table cellpadding=0 cellspacing=0 width=100%>
    <tr>
        <td style="width: 25%; text-align: right; vertical-align: middle;" class=rj>
            <asp:Label ID="lblemail" runat="server" Text="email:" style="font-size: 12px;color:red"></asp:Label>&nbsp</td>
        <td style="width: 75%">
            <asp:TextBox ID="txtemail" runat="server" Width="314px" TabIndex="4" MaxLength="100"></asp:TextBox></td>
    </tr>
    </table>
    <div id=divemail runat=server>
    <table cellpadding=0 cellspacing=0 width=100%>
    <tr>
        <td style="width: 25%">
            </td>
        <td style="width: 75%; color: red; font-family: 'Times New Roman';font-size:14px">
            Email address is not properly formatted</td>
    </tr>
    </table>
    </div>
    <table cellpadding=0 cellspacing=0 width=100%>
        <tr>
            <td style="width: 25%">
            </td>
            <td style="width: 75%">
            </td>
        </tr>
        <tr>
            <td style="width: 25%">
            </td>
            <td style="width: 75%">
                &nbsp;</td>
        </tr>
    <tr>
        <td style="width: 25%;">
            </td>
        <td style="width: 75%;" >
            <asp:LinkButton CssClass=ClickForMore ID="cmdSubmitNewUser" runat="server" Text="Validate" 
                            style="border-right: gray thin solid; border-top: gray thin solid; font-size: 16px; border-left: gray thin solid; color: black; border-bottom: gray thin solid; font-family: Times New Roman, Calibri, Arial; background-color: transparent" Width="145px" 
                            OnClick="cmdSubmitNewUser_Click"  />
            &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;
        </td>
    </tr>

        <tr>
            <td style="width: 25%;">
            </td>
            <td style="width: 75%;">
            </td>
        </tr>
</table>

<!--End of Register New User-->


</asp:Content>

