function maxOfThree() {
    var ans = 0;
    var a = $('#maxNum1').val();
    var b = $('#maxNum2').val();
    var c = $('#maxNum3').val();

        if (a >= b && a >= c){
            ans = a;
        } else if (b >= a && b >= c) {
            ans = b;
        } else {
            ans = c;
        }
        ans = Number(ans);
      $('#answer').replaceWith('<div id=\'answer\'><p>' + ans + '</p></div>');
    }

   