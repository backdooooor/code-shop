$(document).ready(
  function () {

      $(".box").hide();
      checkAuth();
  }
);
  var auth = false;
  function doAbout() {
      //$("title").html("code-shop  Интернет-Магазин будущего.... О нас");
      var tmp = '<div class="close"><a href="#" onclick="doClose();return false;">X</a></div><div class="inner"><div class="section"><h2>О нас</h2><div class="inner"><br><b><br><br><br><br><br><p>Идейный вдохновитель <br /> Артем <a href="http://twitter.com/backd000r">@backdoor</a> Татаринов<br />Любимая цитата "Если тебе плюют в спину,значит ты впереди"<br />Программист Ястребов Вадим<br />Программист Расулов Джавид</p></div></div></div>';
      $(".box").html(tmp);
      $(".box").show();

  }
  function doCatalog() {
      //$("title").html("code-shop  Интернет-Магазин будущего.... Каталог Товаров");
      $.post(
  '/./JS/getCatalog/',
  {
      text: "0"
  },
  function (data) {
      var tmp = '<div class="close"><a href="#" onclick="doClose();return false;">X</a></div><div class="inner"><div class="section"><h2>Каталог</h2><div class="inner"><br><br><br><br><br>'+data+'</div></div></div>';
      $(".box").html(tmp);
      $(".box").show();
  }
);
      
  }
  function doAuth() {
      //$("title").html("code-shop  Интернет-Магазин будущего.... Авторизация");
     
      var tmp = '<div class="close"><a href="#" onclick="doClose();return false;">X</a></div><div class="inner"><div class="section"><h2>Авторизация</h2><a href="#" onclick="getRegister();"><h2>Регистрация</h2></a><div class="inner"><form name="form_adds" action="" onsubmit="setAuth();return false;">Логин<br><input type=text name="login" id="login"><br>Пароль<br><input type=password name="password" id="password"><br><b id="error"></b><br><input type=submit  id="submit" value="Авторизоваться"></form></div></div></div>';
      $(".box").html(tmp);
      $(".box").show();
  }
  function doClose() {
     
      $(".box").hide();
  }
  function setAuth() {
      var login = $("#login").attr("value");
      var password = $("#password").attr("value");
     
      if (login == "") {
          $("#error").html("Логин не может быть пустым");
          return "";

      }
      if (login == "") {
          $("#error").html("пароль не может быть пустым");
          return "";
      }
    
      if (!/^\w+[a-zA-Z0-9_.-]+$/.test(login)) {
          $("#error").html("В поле Логин введено не корректное значение");
          return "";
          }

          $.post(
  '/./JS/doAuth/?login=' + login + "&password=" + password,
  {
      text: "0"
  },
  function (data) {
      if (data == "1") {
          auth = true;
          $(".box").hide();
          var tmp = '<a href="#" onclick="getBuy();" ><h2 class="auth">Купленные товары</h2></a>';
          $(".contacts-item").html(tmp);
          $(".add-item").show();
          $(".edit-profile").show();

      } else {
          $("#error").html("Вы ввели некорректные данные");
      }
  }
);
      
}
function checkAuth() {
    $.post(
  '/./JS/CheckAuth/',
  {
      text: "0"
  },
  function (data) {
      if (data == "1") {
          auth = true;
          var tmp = '<h2 class="auth"><a href="#" onclick="getBuy();" >Купленные товары</a></h2>';
          $(".contacts-item").html(tmp);
          $(".add-item").show();
          $(".edit-profile").show();
      } else {
          var tmp = '<h2><a href="#"  onclick="doAuth();return false;">Авторизация</a></h2>';
          $(".contacts-item").html(tmp);
          $(".add-item").hide();

      }
  }
);
}
function getCategory(id) {
    //$("title").html("code-shop  Интернет-Магазин будущего.... Категории");
    $.post(
  '/./JS/getItemfromCat/?id=' + id,
  {
      text: "0"
  },
  function (data) {
      var tmp = '<div class="close"><a href="#" onclick="doClose();return false;">X</a></div><div class="inner"><div class="section"><h2>Каталог</h2><div class="inner"><br><br><br><br><br>' + data + '</div></div></div>';
      $(".box").html(tmp);
      $(".box").show();

  }
);
}
function getItem(id) {
    if (!parseInt(id)) return "";

    $.post(
  '/./JS/getItem/?id=' + id,
  {
      text: "0"
  },
  function (data) {
      //$("title").html("code-shop  Интернет-Магазин будущего.... Товар");
      if (auth) {
          data = data + "<br><form action='' onsubmit='addComment("+id+"); return false;'><textarea name='text' id='text' rows=5 cols=50></textarea><br><input type=submit value='Добавить комментарий'></form>"
      }
      $("#item").html(data);
      $("#item").show();

  }
);

}
function addComment(id) {
    if (!parseInt(id)) return "";
    var text = document.getElementsByName("text").item(0);
    var str = text.value;
    if (text.value == "") return "";


    $.post(
  '/./JS/addComment/?id='+id+'&text='+ str,
  {
     
  },
  function (data) {
      
      getItem(id);

  }
);
}

