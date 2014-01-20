<%@ Page Language="C#"
         MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
         CodeFile="ReadOnlyUserInformation.aspx.cs" Inherits="ReadOnlyUserInformation" 
         Title="User Detail Confirmed" EnableEventValidation="false"   %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
 
   <table>
       <tr>
           <td style="width: 1%">
           </td>
           <td class="rj" style="font-size: 16px; vertical-align: middle; color: Blue; font-family: Times New Roman, Calibri, Arial; text-align: left;" colspan="3">
               Confirming User Registration Detail</td>
           <td style="width: 6%">
           </td>
       </tr>
       <tr>
           <td style="width: 1%">
           </td>
           <td class="rj" style="width: 17%">
               &nbsp;</td>
           <td colspan="2">
               &nbsp;</td>
           <td style="width: 6%">
           </td>
       </tr>
    <tr>
        <td style="width: 1%">
        </td>
        <td style="width: 17%" class=rj>
            <asp:Label ID="lblemail" runat="server" Text="email:" style="color:blue" Font-Size="14px" ForeColor="#3300CC"></asp:Label>&nbsp</td>
        <td colspan="2">
            <asp:Label ID="txtemail" runat="server" Width="318px" TabIndex="4" Font-Size="14px"></asp:Label></td>
        <td style="width: 6%">
        </td>
    </tr>
    <tr>
        <td style="width: 1%">
        </td>
        <td style="width: 17%; vertical-align: middle; text-align: right;">
            <asp:Label ID="lblNewUserSaluation" runat="server" Text="Salutation:" style="color:blue" Font-Size="14px" ForeColor="#3300CC"></asp:Label>&nbsp
            </td>
        <td colspan="3">
            <asp:Label ID="cmbNewUserSalutation" runat="server" TabIndex="5" Font-Size="14px" >
            </asp:Label>
            &nbsp; &nbsp;&nbsp;
        </td>
    </tr>
       <tr>
           <td style="width: 1%; height: 21px">
           </td>
           <td class="rj" style="width: 17%; height: 21px">
            <asp:Label ID="lblFirstName" runat="server" Text="First Name:" style="color:blue" Width="76px" Font-Size="14px" ForeColor="#3300CC"></asp:Label>&nbsp;</td>
           <td style="width: 13%; height: 21px">
            <asp:Label ID="txtFirstName" runat="server" TabIndex="6" Width="173px" Font-Size="14px"></asp:Label></td>
           <td style="width: 18%; height: 21px">
           </td>
           <td style="width: 6%; height: 21px">
           </td>
       </tr>
       <tr>
           <td style="width: 1%; height: 21px">
           </td>
           <td class="rj" style="width: 17%; height: 21px">
            <asp:Label ID="lblLastName" runat="server" Text="Last Name:" style="color:blue" Width="73px" Font-Size="14px" ForeColor="#3300CC"></asp:Label>&nbsp;</td>
           <td style="width: 13%; height: 21px">
            <asp:Label ID="txtLastName" runat="server" TabIndex="7" Width="170px" Font-Size="14px"></asp:Label></td>
           <td style="width: 18%; height: 21px">
           </td>
           <td style="width: 6%; height: 21px">
           </td>
       </tr>
       <tr>
           <td style="width: 1%; height: 21px">
           </td>
           <td class="rj" style="width: 17%; height: 21px">
           </td>
           <td style="width: 13%; height: 21px" >
               &nbsp;</td>
           <td style="width: 18%; height: 21px">
           </td>
           <td style="width: 6%; height: 21px">
           </td>
       </tr>
    <tr>
        <td style="width: 1%; height: 21px;">
        </td>
        <td style="width: 17%; height: 21px;" class=rj>
            <asp:Label ID="lblCompanyName" runat="server" Text="Company Name:" style="color: blue" Font-Size="14px" ForeColor="#3300CC"></asp:Label>&nbsp</td>
        <td style="width: 13%; height: 21px;">
            <asp:Label ID="txtCompanyName" runat="server" TabIndex="8" Font-Size="14px"></asp:Label></td>
        <td style="width: 18%; height: 21px;">
            </td>
        <td style="width: 6%; height: 21px;">
        </td>
    </tr>
       <tr>
           <td style="width: 1%; height: 21px">
           </td>
           <td class="rj" style="width: 17%; height: 21px">
            <asp:Label ID="lblAddress1" runat="server" Text="First Address Line:" style="color:blue" Font-Size="14px" ForeColor="#3300CC"></asp:Label>&nbsp;</td>
           <td style="width: 13%; height: 21px">
            <asp:Label ID="txtAddress1" Font-Size="14px" runat="server" TabIndex="9" Width="167px" ></asp:Label></td>
           <td style="width: 18%; height: 21px">
           </td>
           <td style="width: 6%; height: 21px">
           </td>
       </tr>
       <tr>
           <td style="width: 1%; height: 21px">
           </td>
           <td class="rj" style="width: 17%; height: 21px">
               <asp:Label ID="lblAddress2" runat="server"  Style="color: blue" Font-Size="14px"
                   Text="Second Address Line:" Width="143px" ForeColor="#3300CC"></asp:Label>&nbsp;</td>
           <td style="width: 13%; height: 21px">
               <asp:Label ID="txtAddress2" runat="server" TabIndex="10" Width="165px" Font-Size="14px"></asp:Label></td>
           <td style="width: 18%; height: 21px">
           </td>
           <td style="width: 6%; height: 21px">
           </td>
       </tr>
    <tr>
        <td style="width: 1%">
        </td>
        <td style="width: 17%" class=rj>
            <asp:Label ID="Label2" runat="server"  Style="color: blue" Text="Third Address Line:" Font-Size="14px"
                Width="143px" ForeColor="#3300CC"></asp:Label>&nbsp</td>
        <td style="width: 13%">
            <asp:Label ID="txtAddress3" runat="server" TabIndex="11" Width="169px" Font-Size="14px" ></asp:Label></td>
        <td style="width: 18%" class=rj>
            &nbsp</td>
        <td style="width: 6%">
        </td>
    </tr>
    <tr>
     
        <td style="width: 1%">
        </td>
        <td style="width: 17%" class=rj>
            <asp:Label ID="lblCity" runat="server" Text="City:" style="color:blue" Font-Size="14px" ForeColor="#3300CC"></asp:Label>&nbsp</td>
        <td style="width: 13%">
            <asp:Label ID="txtCity" runat="server" TabIndex="12" Width="170px" Font-Size="14px"></asp:Label></td>
        <td style="width: 18%" class=rj>
            &nbsp;</td>
        <td style="width: 6%">
        </td>
    </tr>
       <tr>
           <td style="width: 1%">
           </td>
           <td class="rj" style="width: 17%">
            <asp:Label ID="lblState" runat="server" Text="USA/Canada:" style="color:blue" Font-Size="14px" ForeColor="#3300CC"></asp:Label>&nbsp</td>
           <td style="width: 13%">
            <asp:Label ID="cmbState" runat="server" TabIndex="13" Width="169px" Font-Size="14px"></asp:Label></td>
           <td class="rj" style="width: 18%">
           </td>
           <td style="width: 6%">
           </td>
       </tr>
       <tr>
           <td style="width: 1%">
           </td>
           <td class="rj" style="width: 17%">
            <asp:Label ID="lblNewUserRegion" runat="server" Text="Region:" style="color:blue" Font-Size="14px" ForeColor="#3300CC"></asp:Label>&nbsp</td>
           <td style="width: 13%">
            <asp:Label ID="cmbNewUserRegion"  runat="server" TabIndex="14" Font-Size="14px">
            </asp:Label ></td>
           <td class="rj" style="width: 18%">
           </td>
           <td style="width: 6%">
           </td>
       </tr>
    <tr>
      
        <td style="width: 1%">
        </td>
        <td style="width: 17%" class=rj>
            <asp:Label ID="lblCountry" runat="server" Text="Country:" style="color:blue" Font-Size="14px" ForeColor="#3300CC"></asp:Label>&nbsp</td>
        <td style="width: 13%">
        <asp:Label ID="cmbCountries" runat="server" 
         TabIndex="15" Width="151px" Font-Size="14px">
        </asp:Label></td>
        <td style="width: 18%" class=rj>
            &nbsp;</td>
            
         
        <td style="width: 6%">
        </td>
    </tr>
    <tr>
       
        <td style="width: 1%">
        </td>
        <td style="width: 17%" class=rj>
            <asp:Label ID="lblPostal_Code" runat="server" Text="Postal Code:" style="color: blue" Width="174px" Font-Size="14px" ForeColor="#3300CC"></asp:Label>&nbsp</td>
        <td style="width: 13%">
            <asp:Label ID="txtPostal_Code" runat="server" TabIndex="16" Font-Size="14px"></asp:Label></td>
        <td style="width: 18%" class=rj>
            &nbsp;</td>
        <td style="width: 6%">
        </td>
    </tr>
       <tr>
           <td style="width: 1%">
           </td>
           <td class="rj" style="width: 17%">
           </td>
           <td style="width: 13%">
               &nbsp;</td>
           <td class="rj" style="width: 18%">
           </td>
           <td style="width: 6%">
           </td>
       </tr>
       <tr>
           <td style="width: 1%">
           </td>
           <td class="rj" style="width: 17%">
            <asp:Label ID="lblTelephone" runat="server" Text="Telephone:" Font-Size="14px" ForeColor="blue"></asp:Label>&nbsp</td>
           <td style="width: 13%">
            <asp:Label ID="txtTelephone" runat="server" TabIndex="18" Width="137px" Font-Size="14px"></asp:Label></td>
           <td class="rj" style="width: 18%">
           </td>
           <td style="width: 6%">
           </td>
       </tr>
    <tr>
     
        <td style="width: 1%;">
        </td>
        <td style="width: 17%;" class=rj>
            <asp:Label ID="lblFAX" runat="server" Text="FAX:" ForeColor="blue" style="text-align: right" Font-Size="14px"></asp:Label>&nbsp</td>
        <td style="width: 13%;">
            <asp:Label ID="txtFAX" runat="server" TabIndex="19" Width="138px" Font-Size="14px"></asp:Label></td>
        <td style="width: 18%;">
        </td>
        <td style="width: 6%;">
        </td>
    </tr>
    <tr>
        
        <td style="width: 1%">
        </td>
        <td style="width: 17%" class=rj>
            &nbsp;</td>
        <td style="width: 13%">
            &nbsp;</td>
        <td style="width: 18%">
            </td>
        <td style="width: 6%">
        </td>
    </tr>
    <tr>
   
        <td style="width: 1%; height: 26px">
        </td>
        <td style="width: 17%; height: 26px">
            </td>
        <td colspan="2" style="height: 26px">
            &nbsp; &nbsp;
            <asp:LinkButton CssClass=ClickForMore ID="cmdSubmitNewUser" runat="server" Text="Begin Using Application" style="border-right: gray thin solid; border-top: gray thin solid; font-size: 16px; border-left: gray thin solid; color: black; border-bottom: gray thin solid; font-family: Times New Roman, Calibri, Arial; background-color: transparent" Width="174px" OnClick="cmdSubmitNewUser_Click"  />
            &nbsp; &nbsp;&nbsp; &nbsp;
            </td>
        <td style="width: 6%; height: 26px">
        </td>
    </tr>
    <tr>
      
        <td style="width: 1%; height: 8px">
        </td>
        <td style="width: 17%; height: 8px">
        </td>
        <td colspan="2" style="height: 8px">
            &nbsp;</td>
        <td style="width: 6%; height: 8px">
        </td>
    </tr>
</table>

<!--End of Register New User-->


</asp:Content>

