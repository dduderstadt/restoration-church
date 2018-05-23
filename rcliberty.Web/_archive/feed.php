<?php
// Set your return content type
header('Content-type: text/xml');

$feedUrl = $_REQUEST['feedUrl'];

// Get that website's content
$handle = fopen($feedUrl, "r");

// If there is something, read and return
if ($handle) {
    while (!feof($handle)) {
        $buffer = fgets($handle, 4096);
        echo $buffer;
    }
    fclose($handle);
}
?>