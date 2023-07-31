<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NetworkConnect.aspx.cs" Inherits="NetworkDrive.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            height: 26px;
            width: 1099px;
        }
        .auto-style3 {
            width: 222px;
        }
        .auto-style4 {
            height: 26px;
            width: 222px;
        }
        .auto-style5 {
            width: 1099px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table class="auto-style1">
            <tr>
                <td class="auto-style3">Audience</td>
                <td class="auto-style5">
                    <asp:TextBox ID="txtAudience" runat="server" Width="208px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">Year</td>
                <td class="auto-style2">
                    <asp:TextBox ID="txtYear" runat="server" Width="210px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style5">
                    <asp:Button ID="findTemplate" runat="server" Text="FindTemplate" OnClick="findTemplate_Click" />
                </td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style5">
                    <asp:Label ID="message" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
