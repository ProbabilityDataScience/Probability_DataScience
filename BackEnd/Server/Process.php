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

require ("Login.php");

class Protocol {
    static public $Login  = 0;
    static public $blue = 1;
    static public $gary = 3;
    static public $aqua = 4;
}

error_reporting(E_ALL&~E_NOTICE&~E_WARNING);

$method = $_SERVER['REQUEST_METHOD'];

$jsonData = json_decode(file_get_contents('php://input'), true);

$protocol = $jsonData["protocol"];

if($protocol === Protocol::$Login)
{
    LoginProcess($jsonData["datas"]);
}

$result = "protocol : ";

$result .= $protocol;

foreach ($jsonData["datas"] as $data)
{
    $result .= " Data : ";
    $result .= $data;
}

//echo $result;

