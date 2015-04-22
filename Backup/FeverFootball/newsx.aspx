<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="news.aspx.cs" Inherits="_Default" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


        <div class="sideColumn">
            <div class="sideTitleArticles">
                articles
            </div>
            
            <div class="sideContent">
                <asp:GridView ID="GVArticles" runat="server" Width="100%" AutoGenerateColumns="False"
                 ShowHeader="False" CellPadding="4" ForeColor="#333333" GridLines="None" PageSize="5">
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <span> 
                                    + <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "Articles.aspx?id=" + Eval("ArticleID")  %>'
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
            - latest news
            </div>
            
            <asp:GridView ID="GVNews" runat="server" Width="100%" GridLines="None"  ShowHeader="false" Font-Size="15px"
                PageSize="20" AutoGenerateColumns="False" CellPadding="10" ForeColor="#333333" CellSpacing="5" >
                <RowStyle BackColor="#eeeeee" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <span class="largeLinks"> 
                                + <asp:HyperLink ID="HyperLink1" runat="server"
                                NavigateUrl='<%# "NewsDetail.aspx?itemID=" + Eval("NewsID")  %>' >
                                     <%# Eval("Title") %>
                                      
                                     <span class="date">
                                        <%# Eval("Date").ToString().Remove(Eval("Date").ToString().IndexOf(" ")) %>                                         
                                     </span>
                                </asp:HyperLink>
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
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	

<script type="text/javascript">
	$(document).ready(function(){
		$("#featured > ul").tabs({fx:{opacity: "toggle"}}).tabs("rotate", 5000, true);
	});
</script>
    
</asp:Content>

