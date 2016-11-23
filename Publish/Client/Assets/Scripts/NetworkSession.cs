using UnityEngine;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections;


public enum RequestType { GET, PUT, POST, DELETE };

public class NetworkSession : MonoBehaviour
{

    public RequestType type;
    public RequestProtocol protocol;
    public List<string> datas;

    // Use this for initialization
    void Start()
    {

    }

    string Request()
    {
        HttpWebRequest requestData = CreateRequestData();

        switch (type)
        {
            case RequestType.GET:
                return ReadData(requestData);

            case RequestType.PUT:
                WriteData(requestData, CreateJsonData(protocol, datas));

                return "";

            case RequestType.POST:
                WriteData(requestData, CreateJsonData(protocol, datas));

                return ReadData(requestData);

            case RequestType.DELETE:
                WriteData(requestData, CreateJsonData(protocol, datas));

                return "";

            default:
                return "";
        }
    }

    private HttpWebRequest CreateRequestData()
    {
        //HttpWebRequest requestData = WebRequest.Create("http://ec2-52-78-108-112.ap-northeast-2.compute.amazonaws.com/Process.php") as HttpWebRequest;
        HttpWebRequest requestData = WebRequest.Create("http://ec2-52-78-108-112.ap-northeast-2.compute.amazonaws.com/Process.php") as HttpWebRequest;
        //HttpWebRequest requestData = WebRequest.Create("http://localhost/CreateFile.php") as HttpWebRequest;

        requestData.ContentType = "application/json";

        switch (type)
        {
            case RequestType.GET:
                requestData.Method = "GET";

                break;

            case RequestType.PUT:
                requestData.Method = "PUT";

                break;

            case RequestType.POST:
                requestData.Method = "POST";

                break;

            case RequestType.DELETE:
                requestData.Method = "DELETE";

                break;
        }

        return requestData;
    }

    private void WriteData(HttpWebRequest requestData, string jsonData)
    {
        using (StreamWriter stream = new StreamWriter(requestData.GetRequestStream()))
        {
            stream.WriteLine(jsonData);
        }
    }

    private string ReadData(HttpWebRequest requestData)
    {
        using (HttpWebResponse response = requestData.GetResponse() as HttpWebResponse)
        {
            StreamReader reader = new StreamReader(response.GetResponseStream());

            return reader.ReadToEnd();
        }
    }

    private string CreateJsonData(RequestProtocol protocol, List<string> datas)
    {
        DataClass test = new DataClass(protocol, datas);
        print(JsonUtility.ToJson(test, prettyPrint: true));
        return JsonUtility.ToJson(test, prettyPrint: true);
    }

    public int Proc_Login()
    {
        string result = Request();

        print(result);

        return Convert.ToInt32(result);
    }

    public void Proc_SpinButton()
    {
        Request();
    }

    public void Proc_BettingButton()
    {
        Request();
    }
}
