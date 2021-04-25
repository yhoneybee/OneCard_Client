using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiMgr : MonoBehaviour
{
    public static UiMgr Instance = null;

    public GameObject Lobby;
    public GameObject Waiting;
    public GameObject Game;

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
        Waiting.SetActive(false);
        Game.SetActive(false);
    }
}
