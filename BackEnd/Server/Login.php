<?php
/**
 * Created by PhpStorm.
 * User: Kimyoumin
 * Date: 2016-11-20
 * Time: 오후 4:06
 */

function LoginProcess($datas)
{
    //Connect DB
    $connect = mysqli_connect("localhost","root","ef794200") or die ("데이터베이스 연결 실패 : " . mysqli_connect_error());

    //Select DB
    mysqli_select_db($connect, "phpmyadmin") or die("DB선택에러");

    $firstLogin = "SELECT * FROM somaTable WHERE faceBookToken LIKE ";
    $firstLogin .= "'%" . $datas[0] . "%';";

    $result = mysqli_query($connect, $firstLogin) or die("SQL error");

    $jsonResult = "";

    while($row = mysqli_fetch_row($result))
    {
        $jsonResult .= json_encode($row);
    }

    // 계정이 존재하지 않음
    if (empty($jsonResult))
    {
        $createQuery = "INSERT INTO somaTable VALUES(";

        $createQuery .= "\"";
        $createQuery .= $datas[0];
        $createQuery .= "\"";
        $createQuery .= ", ";

        $createQuery .= "10000";
        $createQuery .= ", ";

        $createQuery .= "50";
        $createQuery .= ", ";

        $createQuery .= "1";

        $createQuery .= ");";

        echo $createQuery;

        $result = mysqli_query($connect, $createQuery) or die ("Error");
    }
    else
    {
        echo "asd";
    }
}

