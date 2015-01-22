function maxOfThree() {
    var ans = 0;
    var a = parseInt($('#maxNum1').val());
    var b = parseInt($('#maxNum2').val());
    var c = parseInt($('#maxNum3').val());

        if (a >= b && a >= c){
            ans = a;
        } else if (b >= a && b >= c) {
            ans = b;
        } else if (c >= a && c >= b)
        {
            ans = c;
        }

      $('#answer').replaceWith('<div id=\'answer\'><p>' + ans + '</p></div>');
    }

   