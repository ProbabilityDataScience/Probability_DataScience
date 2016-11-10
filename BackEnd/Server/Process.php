<?php
/**
 * Created by PhpStorm.
 * User: Kimyoumin
 * Date: 2016-11-10
 * Time: 오전 9:24
 */

//$request = explode('/', trim($_SERVER['PATH_INFO'],'/'));
//$table = preg_replace('/[^a-z0-9_]+/i','', array_shift($request));
//$key = array_shift($request);

error_reporting(E_ALL&~E_NOTICE&~E_WARNING);

$method = $_SERVER['REQUEST_METHOD'];

$jsonData = json_decode(file_get_contents('php://input'), true);

$result = "protocol : ";

$protocol = $jsonData["protocol"];

$result .= $protocol;

foreach ($jsonData["datas"] as $data)
{
    $result .= " Data : ";
    $result .= $data;
}

echo $result;

