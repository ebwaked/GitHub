
maxOfThree(55, 465456, 456);
    function maxOfThree(a, b, c){
        if (a >= b && a >= c){
            document.getElementById("maxNumb").innerHTML = a;
        }else if (b >= a && b >= c){
            document.getElementById("maxNumb").innerHTML = b;
        }else{
            document.getElementById("maxNumb").innerHTML = c;
        }
    }

   