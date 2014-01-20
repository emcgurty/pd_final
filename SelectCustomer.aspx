<%@ Page Language="C#" 
         MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
         CodeFile="SelectCustomer.aspx.cs" Inherits="SelectCustomer" 
         Title="Select Customer" EnableEventValidation="false"   
         MaintainScrollPositionOnPostback="true"
         %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


   <table cellpadding=0 cellspacing=0 width=100%>
       <tr>
           <td style="width: 5px">
           </td>
           <td colspan="2" style="width: 543px">
               &nbsp;
           </td>
           <td style="width: 9%">
           </td>
           <td>
           </td>
           <td style="width: 6%">
           </td>
       </tr>
       <tr>
           <td style="width: 5px">
           </td>
           <td colspan="2" style="width: 543px">
            <asp:Label ID="lblNewUserSubTitle" runat="server" 
            Width="669px" style="font-size: 16px; color: black; font-family: Times New Roman, Calibri, Arial" ></asp:Label></td>
           <td style="width: 9%">
           </td>
           <td>
           </td>
           <td style="width: 6%">
           </td>
       </tr>
       <tr>
           <td style="width: 5px">&nbsp;
           </td>
           <td colspan="2" style="width: 543px">
           </td>
           <td style="width: 9%">
           </td>
           <td>
           </td>
           <td style="width: 6%">
           </td>
       </tr>
       </table>
 
   
    
 
    <table cellpadding=0 cellspacing=0 width=100%  >   
        <tr>
            <td style="width: 1%; height: 24px">
                &nbsp;</td>
            <td colspan="2" style="height: 24px">
                <asp:Label ID="lblRepUpdate" 
                runat="server" Style="font-size: 14px; color: green" 
                Text="If your name as a Technical Representative is known to this application, please selected it from 'Select Technical Service Rep's Name'. However you may wish to modify it, please select your known name below, and then choose 'Add or Update Technical Rep's Name'. To add a new name, please verify that the below displays {Select the name of a Technical Rep here}."
                    Width="670px"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 1%; height: 24px;">
            </td>
            <td class="rj" style="vertical-align: middle; width:25%; text-align: right; height: 24px;">
                <asp:Label ID="Label3" runat="server" Style="font-size: 16px; color: blue" Text="Select Technical Service Rep's Name:"
                    Width="246px"></asp:Label>&nbsp;</td>
            <td style="width: 72%; height: 24px;">
                &nbsp;<asp:DropDownList ID="cmbReps" 
            runat="server" TabIndex="1" 
            Width="311px" 
            DataTextField="Full_Name" 
            DataValueField="Rep_ID" DataSourceID="sp_GetReps" 
            AutoPostBack="True" 
            OnSelectedIndexChanged="cmbReps_SelectedIndexChanged"
             ForeColor=Green>
            </asp:DropDownList>
            
            <asp:SqlDataSource ID="sp_GetReps" runat="server" 
                ConnectionString="<%$ ConnectionStrings:EMM_Connection %>"
                SelectCommand="GetTechnicalReps" SelectCommandType="StoredProcedure">
                </asp:SqlDataSource>
                </td>
        </tr>
        <tr>
            <td style="width: 1%; height: 24px">
            </td>
            <td class="rj" style="vertical-align: middle; width:25%; height: 24px; text-align: right">
                <asp:Label ID="Label9" runat="server" Style="font-size: 16px; color: blue" Text="Rep's email address:"
                    Width="257px"></asp:Label></td>
            <td style="width: 72%; height: 24px">
                &nbsp;<asp:Label ID="txtRepEmail" runat="server" Style="font-size: 16px; color: green" Text=""
                    Width="309px"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 1%; height: 24px">
            </td>
            <td class="rj" style="vertical-align: middle; width:25%; height: 24px; text-align: right">
            </td>
            <td style="width: 72%; height: 24px">
                <asp:LinkButton CssClass=ClickForMore ID="lnkAddNewRep" 
                       runat="server" Text="Add or Update Technical Rep's Name" 
                       Width="255px" 
                       OnClick="lnkAddNewTechnicalRep_Click"  /></td>
        </tr>
