<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainTestTask.aspx.cs" Inherits="MainTestTask" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        	Choose text:<br />
            <asp:FileUpload ID="FileUploadControl" runat="server" />
            <asp:Button runat="server" id="ButtonBrowse" text="Browse" onclick="ButtonBrowse_Click" /> 
			<br />
            <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
            <br />
			<br />
			Enter word:<br />
			<asp:TextBox ID="TextBoxWord" runat="server"></asp:TextBox>
			<asp:Button ID="ButtonSearchWord" runat="server" Text="Search" OnClick="ButtonSearchWord_Click" />
			<br />
			<asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
			<br />
			<br />
            <br />
			<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField DataField="label" HeaderText="label" SortExpression="label" />
                    <asp:BoundField DataField="sentence" HeaderText="sentence" SortExpression="sentence" />
                    <asp:BoundField DataField="count" HeaderText="count" SortExpression="count" />
                </Columns>
			</asp:GridView>
        </div>
        <p>
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        </p>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TestTaskConnectionString %>" SelectCommand="SELECT * FROM [TestTaskTable]"></asp:SqlDataSource>
    </form>
</body>
</html>
