<%@ Page Title="主页" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="YDNoteOpenAPI4N.Test.Web._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        欢迎使用 ASP.NET!
    </h2>
    <asp:Label runat="server" ID="lbl"></asp:Label>
    <br />
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        Text="getUserInfo" />
&nbsp;<asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
        Text="getAllNoteBook" />
    <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
        Text="getNotesInBook" />
    <asp:Button ID="Button4" runat="server" onclick="Button4_Click" 
        Text="createNoteBook" />
    <asp:Button ID="Button5" runat="server" onclick="Button5_Click" 
        Text="deleteNoteBook" />
           <br />
           <asp:Button ID="Button6" runat="server"
        Text="createNote" onclick="Button6_Click" />
           <asp:Button ID="Button7" runat="server"
        Text="getNote" onclick="Button7_Click" /> 
           <asp:Button ID="Button8" runat="server"
        Text="updateNote" onclick="Button8_Click" /> 
           <asp:Button ID="Button9" runat="server"
        Text="moveNote" onclick="Button9_Click" /> 
           <asp:Button ID="Button10" runat="server"
        Text="moveNote2" onclick="Button10_Click" /> 
           <asp:Button ID="Button11" runat="server"
        Text="deleteNote" onclick="Button11_Click" /> 
           <asp:Button ID="Button12" runat="server"
        Text="deleteNote2" onclick="Button12_Click" /> 
    <br />
    <asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:Button ID="Button13" runat="server" OnClick="Button13_Click" Text="上传" />
    <br />
    </asp:Content>
