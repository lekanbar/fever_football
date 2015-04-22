<%@ Page Language="C#" MasterPageFile="~/MasterPageCPL.master" AutoEventWireup="true" CodeFile="CPL.aspx.cs" Inherits="EPl" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="pageTitle">
    - Champions League
    </div>
   
   <div>
    
    <div class="mainColumn2" >

    <asp:Panel ID="PanelMain" runat="server">
        <div class="imgNews">
            <asp:Image ID="Image1" runat="server" />
        </div>
        <b><asp:Label ID="lblTitle" runat="server" ></asp:Label></b>
        <br />
        <asp:Label ID="lblDetails" runat="server" ></asp:Label>
        <asp:HyperLink ID="lnkMain" runat="server">read more</asp:HyperLink>
        <br />
        <hr />    
    </asp:Panel>
    
    
            <asp:GridView ID="GVNews" runat="server" Width="100%" GridLines="None"  ShowHeader="false"
                PageSize="20" AutoGenerateColumns="False" CellPadding="10" 
        ForeColor="#333333" CellSpacing="5" 
        onpageindexchanging="GVNews_PageIndexChanging" >
                <RowStyle BackColor="#eeeeee" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <span class="largeLinks2"> 
                                <b>
                                + <asp:HyperLink ID="HyperLink1" runat="server"
                                NavigateUrl='<%# "CPLDetail.aspx?id=" + Eval("NewsID")  %>' >
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
                               
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>    
                </Columns>        
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
		    

    </div>

<div class="sideColumn2">
                
    <div style="width:100%; margin-top:10px; padding-top:3px; padding-bottom:3px; font-size:18px; 
    color:White; background-color:#5D7B9D; font-weight:bold; margin-bottom:5px; text-align:center;">
    LEAGUE TABLE
    </div>
    <asp:GridView ID="GVLeagueTable" runat="server" Width="100%" 
        AutoGenerateColumns="False"  CellPadding="4" 
        ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <Columns>
            <asp:TemplateField>
            <HeaderTemplate>
                <table>
                    <tr>
                        <td style="width:180px; ">
                            
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
                            F
                        </td>
                        <td class="bgGray2">
                            A
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
                        <td style="width:180px; ">
                            <asp:Image ID="Image2" runat="server" ImageUrl='<%# Eval("TeamLogo") %>' 
                            AlternateText="" CssClass="clubIcon" />
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
                            <%# Eval("F") %>
                        </td>
                        <td class="bgGray2">
                            <%# Eval("A") %>
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
        <HeaderStyle  Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
    
    <asp:Panel ID="PanelTopScorer" runat="server">
    <div style="width:100%; margin-top:20px; padding-top:3px; padding-bottom:3px; font-size:18px; 
    color:White; background-color:Maroon; font-weight:bold; margin-bottom:5px; text-align:center;">
    TOP SCORER
    </div>
                <asp:Image ID="imgScorer" runat="server" CssClass="imgScorer" ImageUrl='<%# Eval("") %>' 
                AlternateText="ImageURL" Width="200px" />
                <table>
                    <tr style="font-weight:bold; color:Maroon;">
                        <td>NAME</td>
                        <td>
                            <asp:Label ID="lblName" runat="server" Text="Label"></asp:Label> 
                        </td>
                    </tr>
                    <tr style="font-weight:bold; color:Blue;">
                        <td>GOALS</td>
                        <td>
                            <asp:Label ID="lblGoals" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblScorerDetails" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                </table>
    </asp:Panel>
                
</div>
       

</div> 
</asp:Content>

