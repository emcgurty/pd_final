<%@ Page Language="C#" 
            MasterPageFile="~/MasterPage.master" 
            AutoEventWireup="true" 
            CodeFile="Default.aspx.cs" 
            Inherits="_Default" Title="User Login" 
            EnableEventValidation="false"

%>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   
           
       <table  cellspacing=0 cellpadding=0>
           <tr>
               <td style="width: 15px">
               </td>
               <td colspan="4" style="vertical-align: middle; text-align: left">
                   <asp:Label ID="lblWelcome" runat="server" Style="font-size: 14px; color: green; font-family: Times New Roman, Calibri, Arial"
                       Text="If you are new to this application, you must register at the option 'Register' first.  Otherwise proceed to enter your registered User Name and User Password."
                       Width="577px"></asp:Label></td>
           </tr>
           <tr>
               <td style="width: 15px">
               </td>
               <td style="vertical-align: middle; width: 182px; text-align: right">
                   &nbsp;</td>
               <td colspan="3" style="vertical-align: middle; text-align: left">
                   &nbsp;</td>
           </tr>
         
           <tr>
               <td style="width: 15px; height: 21px;">&nbsp;</td>
               <td style="vertical-align: middle; width: 182px; text-align: right; height: 21px;">
               <asp:label id="lblLoginUserName" runat="server" text="User Name:" 
                Width="158px" style="font-size: 14px; font-family: Times New Roman, Calibri, Arial; vertical-align: middle; text-align: right; position: static;"></asp:label>&nbsp;</td>
               <td colspan="3" style="width: 500px; vertical-align: middle; text-align: left; font-size: 14px; color: purple; font-family: Times New Roman, Calibri, Arial; height: 21px;">
                    <asp:TextBox ID="txtUserName" runat="server" 
                                 Width="150px" 
                                 style="vertical-align: middle; text-align: left; font-size: 14px" TabIndex="1" AutoPostBack="false" MaxLength="20"  Height="14px" ></asp:TextBox>
                    <asp:TextBox ID="txtDup" runat="server" Width="0px" Visible=false></asp:TextBox>
                   &nbsp;<asp:CheckBox ID="chkRememberMe" 
                                 runat="server" 
                                 AutoPostBack=true
                                 OnCheckedChanged="chkRememberMe_CheckedChanged"
                                 Style="font-size: 14px; color: blue;
                       font-family: Times New Roman, Calibri, Arial" Text="Remember Me?" /></td>
           </tr>
           <tr>
               <td style="width: 15px; height: 21px">
               </td>
               <td style="vertical-align: middle; width: 182px; height: 21px; text-align: right">
                   <asp:Label ID="Label1" runat="server" Style="font-size: 14px; vertical-align: middle;
                       font-family: Times New Roman, Calibri, Arial; position: static; text-align: right; color: blue;"
                       Text="User name min 6/max 20 chars" Width="206px"></asp:Label></td>
               <td colspan="3" style="font-size: 14px; vertical-align: middle; width: 500px; color: purple;
                   font-family: Times New Roman, Calibri, Arial; height: 21px; text-align: left">
               </td>
           </tr>
      </table>
           <div id="divNotFound" runat=server style="display:none">     
           <table cellspacing=0 cellpadding=0>
           <tr>
               <td style="width: 15px; ">
                   &nbsp;</td>
               <td style="vertical-align: middle; width: 208px; text-align: right">
               &nbsp;</td>
               <td colspan="3"  style=" font-size: 14px; color: red; font-family: Times New Roman, Calibri, Arial;">
                   <asp:Label ID="lblUserName" runat="server" Style="font-size: 14px; vertical-align: middle;
                       color: red; font-family: Times New Roman, Calibri, Arial; background-color: transparent;
                       text-align: left" Text="Label" Width="474px"></asp:Label></td>
           </tr>
           </table>
           </div>
           
      
           
           
       <table cellspacing=0 cellpadding=0 width=100%>
       <tr>   
                <td style="width: 15px">&nbsp;
                    </td>
                <td style="width: 182px; vertical-align: middle; text-align: right;">
                <asp:label id="lblCurrent" runat="server" text="Current Password:" 
                   Width="158px" Font-Size="XX-Small" style="font-size: 14px; font-family: Times New Roman, Calibri, Arial; vertical-align: middle; text-align: right;">
                </asp:label>&nbsp;</td>
                <td colspan="3" style="vertical-align: middle; text-align: left;">
                <asp:TextBox ID="txtCurrentPassword" runat="server" Width="150px" 
                TextMode="Password" TabIndex="2" MaxLength="12" CausesValidation="True" ></asp:TextBox>
                   <asp:Label ID="lblInstructions" runat="server" Style="font-size: 14px; color: red;
                       font-family: Times New Roman, Calibri, Arial" Text=" 6 - 12 alphanumeric characters"
                       Visible="False" Width="193px"></asp:Label></td>
               </tr>
           <tr>
               <td style="width: 15px">
               </td>
               <td style="vertical-align: middle; width: 182px; text-align: right">
                   <asp:Label ID="Label4" runat="server" Font-Size="XX-Small" Style="font-size: 14px;
                       vertical-align: middle; font-family: Times New Roman, Calibri, Arial; text-align: right; color: blue;"
                       Text="Password min 6/max 12 chars" Width="206px"></asp:Label></td>
               <td colspan="3" style="vertical-align: middle; text-align: left">
               </td>
           </tr>
               </table>
               <div id=divShowPasswordChange runat=server >
               <table cellspacing=0 cellpadding=0 width=100%>
           <tr>
               <td style="width: 15px">
               </td>
               <td style="vertical-align: middle; width: 207px; text-align: right">
                   <asp:Label ID="Label2" runat="server" Font-Size="XX-Small" Style="font-size: 14px;
                       vertical-align: middle; font-family: Times New Roman, Calibri, Arial; text-align: right" Text="New Password (12):"
                       Width="158px"></asp:Label>&nbsp;</td>
               <td colspan="3" style="vertical-align: middle;  text-align: left">
                   <asp:TextBox ID="txtNewPassword" runat="server" CausesValidation="True" MaxLength="12"
                       TabIndex="2" TextMode="Password" Width="150px"></asp:TextBox>
                   <asp:Label ID="Label5" runat="server" Style="font-size: 14px; color: red; font-family: Times New Roman, Calibri, Arial"
                       Text=" Your new password must contain one capital letter and one numeric." Width="433px"></asp:Label></td>
           </tr>
           <tr>
               <td style="width: 15px">
               </td>
               <td style="vertical-align: middle; width: 207px; text-align: right">
                   <asp:Label ID="Label3" runat="server" Font-Size="XX-Small" Style="font-size: 14px;
                       vertical-align: middle; font-family: Times New Roman, Calibri, Arial; text-align: right" Text="Confirm Password (12):"
                       Width="158px"></asp:Label>&nbsp;</td>
               <td colspan="3" style="vertical-align: middle;  text-align: left">
                   <asp:TextBox ID="txtConfirmPassword" runat="server" CausesValidation="True" MaxLength="12"
                       TabIndex="2" TextMode="Password" Width="150px"></asp:TextBox></td>
           </tr>
              
       </table>
       </div>
         <div id="divCurrentPassword" runat=server style="display:none">
         <table cellspacing=0 cellpadding=0>
      
         
           <tr id="tr1" >
               <td style="width: 15px">
                 &nbsp; </td>
               <td style="vertical-align: middle; width: 206px; text-align: right">&nbsp;
               </td>
               <td colspan="3" style="font-size: 14px; color: red; font-family: Times New Roman, Calibri, Arial; width: 205px;">
               <asp:Label ID="lblCurrentPassword" runat="server" Text="Label"  width="462px"
               style="font-size: 14px; vertical-align: middle; color: red; font-family: Times New Roman, Calibri, Arial; background-color: transparent; text-align: left;" ></asp:Label>&nbsp;</td>
           </tr>
         </table>
         </div>
         <div id=divForgot runat=server>
         <table  cellspacing=0 cellpadding=0 width=100%>
           
              
              <tr>
                   <td style="width: 15px; height: 19px;">
                   </td>
                   <td style="width: 205px; height: 19px;">&nbsp;
                   </td>
                   <td colspan="3" style="font-size: 14px; color: red; font-family: Times New Roman, Calibri, Arial; height: 19px;">
                    <asp:HyperLink ID="hyperForgot" runat="server" Style="font-size: 14px; color: blue;
                        font-family: Times New Roman, Calibri, Arial" Target="_self" NavigateUrl="~/Forgot.aspx">
                        Forgot my user name or password
                        </asp:HyperLink></td>
               </tr>
            </table>
            </div>
            
           <table  cellspacing=0 cellpadding=0 width=100%>
            
            <tr>
                <td style="width: 15px">
                    &nbsp;</td>
                <td style="width: 182px">
                    &nbsp;</td>
                <td colspan="3">
                    &nbsp; &nbsp;</td>
            </tr>
               <tr>
                   <td style="width: 15px">
                   </td>
                   <td style="width: 182px; vertical-align: middle; text-align: left;">
                       
                        
                       </td>
                   <td colspan="3">
                   </td>
               </tr>
               </table>
            
            <div id="divShowOneButton" runat=server>
            <table  cellspacing=0 cellpadding=0 width=100% border=0>
            <tr>
                <td colspan="5" style="vertical-align: middle; text-align: left; height: 25px;">
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp; 
                    <asp:LinkButton CssClass=ClickForMore runat="server" id="cmdUserLogin" text="Complete Registration" 
                              style="border-right:gray thin solid; border-top: gray thin solid; border-left: gray thin solid; width: 134px; color: black; border-bottom: gray thin solid; font-family: Times New Roman, Calibri, Arial; background-color: transparent; font-size: 14px; " 
                              onClick="cmdUserLogin_Click" Width="283px" />
      
      
        </td>
            </tr>
            </table>
            </div>
            
            <div id="divBothButtons" runat=server>
            <table  cellspacing=0 cellpadding=0 width=100% border=0>
            <tr>
                <td colspan="5">
                    &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            <asp:LinkButton CssClass=ClickForMore runat="server" id="cmdUserKnownLogin" text="Login" 
                             style="border-right:gray thin solid; border-top: gray thin solid; border-left: gray thin solid; width: 134px; color: black; border-bottom: gray thin solid; font-family: Times New Roman, Calibri, Arial; background-color: transparent; font-size: 14px; " 
                             onClick="cmdUserLogin_Click" Width="35px" />
      
      
        <asp:LinkButton CssClass=ClickForMore  id="btnRegister" runat="server" text="Register" OnClick="cmdNewUserRegistration_Click" 
        Width="124px" Font-Names="Arial" Font-Size="XX-Small" BackColor="#FFFFFF" ForeColor="#CC0033"
         style="text-align:center; border-right: gray thin solid; border-top: gray thin solid; font-size: 14px; border-left: gray thin solid; color: black; border-bottom: gray thin solid; font-family: Times New Roman, Calibri, Arial; background-color: white;" /></td>
            </tr>
            </table>
            </div>
            
            <table  cellspacing=0 cellpadding=0 width=100% border=0>
                <tr>
                    <td style="width: 15px">
                    </td>
                    <td style="font-size: 14px; vertical-align: middle; width: 182px; color: blue;
                        text-align: left">
                        &nbsp;</td>
                    <td colspan="3" style="vertical-align: middle; text-align: left">
                        &nbsp;</td>
                </tr>
        </table> 
        
<!--          <% 

int j;
int i = 0;
j=Session.Contents.Count;
Response.Write("Session variables: " + j);
while (i < j)
{Response.Write(Session.Keys[i] + " " + Session.Contents[i] + "<br />");
i++;
}

%>-->

        
</asp:Content>

