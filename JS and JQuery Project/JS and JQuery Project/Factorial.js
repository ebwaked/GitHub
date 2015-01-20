   
    var n = 5;
var nmt = n * (n - 1);
for (i = n - 2; i > 1; i--) {
    nmt = nmt * i;
}
document.write("Factorial of " + n + " is " + nmt);


