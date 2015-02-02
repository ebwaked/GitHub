function fizzBuzz() {

    var result = "";

    for (var i = 1; i <= 100; i++) {
        if (i % 5 !== 0 && i % 3 !== 0) {
            result += i + "<br/>";
        }
        else if (i % 3 === 0 && i % 5 !== 0) {
            result += "Fizz" + "<br/>";
        }
        else if (i % 5 === 0 && i % 3 !== 0) {
            result += "Buzz" + "<br/>";
        }
        else {
            result += "FizzBuzz" + "<br/>";
        }
    }
    $('#fizzBuzzAnswer').html(result);
}
