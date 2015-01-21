

function sumNumbers(sumList) {
    var sumList = $('#sumNumbers').val().split(',');
    var sumTotal = 0;
        for (i = 0; i < sumList.length; i++) {
            sumTotal += parseInt(sumList[i]);     
        }
        $('#sumAnswer').replaceWith('<div id=\'sumAnswer\'><p>' + sumTotal + '</p></div>');
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
