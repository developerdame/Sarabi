<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Sarabi.Web.Controllers" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h2>I've seen a star</h2>
    
    <% using(Html.BeginForm<CelebrityController>(c => c.Seen(string.Empty), FormMethod.Get)) { %>
    
        <%= Html.TextBox("name") %>
        <%= Html.SubmitButton() %>
        
    <% } %>
    
</asp:Content>
