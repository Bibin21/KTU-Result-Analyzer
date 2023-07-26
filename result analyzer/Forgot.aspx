<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forgot.aspx.cs" Inherits="result_analyzer.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="styles/Forgot.css" rel="stylesheet" />
</head>
    <script>
       
            function reemail() {
                document.getElementById("noaccount").style.display = "none";
        }
        function reotp() {
            document.getElementById("wrongotp").style.display = "none";
            document.getElementById("labelotp").style.display = "none";
        }
        function repass() {
            document.getElementById("wrongrepass").style.display = "none";
        }

    </script>
    
<body>
 <div class="container" >
       <h2 id="forgotlabel" runat="server">Forgot Password</h2>
       <form class="form" id="form" runat="server" defaultbutton="sendotp">
           <div class="forgot" id="forgot" runat="server">
           <input  id="smail" runat="server" class="smail" type="email" onkeyup="reemail()" placeholder="Email" required/>
           <input  id="otpenter" runat="server" class="otpenter" type="number" onkeyup="reotp()" style="display:none;" placeholder="Enter OTP"/>
           <asp:button id="sendotp" runat="server" class="sendotp" type="button" onclick="sendotp_click" text="sendotp" />
                  <label  id="noaccount" runat="server" style="display:none; color:red;">Email Does Not Have an Account</label>
           <label  id="labelotp" runat="server" style="display:none; color:forestgreen;">We have sent OTP to your Mail</label>
            <asp:button id="verifybtn" runat="server" class="verifybtn" style="display:none;" type="button" OnClick="verifybtn_click" text="verify" />
                          <label  id="wrongotp" runat="server" style="display:none; color:red;">OTP Incorrect</label>
               </div>
    <div class="changepass" id="changepass" style="display:none;" runat="server">
       <h2>Reset Password</h2>
           <input  class="newpass" id="newpass" runat="server" type="password" placeholder="Enter New Password" />
           <input class="retypenewpass" id="retypenewpass" runat="server" onkeyup="repass()" type="password" placeholder="Retype New Password"/>
           <asp:button  id="reset" class="resetbtn"  runat="server" type="submit"  onkeyup="repass()" onclick="change_pass" Text="Change Password" />
          <label  id="wrongrepass" runat="server" style="display:none; color:red;">Password and Retype Password Must be Same!</label>
</div>
        </form>
   </div>
     
</body>
</html>