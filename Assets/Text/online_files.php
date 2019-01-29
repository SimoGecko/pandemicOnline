//WRITE
<?php
header("Access-Control-Allow-Origin: *");

$fname = isset($_POST['name']) ? $_POST['name'] : '';
$fdata = isset($_POST['data']) ? $_POST['data'] : '';

$fileName = dirname(__DIR__) . "/pandemic_online/" . $fname;

if (file_exists($fileName)) {
  file_put_contents($fileName, $fdata, FILE_APPEND);
} else {
  file_put_contents($fileName, $fdata);
}

?>



//READ
<?php
header("Access-Control-Allow-Origin: *");

$fname = isset($_POST['name']) ? $_POST['name'] : '';
$fileName = dirname(__DIR__) . "/pandemic_online/" . $fname;

if (file_exists($fileName)) {
  $myfile = fopen($fileName, "r") or die("Unable to open file!");
  echo fread($myfile,filesize($fileName));
  fclose($myfile);
}

?>


//DELETE
<?php
header("Access-Control-Allow-Origin: *");

$fname = isset($_POST['name']) ? $_POST['name'] : '';
$fileName = dirname(__DIR__) . "/pandemic_online/" . $fname;

if (file_exists($fileName)) {
  if (!unlink($fileName)) {
    echo ("$fileName cannot be deleted due to an error");
  } else {
    echo ("$fileName has been deleted");
  }
}

?>