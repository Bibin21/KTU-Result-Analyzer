<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="result_analyzer.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
        <link href="styles/Login.css" rel="stylesheet" />
         <link rel="stylesheet" href="styles/Materialize.css" />
      <script src="https://kit.fontawesome.com/d22e378a1e.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/materialize/1.0.0-beta/js/materialize.min.js"></script>
</head>
     <script>
        function checkpass() {
            document.getElementById("wrongpass").style.display = "none";
         }
         function checkemail() {
             document.getElementById("wrongemail").style.display = "none";
         }
      
     
     </script>
<body>
    <div class="box">
 <div class="Login">
       <h2>Login</h2>
       <form class="form" runat="server" method="post">
         <input class="email" name="email" id="email" runat="server" type="email" placeholder="Email" required onkeyup="checkemail()" />
           <asp:TextBox class="pass" ID="pass" runat="server" name="pass" textmode="Password" placeholder="Enter Password" onkeyup="checkpass()" required></asp:TextBox>
             <a  style="color:#8B989C;text-align:left; text-decoration:none; font-weight:lighter;" href="Forgot">Forgot Password?</a>
           <asp:button class="loginbtn" runat="server" type="submit" text="Login" OnClick="loginbtn_click" />
               <asp:LinkButton class="oauth-container btn darken-4 white black-text" runat="server" onClick="googlelogin"  CauseValidation="false" style="text-transform:none;color:black;text-decoration:none;display:flex;justify-content:center;align-items:center;width:fit-content;font-weight:lighter;margin-left:auto;margin-right:auto;border-radius:5px;margin-bottom:10px;">
            <img width="20" style="margin-right:10px;margin-left:5px;" alt="Google sign-in" 
                src="https://upload.wikimedia.org/wikipedia/commons/thumb/5/53/Google_%22G%22_Logo.svg/512px-Google_%22G%22_Logo.svg.png" />
        Login with Google
    </asp:LinkButton>
           </form>
      <label runat="server" id="wrongpass" style="display:none;color:red;">Incorrect Password</label>
       <label runat="server" id="wrongemail" style="display:none;color:red;" >Entered Email does not have an Account</label>
       <a style="margin-top:-30px; margin-bottom:10px; color:#8B989C;" href="Signup">Do not Have Account?</a>
   </div>
        <div class="info">
            <img class="infoimg" src="images/img.svg" alt="" />
            <h3 >Welcome To Result Analyzer. <br> Please Login To Continue.. </h3>
        </div>
     
        </div>
        
     <div class="dim" runat="server" id="dim" ></div> 
       <div class="check-mark" id="check" runat="server">
           <i id="cross" onclick="check()" onmouseover="cursor()" style="font-size:20px !important;color:black !important; margin-left:80%;margin-top:-40px !important;" class="fa-solid fa-xmark" ></i>
             <i class="fa-solid fa-circle-check" ></i>
       <h3 style="font-size:20px !important;margin-top:0px !important;" class="success-login">Signup Success</h3>
    
</div>
</body>
</html>
<script>
    function check() {
        document.getElementById('check').style = 'display: none !important';
        document.getElementById('dim').style.display = "none";
    }
    function cursor() {
        document.getElementById('cross').style.cursor = "pointer";
    }
</script>
