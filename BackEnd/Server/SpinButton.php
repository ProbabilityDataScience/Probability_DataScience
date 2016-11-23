<?php
/**
 * Created by PhpStorm.
 * User: Kimyoumin
 * Date: 2016-11-22
 * Time: 오후 9:53
 */

// Protocol : Login
// 클라->서버 : 토큰 값 / 처음 접속 여부 (True, false)
// 서버->클라 : 현재 돈
function SpinButtonProtocol($datas)
{
    //Connect DB
    $connect = mysqli_connect("localhost","root","ef794200") or die ("데이터베이스 연결 실패 : " . mysqli_connect_error());

    //Select DB
    mysqli_select_db($connect, "phpmyadmin") or die("DB선택에러");

    // DB 기록
    $dataSetQuery = "";
    $dataSetQuery .= "UPDATE somaTable SET money=";
    $dataSetQuery .= "\"";  $dataSetQuery .= $datas[2]; $dataSetQuery .= "\"";
    $dataSetQuery .= "\"";  $dataSetQuery .= $datas[3]; $dataSetQuery .= "\"";
    $dataSetQuery .= "WHERE faceBookToken=";
    $dataSetQuery .= "\"";  $dataSetQuery .= $datas[0]; $dataSetQuery .= "\";";

    if (mysqli_query($connect, $dataSetQuery) == false)
        echo("DB Update Failed");

    // 로그 기록
    $fileNameQuery = mysqli_query($connect, "SELECT fileName FROM somaTable WHERE faceBookToken = " . $datas[0]) or die("SQL error");
    $fileQueryResult = mysqli_fetch_array($fileNameQuery);

    $fileName = "";
    $fileName .= $fileQueryResult[0];
    $fileName .= ".txt";

    $file = fopen($fileName, "a+");

    if(!$file) die("Cannot open the file.");

    $dataString = "";
    $dataString .= $datas[1] . " ";
    $dataString .= $datas[2] . " ";
    $dataString .= $datas[3] . " ";
    $dataString .= $datas[4] . " ";
    $dataString .= $datas[5] . "\n";

    fwrite($file, $dataString);

    fclose($file);
}
