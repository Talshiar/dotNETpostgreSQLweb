<%@ Page Title="Search" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="DocumentSearch.Search" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h4>Search the movie database using given parameters.</h4>
        <p>
            </hgroup>
            <asp:TextBox ID="TextBoxSearch" runat="server"></asp:TextBox>
            <asp:Button ID="BtnSearch" runat="server" BorderStyle="Solid" Font-Size="Small" OnClick="BtnSearch_Click" Text="Search" Width="64px" Height="31px" />
        </p>
        <table>
            <tr>
            <td><asp:RadioButtonList ID="RadioListAndOr" runat="server" Autosize="false" Font-Size="Small" Height="56px">
                <asp:ListItem>AND</asp:ListItem>
                <asp:ListItem>OR</asp:ListItem>
            </asp:RadioButtonList></td>
            <td><asp:RadioButtonList ID="RadioListType" runat="server" Autosize="false" Font-Size="Small" Height="16px" Width="352px">
                <asp:ListItem Value="0">Exact string matching</asp:ListItem>
                <asp:ListItem Value="1">Use dictionaries</asp:ListItem>
                <asp:ListItem Value="2">Fuzzy string matching</asp:ListItem>
            </asp:RadioButtonList></td>
            </tr>
        </table>
        
    <asp:Label ID="Label1" runat="server" Text="SQL Statement"></asp:Label>
    <br />
    <asp:TextBox ID="TextBoxSQL" runat="server" Height="197px" Width="517px" Columns="50" ReadOnly="True" Rows="20" TextMode="MultiLine"></asp:TextBox>
        
    <br />
    Results:<br />
    <asp:Literal ID="LiteralResult" runat="server"></asp:Literal>
    <br />
        
    <hgroup>
        
    </hgroup>

</asp:Content>