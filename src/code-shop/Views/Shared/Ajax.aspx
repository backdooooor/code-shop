<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Beta.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%: ViewData["Message"] %>
    

</asp:Content>
