using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class RestClient : MonoBehaviour
{
    private static RestClient _instance;

    public static RestClient Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<RestClient>();
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(RestClient).Name;
                    _instance = go.AddComponent<RestClient>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    public IEnumerator Get(string url, System.Action<PlayerList> callBack)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {

                if (www.isDone)
                {
                    string data = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);

                    Debug.Log("Legenda: " + data);

                    //  callBack(Legendas);
                }
            }
        }
    }

    public IEnumerator Delete(string url, int id)
    {
        string urlWithParams = string.Format("{0}{1}", url, id);

        using (UnityWebRequest www = UnityWebRequest.Delete(urlWithParams))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    Debug.Log(string.Format("Item com id {0} deletado", id));
                }
            }
        }
    }

    public IEnumerator Post(string url, System.Action<PlayerList> callBack, Player newPlayer)
    {

        string jsonData = JsonUtility.ToJson(newPlayer);

        Debug.Log(jsonData);

        using (UnityWebRequest www = UnityWebRequest.Post(url, jsonData))
        {

            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));

            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {

                if (www.isDone)
                {
                    string data = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);

                    Debug.Log("Player: " + data);

                    //  callBack(Legendas);
                }
            }
        }
    }
}
