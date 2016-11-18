using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum RequestProtocol { Test, LoginWithFacebook };

public class DataClass {

    public DataClass(RequestProtocol protocol, List<string> datas)
    {
        this.protocol = protocol;
        this.datas = datas;
    }

    public RequestProtocol protocol;
    public List<string> datas;
}