</table>
<div id=divRepName runat=server style="display:none">
        <table cellpadding=0 cellspacing=0 width=100%  > 
            <tr>
                <td style="width: 1%">
                </td>
                <td class="rj" style="vertical-align: middle; width:25%; text-align: right">
                    &nbsp;</td>
                <td style="width: 74%">
                    &nbsp;</td>
            </tr>
        <tr>
            <td style="width: 1%">
                &nbsp;</td>
            <td class="rj" style="vertical-align: middle; width:25%; text-align: right">
                <asp:Label ID="Label6" runat="server" ForeColor="Purple" Style="font-size: 16px;
                    color: purple" Text="Technical Service Rep's First Name:" Width="243px"></asp:Label>&nbsp;</td>
            <td style="width: 74%">
                &nbsp;<asp:TextBox ID="txtRepFirstName" runat="server" MaxLength="50" Width="209px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 1%; height: 19px">
                &nbsp;</td>
            <td class="rj" style="vertical-align: middle; width:25%; height: 19px; text-align: right">
                <asp:Label ID="Label7" runat="server" Style="font-size: 16px; color: purple" 
                Text="Last Name:"
                    Width="246px"></asp:Label>&nbsp;</td>
            <td style="width: 74%; height: 19px">
                &nbsp;<asp:TextBox ID="txtRepLastName" runat="server" MaxLength="50" Width="209px"></asp:TextBox></td>
        </tr>
            <tr>
                <td style="width: 1%; height: 19px">
                    &nbsp;</td>
                <td class="rj" style="vertical-align: middle; width:25%; height: 19px; text-align: right">
                    <asp:Label ID="Label10" runat="server" Style="font-size: 16px; color: purple" Text="email Address:"
                        Width="246px"></asp:Label>&nbsp;</td>
                <td style="width: 74%; height: 19px">
                    &nbsp;<asp:TextBox ID="txtRepNewEmail" runat="server" MaxLength="50" Width="209px"></asp:TextBox></td>
            </tr>
        </table>
</div>

