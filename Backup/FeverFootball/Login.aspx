<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="pageTitle">
- login page
</div>

<br /><br />
<center>
<b>DO YOU HAVE AN ACCOUNT WITH US?</b>

    <table style=" background-color:#dddddd; padding:10px;">
        <tr>
            <td>
                USERNAME:
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="*" ControlToValidate="txtUsername"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                PASSWORD:
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="*" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnLogin" runat="server" Text="SIGN IN" OnClick="LoginUser" />
                <br />
                <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="#">Have you forgotten your password? </asp:HyperLink>
            </td>
        </tr>
    </table>


            <br /><br />
              <b> <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="#">JOIN THE FEVER FOOTBALL MOVEMENT.
              <br />
              <span style="color:Black">CREATE AN ACCOUNT.</span> 
              </asp:HyperLink></b> 
                <br />
                

</center>
</asp:Content>

