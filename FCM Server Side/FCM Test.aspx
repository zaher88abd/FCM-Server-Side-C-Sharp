﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FCM Test.aspx.cs" Inherits="FCM_Server_Side.FcmTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
            <asp:TextBox ID="tbDeviceId" runat="server"></asp:TextBox><br />
            <asp:TextBox ID="tbData" runat="server"></asp:TextBox><br />
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" /><br />
            <asp:Label ID="Result" Text="text" runat="server" />
        </div>
    </form>
</body>
</html>
