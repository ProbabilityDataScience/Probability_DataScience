<?php
/**
 * Created by PhpStorm.
 * User: Kimyoumin
 * Date: 2016-11-10
 * Time: 오전 10:52
 */

$jsonData = json_decode("{\"Test\" : \"1\", \"Test2\" : \"2\"}");

print $jsonData->Test;