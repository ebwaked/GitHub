   
function factorial(nFactor) {
    var nFactor = $('#xFactorial').val();
    var factorialAnswer = nFactor * (nFactor - 1);
    for (var i = nFactor - 2; i > 1; i--) {
        factorialAnswer *= i;
    }
    $('#factorialAnswer').replaceWith('<div id=\'factorialAnswer\'><p>' + "Factorial of "  + nFactor +  " is "  + factorialAnswer + '</p></div>');
}

