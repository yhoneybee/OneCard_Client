using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nettention.Proud;
using System;

public class Client : MonoBehaviour
{
    public static NetClient C { get; set; } = new NetClient();
    NetConnectionParam Param { get; set; } = new NetConnectionParam();
    public static C2S.Proxy Proxy { get; set; } = new C2S.Proxy();
    public static S2C.Stub Stub { get; set; } = new S2C.Stub();

    public static string Room_name;

    public static bool isReady = false;

    void Start()
    {
        Param.serverIP = "127.0.0.1";
        Param.protocolVersion = new Nettention.Proud.Guid("{E54C4938-8BFC-4443-87F3-386C1AA388F0}");
        Param.serverPort = 6475;

        Stub.Start = OnStart;
        Stub.TurnStart = OnTurnStart;
        Stub.Rank = OnRank;
        Stub.ExcludeGame = OnExcludeGame;

        C.AttachProxy(Proxy);
        C.AttachStub(Stub);
        C.Connect(Param);
        Debug.Log("Connect!");
        UiMgr.Instance.GoLobby();
    }

    private bool OnStart(HostID remote, RmiContext rmiContext)
    {
        UiMgr.Instance.GoGame();
        Debug.Log("Game Start!");
        return true;
    }

    private bool OnTurnStart(HostID remote, RmiContext rmiContext)
    {
        throw new NotImplementedException();
    }

    private bool OnRank(HostID remote, RmiContext rmiContext, int rank)
    {
        throw new NotImplementedException();
    }

    private bool OnExcludeGame(HostID remote, RmiContext rmiContext)
    {
        throw new NotImplementedException();
    }

    void Update()
    {
        C.FrameMove();
    }

    private void OnApplicationQuit()
    {
        C.Disconnect();
    }

    public void Ready()
    {
        isReady = !isReady;
        if (isReady)
            Proxy.Ready(HostID.HostID_Server, RmiContext.ReliableSend);
        else
            Proxy.UnReady(HostID.HostID_Server, RmiContext.ReliableSend);
    }
}
