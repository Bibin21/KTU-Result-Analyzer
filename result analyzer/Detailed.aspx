<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Detailed.aspx.cs" Inherits="result_analyzer.WebForm7" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="styles/Detailed.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/chart.js@4.3.0/dist/chart.umd.min.js"></script>
  
    <div class="detcontainer">
    <div id=container class="bargraph">
        <h2 style="color: black;font-size:20px;">  
        Arrears Per Semester 
    </h2> 
<asp:Literal ID="ltChartData" runat="server" />

        </div>
    <div id=linegraph class="bargraph">
        <h2 style="color: black;font-size:20px;">  
        Arrears Summary
    </h2> 
       <h3 id="percent" style="position:absolute;top:50%;left:40%;font-size:16px" runat="server">Pass Percent: </h3>
          <asp:Literal  ID="piedata" runat="server" />
        </div>
        
        </div>
    <div id=graph class="linegraph">
        <h2 style="color: black;font-size:20px;">  
        Arrears Per Student
    </h2> 
          <asp:Literal  ID="linedata" runat="server" />
        </div>

</asp:Content>
