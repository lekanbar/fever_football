<%@ Page Language="C#" MasterPageFile="~/MasterPageEPL.master" AutoEventWireup="true" CodeFile="EPL.aspx.cs" Inherits="EPl" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="pageTitle">
    - English Premier League
    </div>
   
   <div>
    
    <div class="pageColumn1" >

    <asp:Panel ID="PanelMain" runat="server">
        <asp:Image ID="Image1" runat="server" CssClass="pageImage"   />
        <b><asp:Label ID="lblTitle" runat="server" ></asp:Label></b>
        <br />
        <asp:Label ID="lblDetails" runat="server" ></asp:Label>
        <asp:HyperLink ID="lnkMain" runat="server">read more</asp:HyperLink>
        <br />
        <br />    
    </asp:Panel>
    
    
      <asp:ListView ID="LVNews" runat="server" ItemPlaceholderID="ItemContent" GroupPlaceholderID="GroupContent"
       GroupItemCount="2" DataKeyNames="NewsID"
          EnableViewState="true" 
          onpagepropertieschanged="LVNews_PagePropertiesChanged"  >
        <LayoutTemplate> 
            <asp:PlaceHolder ID="GroupContent" runat="server" ></asp:PlaceHolder>     
             <div style="float:right;  font-size:16px; color:green;">
        <asp:DataPager ID="Pager" runat="server"  PagedControlID="LVNews" PageSize="6" EnableViewState="true" >                       
        <Fields>
            <asp:numericpagerfield ButtonCount="10" NextPageText="..." 
                PreviousPageText="..."  />
        </Fields>
        </asp:DataPager>
           </div>
        </LayoutTemplate>
        
        <GroupTemplate> 
            <div style="width:100%; overflow:auto; margin-bottom:5px; border:solid 1px #cccccc ; 
                padding-top:5px; padding-bottom:5px; height:100%;">
                    <asp:PlaceHolder ID="ItemContent" runat="server" >
                    </asp:PlaceHolder>
            </div>
        </GroupTemplate>
        
        <ItemTemplate>            
            <div class="largeLinks2" style=" width:270px; height:100%; margin-left:5px; float:left; border:solid 1px white;
                 background-color:#eeeeee; overflow:auto; padding:5px;"> 
                <b>
                <asp:HyperLink ID="HyperLink1" runat="server"
                NavigateUrl='<%# "EPLDetail.aspx?id=" + Eval("NewsID")  %>' >
                     <%# Eval("Title") %>                                      
                   </asp:HyperLink> 
                   </b>  
                   <br />
                    <asp:Image ID="C4Image" runat="server" CssClass="frontImage" 
                    ImageUrl='<%# Eval("ImageURL") %> ' />
                   
                   <%# shortner( Eval("Details").ToString(),150) %>
                   <span class="date">
                        <%# Eval("Date").ToString().Remove(Eval("Date").ToString().IndexOf(" ")) %>                                         
                     </span>
               
            </div>
        </ItemTemplate>
      </asp:ListView>


    </div>

<div class="pageColumn2">
                
    <div style="width:100%; margin-top:10px; padding-top:3px; padding-bottom:3px; font-size:18px; 
    color:White; background-color:#5D7B9D; font-weight:bold; margin-bottom:5px; text-align:center;">
    LEAGUE TABLE
    </div>
    <asp:GridView ID="GVLeagueTable" runat="server" Width="100%" 
        AutoGenerateColumns="False"  CellPadding="4"  Font-Size="12px"
        ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <Columns>
            <asp:TemplateField>
            <HeaderTemplate>
                <table>
                    <tr>
                        <td style="width:130px; ">
                            
                        </td>
                        <td class="bgGray">
                            P
                        </td>
                        <td class="bgGray2">
                            W
                        </td>
                        <td class="bgGray">
                            D
                        </td>
                        <td class="bgGray2">
                            L
                        </td>
                        <td class="bgGray">
                            GD
                        </td>
                        <td class="bgGray2">
                            PTS
                        </td>
                    </tr>
            </table>
            </HeaderTemplate>
            <ItemTemplate>
                <table>
                    <tr>
                        <td style="width:130px; ">
                             <%# Eval("TeamName") %> 
                        </td>
                        <td class="bgGray">
                            <%# Eval("P") %>
                        </td>
                        <td class="bgGray2">
                            <%# Eval("W") %>
                        </td>
                        <td class="bgGray">
                            <%# Eval("D") %>
                        </td>
                        <td class="bgGray2">
                            <%# Eval("L") %>
                        </td>
                        <td class="bgGray">
                            <%# Eval("Diff") %>
                        </td>
                        <td class="bgGray2">
                            <%# Eval("Points") %>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
            
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle  Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
    
    
              
    <div style="width:100%; margin-top:10px; padding-top:3px; padding-bottom:3px; font-size:18px; 
    color:White; background-color:#555555; font-weight:bold; margin-bottom:5px; text-align:center;">
    ASSIST TABLE
    </div>
    
    <asp:GridView ID="gvAssists" runat="server" Width="100%"  ShowHeader="false"
        AutoGenerateColumns="False"  CellPadding="4"  Font-Size="12px" PageSize="10"
        ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <Columns>
            <asp:TemplateField>
            <ItemTemplate>
            
                <asp:Image ID="Image2" runat="server" ImageUrl='<%# Eval("ImageURL") %>' AlternateText='image' 
                CssClass="left allowanceRight" Width="40px" />
                <%# Eval("Name") %> <br />
                <%# Eval("AssistCount") + " Assist(s)" %>
            </ItemTemplate>            
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle  Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
    
    
    <asp:Panel ID="PanelTopScorer" runat="server">
    <div style="width:100%; margin-top:20px; padding-top:3px; padding-bottom:3px; font-size:18px; 
    color:White; background-color:Maroon; font-weight:bold; margin-bottom:5px; text-align:center;">
    TOP SCORER
    </div>
                <asp:Image ID="imgScorer" runat="server" AlternateText="ImageURL" Width="150px" CssClass="left allowanceRight"  />
                <table>
                    <tr style="font-weight:bold; color:Maroon;">
                        
                        <td>
                            <asp:Label ID="lblName" runat="server" Text="Label"></asp:Label> 
                        </td>
                    </tr>
                    <tr style="font-weight:bold; color:Blue;">
                        
                        <td>
                            <asp:Label ID="lblGoals" runat="server" Text="Label"></asp:Label> Goal(s)
                        </td>
                    </tr>
                </table>
    </asp:Panel>
                
</div>
       

</div> 
</asp:Content>

