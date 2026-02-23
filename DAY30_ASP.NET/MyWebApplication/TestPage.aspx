<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="MyWebApplication.TestPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Hello Asad</h1>
            <p>Welcome to Asp.Net</p>
            <p>Name: Asad</p>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <input id="Submit1" type="submit" value="submit" /><br />

            &nbsp; <hr />
            <div style ="background-color:aqua">
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="Button" />
            </div>
        </div>
    </form>
    
</body>
</html>
