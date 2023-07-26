<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="result_analyzer.WebForm5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent"  runat="server">
    <link href="Content/Site.css" rel="stylesheet" />
         <link rel="stylesheet" href="styles/Materialize.css" />
    <script src="Scripts/Fontaswesome.js"></script>
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@20..48,100..700,0..1,-50..200" />
    <script src="Scripts/Materialize.js"></script>
 <div class="containermain" id="cmain" runat="server">
     <div class="topdiv" runat="server" id="topdiv">
     <div class="s1" id="s1" runat="server">
        <label class="s1label">Semester 1</label>
          <asp:FileUpload ID="FileUpload1" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"  runat="server" />
         <asp:Button class="s1upload" id="s1upload" runat="server" type="submit" Text="Upload" onclick="upload_sheet"/>
          <asp:Button class="s1update" ID="s1update" runat="server" type="submit" Text="Update" onclick="update_sheet"/>
          
     </div>
     <div class="s2" id="s2" runat="server">
             <label class="s2label">Semester 2</label>
       <asp:FileUpload ID="FileUpload2" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"  runat="server" />
         <asp:Button class="s2upload" ID="s2upload" runat="server" type="submit" Text="Upload" onclick="upload_sheet"/>
          <asp:Button class="s2update" id="s2update" runat="server" type="submit" Text="Update" onclick="update_sheet"/>
      
             </div>
 
     <div class="s3" id="s3" runat="server">
             <label class="s3label">Semester 3</label>
         <asp:FileUpload ID="FileUpload3" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"  runat="server" />
         <asp:Button class="s3upload" id="s3upload" runat="server" type="submit" Text="Upload" onclick="upload_sheet"/>
          <asp:Button class="s3update" id="s3update" runat="server" type="submit" Text="Update" onclick="update_sheet"/> 
       
     </div>
      
     <div class="s4" id="s4" runat="server">
             <label class="s4label">Semester 4</label>
         <asp:FileUpload ID="FileUpload4" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"  runat="server" />
         <asp:Button class="s4upload"  id="s4upload" runat="server" type="submit" Text="Upload" onclick="upload_sheet"/>
          <asp:Button class="s4update"  id="s4update" runat="server" type="submit" Text="Update" onclick="update_sheet"/>
        
     </div>

     <div class="s5" id="s5" runat="server">
             <label class="s1label">Semester 5</label>
         <asp:FileUpload ID="FileUpload5" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"  runat="server" />
         <asp:Button class="s5upload"   id="s5upload" runat="server" type="submit" Text="Upload" onclick="upload_sheet"/>
          <asp:Button class="s5update"  id="s5update" runat="server" type="submit" Text="Update" onclick="update_sheet"/>
      
             </div>
         

     <div class="s6" id="s6" runat="server">
          <label class="s1label">Semester 6</label>
           <asp:FileUpload ID="FileUpload6" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"  runat="server" />
         <asp:Button class="s6upload"   id="s6upload" runat="server" type="submit" Text="Upload" onclick="upload_sheet"/>
          <asp:Button class="s6update"  id="s6update" runat="server" type="submit" Text="Update" onclick="update_sheet"/>
         
         </div>
     <div class="s7" id="s7" runat="server">
      
             <label class="s7label">Semester 7</label>
       
         <asp:FileUpload ID="FileUpload7" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"  runat="server" />
         <asp:Button class="s7upload"  id="s7upload" runat="server" type="submit" Text="Upload" onclick="upload_sheet"/>
          <asp:Button class="s7update" id="s7update" runat="server" type="submit" Text="Update" onclick="update_sheet"/>
         
             </div>

     <div class="s8" id="s8" runat="server">
             <label class="s1label">Semester 8</label>
         <asp:FileUpload ID="FileUpload8" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"  runat="server" />
         <asp:Button class="s8upload" id="s8upload" runat="server" type="submit" Text="Upload" onclick="upload_sheet"/>
          <asp:Button class="s8update" id="s8update" runat="server" type="submit" Text="Update" onclick="update_sheet"/>
         
             </div>
       
      <div id="popup" class="popup" runat="server" > 
  <svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px"
	 viewBox="0 0 100 100" style="enable-background:new 0 0 100 100;" xml:space="preserve">
