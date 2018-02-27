<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="JJ.Demos.JavaScript._Default" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript" src="Scripts/JJ.Framework.Url.js"></script>
    <script type="text/javascript">
        var url = "http://www.google.com/";
        alert(url);
        url = JJ.Framework.Url.setParameter(url, "param1", "bla1");
        alert(url);
        url = JJ.Framework.Url.setParameter(url, "param2", "bla2");
        alert(url);
        url = JJ.Framework.Url.setParameter(url, "param2", "bla2_changed");
        alert(url);
        url = JJ.Framework.Url.setParameter(url, "param1", "bla1_changed");
        alert(url);

    </script>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    JJ.Demos.JavaScript
</asp:Content>
