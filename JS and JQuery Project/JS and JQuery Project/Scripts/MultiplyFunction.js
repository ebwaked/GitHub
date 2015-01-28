function multiplyNumbers(multiplyList) {
    var multiplyList = $('#multiplyNumbers').val().split("*");
    var multiplyTotal = 1;
    for (i = 0; i < multiplyList.length; i++) {
        multiplyTotal *= parseInt(multiplyList[i]);
    }
    $('#multiplyAnswer').replaceWith('<div id=\'multiplyAnswer\'><p>' + multiplyTotal + '</p></div>');
}

