<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="result_analyzer.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Signup</title>
    <link href="styles/Signup.css" rel="stylesheet" />
    <link rel="stylesheet" href="styles/Materialize.css" />
<script src="https://cdnjs.cloudflare.com/ajax/libs/materialize/1.0.0-beta/js/materialize.min.js"></script>
</head>
    <script>
        function repassc() {
            document.getElementById("samepass").style.display = "none";
        }
     
        </script>
<body>
    <div class="box">
   <div class="signup">
       <h2>Signup</h2>
       <form class="form" runat="server" method="post">
             <asp:TextBox class="email" name="email" ID="emails" runat="server" textmode="Email" placeholder="Email" required ></asp:TextBox>
           <asp:TextBox class="pass" ID="pass" runat="server" name="pass" textmode="Password" onkeyup="repassc()" placeholder="Enter Password" required></asp:TextBox>
              <asp:TextBox class="repass" ID="repass" runat="server" name="repass" textmode="Password" onkeyup="repassc()" placeholder="Retype Password" required></asp:TextBox>
    
           <asp:button class="signupbtn" runat="server" type="submit" text="Signup" OnClick="Singupbtn_click" />
            <asp:LinkButton class="oauth-container btn darken-4 white black-text"  runat="server" onClick="googlelogin"  CauseValidation="false" style="text-transform:none;color:black;text-decoration:none;display:flex;justify-content:center;align-items:center;font-weight:lighter; width:fit-content;margin-left:auto;margin-right:auto;border-radius:5px;margin-bottom:10px;">
            <img width="20" style="margin-right:10px;margin-left:5px;" alt="Google sign-in" 
                src="https://upload.wikimedia.org/wikipedia/commons/thumb/5/53/Google_%22G%22_Logo.svg/512px-Google_%22G%22_Logo.svg.png" />
        Continue with Google
    </asp:LinkButton>
           </form>
       <label runat="server" id="samepass" style="display:none;color:red;">Password and Retype Password Must be same</label>
       <label runat="server" id="alreadyexist" style="display:none;color:red;" href="Login">Account Already Exist For this Mail Try Login</label>
      
       <a runat="server" style="color:#8B989C;margin-top:-20px; margin-bottom:-20px;" href="~/Login">Already Have Account?</a>
   </div>
        <div class="info">
            <img src="images/signup.svg" class="infoimg" alt="" />
               <h3 >Welcome To Result Analyzer. <br> Please Signup If You Do Not Have An Account.</h3>
        </div>
</body>
</html>
<script>
 
  
</script>