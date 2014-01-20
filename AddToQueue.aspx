<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" 
         AutoEventWireup="true" 
         CodeFile="AddToQueue.aspx.cs" 
         Inherits="AddToQueue" Title="Add to Declarations"
         EnableEventValidation="false"
         %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



      <div id=divButtons runat=server>
        <p>
        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:LinkButton CssClass=ClickForMore ID="btnViewSelected" 
                        runat="server" Text="View Selected" 
                        OnClick="ViewSelected_Click"  
                        Font-Size="14px" Width="106px" />&nbsp;<br />
        <br />
        &nbsp;<asp:Label ID="lblLimitSelection" runat="server" Text="Check 'View?' to add to your selection of declarations to view. You may select up to 5 declaration statements. If you require additional declarations, return and repeat the process selecting those you desire.    Retained records appear with a yellow background.  Please note that if you do not choose to retain a record for future use, you will have to retrieve that record again from Product Search." Font-Size="14px" width=617px />
        </p>
        </div>
        
        <div id=divNoRecords runat=server>
        <p style="font-size: 14px; font-family: Times New Roman, Times New Roman, Calibri, Arial" >
          No records have been placed in PDs Search
        </p>
        </div>
        

     
    
    <asp:GridView ID="QueueView" 
                  runat="server" AutoGenerateColumns="False" 
                  Width="811px" 
                  style="border:none;font-size: 16px; font-family: Times New Roman, Times New Roman, Calibri, Arial" 
                  GridLines="None" 
                  OnSelectedIndexChanged="QueueView_SelectedIndexChanged" 
                  Font-Names="Times New Roman"
     >
        <Columns>
            <asp:BoundField DataField="Product_Compliance_ID" HeaderText="CP ID" ReadOnly="True"
                SortExpression="Product_Compliance_ID" ShowHeader="False" >
                <ItemStyle Width="50px" Font-Size="11px" ForeColor="#0000C0" HorizontalAlign="Center" VerticalAlign="Middle" />
                 <HeaderStyle Font-Size="14px" />
            </asp:BoundField>

            <asp:BoundField DataField="Selection_Date" HeaderText="Queue Date" SortExpression="Selection_Date"  >
                <ItemStyle Width="150px" Font-Size="11px" ForeColor="Navy" HorizontalAlign="Left" VerticalAlign="Middle" />
                 <HeaderStyle Font-Size="14px" />
            </asp:BoundField>

            <asp:BoundField DataField="Product_Name" HeaderText="Product Name" SortExpression="Product_Name" >
            <ItemStyle Width="125px" HorizontalAlign="Left" VerticalAlign="Middle" Font-Size="14px" />
                <ControlStyle Font-Size="14px" />
                <HeaderStyle Font-Size="14px" />
            </asp:BoundField>

            <asp:BoundField DataField="Concern_Issue" HeaderText="Issue of Concern" SortExpression="Concern_Issue" >
            <ItemStyle Width="175px" HorizontalAlign="Left" VerticalAlign="Middle" Font-Size="14px" />
                <ControlStyle Font-Size="14px" />
                <HeaderStyle Font-Size="14px" />
            </asp:BoundField>

            <asp:BoundField DataField="Agency" HeaderText="Agency/Reference" SortExpression="Agency" >
            <ItemStyle Width="175px" HorizontalAlign="Left" VerticalAlign="Middle" Font-Size="14px" />
                <ControlStyle Font-Size="14px" />
                 <HeaderStyle Font-Size="14px" />
            </asp:BoundField>
           
                <asp:TemplateField HeaderText="View?">
                    <ItemTemplate>
                        <asp:CheckBox ID="ProductSelector" runat="server" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                     <HeaderStyle Font-Size="14px" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                
                  <asp:TemplateField HeaderText="Retain for future use?">
                    <ItemTemplate>
                    <asp:CheckBox AutoPostBack=true    OnCheckedChanged=SubmitNewQueueStatus
                    ID="RetainSelector" runat="server" Checked='<%# Convert.ToBoolean(Eval("Retain"))  %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                     <HeaderStyle Font-Size="14px" HorizontalAlign="Center" VerticalAlign="Middle"  />
                      <ControlStyle  />
                </asp:TemplateField>
        </Columns>
        <SelectedRowStyle  BorderColor="Black" />
        <AlternatingRowStyle  BorderColor="Black" />
    </asp:GridView>
    

</asp:Content>



