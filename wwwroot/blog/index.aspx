<%@ Page Title="" Language="C#" MasterPageFile="BlogMasterPage.master" AutoEventWireup="true" Inherits="Home.blog_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BlogContent" Runat="Server">
<%= RenderArticles() %>
</asp:Content>
