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
                    <select id="audience" name="D1" runat="server">
                        <option value="MBA2024">MBA Class of 2024</option>
                        <option value="MBA2025">MBA Class of 2025</option>
                        <option value="MBAAdmitR12025">MBA 2025 Admit Round 1</option>
                        <option value="MBAAdmitR22025">MBA 2025 Admit Round 2</option>
                        <option value="MBAAdmitR32025">MBA 2025 Admit Round 3</option>
                        <option value="MSx2024">MSx 2024</option>
                        <option value="MSxAdmit2024">MSx 2024 Admit</option>
                        <option value="phd">PhD</option>
                        <option value="phdAdmit">PhD Admit</option>
                    </select></td>
            </tr>
            <tr>
                <td class="auto-style4">Matching Template</td>
                <td class="auto-style2">
                    <asp:Label ID="lblTemplatePath" runat="server"></asp:Label>
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
