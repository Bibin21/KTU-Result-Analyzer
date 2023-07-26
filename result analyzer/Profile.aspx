<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="result_analyzer.WebForm8" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="Scripts/Fontaswesome.js"></script>
    <link href="styles/Profile.css" rel="stylesheet" />
    <div class="containermain" runat="server">
    <div class="profile" runat="server">
    <h2>Profile</h2>
        <div class="profpic" runat="server">
        <i class="fa-solid fa-user"></i>
            </div>
        <label for="name">Name:</label>
        <input class="name" id="name" runat="server" type="text">
          <label for="clg">College:</label>
        <input class="clg" id="clg" runat="server" type="text">
          <label for="email">Email:</label>
         <input type="email" id="email" runat="server"  readonly>
        <asp:button class="updatebtn" runat="server" type="submit" text="Update Profile" OnClick="profile_save" />
        </div>

    </div>
</asp:Content>
