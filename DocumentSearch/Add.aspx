<%@ Page Title="Add" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="DocumentSearch.Add" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h4>Add a movie to the database by typing its name and clicking on the Add button.</h4>
        <p>
            </hgroup>
            <asp:TextBox ID="TextBoxAdd" runat="server"></asp:TextBox>
            <asp:Button ID="BtnAdd" runat="server" BorderStyle="Solid" CssClass="zero" Font-Size="Medium" Height="32px" OnClick="BtnAdd_Click" Text="Add" Width="76px" />
        </p>
    <hgroup>
    </hgroup>

</asp:Content>