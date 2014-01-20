<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" 
         AutoEventWireup="true" 
         EnableEventValidation="false"
         CodeFile="ProductSearchCriteria.aspx.cs" 
         Inherits="ProductSearchCriteria" 
         Title="EMM Product Search Criteria"
          %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   <table  cellpadding=0 cellspacing=0>
        <tr>
            <td style="width: 14px">
            </td>
            <td style="width: 114px">
            </td>
            <td style="width: 100px">
            </td>
            <td style="width: 24px">
            </td>
        </tr>
        <tr>
            <td style="width: 14px">
            </td>
            <td colspan="2" rowspan="2">
            <asp:Label ID="lblSearchInstructions" runat="server" 
            Text="Instructions:  Select a Product first.  The Agency/Reference drop down list will be populated and associated Issue of Concern will be displayed.  The history of your selections will be displayed in 'Selected Declarations'. The form will clear each time you select a different product yet your selection history will be retained." Width="568px" 
            style="font-size: 16px; color: green; font-family: Times New Roman, Calibri, Arial"></asp:Label></td>
            <td style="width: 24px">
            </td>
        </tr>
        <tr>
            <td style="width: 14px; height: 24px;">
            </td>
            <td style="width: 24px; height: 24px;">
            </td>
        </tr>
        <tr>
            <td style="width: 14px; height: 24px">
            </td>
            <td colspan="2" rowspan="1">
            </td>
            <td style="width: 24px; height: 24px">
            </td>
        </tr>
        <tr>
            <td style="width: 14px; height: 24px">
            </td>
            <td colspan="2" rowspan="1">
                <asp:Label ID="Label2" runat="server" Style="font-size: 1em; color: blue" Text="If the declaration you need is not available here, please click the Contact Us link and identify what you need."
                    Width="566px"></asp:Label></td>
            <td style="width: 24px; height: 24px">
            </td>
        </tr>
            <tr>
                <td style="width: 14px">
                </td>
                <td colspan="3" rowspan="7">
                    &nbsp; &nbsp;&nbsp;<table style="width: 636px">
                        <tr>
                            <td style="width: 57px; text-align: center">
                                </td>
                            <td style="width: 107px">
                            </td>
                            <td style="width: 97px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 57px">
                                Step 1:</td>
                            <td style="width: 107px">
                <asp:Label ID="lblProductName" runat="server" Text="Choose a CG Product: " Width="302px"></asp:Label></td>
                            <td style="width: 97px">
                </td>
                        </tr>
                        <tr>
                            <td style="width: 57px">
                            </td>
                            <td style="width: 107px">
                <asp:DropDownList  runat="server" ID="cmbProduct" 
                width="200" DataValueField="Product_ID" 
                DataTextField="Product_Name" 
                OnSelectedIndexChanged="cmbProduct_SelectedIndexChanged" 
                AutoPostBack="True" AppendDataBoundItems="True" >
                </asp:DropDownList></td>
                            <td style="width: 97px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 57px">
                            </td>
                            <td style="width: 107px">
                                &nbsp;
                            </td>
                            <td style="width: 97px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 57px">
                                Step 2:</td>
                            <td style="width: 107px">
                    <asp:Label ID="lblAgency" runat="server" Text="Choose an Agency/Reference: " Width="290px"></asp:Label></td>
                            <td style="width: 97px">
                        </td>
                        </tr>
                        <tr>
                            <td style="width: 57px">
                            </td>
                            <td style="width: 107px">
                        <asp:DropDownList ID="cmbSearchAgency" 
                            runat="server" 
                            DataTextField="Agency" 
                            DataValueField="Agency_ID"       
                            Width="402px" 
                            AutoPostBack="true" 
                            OnSelectedIndexChanged="cmbSearchAgency_SelectedIndexChanged" 
                            AppendDataBoundItems="True" >
                    </asp:DropDownList></td>
                            <td style="width: 97px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 57px">
                            </td>
                            <td style="width: 107px">
                                &nbsp;</td>
                            <td style="width: 97px">
                            </td>
                        </tr>
                         </table>
                         <div id=divShowWarning runat=server style="display:none" >
                         <table> 
                        
                        <tr>
                            <td style="width: 57px">
                            </td>
                            <td style="width: 107px">
                                <asp:Label ID="lblWarning" runat="server" Font-Bold="True" Font-Names="Times New Roman,Arial"
                                    Font-Size="Medium" ForeColor="Red" Text="Warning" Width="403px"></asp:Label></td>
                            <td style="width: 97px">
                            </td>
                        </tr>
                        </table>
                        </div>
                        <div id=divShowGrid style="display:none" runat=server >
                        <table  cellpadding=0 cellspacing="0" style="border-right: CornflowerBlue thin solid; border-top: CornflowerBlue thin solid; border-left: CornflowerBlue thin solid; border-bottom: CornflowerBlue thin solid">
                        <tr>
                            <td style="width: 66px; vertical-align: text-top; text-align: left;">
                                Step 3:</td>
                            <td colspan="2" style="width: 465px">
                                
                                <asp:GridView ID="UserSelectionView" 
                                    runat="server"
                                    AlternatingRowStyle-BackColor="CornflowerBlue"
                                    HeaderStyle-BackColor=CornflowerBlue
                                    BorderColor=AntiqueWhite
                                    AutoGenerateColumns=False 
                                    Width="585px"
                                    ShowFooter=True 
                                    OnRowCommand="UserSelectionView_RowCommand" 
                                    GridLines="None" 
                                    Font-Names="Times New Roman,Arial"
                                    Font-Size=Medium>
                                    <Columns>
                                    <asp:BoundField DataField=ColrowID ReadOnly=True >
                                        <ItemStyle Font-Size="XX-Small" HorizontalAlign="Left" VerticalAlign="Middle" Width="10px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField=Product_Name ReadOnly=True Visible=False/>
                                    <asp:BoundField DataField=Product_ID ReadOnly=True Visible=False/>
                                    <asp:BoundField DataField=Agency ReadOnly=True HeaderText="Agency" Visible=False/>
                                    <asp:BoundField DataField=Agency_ID ReadOnly=True Visible=False/>
                                        <asp:TemplateField HeaderText="Issue of Concern">
                                            <EditItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Concern_Issue") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Concern_Issue") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <FooterTemplate>
                                        <asp:LinkButton  
                                        ID="btnIncludeSelected" runat="server" Text="CLICK here to place the above Issue(s) of Concern in your Declarations listing" 
                                         CommandName="IncludeSelected" />
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign=Left VerticalAlign="Middle"  Width="550px" Font-Italic="True" Font-Names="Times New Roman,Arial" Font-Size="Medium" ForeColor="Green" />
                                            <ControlStyle Width="550px" />
                                            <ItemStyle Width="550px" Font-Names="Times New Roman,Arial"/>
                                        </asp:TemplateField>
                                    <asp:BoundField DataField=Issue_ID ReadOnly=True Visible=False/>
                                 
                                    <asp:TemplateField HeaderText="Select" Visible=False>
                                        <ItemTemplate >
                                        <asp:Checkbox ID="chkIsSelected" runat="server" Checked='<%#Convert.ToBoolean(Eval("Is_Selected")) %>' >
                                    </asp:Checkbox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"   Wrap="True" 
                                    VerticalAlign="Middle" Width="25px" Font-Names="Times New Roman,Arial"/>
                                    <HeaderStyle HorizontalAlign="Center" Width="25px" />
                                        <ControlStyle Width="25px" />
                                        <FooterStyle Width="25px" />
                                    
                                        
                                    </asp:TemplateField>
                                                                     
                                    </Columns>
                                    <AlternatingRowStyle BackColor="CornflowerBlue" />
                                    <HeaderStyle BackColor="CornflowerBlue" />
                                </asp:GridView>
                                    <asp:ObjectDataSource ID="UserViewDataSource" 
                                    runat="server" 
                                    UpdateMethod="UpdateCurrentRow"
                                    SelectMethod="GetRowValuesCriteria"
                                    TypeName="DB_Values" 
                                    >
                                    <SelectParameters>
                                    <asp:Parameter Name="Product_ID" Type="Int32" />
                                    <asp:Parameter Name="Agency_ID" Type="Int32" />
                                    </SelectParameters>
                                    </asp:ObjectDataSource>
                                    
                                
                                
                               
                            </td>
                        </tr>
                            <tr>
                                <td style="vertical-align: text-top; width: 66px; text-align: left">
                                </td>
                                <td colspan="2" style="width: 465px">
                                    &nbsp;</td>
                            </tr>
                         <tr>
                            <td style="width: 66px; vertical-align: text-top; text-align: left;">
                                &nbsp;
                                </td>
                            <td colspan="2" style="width: 465px; background-color: bisque;">
                            &nbsp; <span style="color: black; font-weight: bold; font-size: 16px; vertical-align: middle; font-family: Times New Roman, Calibri, Arial; text-align: left;">Selected Declarations</span></td>
                         </tr>  <tr>
                            <td style="width: 66px; vertical-align: text-top; text-align: left;">
                                </td>
                            <td colspan="2" style="width: 465px">
                                
                                <asp:GridView ID="UserSelections" 
                                    runat="server"
                                    AlternatingRowStyle-BackColor="Bisque"
                                    HeaderStyle-BackColor=Bisque
                                    BorderColor=CornflowerBlue
                                    AutoGenerateColumns=False Width="597px"
                                    ShowFooter=True 
                                    GridLines="None"
                                    BorderStyle=Solid
                                    BorderWidth="1px"
                                    Font-Names="Times New Roman,Arial"
                                    >
                                    <Columns>
                                    <asp:BoundField DataField=ColrowID ReadOnly=True Visible=False/>
                                    <asp:BoundField DataField=Product_Name ReadOnly=True HeaderText="Product">
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField=Product_ID ReadOnly=True Visible=False/>
                                    <asp:BoundField DataField=Agency ReadOnly=True HeaderText="Agency">
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField=Agency_ID ReadOnly=True Visible=False/>
                                    <asp:BoundField DataField=Concern_Issue ReadOnly=True HeaderText="Issue of Concern">
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField=Issue_ID ReadOnly=True Visible=False/>
                                    <asp:BoundField DataField=Is_Selected ReadOnly=True Visible=False/>
                                    
                                    
                                    
                                    </Columns>
                                    <AlternatingRowStyle BackColor="Bisque" />
                                    <HeaderStyle BackColor="Bisque" />
                                </asp:GridView>
                                    <asp:ObjectDataSource ID="ObjectDataSource2" 
                                    runat="server" 
                                    SelectMethod="GetRowValuesSelected"
                                    TypeName="DB_Values" >
                                    <SelectParameters>
                                    <asp:Parameter Name="Product_ID" Type="Int32" />
                                    <asp:Parameter Name="Agency_ID" Type="Int32" />
                                    </SelectParameters>
                                    </asp:ObjectDataSource>
                                    
                                
                                
                               
                            </td>
                        </tr>
                        </table>
                        </div>
                        <div id=divSearchForResult style="display:none" runat=server >
                        <table cellpadding=0 cellspacing="0"  >
                        <tr>
                            <td style="width: 57px; vertical-align: text-top; text-align: left;">
                                &nbsp;</td>
                            <td colspan="2">
                                &nbsp;</td>
                            <tr>
                            <td style="width: 57px; vertical-align: text-top; text-align: left;">
                                Step 4:</td>
                            <td colspan="2">
                            <asp:LinkButton CssClass=ClickForMore id=btnShowQueueResults runat=server OnClick="ShowResultsToQueue"
                            Text="Add to Declarations" Width="156px" />
                            </td>
                       
                       
                       
                        </tr>
                          <tr>
                            <td style="width: 57px; vertical-align: text-top; text-align: left;">
                                &nbsp;</td>
                            <td colspan="2">
                               <asp:Label runat=server ID=lblContinue Text="Continue with Step #1 and Step #2, if you wish">
                               </asp:Label>                        
                                
                               
                            </td>
                        </tr>
                        </table>
                        </div>                       
                    </td>
            </tr>
            <tr>
                <td style="width: 14px; height: 21px;">
                </td>
            </tr>
            <tr>
                <td style="width: 14px">
                </td>
            </tr>
            <tr>
                <td style="width: 14px; height: 24px">
                </td>
            </tr>
            <tr>
                <td style="width: 14px">
                </td>
            </tr>
        <tr>
            <td style="width: 14px">
            </td>
        </tr>
            <tr>
                <td style="width: 14px" >
                </td>
            </tr>
            
 
</table>



</asp:Content>

