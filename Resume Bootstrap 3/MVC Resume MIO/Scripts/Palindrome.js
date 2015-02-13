function checkPalindrome(word) {
    var word = $('#word').val();
    var wordLength = $('#word').val().length;
    var isPal = true;
    for (var i = 0; i < wordLength / 2; i++) {
        if (word.charAt(i) !== word.charAt(wordLength - 1 - i)) {
            isPal = false;
            break;
        } 
    }
    if (isPal) {
        $('#palindromeAnswer').html(word + " is a palindrome");
    } else {
        $('#palindromeAnswer').html(word + " is not a palindrome");
    }
}

