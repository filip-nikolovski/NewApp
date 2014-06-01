
             function openWindow() {
                 window.open('Popoup.aspx', null, 'height=200,width=400,status=no,toolbar=no,menubar=no,location=no, scrollbars=no, titlebar=no,directories=no,location=no,screenX=400,screenY=400');
                 return false;
             }



  
  

        function display(id) {

            var traget = document.getElementById(id);

            traget.style.display = "block";
            //window.location = "#addConference ";
            $('html,body').animate({
                scrollTop: $("#addWork").offset().top
            }, 1000);

            return false;
        }

        function hide(id) {
            var target = document.getElementById(id);
            //window.location = "#gv";
            target.style.display = "none";
            $('html,body').animate({
                scrollTop: $("#newWork").offset().top
            }, 2000);
            return false;
        }


        $(document).ready(function () {
            $(".scroll").click(function (event) {
                $('html,body').animate({ scrollTop: $(this.hash).offset().top }, 500);
            });
        });


        function display1(id) {
            var traget = document.getElementById(id);

            traget.style.display = "block";

        }

        function hide1(id) {
            var target = document.getElementById(id);
            target.style.display = "none";
        }

        function hide11(id) {
            var target = document.getElementById(id);
            target.style.display = "none";

            $('html,body').animate({
                scrollTop: $("#addWork").offset().top
            }, 1000);
        }

