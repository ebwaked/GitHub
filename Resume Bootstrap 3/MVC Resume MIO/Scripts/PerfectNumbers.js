var userInput = $('#perfect').val();
var perfect = function (x) {
    var sumOfPositiveIntergers = 0;
    var perfNumbPossible = parseInt(userInput.value);
    for (var i = 1; i <= perfNumbPossible - 1; i++) {
        var modEquation = perfNumbPossible % i;
        if (modEquation === 0) {
            sumOfPositiveIntergers += i;
        }
    }

    // if the sum of positive intergers is equal to perfectnumbpossible(user input) than it is
    if (sumOfPositiveIntergers === perfNumbPossible) {
        $('#perfect').html(perfNumbPossible + " " + "is a perfect number");
    } else {
        document.getElementById('answer').innerHTML = perfNumbPossible + " " + "is not a perfect number";
        $('#perfect').html(perfNumbPossible + " " + "is not a perfect number");
    }
}
