# Register
@{
    ViewBag.Title = "Register";
}
    <script>
        function validate() {
            var re = "/^(\w-*\.*)+@@(\w-?)+(\.\w{2,})+$/";
            var email = document.getElementById("Email").value;
            var pwd1 = document.getElementById("Password").value;
            var pwd2 = document.getElementById("RePassword").value;
            var ErrorMessage = "";
            if (pwd1 != pwd2)
            {
                ErrorMessage+="两次密码输入不正确！"
            }
            else {
                ErrorMessage += "";
            }
            if (ErrorMessage=="") return true;
            else
            {
                alert(ErrorMessage);
                return false;
            }
        }


        function mouseover(x) {
            x.style.background = "aqua";
            // x.css("background-color", "ghostwhite");
        }
        function mouseout(x) {
            x.style.background = "beige";
            // x.css("background-color","red");
        }
    </script>
    <h2>Register</h2>
    <div>
        <form >
            <fieldset>
                <legend align="center" >注册</legend>
                姓名：<input type="text" name="UserName" value="" /><lable style="color:red"></lable><br />
                密码：<input type="password" name="Password" id="Password" value="" /><lable style="color:red">@ViewData["passError"]</lable><br />
                密码：<input type="password" name="RePassword" id="RePassword" value="" /><br />
                邮箱：<input type="text" name="Email" id="Email" value="" /><lable style="color:red">@ViewData["EmailError"] </lable><br />
                <input type="submit" value="Create" onclick="return validate()"  onmouseout="mouseout(this)" onmouseover="mouseover(this)" class="btn btn-default"  />
            </fieldset>
        </form>
    </div>