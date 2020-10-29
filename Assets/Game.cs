using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public string WEB_URL = "";

    // Start is called before the first frame update
    void Start()
    {
       //  StartCoroutine(RestClient.Instance.Get(WEB_URL, GetLegendas));

        StartCoroutine(RestClient.Instance.Post(WEB_URL, GetLegendas, new Player
        {
            Location = "03:6060",
            DeviceType = "Guarana",
            SupportedFormats = new string[] { "x−application−ncl360", "x−application−x3d" },
            RecognizableEvents = new string[] { "lookAt", "lookAway" }
    }));
    }

    void GetLegendas(PlayerList Players)
    {
        foreach(Player player in Players.Players)
        {
            Debug.Log("Location: " + player.Location);
            Debug.Log("DeviceType: " + player.DeviceType);
            Debug.Log("SupportedFormats: " + player.SupportedFormats);
            Debug.Log("RecognizableEvents: " + player.RecognizableEvents);
        }
    }

   }
