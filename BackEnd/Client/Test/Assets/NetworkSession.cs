using UnityEngine;
using System.Net;
using System.IO;
using System.Collections.Generic;

public enum RequestType { GET, PUT, POST, DELETE };


public class NetworkSession : MonoBehaviour {
    
    public RequestType type;
    public RequestProtocol protocol;
    public List<string> datas;

    private HttpWebRequest webRequest;

    // Use this for initialization
    void Start () {
        //webRequest = WebRequest.Create("http://localhost/Process.php") as HttpWebRequest;
        webRequest = WebRequest.Create("http://ec2-52-78-108-112.ap-northeast-2.compute.amazonaws.com/Process.php") as HttpWebRequest;
        
        webRequest.ContentType = "application/json";
    }

    public void Request()
    {
        switch (type)
        {
            case RequestType.GET:
                webRequest.Method = "GET";

                print(ReadData());

                break;

            case RequestType.PUT:
                webRequest.Method = "PUT";
                WriteData(CreateJsonData(protocol, datas));

                break;

            case RequestType.POST:
                webRequest.Method = "POST";
                WriteData(CreateJsonData(protocol, datas));
                print(ReadData());

                break;


            case RequestType.DELETE:
                webRequest.Method = "DELETE";
                WriteData(CreateJsonData(protocol, datas));

                break;
        }
    }

    private void WriteData(string jsonData)
    {
        using (StreamWriter stream = new StreamWriter(webRequest.GetRequestStream()))
        {
            stream.WriteLine(jsonData);
        }
    }
    // Datas 안들어감
    private string ReadData()
    {
        using (HttpWebResponse response = webRequest.GetResponse() as HttpWebResponse)
        {
            StreamReader reader = new StreamReader(response.GetResponseStream());

            return reader.ReadToEnd();
        }
    }

    private string CreateJsonData(RequestProtocol protocol, List<string> datas)
    {
        DataClass test = new DataClass(protocol, datas);
        print(JsonUtility.ToJson(test, prettyPrint: true));
        return JsonUtility.ToJson(test, prettyPrint:true);
    }
}
