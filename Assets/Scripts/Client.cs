using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nettention.Proud;
using S2C;
using System;

public class Client : MonoBehaviour
{
    NetClient C { get; set; } = new NetClient();
    NetConnectionParam Param { get; set; } = new NetConnectionParam();

    void Start()
    {
        Param.serverIP = "192.168.1.254";
        Param.protocolVersion = new Nettention.Proud.Guid("{E54C4938-8BFC-4443-87F3-386C1AA388F0}");
        Param.serverPort = 6475;

        C2S.Proxy C2SProxy = new C2S.Proxy();
        C2C.Proxy C2CProxy = new C2C.Proxy();
        S2C.Stub S2CStub = new S2C.Stub();
        C2C.Stub C2CStub = new C2C.Stub();

        S2CStub.Create_OK = OnCreate_OK;
        S2CStub.Create_Fail = OnCreate_Fail;
        S2CStub.Join_OK = OnJoin_OK;
        S2CStub.Join_Fail = OnJoin_Fail;
        S2CStub.Leave_OK = OnLeave_OK;
        S2CStub.Leave_Fail = OnLeave_Fail;
        S2CStub.Start_OK = OnStart_OK;
        S2CStub.Start_Fail = OnStart_Fail;

        C2CStub.TurnChanged = OnTurnChanged;
        C2CStub.Turn = OnTurn;
        C2CStub.Draw = OnDraw;
        C2CStub.Invalid = OnInvalid;
        C2CStub.Jump = OnJump;
        C2CStub.Reverse = OnReverse;
        C2CStub.ChangeColor = OnChangeColor;
        C2CStub.Timer = OnTimer;
        C2CStub.Rank = OnRank;

        C.AttachProxy(C2SProxy);
        C.AttachProxy(C2CProxy);
        C.AttachStub(S2CStub);
        C.AttachStub(C2CStub);

        C.Connect(Param);
    }

    private bool OnRank(HostID remote, RmiContext rmiContext, int rank)
    {
        throw new NotImplementedException();
    }

    private bool OnTimer(HostID remote, RmiContext rmiContext, float time)
    {
        throw new NotImplementedException();
    }

    private bool OnChangeColor(HostID remote, RmiContext rmiContext, bool black)
    {
        throw new NotImplementedException();
    }

    private bool OnReverse(HostID remote, RmiContext rmiContext, HostID caster)
    {
        throw new NotImplementedException();
    }

    private bool OnJump(HostID remote, RmiContext rmiContext, HostID caster)
    {
        throw new NotImplementedException();
    }

    private bool OnInvalid(HostID remote, RmiContext rmiContext)
    {
        throw new NotImplementedException();
    }

    private bool OnDraw(HostID remote, RmiContext rmiContext, HostID target, int count)
    {
        throw new NotImplementedException();
    }

    private bool OnTurn(HostID remote, RmiContext rmiContext, HostID turn)
    {
        throw new NotImplementedException();
    }

    private bool OnTurnChanged(HostID remote, RmiContext rmiContext)
    {
        throw new NotImplementedException();
    }

    private bool OnStart_Fail(HostID remote, RmiContext rmiContext)
    {
        throw new NotImplementedException();
    }

    private bool OnStart_OK(HostID remote, RmiContext rmiContext)
    {
        throw new NotImplementedException();
    }

    private bool OnLeave_Fail(HostID remote, RmiContext rmiContext)
    {
        throw new NotImplementedException();
    }

    private bool OnLeave_OK(HostID remote, RmiContext rmiContext)
    {
        throw new NotImplementedException();
    }

    private bool OnJoin_Fail(HostID remote, RmiContext rmiContext)
    {
        throw new NotImplementedException();
    }

    private bool OnJoin_OK(HostID remote, RmiContext rmiContext)
    {
        throw new NotImplementedException();
    }

    private bool OnCreate_Fail(HostID remote, RmiContext rmiContext)
    {
        throw new NotImplementedException();
    }

    private bool OnCreate_OK(HostID remote, RmiContext rmiContext)
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
}
