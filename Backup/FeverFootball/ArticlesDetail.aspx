<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ArticlesDetail.aspx.cs" Inherits="_Default" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


        <div class="sideColumn">
            <div class="sideTitleArticles">
                news
            </div>
            
            <div class="sideContent">
                <asp:GridView ID="GVNews" runat="server" Width="100%" AutoGenerateColumns="False"
                 ShowHeader="False" CellPadding="4" ForeColor="#333333" GridLines="None" PageSize="5">
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <span> 
                                    + <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# FF_Classes.Misc.getURL(int.Parse(Eval("LeagueID").ToString()), new Guid( Eval("NewsID").ToString())) %>'
                                     ForeColor="Green">
                                         <%# Eval("Title") %> 
                                    </asp:HyperLink>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </div>
            
            
            
            <div class="sideTitlePolls">
                Polls
            </div>
            
            <div class="sideContent">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        
                        <asp:Label ID="lblQuestion" runat="server" style="color: #FF3300"></asp:Label>
                        <br />
                        <br />
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                        </asp:RadioButtonList>
                        <br />
                        <asp:Button ID="btnPollSubmit" runat="server" onclick="BtnPollSubmit_Click" Text="Submit!" />
                        <br />
                        <br />
                        <asp:Label ID="lblResult" runat="server" Text="Result" Visible="false"></asp:Label>
                        <br />
                        <asp:BulletedList ID="BulletedList1" runat="server" >
                        </asp:BulletedList>
                    
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <img src="images/loadingSmall.gif" alt="" /> loading...
                    </ProgressTemplate>
                </asp:UpdateProgress>
                
            </div>
        </div>
        
        <div class="mainColumn">
            <div class="pageTitle">
            - articles
            </div>
        
        <div class="imgArticle">
            <asp:Image ID="ImageArticle" runat="server" />
        </div>
        <b><asp:Label ID="lblTitle" runat="server" ></asp:Label></b>
        <br />
        <asp:Label ID="lblDetails" runat="server" ></asp:Label>
        <br />  <br />
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Articles.aspx"> VIEW ALL ARTICLES </asp:HyperLink>
     
        <hr />  
        Share:   
		    
<a href="http://twitter.com/share" class="twitter-share-button" data-count="horizontal">Tweet</a><script type="text/javascript" src="http://platform.twitter.com/widgets.js"></script>

 <script src="http://connect.facebook.net/en_US/all.js#xfbml=1">
                </script>
                <fb:like href='www.feverfootball.com' show_faces="true" width="400" action="like" font="arial"></fb:like>
    	  
      <br />
         
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
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	

<script type="text/javascript">
	$(document).ready(function(){
		$("#featured > ul").tabs({fx:{opacity: "toggle"}}).tabs("rotate", 5000, true);
	});
</script>
    
</asp:Content>

