<?php
/**
 * Created by PhpStorm.
 * User: Kimyoumin
 * Date: 2016-11-20
 * Time: 오후 4:06
 */

// Protocol : Login
// 클라->서버 : 토큰 값 / 처음 접속 여부 (True, false)
// 서버->클라 : 현재 돈
function LoginProcess($datas)
{
    //Connect DB
    $connect = mysqli_connect("localhost","root","ef794200") or die ("데이터베이스 연결 실패 : " . mysqli_connect_error());

    //Select DB
    mysqli_select_db($connect, "phpmyadmin") or die("DB선택에러");

    $countQuery = "SELECT count(*) from somaTable;";
    $count = mysqli_query($connect, $countQuery) or die("SQL error");

    $dataCount = mysqli_fetch_array($count);

    if ($datas[1] == 0)
    {
        // DB 테이블 새로 만들기 요청
        $createQuery = "INSERT INTO somaTable VALUES(";
        $createQuery .= "\"";       $createQuery .= $datas[0];  $createQuery .= "\"";   $createQuery .= ", ";
        $createQuery .= "10000";    $createQuery .= ", ";
        $createQuery .= "10";       $createQuery .= ", ";
        $createQuery .= $dataCount[0];
        $createQuery .= ");";

        $result = mysqli_query($connect, $createQuery) or die ("Error");

        echo "10000";
    }
    // 기존 테이블에서 데이터 꺼내옴
    else if ($datas[1] == 1)
    {
        $firstLogin = "SELECT * FROM somaTable WHERE faceBookToken LIKE ";
        $firstLogin .= "'%" . $datas[0] . "%';";

        $result = mysqli_query($connect, $firstLogin) or die("SQL error");

        if(!$result) {
            echo "NO Data";
        }

        $row = mysqli_fetch_array($result);

        echo $row["money"];
    }

    $fileName = $dataCount[0] . ".txt";
    
    $file = fopen($fileName, "a+");

    if(!$file) die("Cannot open the file.");

    fclose($file);
}

