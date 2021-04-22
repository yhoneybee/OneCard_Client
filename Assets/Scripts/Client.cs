using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nettention.Proud;
using System;

public class Client : MonoBehaviour
{
    NetClient C { get; set; } = new NetClient();
    NetConnectionParam Param { get; set; } = new NetConnectionParam();

/*    C2S.Proxy C2SProxy { get; set; } = new C2S.Proxy();
    C2C.Proxy C2CProxy { get; set; } = new C2C.Proxy();
    S2C.Stub S2CStub { get; set; } = new S2C.Stub();
    C2C.Stub C2CStub { get; set; } = new C2C.Stub();*/

    void Start()
    {
        Param.serverIP = "172.30.1.22";
        Param.protocolVersion = new Nettention.Proud.Guid("{E54C4938-8BFC-4443-87F3-386C1AA388F0}");
        Param.serverPort = 6475;

        C.P2PMemberJoinHandler = OnP2PJoin;
        C.P2PMemberLeaveHandler = OnP2PLeave;

/*        C.AttachProxy(C2SProxy);
        C.AttachProxy(C2CProxy);
        C.AttachStub(S2CStub);
        C.AttachStub(C2CStub);*/

        C.Connect(Param);
        Debug.Log("Connect!");
    }

    private void OnP2PLeave(HostID memberHostID, HostID groupHostID, int memberCount)
    {
    }

    private void OnP2PJoin(HostID memberHostID, HostID groupHostID, int memberCount, ByteArray message)
    {
    }

    void Update()
    {
        C.FrameMove();
    }

    private void OnApplicationQuit()
    {
        C.Disconnect();
    }
}
