
        function GetData(data) {
            var u = data[0];
            document.getElementById("UserName").value = u.UserName;
            document.getElementById("Email").value = u.Email;
            document.getElementById("Password").value = u.Password;
        }
        function Check() {
            var reg = /^([a-zA-Z0-9_-])+@@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])+/;
            var UserName = document.getElementById("UserName");
            var email = document.getElementById("Email");
            var pwd1 = document.getElementById("Password");
            var pwd2 = document.getElementById("RePassword");
            var flag = 0;
            
            if (UserName.value == "" )
            {
                document.getElementById("UserNameL").innerText = "*用户名不能为空";
            }
            if (email.value == "")
            {
                document.getElementById("EmailL").innerText = "*Email出错！"
            }
            if (pwd1.value == "" || pwd2.value == "")
            {
                document.getElementById("PasswordL").innerText = "*密码不能为空";
            }
           
           
            if (pwd1.value != pwd2.value) {
                document.getElementById("PasswordL").innerText = "*两次密码输入不正确！"
            }
            else {
                flag = 1;
            }

            if (email.value!=null&&reg.test(email.value))
            {
                flag = flag + 1;
            }
            else
            {
                falg = 0;
                document.getElementById("EmailL").innerText = "*Email出错！";
            }
            if (flag==2) return true;
            else
            {
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

        function _focus(ipt) {
            var nextnode = ipt.nextElementSibling;
            nextnode.innerText = "";
           // document.getElementById("EmailL").innerText = "";
            //document.getElementById("PasswordL").innerText = "";
            //document.getElementById("UserNameL").innerText = "";
        }
    