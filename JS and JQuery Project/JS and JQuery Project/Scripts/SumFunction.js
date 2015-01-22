function sumNumbers(sumList) {
    var sumList = $('#sumNumbers').val().split('+');
    var sumTotal = 0;
        for (i = 0; i < sumList.length; i++) {
            sumTotal += parseInt(sumList[i]);     
        }
        $('#sumAnswer').replaceWith('<div id=\'sumAnswer\'><p>' + sumTotal + '</p></div>');
}











