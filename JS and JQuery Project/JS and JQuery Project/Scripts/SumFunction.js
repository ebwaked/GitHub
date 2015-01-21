$('#sumNumbers');
var sumNumbers = [];
var sumList = sumNumbers.split(",");
totalSum = 0;

function sumNumbers(sumList) {
    for (var i = 0; i < sumNumbers.length; i++) {

        totalSum += sumList[i];
        $('#sumAnswer').replaceWith('<div id=\'sumAnswer\'><p>' + totalSum + '</p></div>');
    }
}












//Sum Function
//var numbers = [6, 7, 3, 4];
//var totalAmount = 0;

//for (var i = 0; i < numbers.length; i++) {

//    totalAmount += numbers[i];
//}
//document.write(totalAmount);


////Multiplier Function
//    var numbers = [1, 2, 3, 4];
//var totalAmount = 1;

//for (var i = 0; i < numbers.length; i++) {

//    totalAmount *= numbers[i];
//}
//document.write(totalAmount);
//document.write('<br>');
