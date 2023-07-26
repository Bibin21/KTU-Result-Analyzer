<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="result_analyzer.WebForm6" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <title>Error 404</title>
</head>
<body>
    <form runat="server">
        <div class="alert alert-danger" role="alert" style="text-align:center;font-size:20px;">
            Error Invalid Access!
            <asp:button runat="server" onclick="redirectlogin" style="margin-left:10px;color:#A94442;" Text="Click Here To Redirect To Login"  />
        </div>
    </form>
</body>
</html>