<style type="text/css">
	.st0{fill:none;stroke:#9254CD;;stroke-width:2;stroke-miterlimit:10;}
	.st1{clip-path:url(#SVGID_2_);}
	.st2{fill:#87CDAE;stroke:#9254CD;;stroke-width:2;stroke-linecap:round;stroke-linejoin:round;stroke-miterlimit:10;}
	.st3{fill:none;stroke:#87CDAE;stroke-width:2;stroke-linecap:square;stroke-miterlimit:10;}
	.st4{fill:#87CDAE;}
	.st5{fill:#FFFFFF;}
</style>
<path class="st0" d="M48.8,83.4L20.2,66.9c-0.4-0.2-0.7-0.7-0.7-1.2v-33c0-0.5,0.3-0.9,0.7-1.2l28.6-16.5c0.4-0.2,1-0.2,1.4,0
	l28.6,16.5c0.4,0.2,0.7,0.7,0.7,1.2v33c0,0.5-0.3,0.9-0.7,1.2L50.2,83.4C49.8,83.7,49.2,83.7,48.8,83.4z"/>
<g>
	<defs>
		<path id="SVGID_1_" d="M48.8,82.3L21.2,66.3c-0.4-0.2-0.7-0.7-0.7-1.2V33.2c0-0.5,0.3-0.9,0.7-1.2l27.6-15.9c0.4-0.2,1-0.2,1.4,0
			l27.6,15.9c0.4,0.2,0.7,0.7,0.7,1.2v31.9c0,0.5-0.3,0.9-0.7,1.2L50.2,82.3C49.8,82.5,49.2,82.5,48.8,82.3z"/>
	</defs>
	<clipPath id="SVGID_2_">
		<use xlink:href="#SVGID_1_"  style="overflow:visible;"/>
	</clipPath>
	<g class="st1">
   <g class="cboard"> 
		<path class="st0" style="stroke:#9254CD !important;" d="M66.3,33.3h9.6c2.1,0,3.8,1.7,3.8,3.8v50.8H34.8V37.1c0-2.1,1.7-3.8,3.8-3.8h8.9"/>
		<path class="st2" d="M65.2,37.2H48.6c-1,0-1.7-0.9-1.5-1.9l1.4-7.5c0.1-0.7,0.8-1.3,1.5-1.3h13.7c0.8,0,1.4,0.5,1.5,1.3l1.4,7.5
			C66.9,36.3,66.1,37.2,65.2,37.2z"/>
		<g>
			<polyline class="st3" points="41.1,46.6 42.6,48.1 46.6,44.1 			"/>
		</g>
		<g>
			<g>
				<line class="st3" x1="41.3" y1="59.3" x2="45.3" y2="55.4"/>
				<line class="st3" x1="45.3" y1="59.3" x2="41.3" y2="55.4"/>
			</g>
		</g>
		<g>
			<polyline class="st3" points="41.1,69.1 42.6,70.5 46.6,66.6 			"/>
		</g>
		<g>
			<polyline class="st3" points="41.1,80.3 42.6,81.8 46.6,77.8 			"/>
		</g>
		<g>
			<rect x="57.7" y="43.6" class="st4" width="17" height="2.2"/>
			<rect x="50" y="43.6" class="st4" width="5.9" height="2.2"/>
			<rect x="50" y="47.1" class="st4" width="9.5" height="2.2"/>
			<rect x="61.3" y="47.1" class="st4" width="10.7" height="2.2"/>
		</g>
		<g>
			<rect x="57.7" y="57.9" class="st4" width="8.5" height="2.2"/>
			<rect x="67.6" y="57.9" class="st4" width="7.7" height="2.2"/>
			<rect x="50" y="57.9" class="st4" width="5.9" height="2.2"/>
			<rect x="50" y="54.3" class="st4" width="9.5" height="2.2"/>
			<rect x="61.3" y="54.3" class="st4" width="10.7" height="2.2"/>
		</g>
		<g>
			<rect x="54.7" y="69.4" class="st4" width="13.5" height="2.2"/>
			<rect x="69.6" y="69.4" class="st4" width="5.7" height="2.2"/>
			<rect x="50" y="69.4" class="st4" width="2.9" height="2.2"/>
			<rect x="50" y="65.8" class="st4" width="15.5" height="2.2"/>
			<rect x="67.3" y="65.8" class="st4" width="4.7" height="2.2"/>
		</g>
		<g>
			<rect x="54.7" y="80.4" class="st4" width="13.5" height="2.2"/>
			<rect x="69.6" y="80.4" class="st4" width="5.7" height="2.2"/>
			<rect x="50" y="80.4" class="st4" width="2.9" height="2.2"/>
			<rect x="50" y="76.8" class="st4" width="15.5" height="2.2"/>
			<rect x="67.3" y="76.8" class="st4" width="4.7" height="2.2"/>
		</g>
		</g>
	</g>
</g>
<g>
	<path class="st5" d="M22.3,76.4l0.1-37.7c0-2.4,2.2-4.4,4.9-4.4c2.7,0,4.9,2,4.9,4.4l-0.1,37.8l-4.7,8.2L22.3,76.4z"/>
	<path class="st2" d="M27.3,79.9l-2.5-5.7l0.1-34.3c0-1.3,1.1-2.4,2.4-2.4h0c1.3,0,2.4,1.1,2.4,2.4l-0.1,34.4L27.3,79.9z"/>
	<path class="st2" d="M25,73.3c0,0,2-1,4.4,0"/>
	<line class="st2" x1="24.9" y1="44.4" x2="29.7" y2="44.4"/>
</g> 
</svg>  
<h3 id="reportlab">Generating Report ..</h3>
</div>   

            <asp:Button class="reportbtn" id="reportgen" OnClientClick="loading()"  runat="server" type="submit" Text="View Report" onclick="generate_report"/>

     </div>
         <div class="gridcontainer"  runat="server" id="gridcontainer" >
             <div class="gridcon">
     
     <asp:GridView ID="gridview" CssClass="GridView" onrowdatabound="grid_rowdatabound"  runat="server" style="text-decoration:none;font-size:16px; "/>
               
        </div>
             
     </div>
       <label runat="server" id="percentpass" style="display:none;">Pass Percentage:</label>
   <div class="gridbtn" runat="server">
      <asp:Button class="reportbtn"  id="closereport" runat="server"  style="background-color:red;display:none;" type="submit" Text="Close Report" onclick="close_report"/>
         <asp:Button class="export"  id="exportbtn" runat="server" type="submit" Text="Export as Excel" style="display:none;background-color:#1ED861;border-radius:10px;" onclick="export_grid"/>
    </div>
       <div class="check-mark" id="check" runat="server">
           <i id="cross" onclick="check()" onmouseover="cursor()" style="font-size:20px !important;color:black !important; margin-left:80%;" class="fa-solid fa-xmark" ></i>
             <i class="fa-solid fa-circle-check" ></i>
       <h3 class="success-login">Login Success</h3>
    
</div>
     </div>
  


      <div class="dim" runat="server" id="dim" ></div> 
    <script type="text/javascript" language="javascript">
        function gridscroll() {

            window.scrollTo({
                top: 593,
                behavior: 'smooth'
            });
        }
        function loading() {
            document.getElementById('MainContent_dim').style.display = "block";
            document.getElementById('MainContent_popup').style.display = "flex";
            document.getElementById('reportlab').style.display = "block";
        }

            $(function () {
                $("[id*=gridview] td").hover(function () {
                    $("td", $(this).closest("tr")).addClass("hover_row");
                }, function () {
                    $("td", $(this).closest("tr")).removeClass("hover_row");
                });
            });
        function check() {
            document.getElementById('MainContent_check').style = 'display: none !important; ';
            document.getElementById('MainContent_dim').style = "display:none !important";
        }
        function cursor() {
            document.getElementById('cross').style.cursor = "pointer";
        }
    </script>
  
</asp:Content>

