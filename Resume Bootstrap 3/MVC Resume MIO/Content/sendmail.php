<?php
$headers  = 'MIME-Version: 1.0' . "\r\n";
$headers .= 'Content-type: text/html; charset=iso-8859-1' . "\r\n";
$headers .= 'From:'.$_REQUEST['name'].' '.$_REQUEST['email'];
$message = $_REQUEST['message'] ;
// Replace mail@website.com with your email
mail( "ebwaked87@gmail.com", "Request Information",$message, $headers );
print "<strong>Form submitted successfully.</strong>";
?>