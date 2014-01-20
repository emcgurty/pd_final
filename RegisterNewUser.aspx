<%@ Page Language="C#" 
         MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
         CodeFile="RegisterNewUser.aspx.cs" Inherits="RegisterNewUser" 
         Title="User Detail" EnableEventValidation="false"   %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script  language="javascript" type="text/javascript">

function LearnSelectedValue(cmbValue)
{
   
   var vcmbCountry = document.getElementById("<%= cmbCountries.ClientID%>");
   var vtxtSelectedCountry = document.getElementById("<%= txtSelectedCountry.ClientID%>");
   vtxtSelectedCountry.value = vcmbCountry.value;
    
}

function PicklistChanged()
{
       
        var vRegion = document.getElementById("<%= cmbNewUserRegion.ClientID%>");
        var IDval = vRegion.value;
        var vCountry = document.getElementById("<%= cmbCountries.ClientID%>");
      
        
     
        if ( vCountry.length > 0)
         {
          for (i=vCountry.length-1; i>=0; i--)
         {  vCountry.remove(i);
         }  
          }
         var opt = document.createElement("option");
         
<%                
        BuildIfStatements();   
 %>
}

  




</script>
   
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
            Width="516px" style="font-size: 16px; color: green; font-family: Times New Roman, Calibri, Arial" ></asp:Label></td>
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
       <tr>
           <td style="width: 5px">
           </td>
           <td colspan="2" style="width: 543px">
            <asp:Label ID="lblRequired" runat="server" Style="font-size: 16px; color: red; font-family: Times New Roman, Calibri, Arial"
                Text="Red fields are required. "
                ForeColor="#3300CC"></asp:Label></td>
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
            <td style="width: 3%">
            </td>
            <td class="rj" style="vertical-align: middle; width: 30%; text-align: right">
                <asp:Label ID="lblUserName" runat="server" Style="color: black" Text="User Name:">
                </asp:Label>
                &nbsp</td>
            <td style="width: 75%">
                <asp:Label ID="txtUserName" runat="server" Width="218px"></asp:Label></td>
        </tr>
     <tr>
     <td style="width: 3%">
        </td>
        <td style="width: 30%; text-align: right; vertical-align: middle;" class=rj>
            <asp:Label ID="lblemail" runat="server" Text="email:" style="color:black"></asp:Label>&nbsp</td>
        <td style="width: 75%">
            <asp:label ID="txtemail" runat="server" Width="430px" ></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 3%">
        </td>
        <td style="width: 30%; vertical-align: middle; text-align: right;">
            <asp:Label ID="lblNewUserSaluation" runat="server" Text="Salutation:" style="color:black">
            </asp:Label>&nbsp
            </td>
        <td style="width: 75%">
            <asp:DropDownList ID="cmbNewUserSalutation" runat="server"  DataValueField="Salutation_ID" DataTextField="Salutation_Text" TabIndex="1" >
            </asp:DropDownList>
            &nbsp; &nbsp;&nbsp;
        </td>
    </tr>
        <tr>
            <td style="width: 3%">
            </td>
            <td class="rj" style="vertical-align: middle; width: 30%; text-align: right">
            <asp:Label ID="lblFirstName" runat="server" Text="First Name (50 characters):" style="color:red; font-size: 16px;" Width="189px"></asp:Label>&nbsp</td>
            <td style="width: 75%">
            <asp:TextBox ID="txtFirstName" runat="server" TabIndex="2" MaxLength="50" Width="210px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 3%">
            </td>
            <td class="rj" style="vertical-align: middle; width: 30%; text-align: right">
            <asp:Label ID="lblLastName" runat="server" Text="Last Name (50):" style="color:red" Width="108px"></asp:Label>&nbsp</td>
            <td style="width: 75%">
            <asp:TextBox ID="txtLastName" runat="server" TabIndex="3" MaxLength="50" Width="210px"></asp:TextBox>
            </td>
        </tr>
        </table>
        <div id=divFirstLast runat=server style="display:none">
        <table cellpadding=0 cellspacing=0  width=100% >  
        <tr>
            <td style="width: 3%">
            </td>
            <td  style="vertical-align: middle; width: 30%; text-align: right">
            </td>
            <td style="width: 75%; font-size: 16px; color: purple;">
                A first and last name is required</td>
        </tr>
        </table>
        </div>
    
    <table cellpadding=0 cellspacing=0 width=100%  >  
    <tr>
        <td style="width: 3%; ">
        </td>
        <td style="width: 30%; vertical-align: middle; text-align: right;" class=rj>
            <asp:Label ID="lblCompanyName" runat="server" Text="Company Name (100):" style="color: black" Width="151px"></asp:Label>&nbsp</td>
        <td style="width: 75%; ">
            <asp:TextBox ID="txtCompanyName" runat="server" TabIndex="4" MaxLength="100" Width="430px"></asp:TextBox></td>
    </tr>
       <tr>
           <td style="width: 3%;">
           </td>
           <td class="rj" style="width: 30%; vertical-align: middle; text-align: right;">
            <asp:Label ID="lblAddress1" runat="server" Text="First Address Line (50):" style="color:red" Width="152px"></asp:Label>&nbsp;</td>
           <td style="width: 75%; ">
            <asp:TextBox ID="txtAddress1" runat="server" TabIndex="5" MaxLength="50" Width="210px" ></asp:TextBox></td>
       </tr>
       </table>
        <div id=divFirstAddress runat=server style="display:none">
        <table cellpadding=0 cellspacing=0 width=100%  >  
        <tr>
            <td style="width: 3%">
            </td>
            <td style="vertical-align: middle; width: 30%; text-align: right">
            </td>
            <td style="width: 75%; font-size: 16px; color: purple;">
                A first address line is required</td>
        </tr>
        </table>
        </div>
        
      <table cellpadding=0 cellspacing=0 width=100%  >    
       <tr>
           <td style="width: 3%;" >
           </td>
           <td  style="width: 30%; vertical-align: middle; text-align: right; ">
               <asp:Label ID="lblAddress2" runat="server"  Style="color: black"
                   Text="Second Address Line (50):" Width="192px" ></asp:Label>&nbsp;</td>
           <td style="width: 75%; ">
               <asp:TextBox ID="txtAddress2" runat="server" TabIndex="6" MaxLength="50" Width="210px"></asp:TextBox></td>
       </tr>
    <tr>
        <td style="width: 3%; ">
        </td>
        <td style="width: 30%; vertical-align: middle; text-align: right;" class=rj>
            <asp:Label ID="Label2" runat="server"  Style="color: black" Text="Third Address Line (50):"
                Width="188px" ></asp:Label>&nbsp</td>
        <td style="width: 75%; ">
            <asp:TextBox ID="txtAddress3" runat="server" TabIndex="7" MaxLength="50" Width="210px"></asp:TextBox></td>
    </tr>
    <tr>
     
        <td style="width: 3%">
        </td>
        <td style="width: 30%; vertical-align: middle; text-align: right;" class=rj>
            <asp:Label ID="lblCity" runat="server" Text="City (50):" style="color:red"></asp:Label>&nbsp</td>
        <td style="width: 75%">
            <asp:TextBox ID="txtCity" runat="server" TabIndex="8" MaxLength="50" Width="210px"></asp:TextBox></td>
    </tr>
     </table>
        <div id=divCity runat=server style="display:none">
        <table cellpadding=0 cellspacing=0 width=100%  >
              <tr>
            <td style="width: 3%">
            </td>
            <td class="rj" style="vertical-align: middle; width: 30%; text-align: right">
            </td>
            <td style="width: 75%; font-size: 16px; color: purple;">
                A city is required</td>
        </tr>
        </table>
        </div>
     <table cellpadding=0 cellspacing=0 width=100% >   
    <tr>
      
        <td style="width: 3%">
        </td>
        <td style="width: 30%; vertical-align: middle; text-align: right;" class=rj>
            <asp:Label ID="lblState" runat="server" Text="USA/Canada:" 
            ForeColor="Red"></asp:Label>&nbsp</td>
        <td style="width: 75%">
            <asp:DropDownList ID="cmbState" runat="server" DataTextField="State_Name" 
            DataValueField="State_ID" TabIndex="9" Width="175px" AutoPostBack="false">
            </asp:DropDownList></td>
    </tr>
     </table>
        <div id=divNAUSCanada runat=server style="display:none">
        <table cellpadding=0 cellspacing=0 width=100%  >
        <tr>
            <td style="width: 3%">
            </td>
            <td class="rj" style="vertical-align: middle; width: 30%; text-align: right">
            </td>
            <td style="width: 75%; font-size: 16px; color: purple; font-family: Times New Roman, Calibri, Arial;">
                A State/Territory/Province selection is required of North America</td>
        </tr>
        </table>
        </div>
        
        <table cellpadding=0 cellspacing=0 width=100%  >
        <tr>
            <td style="width: 3%">
            </td>
            <td class="rj" style="width: 30%; vertical-align: middle; text-align: right;">
            <asp:Label ID="lblNewUserRegion" runat="server" Text="Region:" style="color:red">
            </asp:Label>&nbsp</td>
            <td style="width: 75%">
            <asp:DropDownList ID="cmbNewUserRegion"  
             AutoPostBack="True" runat="server" 
             DataTextField="Region" DataValueField="Region_ID" TabIndex="10" OnSelectedIndexChanged="cmbNewUserRegion_SelectedIndexChanged" >
            </asp:DropDownList ></td>
        </tr>
        <tr>
            <td style="width: 3%">
            </td>
            <td class="rj" style="vertical-align: middle; width: 30%; text-align: right">
            </td>
            <td style="font-size: 14px; width: 75%; color: green; font-family: Times New Roman, Calibri, Arial">
                Note: Before selecting a Country, please select a Region first.</td>
        </tr>
    <tr>
       
        <td style="width:3%">
        </td>
        <td style="width: 30%; vertical-align: middle; text-align: right;" class=rj>
            <asp:Label ID="lblCountry" runat="server" Text="Country:" style="color:red"></asp:Label>&nbsp</td>
        <td style="width: 75%">
        <asp:DropDownList ID="cmbCountries" runat="server"  
                          DataTextField="Country" DataValueField="Country_ID" 
                          AutoPostBack=false
         TabIndex="11" Width="177px"  >
        </asp:DropDownList>
            <asp:HiddenField ID="txtSelectedCountry" runat="server" Value=0  
            >
            </asp:HiddenField></td>
    </tr>
     </table>
        <div id=divRegion runat=server style="display:none">
        <table cellpadding=0 cellspacing=0 width=100%  >
        <tr>
            <td style="width: 3%">
            </td>
            <td class="rj" style="vertical-align: middle; width: 30%; text-align: right">
            </td>
            <td style="width: 75%; font-size: 16px; color: purple; font-family: Times New Roman, Calibri, Arial;">
                A region with country selection is required</td>
        </tr>
        </table>
        </div>
        <table cellpadding=0 cellspacing=0 width=100%  >
        <tr>
            <td style="width:3%">
            </td>
            <td class="rj" style="width: 30%; vertical-align: middle; text-align: right;">
            <asp:Label ID="lblPostal_Code" runat="server" Text="Postal or Zip Code (15):" 
             ForeColor="#FF0000" ></asp:Label>&nbsp</td>
            <td style="width: 75%">
            <asp:TextBox ID="txtPostal_Code" runat="server" TabIndex="12" AutoPostBack="False" MaxLength="15" ></asp:TextBox></td>
        </tr>
        </table>
        <div id=divZip runat=server>
        <table cellpadding=0 cellspacing=0 width=100%  >
        <tr>
            <td style="width: 3%">
            </td>
            <td class="rj" style="vertical-align: middle; width: 30%; text-align: right">
            </td>
            <td style="width: 75%; font-size: 16px; color: purple; font-family: Times New Roman, Calibri, Arial;">
                A postal or zip code is required</td>
        </tr>
        </table>
        </div>
        <table cellpadding=0 cellspacing=0 width=100%  >
    <tr>
     
        <td style="width:3%;">
        </td>
        <td style="width: 30%; vertical-align: middle; text-align: right;" class=rj>
            <asp:Label ID="lblTelephone" runat="server" Text="Telephone (50):"></asp:Label>&nbsp</td>
        <td style="width: 75%;">
            <asp:TextBox ID="txtTelephone" runat="server" TabIndex="13" MaxLength="50" Width="210px"></asp:TextBox></td>
    </tr>
    <tr>
        
        <td style="width:3%">
        </td>
        <td style="width: 30%; vertical-align: middle; text-align: right;" class=rj>
            <asp:Label ID="lblFAX" runat="server" Text="FAX (50):" ForeColor="Black" style="text-align: right"></asp:Label>&nbsp</td>
        <td style="width: 75%">
            <asp:TextBox ID="txtFAX" runat="server" TabIndex="14" MaxLength="50" Width="210px" ></asp:TextBox></td>
    </tr>
    <tr>

        <td style="width:3%">
        </td>
        <td style="width: 30%">
            &nbsp;</td>
        <td style="width: 75%">
            &nbsp;</td>
    </tr>
    <tr>
   
        <td style="width:3%; ">
        </td>
        <td style="width: 30%;">
            </td>
        <td style="width: 75%;" >
            <asp:LinkButton CssClass=ClickForMore ID="cmdSubmitNewUser" runat="server" Text="Complete Registration" style="border-right: gray thin solid; border-top: gray thin solid; font-size: 16px; border-left: gray thin solid; color: black; border-bottom: gray thin solid; font-family: Times New Roman, Calibri, Arial; background-color: transparent" Width="184px" OnClick="cmdSubmitNewUser_Click" TabIndex="15"  />
            &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;
        </td>
    </tr>

        <tr>
            <td style="width:3%; ">&nbsp;
            </td>
            <td style="width: 30%;">
            </td>
            <td style="width: 75%;">
            </td>
        </tr>
</table>

<!--End of Register New User-->


</asp:Content>

