Max of three function
    <button onclick="maxOfThree(55, 465456, 456)">The max is...</button>
    <p> id="maxNumb"></p>
 
    function maxOfThree(a, b, c){
        if (a >= b && a >= c){
            document.getElementById("maxNumb").innerHTML = a;
        }else if (b >= a && b >= c){
            document.getElementById("maxNumb").innerHTML = b;
        }else{
            document.getElementById("maxNumb").innerHTML = c;
        }
    }
 