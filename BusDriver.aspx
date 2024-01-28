<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BusDriver.aspx.cs" Inherits="nbaJSON.Bus_Driver" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<br /><br /><br />
<div class="table">
    <div class="col-lg-3" >
        <div class="row">
        <asp:Button ID="loadB" runat="server" Text ="First time load" OnClick="loadB_Click" height="75px" Width="250px" Font-Size="XX-Large" 
            style="color:cornflowerblue; background-color:black; border-color:black; text-decoration:none;  text-align:center;border-radius: 15px; border: 3px solid grey;"/>
        </div>
    <br />
    
        <div class="row">
        <asp:Button ID="gameB" runat="server" Text ="Games" OnClick="gameB_Click" height="75px" Width="250px" Font-Size="XX-Large" 
        style="color:black; background-color:cornflowerblue; border-color:black; text-decoration:none;  text-align:center;border-radius: 15px; border: 3px solid black;"/>
        </div>
    <br />
        <div class="row">
            <asp:Button ID="boxB" runat="server" Text ="Box Score" OnClick="boxB_Click" height="75px" Width="250px" Font-Size="XX-Large" 
            style="color:black; background-color:mediumseagreen; border-color:black; text-decoration:none;  text-align:center;border-radius: 15px; border: 3px solid black;"/>
        </div>
    <br />
        <div class="row">
            <asp:Button ID="pbpB" runat="server" Text ="Play by Play" OnClick="pbpB_Click" height="75px" Width="250px" Font-Size="XX-Large"
            style="color:black; background-color:indianred; border-color:black; text-decoration:none;  text-align:center;border-radius: 15px; border: 3px solid black;"/>
        </div>
    </div>
     <div class="col-lg-3">
         <div class="row" style="text-align:left; padding:15px 0px" ></div>
         <div class="row" style="text-align:left; padding:15px 0px" ></div>
         <div class="row" style="text-align:left; padding:15px 0px" ></div>
         <div class="row" style="text-align:left; padding:15px 0px" >
             <asp:Label ID="gamesCreatedLbl" runat="server"  ForeColor="black" Font-Size="Large" Visible="true" style="text-align:center; vertical-align:middle; padding:10px 0px 0px 0px" ></asp:Label>
             <br />
             <asp:Label ID="gamesUpdatedLbl" runat="server"  ForeColor="black" Font-Size="Large" Visible="true" style="text-align:center; vertical-align:middle; padding:10px 0px 0px 0px" ></asp:Label>
         </div>   
         <div class="row" style="text-align:left; padding:15px 0px" ></div>
         <div class="row" style="text-align:left; padding:15px 0px" ></div>
         <div class="row" style="text-align:left; padding:15px 0px" >
             <asp:Label ID="boxCreatedLbl" runat="server"  ForeColor="black" Font-Size="Large" Visible="true" style="text-align:center; vertical-align:middle; padding:10px 0px 0px 0px" ></asp:Label>
             <br />
             <asp:Label ID="boxUpdatedLbl" runat="server"  ForeColor="black" Font-Size="Large" Visible="true" style="text-align:center; vertical-align:middle; padding:10px 0px 0px 0px" ></asp:Label>
         </div>   
    </div>  
</div>
<br /><br />

    
   
<br /><br />

    

  
<br /><br /><br />
        <asp:GridView ID="GamesGrid" runat="server" ></asp:GridView> 
        <asp:GridView ID="grid1" runat="server" ></asp:GridView> 
        <asp:GridView ID="grid2" runat="server" ></asp:GridView> 
        <asp:GridView ID="gridD" runat="server" ></asp:GridView> 
  

</asp:Content>
