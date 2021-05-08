using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiMgr : MonoBehaviour
{
    public static UiMgr Instance = null;

    public GameObject Connect;
    public GameObject Lobby;
    public GameObject Waiting;
    public GameObject Game;

    public GameObject Down;

    private void Awake()
    {
        Instance = this;
    }

    public void GoGame()
    {
        Game.SetActive(true);
    }

    public void GoWaiting()
    {
        Waiting.SetActive(true);
        Game.SetActive(false);
    }

    public void GoLobby()
    {
        Lobby.SetActive(true);
        Waiting.SetActive(false);
        Game.SetActive(false);
    }

    public void GoConnect()
    {
        Connect.SetActive(true);
        Lobby.SetActive(false);
        Waiting.SetActive(false);
        Game.SetActive(false);
    }
}
