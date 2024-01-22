<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PlayByPlay.aspx.cs" Inherits="nbaJSON.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <asp:GridView ID="GamesGrid" runat="server" ></asp:GridView> 
        <asp:GridView ID="grid1" runat="server" ></asp:GridView> 
        <asp:GridView ID="grid2" runat="server" ></asp:GridView> 
        <asp:GridView ID="gridD" runat="server" ></asp:GridView> 
    </main>

</asp:Content>