function setCategory() {
    $.post(
  '/./JS/doSelectCategory/',
  {
      text: "0"
  },
  function (data) {
      $("#id_category").html(data);

  }
);

}
function doPlus(id) {
    $.post(
 '/./JS/doPlusUser/?id='+ id,
  {
      text: "0"
  },
  function (data) {
     if(data=="1"){
     $("#item").html("Ваш голос принят");
     } else {
     $("#item").html("Ваш голос не будет обработан,из за того что вы не авторизованы!");
     }

  }
);
}
function doMinus(id) {
  $.post(
 '/./JS/doMinusUser/?id='+ id,
  {
      text: "0"
  },
  function (data) {
     if(data=="1"){
     $("#item").html("Ваш голос принят");
     } else {
     $("#item").html("Ваш голос не будет обработан,из за того что вы не авторизованы!");
     }

  }
);
}


function doBuy(id) {
    if (!auth) {
        $("#item").html("Вы не авторизованы!");
        return "";
    }
    $.post(
 '/./JS/doBuy/?id_tovar=' + id,
  {
      text: "0"
  },
  function (data) {
      if (data == "1") {
          $("#item").html("Вы успешно купили товар!");
      } else {
          $("#item").html("У вас нехватает средств на счету!");
      }

  }
);
}
function getBuy() {
    if (!auth) {
    $("#item").html("Вы не авторизованы!");
        return "";
    }
    $.post(
 '/./JS/getBuy/',
  {
      text: "0"
  },
  function (data) {
     // $("title").html("code-shop  Интернет-Магазин будущего.... Купленные товары");
      var tmp = '<div class="close"><a href="#" onclick="doClose();return false;">X</a></div><div class="inner"><div class="section"><h2>Каталог купленных товаров</h2><div class="inner"><br><br><br><br><br>' + data + '</div></div></div>';
      $(".box").html(tmp);
      $(".box").show();


  }
);

}
function getRegister() {
    //$("title").html("code-shop  Интернет-Магазин будущего.... Регистрация");
    var tmp = '<div class="close"><a href="#" onclick="doClose();return false;">X</a></div><div class="inner"><div class="section"><h2>Авторизация</h2><a href="#" onclick="getRegister();"><h2>Регистрация</h2></a><div class="inner"><form name="form_adds" action="" onsubmit="doRegister();return false;">Логин<br><input type=text name="login" id="login"><br>Пароль<br><input type=password name="password" id="password"><br><b id="error"></b><br><input type=submit  id="submit" value="Регистрация"></form></div></div></div>';
    $(".box").html(tmp);
    $(".box").show();
    
}

function doRegister() {
    
    var login = $("#login").attr("value");
    var password = $("#password").attr("value");

    if (login == "") {
        $("#error").html("Логин не может быть пустым");
        return "";

    }
    if (login == "") {
        $("#error").html("пароль не может быть пустым");
        return "";
    }

    if (!/^\w+[a-zA-Z0-9_.-]+$/.test(login)) {
        $("#error").html("В поле Логин введено не корректное значение");
        return "";
    }

    $.post(
  '/./JS/doRegister/?login=' + login + "&password=" + password,
  {
      text: "0"
  },
  function (data) {
      if (data == "1") {
          $(".box").hide();
          $("#item").html("Вы успешно зарегистрировались<br>Теперь вы можете авторизоваться");
        

      } else {
          $("#error").html("Вы ввели некорректные данные");
      }
  }
);
}

function exit() {

    auth = false;
    $.post(
  '/./JS/doExit/',
  {
      text: "0"
  },
  function (data) {
      checkAuth();
      $("#item").html("Вы успешно вышли!");
    
  }
);
}
function getProfile(id) {
    //$("title").html("code-shop  Интернет-Магазин будущего.... Профиль пользователя");
    $.post(
  '/./JS/getProfile/?id_user=' + id,
  {
      text: "0"
  },
  function (data) {
      var tmp = '<div class="close"><a href="#" onclick="doClose();return false;">X</a></div><div class="inner"><div class="section"><h2>@username@</h2><div class="inner"><br><br><br><br><br>' + data + '</div></div></div>';
      $(".box").html(tmp);
      $(".box").show();

  }
);

}
function doContactSave() {
    
    var icq = $("#icq").attr("value");
    var skype = $("#skype").attr("value");
    var twitter = $("#twitter").attr("value");
    
    if (!/^[a-zA-Z0-9]+$/.test(skype)) {
         $("#error").html("Логин skype содержит запрещенные символы");
        return false;
    }
    if (!/^[a-zA-Z0-9]+$/.test(twitter)) {
        
        $("#error").html("Логин  twitter содержит запрещенные символы");
        return false;
    }
    if (!/^[0-9]+$/.test(icq)) {
         $("#error").html("Номер icq  должен содержать только цифры");
        return false;
    }

    $.post(
  '/./JS/doEditProfile/?icq=' + icq + '&twitter=' + twitter + '&skype='+ skype,
  {
     
  },
 function (data) {
    
     if (data == "1") {
         $(".box").hide();
     } else {
         $("#error").html("Произошла непредвиденная ошибка");
     }

 }
);
}
function doShowContact() {
    
    $.post(
  '/./JS/edit_profile/',
  {

},
 function (data) {

     var tmp = '<div class="close"><a href="#" onclick="doClose();return false;">X</a></div><div class="inner"><div class="section"><h2>Редактирование</h2><div class="inner"><br><br><br><br><br>' + data + '</div></div></div>';
     $(".box").html(tmp);
     $(".box").show();

 }
);
    
    
}