<table cellpadding=0 cellspacing=0 width=100%  > 
        <tr>
            <td style="width: 1%">
            </td>
            <td class="rj" style="vertical-align: middle; width: 36%; text-align: right">
                &nbsp;&nbsp;</td>
            <td style="width: 75%">
                &nbsp;</td>
        </tr>
    <tr>
        <td style="width: 1%">
            &nbsp;</td>
        <td colspan="2">
            <asp:Label ID="Label8" runat="server" Style="font-size: 14px; color: green" Text="Now that you have identified yourself as a Technical Representative with prehaps additions or modifications to that name, your next step is to select the Company whom you are representing. Form fields that are labelled in red are required"
                Width="670px"></asp:Label></td>
    </tr>
        <tr>
            <td style="width: 1%">
                &nbsp;</td>
            <td class="rj" style="vertical-align: middle; width: 36%; text-align: right">
            <asp:Label ID="Label1" runat="server" Style="font-size: 16px; color: blue" Text="Select an exisiting Company Customer here:"
                Width="296px"></asp:Label>&nbsp;</td>
            <td style="width: 75%">
            <asp:DropDownList ID="cmbCustomerName" runat="server" AutoPostBack=true
            TabIndex="2" Width="266px" DataSourceID="SqlDataSource_customer_Name" 
            DataTextField="Customer_Name" DataValueField="Customer_ID" 
            OnSelectedIndexChanged="cmbCustomerName_SelectedIndexChanged" >
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource_customer_Name" runat="server"
               ConnectionString="<%$ ConnectionStrings:EMM_Connection %>" SelectCommand="GetCustomerName"
                SelectCommandType="StoredProcedure">
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td style="width: 1%">
                &nbsp;</td>
            <td class="rj" style="vertical-align: middle; width: 36%; text-align: right">
                <asp:Label ID="Label4" runat="server" Style="font-size: 16px; color: blue" Text="OR: Enter a new Company Customer here:"
                    Width="278px"></asp:Label>&nbsp;</td>
            <td style="width: 75%; vertical-align: top; text-align: left;">
                <asp:TextBox ID="txtNewCustomerName" runat="server" MaxLength="50" TabIndex="3" Width="261px"></asp:TextBox>
                <asp:LinkButton CssClass=ClickForMore ID="lnkAddNewCustomer" 
                       runat="server" Text="Add New Customer" 
                       Width="206px" 
                       OnClick="lnkAddNewCustomer_Click"  /></td>
        </tr>
        <tr>
            <td style="width: 1%">
            </td>
            <td class="rj" style="vertical-align: middle; width: 36%; text-align: right">
                &nbsp;&nbsp;</td>
            <td style="width: 75%">
            </td>
        </tr>
        </table>
      <div id="divResponse" runat=server style="display:none">
        <table cellpadding=0 cellspacing=0 width=100%  >  
        <tr>
            <td style="width: 3%">
            </td>
            <td class="rj" style="vertical-align: middle; width: 1%; text-align: right">
            </td>
            <td style="width: 75%">
            <asp:Label ID="lblReponse" runat="server" 
                      Style="font-size: 16px; color: red"
                    Width="602px"></asp:Label></td>
        </tr>
        </table>
        </div>
    
     
    <div id=divCustomerDetail runat=server style="display:none">
      
    <table cellpadding=0 cellspacing=0 width=100%  > 
        <tr>
            <td style="width: 3%">
            </td>
            <td class="rj" style="vertical-align: middle; width:25%; text-align: right">
            <asp:Label ID="Label5" runat="server" Text="Company Name:" style="color:red; font-size: 16px;" 
            Width="189px" ></asp:Label>&nbsp</td>
            <td style="width: 75%">
            <asp:TextBox ID="txtCompanyName" runat="server" TabIndex="4" MaxLength="50" Width="294px"></asp:TextBox></td>
        </tr>
          </table>
        
        
        
        
        
        
        
         <div id=divCompanyName runat=server style="display:inline">
         <table cellpadding=0 cellspacing=0 width=100%  > 
            <tr>
            <td style="width: 3%; height: 19px;">
            </td>
            <td class="rj" style="vertical-align: middle; width:25%; text-align: right; height: 19px;">
                &nbsp;&nbsp</td>
            <td style="width: 75%; height: 19px;">
            <asp:Label ID="lblCompanyName" runat="server" style="color:purple; font-size: 16px;" 
            Width="395px"></asp:Label></td>
           </tr>
           </table>
        </div>
        
        <table cellpadding=0 cellspacing=0 width=100%  > 
        
        
        <tr>
            <td style="width: 3%">
            </td>
            <td class="rj" style="vertical-align: middle; width:25%; text-align: right">
            <asp:Label ID="lblNewUserSaluation" runat="server" Text="Contact Salutation:" style="color:black">
            </asp:Label>&nbsp;</td>
            <td style="width: 75%">
       <asp:DropDownList ID="cmbSalutation" runat="server" 
       TabIndex="5" Width="118px" DataTextField="Salutation_Text" 
       DataValueField="Salutation_ID"  >
        </asp:DropDownList></td>
        </tr>
        </table>
        <table cellpadding=0 cellspacing=0 width=100%  >  
        
        <tr>
            <td style="width: 3%">
            </td>
            <td class="rj" style="vertical-align: middle; width:25%; text-align: right">
                <asp:Label ID="lblFirstName" runat="server" Style="font-size: 16px; color: red"
                    Text="Contact First Name:" Width="189px">
                    </asp:Label>&nbsp;</td>
            <td style="width: 75%">
            <asp:TextBox ID="txtFirstName" runat="server" MaxLength="50" TabIndex="6" Width="209px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 3%">
            </td>
            <td class="rj" style="vertical-align: middle; width:25%; text-align: right">
            <asp:Label ID="lblLastName" runat="server" Text="Contact Last Name:" style="color:red" Width="188px"></asp:Label>&nbsp</td>
            <td style="width: 75%">
            <asp:TextBox ID="txtLastName" runat="server" TabIndex="7" MaxLength="50" Width="210px">
            </asp:TextBox></td>
        </tr>
        </table> 
           <div id=divFirstLast runat=server style="display:inline"> 
            <table cellpadding=0 cellspacing=0 width=100%  > 
            <tr>
            <td style="width: 3%">
            </td>
            <td class="rj" style="vertical-align: middle; width:25%; text-align: right">
            &nbsp</td>
            <td style="width: 75%">
            <asp:Label ID="lblContactName" runat="server" style="color:purple; font-size: 16px;" Width="394px"></asp:Label></td>
        </tr>
        </table>
           </div>
        <table cellpadding=0 cellspacing=0 width=100%  > 
       <tr>
           <td style="width: 3%;">
           </td>
           <td class="rj" style="width:25%; vertical-align: middle; text-align: right;">
            <asp:Label ID="lblAddress1" runat="server" Text="First Address Line:" style="color:red" Width="152px"></asp:Label>&nbsp;</td>
           <td style="width: 75%; ">
            <asp:TextBox ID="txtAddress1" runat="server" TabIndex="8" MaxLength="50" Width="210px" ></asp:TextBox></td>
       </tr>
       </table>
   <div id=divFirstAddress runat=server style="display:inline"> 
            <table cellpadding=0 cellspacing=0 width=100%  > 
            <tr>
            <td style="width: 3%">
            </td>
            <td class="rj" style="vertical-align: middle; width:25%; text-align: right">
                &nbsp;&nbsp</td>
            <td style="width: 75%">
            <asp:Label ID="lblContactAddress" runat="server" style="color:purple; font-size: 16px;" Width="399px"></asp:Label></td>
        </tr>
        </table>
       </div>
    
        
      <table cellpadding=0 cellspacing=0 width=100%  >    
       <tr>
           <td style="width: 3%; height: 24px;" >
           </td>
           <td  style="width:25%; vertical-align: middle; text-align: right; height: 24px;">
               <asp:Label ID="lblAddress2" runat="server"  Style="color: black"
                   Text="Second Address Line:" Width="192px" ></asp:Label>&nbsp;</td>
           <td style="width: 75%; height: 24px;">
               <asp:TextBox ID="txtAddress2" runat="server" TabIndex="9" MaxLength="50" Width="210px"></asp:TextBox></td>
       </tr>
    <tr>
        <td style="width: 3%; ">
        </td>
        <td style="width:25%; vertical-align: middle; text-align: right;" class=rj>
            <asp:Label ID="Label2" runat="server"  Style="color: black" Text="Third Address Line:"
                Width="188px" ></asp:Label>&nbsp</td>
        <td style="width: 75%; ">
            <asp:TextBox ID="txtAddress3" runat="server" TabIndex="10" MaxLength="50" Width="210px"></asp:TextBox></td>
    </tr>
    <tr>
     
        <td style="width: 3%">
        </td>
        <td style="width:25%; vertical-align: middle; text-align: right;" class=rj>
            <asp:Label ID="lblCity" runat="server" Text="City:" style="color:red"></asp:Label>&nbsp</td>
        <td style="width: 75%">
        <asp:TextBox ID="txtCity" runat="server" TabIndex="11" MaxLength="50" Width="210px"></asp:TextBox></td>
    </tr>
     </table>
        <div id=divCity runat=server style="display:inline">
           <table cellpadding=0 cellspacing=0 width=100%  > 
            <tr>
            <td style="width: 3%">
            </td>
            <td class="rj" style="vertical-align: middle; width:25%; text-align: right">
                &nbsp;&nbsp</td>
            <td style="width: 75%">
            <asp:Label ID="lblDivCity" runat="server" style="color:purple; font-size: 16px;" Width="402px"></asp:Label></td>
        </tr>
        </table>
        </div>
     <table cellpadding=0 cellspacing=0 width=100% >   
    <tr>
      
        <td style="width: 3%">
        </td>
        <td style="width:25%; vertical-align: middle; text-align: right;" class=rj>
            <asp:Label ID="lblState" runat="server" Text="USA/Canada:" 
            ForeColor="black"></asp:Label>&nbsp</td>
        <td style="width: 75%">
        <asp:DropDownList ID="cmbState" runat="server" 
        TabIndex="12" Width="257px" 
        DataTextField="State_Name" DataValueField="State_ID" >
        </asp:DropDownList></td>
    </tr>
     </table>
        <div id=divNAUSCanada runat=server style="display:none">
           <table cellpadding=0 cellspacing=0 width=100%  > 
            <tr>
            <td style="width: 3%">
            </td>
            <td class="rj" style="vertical-align: middle; width:25%; text-align: right">
                &nbsp;&nbsp</td>
            <td style="width: 75%">
            <asp:Label ID="lblAmerica" runat="server" style="color:purple; font-size: 16px;" Width="402px"></asp:Label></td>
        </tr>
        </table>
        </div>
        
        <table cellpadding=0 cellspacing=0 width=100%  >
        <tr>
            <td style="width: 3%">
            </td>
            <td class="rj" style="width:25%; vertical-align: middle; text-align: right;">
            <asp:Label ID="lblNewUserRegion" runat="server" Text="Region:" style="color:red; vertical-align: middle; text-align: right;" 
            Width="67px"></asp:Label>&nbsp</td>
            <td style="width: 75%">
            <asp:DropDownList ID="cmbRegion" runat="server" TabIndex="13" Width="257px" 
            DataTextField="Region" 
            DataValueField="Region_ID" 
            AutoPostBack=true
            OnSelectedIndexChanged="cmbRegion_SelectedIndexChanged" >
            </asp:DropDownList>
            <asp:Label ID="Label11" runat="server" Text="{Select 'Region' before 'Country', please.}" style="color: purple"></asp:Label></td>
        </tr>
        </table>
        <div id=DivShowCountry runat=server style="display:inline">
        <table cellpadding=0 cellspacing=0 width=100%  >
        
    <tr>
       
        <td style="width:3%">
        </td>
        <td style="width:25%; vertical-align: middle; text-align: right;" class=rj>
            <asp:Label ID="lblCountry" runat="server" Text="Country:" style="color:red"></asp:Label>&nbsp</td>
        <td style="width: 75%">
        <asp:DropDownList 
                  ID="cmbCountry" 
                  runat="server" 
                  TabIndex="15" 
                  Width="257px" 
                  DataTextField="Country" DataValueField="Country_ID"   >
        </asp:DropDownList>
            <asp:HiddenField ID="txtSelectedCountry" runat="server" Value=0  >
            </asp:HiddenField></td>
    </tr>
     </table>
     </div>
        <div id=divRegion runat=server style="display:inline">
           <table cellpadding=0 cellspacing=0 width=100%  > 
            <tr>
            <td style="width: 3%">
            </td>
            <td class="rj" style="vertical-align: middle; width:25%; text-align: right">
            &nbsp</td>
            <td style="width: 75%">
            <asp:Label ID="lblRegion" runat="server" style="color:purple; font-size: 16px;" Width="399px">
            </asp:Label></td>
        </tr>
        </table>
        </div>
        <table cellpadding=0 cellspacing=0 width=100%  >
        <tr>
            <td style="width:3%">
            </td>
            <td class="rj" style="width:25%; vertical-align: middle; text-align: right;">
            <asp:Label ID="lblPostal_Code" runat="server" Text="Postal or Zip Code:" 
             ForeColor="red" ></asp:Label>&nbsp</td>
            <td style="width: 75%">
            <asp:TextBox ID="txtPostal_Code" runat="server" TabIndex="16" AutoPostBack="False" MaxLength="15" ></asp:TextBox></td>
        </tr>
        </table>
        <div id=divZip runat=server>
           <table cellpadding=0 cellspacing=0 width=100%  > 
            <tr>
            <td style="width: 3%; height: 19px;">
            </td>
            <td class="rj" style="vertical-align: middle; width:25%; text-align: right; height: 19px;">
                &nbsp;&nbsp</td>
            <td style="width: 75%; height: 19px;">
            <asp:Label ID="lblZip" runat="server" style="color:purple; font-size: 16px;" Width="404px"></asp:Label></td>
        </tr>
        </table>
        </div>
        <table cellpadding=0 cellspacing=0 width=100%  >
            <tr>
                <td style="width: 3%">
                </td>
                <td class="rj" style="vertical-align: middle; width:25%; text-align: right">
            <asp:Label ID="lblemail" runat="server" Text="Contact email:" style="color:black"></asp:Label>&nbsp;</td>
                <td style="width: 75%">
                    <asp:TextBox ID="txtemail" runat="server" 
                    MaxLength="50" TabIndex="17" 
                    Width="210px"></asp:TextBox></td>
            </tr>
    <tr>
     
        <td style="width:3%;">
        </td>
        <td style="width:25%; vertical-align: middle; text-align: right;" class=rj>
            <asp:Label ID="lblTelephone" runat="server" Text="Telephone:"></asp:Label>&nbsp</td>
        <td style="width: 75%;">
            <asp:TextBox ID="txtTelephone" runat="server" TabIndex="18" MaxLength="50" Width="210px"></asp:TextBox></td>
    </tr>
    <tr>
        
        <td style="width:3%; height: 24px;">
        </td>
        <td style="width:25%; vertical-align: middle; text-align: right; height: 24px;" class=rj>
            <asp:Label ID="lblFAX" runat="server" Text="FAX:" ForeColor="Black" style="text-align: right"></asp:Label>&nbsp</td>
        <td style="width: 75%; height: 24px;">
            <asp:TextBox ID="txtFAX" runat="server" TabIndex="19" MaxLength="50" Width="210px" ></asp:TextBox></td>
    </tr>
    <tr>

        <td style="width:3%">
        </td>
        <td style="width:25%">
            &nbsp;</td>
        <td style="width: 75%">
            &nbsp;</td>
    </tr>
            <tr>
                <td style="width: 3%">
                </td>
                <td style="width:25%">
                </td>
                <td style="width: 75%">
                    <asp:LinkButton CssClass=ClickForMore ID="cmdUpdateCustomerInformation" 
             runat="server" Text="Update Customer Information" 
              Width="211px" OnClick="cmdSubmitNewUser_Click" TabIndex="20"  /></td>
            </tr>
            <tr>
                <td style="width: 3%">
                </td>
                <td style="width: 25%">
                </td>
                <td style="width: 75%">
                    &nbsp;<asp:Label ID="Label12"
                  runat="server" Style="color: purple" Text="{Please select 'Update Customer Information' if you have modified any data above. }"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 3%">
                </td>
                <td style="width:25%">
                    &nbsp;</td>
                <td style="width: 75%">
                </td>
            </tr>
    <tr>
   
        <td style="width:3%; ">
        </td>
        <td style="width:25%;">
            </td>
        <td style="width: 75%;" >
            <asp:LinkButton CssClass=ClickForMore 
             ID="cmdSubmitCustomer" 
             runat="server" Text="Begin Customer PD Search" 
             Width="312px" OnClick="cmdEnterApplication_Click" TabIndex="21"  />
            &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;
        </td>
    </tr>

        <tr>
            <td style="width:3%; ">&nbsp;
            </td>
            <td style="width:25%;">
            </td>
            <td style="width: 75%;">
                &nbsp;</td>
        </tr>
</table>
 </div>

<!--End of Register New User-->


</asp:Content>

