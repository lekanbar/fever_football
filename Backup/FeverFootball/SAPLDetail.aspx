<%@ Page Language="C#" MasterPageFile="~/MasterPageSAPL.master" AutoEventWireup="true" CodeFile="SAPLDetail.aspx.cs" Inherits="EPl" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="pageTitle">
    - South African Premier League
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
        <br />  
        <br />   
       <b> <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="SAPL.aspx"> VIEW ALL NEWS UPDATES</asp:HyperLink></b>
    
        <hr />  
<a href="http://twitter.com/share" class="twitter-share-button" data-count="horizontal">Tweet</a><script type="text/javascript" src="http://platform.twitter.com/widgets.js"></script>

 <script src="http://connect.facebook.net/en_US/all.js#xfbml=1">
                </script>
                <fb:like href='www.feverfootball.com' show_faces="true" width="400" action="like" font="arial"></fb:like>
    	</asp:Panel>
    
        <asp:Panel ID="PanelComments" runat="server">
               <br />
               <div class="right" style="color:Red; margin-bottom:5px;">
                COMMENTS: (<asp:Label ID="lblCommentCount" runat="server" Text=""></asp:Label>)
                <br />
                </div>
                
               <asp:GridView ID="GVComments" runat="server" AutoGenerateColumns="False" Width="100%"  CssClass="right"
                                    ShowHeader="False" CellPadding="4" ForeColor="#333333" GridLines="None" >
                   <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <i>
                               <b> <%# getAuthor( Eval("ID").ToString() ) %></b> said:
                            </i>
                            <br />
                            <%# Eval("Details") %>
                            <br />
                            <div class="right">
                                <%# Eval("Date") %>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                   <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                   <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                   <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                   <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                   <AlternatingRowStyle BackColor="White" />
               </asp:GridView>
               
               <br /><br />
               <div class="right" style="width:100%; margin-top:10px; margin-bottom:20px;">
               <div class="right" style="color:Red;">
                LEAVE A COMMENT:
                </div>
                <br />
                   <asp:Panel ID="pnlUser1" runat="server">
                       <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>
                   </asp:Panel>
                   <asp:Panel ID="pnlUser2" runat="server">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  Display="Dynamic"
                                ErrorMessage="*" ValidationGroup="c1" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                                Your name:    
                            </td>
                            <td>
                                <asp:TextBox ID="txtName" runat="server" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  Display="Dynamic"
                                ErrorMessage="*" ValidationGroup="c1" ControlToValidate="txtEmailAddress"></asp:RequiredFieldValidator>
                                Your email address:    
                             </td>
                            <td>
                                <asp:TextBox ID="txtEmailAddress" runat="server" Width="100%"></asp:TextBox>
                                <br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
                                ErrorMessage="invalid email address" ValidationGroup="c1" 
                                    ControlToValidate="txtEmailAddress" 
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"  Display="Dynamic"
                                ErrorMessage="*" ValidationGroup="c1" ControlToValidate="txtComment"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"  Display="Dynamic"
                                ErrorMessage="*" ValidationGroup="c2" ControlToValidate="txtComment"></asp:RequiredFieldValidator>
                                Your comment: </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:TextBox ID="txtComment" runat="server" Width="100%" TextMode="MultiLine"></asp:TextBox>
                                <br />
                                
                            </td>
                        </tr>
                    </table>
                   </asp:Panel>
                    <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                   <br />
                   <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT COMMENT" OnClick="btnSubmit_Click" />
                </div>
        
        </asp:Panel>
               
    
		    

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

