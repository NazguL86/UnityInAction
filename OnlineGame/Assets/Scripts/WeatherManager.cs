using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;
using UnityEngine;
using MiniJSON;

public class WeatherManager : MonoBehaviour, IGameManager {
    public ManagerStatus status { get; private set; }
    public float cloudValue { get; private set; }

    // Add cloud value here (listing 10.8)
    private NetworkService _network;

    // Use this for initialization
    public void Startup(NetworkService service) {
        Debug.Log("Weather manager starting...");
        _network = service;

        // XML based request
        //StartCoroutine(_network.GetWeatherXML(OnXMLDataLoaded));

        // JSON based request
        StartCoroutine(_network.GetWeatherJSON(OnJSONDataLoaded));

        status = ManagerStatus.Initializing;
	}

    public void OnXMLDataLoaded(string data)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(data);
        XmlNode root = doc.DocumentElement;
        XmlNode node = root.SelectSingleNode("clouds");
        string value = node.Attributes["value"].Value;
        cloudValue = Convert.ToInt32(value) / 100f;
        Debug.Log("Value: " + cloudValue);
        Messenger.Broadcast(GameEvent.WEATHER_UPDATED);
        status = ManagerStatus.Started;
    }

    public void OnJSONDataLoaded(string data)
    {
        Dictionary<string, object> dict;
        dict = Json.Deserialize(data) as Dictionary<string, object>;
        Dictionary<string, object> clouds = (Dictionary<string, object>) dict["clouds"];
        cloudValue = (long)clouds["all"] / 100f;
        Debug.Log("Value: " + cloudValue);
        Messenger.Broadcast(GameEvent.WEATHER_UPDATED);
        status = ManagerStatus.Started;
    }
}
