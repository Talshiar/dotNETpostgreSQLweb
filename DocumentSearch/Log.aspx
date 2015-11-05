<%@ Page Title="Log" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Log.aspx.cs" Inherits="DocumentSearch.Log" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h4>History of made queries sortable by date and time.</h4>
    </hgroup>
    <table>
        <tr>
            <td class="auto-style2">
                <asp:Label ID="Label1" runat="server" Text="Granulation:"></asp:Label>
                <asp:RadioButtonList ID="RadioListGranulation" runat="server" Autosize="false" Font-Size="Small" Height="56px">
                    <asp:ListItem>Hours</asp:ListItem>
                    <asp:ListItem>Days</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="auto-style1">Write the date in YYYY-MM-DD format.<br />

                Date From:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date to:<br />
                <asp:TextBox ID="TextBoxDateFrom" runat="server"></asp:TextBox>
                <asp:TextBox ID="TextBoxDateTo" runat="server"></asp:TextBox>
                <br />
            </td>
        </tr>
    </table>
    <asp:Button ID="BtnLog" runat="server" Text="Generate Report" OnClick="BtnLog_Click" />


    <br />
    <asp:GridView ID="GridViewLog" runat="server">
    </asp:GridView>


</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .auto-style1 {
            height: 128px;
            width: 344px;
        }

        .auto-style2 {
            height: 128px;
            width: 94px;
        }
    </style>
</asp:Content>

