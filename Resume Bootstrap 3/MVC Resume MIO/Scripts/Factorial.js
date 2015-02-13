function factorial(nFactor) {
    var nFactor = $('#xFactorial').val();
    var factorialAnswer = nFactor * (nFactor - 1);
    for (var i = nFactor - 2; i > 1; i--) {
        factorialAnswer *= i;
    }
    $('#factorialAnswer').html('<p>' + "Factorial of " + nFactor + " is " + factorialAnswer + '</p>');
}

