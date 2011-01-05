<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Add.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%: ViewData["Message"] %>
    <h2>Добавление товара</h2>
    <form action="" method="post" enctype="multipart/form-data">  
     <label for="file">Сама Программа:</label> <br /> 
     <input type="file" name="file" id="file" />
     <p>Название программы<br /> <input type="text" name="title" id="title" /></p>
     <p>Описание программы</p>
     <p><textarea id="description" name="description" ></textarea></p>
     <p>Скриншот <input type="file" name="screenshot" id="screenshot" /></p>
     <p>Категория <select name="id_category" id="id_category"><%=ViewData["category"]%></select>
     <p>Цена <input type="text" name="value"  id="value" /></p>
     <input type="submit" value="Добавить товар" />  
     </form> 


</asp:Content>

