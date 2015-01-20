    <label label id="per">Is this number perfect?</label>
    <button onclick="perfect()" href="javascript:;">Enter</button>
    <input type="number" id="perfect" />
    <p> id="answer"></p>
        var userInput = document.getElementById('perfect');
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
        document.getElementById('answer').innerHTML = perfNumbPossible + " " + "is a perfect number";
        // if not user input is not perfect number
    } else {
        document.getElementById('answer').innerHTML = perfNumbPossible + " " + "is not a perfect number";
    }
}
