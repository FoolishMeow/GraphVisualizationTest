using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.HttpClient;

public class APIData {
    public HttpClient client = new HttpClient();
    public DataManager dataManager;
    private static string API_BASE_URL = "http://api.cassiuschen.com";

    public APIData(DataManager manager) {
        dataManager = manager;
    }

    // WARNING!!! 这里暂时没有对服务器异常情况进行处理，需要注意！！！
    // 获得 Spherical 数据，回调 dataManager 里的回调函数接收数据，写法很脏，暂时这么用。
    public void GetSphericalLayoutData() {
        string url = "/v1/layout";
        Dictionary<string, string> param = new Dictionary<string, string>() {
            {"type", "spherical"}
        };
        client.GetString(new System.Uri(generateUrl(url, param)), (HttpResponseMessage<string> obj) => {
            dataManager.loadSphericalDataFromServer(obj.Data);
        });
    }

    // 获得 Hierarchy 数据，同上
    public void GetHierarchyLayoutData()
    {
        string url = "/v1/layout";
        Dictionary<string, string> param = new Dictionary<string, string>() {
            {"type", "spherical"}
        };
        client.GetString(new System.Uri(generateUrl(url, param)), (HttpResponseMessage<string> obj) => {
            dataManager.loadHierarchyDataFromServer(obj.Data);
        });
    }

    // 以下抽象暂时无用 -------------------------------------------
    private string GetLayout(string layoutType) {
        return GetDataFromAPI("/v1/layout", new Dictionary<string, string>() {
            {"type", layoutType}
        });
    }

    public string TestApi() {
        return GetDataFromAPI("/");    
    }

    private string GetDataFromAPI(string url) {
        return GetDataFromAPI(url, new Dictionary<string, string>() { });
    }

    private string GetDataFromAPI(string url, Dictionary<string, string> param) {
        string response = "";
        client.GetString(new System.Uri(generateUrl(url, param)), (HttpResponseMessage<string> obj) => {
            dataManager.loadSphericalDataFromServer(obj.Data);
        });
        return response;
    }

    private string generateUrl(string url, Dictionary<string, string> param) {
        string completeUrl = API_BASE_URL + url;
        if (param.Keys.Count > 0) {
            string[] paramStr = new string[param.Keys.Count];
            int i = 0;
            foreach (var p in param) {
                paramStr[i] = p.Key + "=" + p.Value;
                i++;
            }
            completeUrl += "?" + string.Join("&", paramStr);
            Debug.Log(completeUrl);
        }
        return completeUrl;
    }
}