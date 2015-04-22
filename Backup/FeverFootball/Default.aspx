<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="Untitled Page" %>

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
                                     >
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
        
		    <div id="featured"  >
		      <ul class="ui-tabs-nav">
	            <li class="ui-tabs-nav-item ui-tabs-selected" id="nav-fragment-1">
	            <a href="#fragment-1">
	            <asp:Image ID="GTImage1" runat="server" ImageUrl="" AlternateText="" />
	            <span><asp:Label ID="GTLabel1" runat="server" Text=""></asp:Label></span>
	            </a></li>
	            
	            <li class="ui-tabs-nav-item" id="nav-fragment-2">
	            <a href="#fragment-2">
	            <asp:Image ID="GTImage2" runat="server" ImageUrl="" AlternateText="" />
	            <span><asp:Label ID="GTLabel2" runat="server" Text=""></asp:Label></span>
	            </a></li>
	            
	            <li class="ui-tabs-nav-item" id="nav-fragment-3">
	            <a href="#fragment-3">
	            <asp:Image ID="GTImage3" runat="server" ImageUrl="" AlternateText="" />
	            <span><asp:Label ID="GTLabel3" runat="server" Text=""></asp:Label></span>
	            </a></li>
	            
	            <li class="ui-tabs-nav-item" id="nav-fragment-4">
	            <a href="#fragment-4">
	            <asp:Image ID="GTImage4" runat="server" ImageUrl="" AlternateText="" />
	            <span><asp:Label ID="GTLabel4" runat="server" Text=""></asp:Label></span>
	            </a></li>
	          </ul>

	        <!-- First Content -->
	        <div id="fragment-1" class="ui-tabs-panel" style="">
	            <asp:Image ID="GImage1" runat="server" ImageUrl="" AlternateText="" />
			     <div class="info" >
				    <h2>				    
				    <asp:Label ID="GCLabel1" runat="server" Text=""></asp:Label>
				    </h2>
				    <p>
                        <asp:Label ID="GDLabel1" runat="server" Text=""></asp:Label>
                        <asp:HyperLink ID="GDHyperLink1" runat="server" ForeColor="White">READ MORE</asp:HyperLink>
                    </p>
			     </div>
	        </div>

	        <!-- Second Content -->
	        <div id="fragment-2" class="ui-tabs-panel ui-tabs-hide" style="">
	            <asp:Image ID="GImage2" runat="server" ImageUrl="" AlternateText="" />
			     <div class="info" >
				    <h2>
				    <asp:Label ID="GCLabel2" runat="server" Text=""></asp:Label>
				    </h2>
			        <p>
                        <asp:Label ID="GDLabel2" runat="server" Text=""></asp:Label>
                        <asp:HyperLink ID="GDHyperLink2" runat="server" ForeColor="White">READ MORE</asp:HyperLink>
                    </p>
			     </div>
	        </div>

	        <!-- Third Content -->
	        <div id="fragment-3" class="ui-tabs-panel ui-tabs-hide" style="">
	            <asp:Image ID="GImage3" runat="server" ImageUrl="" AlternateText="" />
			     <div class="info" >
				    <h2>				    
				    <asp:Label ID="GCLabel3" runat="server" Text=""></asp:Label>
				    </h2>
			        <p>
                        <asp:Label ID="GDLabel3" runat="server" Text=""></asp:Label>
                        <asp:HyperLink ID="GDHyperLink3" runat="server" ForeColor="White">READ MORE</asp:HyperLink>
                    </p>
	             </div>
	        </div>

	        <!-- Fourth Content -->
	        <div id="fragment-4" class="ui-tabs-panel ui-tabs-hide" style="">
	            <asp:Image ID="GImage4" runat="server" ImageUrl="" AlternateText="" />
			     <div class="info" >
				    <h2>
				    <asp:Label ID="GCLabel4" runat="server" Text=""></asp:Label>
				    </h2>
			        <p>
                        <asp:Label ID="GDLabel4" runat="server" Text=""></asp:Label>
                        <asp:HyperLink ID="GDHyperLink4" runat="server" ForeColor="White">READ MORE</asp:HyperLink>
                    </p>
	             </div>
	        </div>

		    </div>
	
	
	    
	    <div class="leftPanel">
	        <div class="frontMini">
	        <div class="frontMiniTitle">
	        
            <asp:Image ID="Image1" runat="server" CssClass="clubIcon" ImageUrl="images/iconEPL.jpg" AlternateText="" />
	        ENGLISH PREMIER LEAGUE
	        </div>
	        
	        <div  class="frontMiniDetails">
	            
	            <asp:Image ID="C1Image" runat="server" CssClass="frontImage" ImageUrl="uploads/image1.jpg" />
	            <asp:Label ID="C1LabelTitle" runat="server" CssClass="frontTitle" Text="20 Beautiful Long Exposure Photographs"></asp:Label>
	            <br />
                <asp:Label ID="C1LabelDetails" runat="server" CssClass="frontDetails" Text="Vestibulum leo quam, accumsan nec porttitor a, euismod ac tortor. Sed ipsum lorem, 
	            sagittis non egestas id, suscipit...."></asp:Label>
                <asp:HyperLink ID="C1Recommend" runat="server" ForeColor="Black">read more</asp:HyperLink>
			    <hr />
	            <ul class="design">
	                <li>
	                <asp:HyperLink ID="C1HyperLink1" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	                <li>
	                <asp:HyperLink ID="C1HyperLink2" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	                <li>
	                <asp:HyperLink ID="C1HyperLink3" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	                <li>
	                <asp:HyperLink ID="C1HyperLink4" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	                <li>
	                <asp:HyperLink ID="C1HyperLink5" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	            </ul>
	            
	        </div>
	        </div>
	
	        <div class="frontMini2" >
	        <div class="frontMiniTitle">
	            
            <asp:Image ID="Image2" runat="server" CssClass="clubIcon" ImageUrl="images/iconSPL.jpg" AlternateText="" /> 
            SPANISH LA LIGA
	        </div>
	        
	        <div  class="frontMiniDetails">
	        
	            <asp:Image ID="C2Image" runat="server" CssClass="frontImage" ImageUrl="uploads/image1.jpg" />
	            <asp:Label ID="C2LabelTitle" runat="server" CssClass="frontTitle" Text="20 Beautiful Long Exposure Photographs"></asp:Label>
	            <br />
                <asp:Label ID="C2LabelDetails" runat="server" CssClass="frontDetails" Text="Vestibulum leo quam, accumsan nec porttitor a, euismod ac tortor. Sed ipsum lorem, 
	            sagittis non egestas id, suscipit...."></asp:Label>
                <asp:HyperLink ID="C2Recommend" runat="server" ForeColor="Black">read more</asp:HyperLink>
			    <hr />
	            <ul class="design">
	                <li>
	                <asp:HyperLink ID="C2HyperLink1" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	                <li>
	                <asp:HyperLink ID="C2HyperLink2" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	                <li>
	                <asp:HyperLink ID="C2HyperLink3" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	                <li>
	                <asp:HyperLink ID="C2HyperLink4" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	                <li>
	                <asp:HyperLink ID="C2HyperLink5" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	            </ul>
	            
	        </div>
	        </div>
	    </div> 
	
	    <div class="leftPanel">
	        <div class="frontMini">
	        <div class="frontMiniTitle">
	        
            <asp:Image ID="Image3" runat="server" CssClass="clubIcon" ImageUrl="images/iconIPL.jpg" AlternateText="" />
	        ITALIAN SERIE A
	        </div>
	        
	        <div  class="frontMiniDetails">
	            
	            <asp:Image ID="C3Image" runat="server" CssClass="frontImage" ImageUrl="uploads/image1.jpg" />
	            <asp:Label ID="C3LabelTitle" runat="server" CssClass="frontTitle" Text="20 Beautiful Long Exposure Photographs"></asp:Label>
	            <br />
                <asp:Label ID="C3LabelDetails" runat="server" CssClass="frontDetails" Text="Vestibulum leo quam, accumsan nec porttitor a, euismod ac tortor. Sed ipsum lorem, 
	            sagittis non egestas id, suscipit...."></asp:Label>
                <asp:HyperLink ID="C3Recommend" runat="server" ForeColor="Black">read more</asp:HyperLink>
			    <hr />
	            <ul class="design">
	                <li>
	                <asp:HyperLink ID="C3HyperLink1" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	                <li>
	                <asp:HyperLink ID="C3HyperLink2" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	                <li>
	                <asp:HyperLink ID="C3HyperLink3" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	                <li>
	                <asp:HyperLink ID="C3HyperLink4" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	                <li>
	                <asp:HyperLink ID="C3HyperLink5" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	            </ul>
	            
	        </div>
	        </div>
	
	        <div class="frontMini2">
	        <div class="frontMiniTitle">
	        
            <asp:Image ID="Image4" runat="server" CssClass="clubIcon" ImageUrl="images/iconGPL.jpg" AlternateText="" />
	            GERMAN BUNDESLIGA
	        </div>
	        
	        <div  class="frontMiniDetails">
	            
	            <asp:Image ID="C4Image" runat="server" CssClass="frontImage" ImageUrl="uploads/image1.jpg" />
	            <asp:Label ID="C4LabelTitle" runat="server" CssClass="frontTitle" Text="20 Beautiful Long Exposure Photographs"></asp:Label>
	            <br />
                <asp:Label ID="C4LabelDetails" runat="server" CssClass="frontDetails" Text="Vestibulum leo quam, accumsan nec porttitor a, euismod ac tortor. Sed ipsum lorem, 
	            sagittis non egestas id, suscipit...."></asp:Label>
                <asp:HyperLink ID="C4Recommend" runat="server" ForeColor="Black">read more</asp:HyperLink>
			    <hr />
	            <ul class="design">
	                <li>
	                <asp:HyperLink ID="C4HyperLink1" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	                <li>
	                <asp:HyperLink ID="C4HyperLink2" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	                <li>
	                <asp:HyperLink ID="C4HyperLink3" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	                <li>
	                <asp:HyperLink ID="C4HyperLink4" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	                <li>
	                <asp:HyperLink ID="C4HyperLink5" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	            </ul>
	            
	        </div>        
	    </div>  
	
	    </div> 
	    
	    <div class="leftPanel">
	 
	        <div class="frontMini" >
	        <div class="frontMiniTitle">
	            
            <asp:Image ID="Image5" runat="server" CssClass="clubIcon" ImageUrl="images/iconNPL.jpg" AlternateText="" />
	            NIGERIAN PREMIER LEAGUE
	        </div>
	        
	        <div  class="frontMiniDetails">
	        
	            <asp:Image ID="C5Image" runat="server" CssClass="frontImage" ImageUrl="uploads/image1.jpg" />
	            <asp:Label ID="C5LabelTitle" runat="server" CssClass="frontTitle" Text="20 Beautiful Long Exposure Photographs"></asp:Label>
	            <br />
                <asp:Label ID="C5LabelDetails" runat="server" CssClass="frontDetails" Text="Vestibulum leo quam, accumsan nec porttitor a, euismod ac tortor. Sed ipsum lorem, 
	            sagittis non egestas id, suscipit...."></asp:Label>
                <asp:HyperLink ID="C5Recommend" runat="server" ForeColor="Black">read more</asp:HyperLink>
			    <hr />
	            <ul class="design">
	                <li>
	                <asp:HyperLink ID="C5HyperLink1" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	                <li>
	                <asp:HyperLink ID="C5HyperLink2" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	                <li>
	                <asp:HyperLink ID="C5HyperLink3" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	                <li>
	                <asp:HyperLink ID="C5HyperLink4" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	                <li>
	                <asp:HyperLink ID="C5HyperLink5" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	            </ul>
	            
	        </div>
	        </div>  
	        
	        <div class="frontMini2">
	        <div class="frontMiniTitle">
	            
            <asp:Image ID="Image6" runat="server" CssClass="clubIcon" ImageUrl="images/iconSAPL.jpg" AlternateText="" />
	            SOUTH AFRICAN LEAGUE
	        </div>
	        
	        <div  class="frontMiniDetails">
	            
	            <asp:Image ID="C6Image" runat="server" CssClass="frontImage" ImageUrl="uploads/image1.jpg" />
	            <asp:Label ID="C6LabelTitle" runat="server" CssClass="frontTitle" Text="20 Beautiful Long Exposure Photographs"></asp:Label>
	            <br />
                <asp:Label ID="C6LabelDetails" runat="server" CssClass="frontDetails" Text="Vestibulum leo quam, accumsan nec porttitor a, euismod ac tortor. Sed ipsum lorem, 
	            sagittis non egestas id, suscipit...."></asp:Label>
                <asp:HyperLink ID="C6Recommend" runat="server" ForeColor="Black">read more</asp:HyperLink>
			    <hr />
	            <ul class="design">
	                <li>
	                <asp:HyperLink ID="C6HyperLink1" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	                <li>
	                <asp:HyperLink ID="C6HyperLink2" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	                <li>
	                <asp:HyperLink ID="C6HyperLink3" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	                <li>
	                <asp:HyperLink ID="C6HyperLink4" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	                <li>
	                <asp:HyperLink ID="C6HyperLink5" runat="server"> Velit enim a, amet cursus ut, et! Quis! Turpis.</asp:HyperLink>
	                </li>
	            </ul>
	            
	        </div>        
	    </div>  
	    </div> 
	    
	    
	    
	        
	        
        </div>
	
	
	
	
	
	

<script type="text/javascript">
	$(document).ready(function(){
		$("#featured > ul").tabs({fx:{opacity: "toggle"}}).tabs("rotate", 5000, true);
	});
</script>
    
</asp:Content>

