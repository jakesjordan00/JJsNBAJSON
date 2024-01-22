<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BusDriver.aspx.cs" Inherits="nbaJSON.Bus_Driver" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<br /><br /><br />

    <asp:Button ID="loadB" runat="server" Text ="First time load" OnClick="loadB_Click" height="75px" Width="250px" Font-Size="XX-Large" 
        style="color:cornflowerblue; background-color:black; border-color:black; text-decoration:none;  text-align:center;border-radius: 15px; border: 3px solid grey;"/>
    
<br /><br />

    <asp:Button ID="gameB" runat="server" Text ="Games" OnClick="gameB_Click" height="75px" Width="150px" Font-Size="XX-Large" 
        style="color:black; background-color:cornflowerblue; border-color:black; text-decoration:none;  text-align:center;border-radius: 15px; border: 3px solid black;"/>
    
<br /><br />

    <asp:Button ID="teamB" runat="server" Text ="Teams" OnClick="teamB_Click" height="75px" Width="150px" Font-Size="XX-Large" 
        style="color:black; background-color:mediumseagreen; border-color:black; text-decoration:none;  text-align:center;border-radius: 15px; border: 3px solid black;"/>
   
<br /><br />

    <asp:Button ID="pbpB" runat="server" Text ="Play by Play" OnClick="pbpB_Click" height="75px" Width="250px" Font-Size="XX-Large"
        style="color:black; background-color:indianred; border-color:black; text-decoration:none;  text-align:center;border-radius: 15px; border: 3px solid black;"/>
   
<br /><br /><br />
    <main>
        <asp:GridView ID="GamesGrid" runat="server" ></asp:GridView> 
        <asp:GridView ID="grid1" runat="server" ></asp:GridView> 
        <asp:GridView ID="grid2" runat="server" ></asp:GridView> 
        <asp:GridView ID="gridD" runat="server" ></asp:GridView> 
    </main>

</asp:Content>
