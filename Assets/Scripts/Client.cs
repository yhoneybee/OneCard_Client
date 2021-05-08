using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Nettention.Proud;
using System;
using TMPro;

public class Client : MonoBehaviour
{
    public static NetClient C { get; set; } = new NetClient();
    NetConnectionParam Param { get; set; } = new NetConnectionParam();
    public static C2S.Proxy Proxy { get; set; } = new C2S.Proxy();
    public static S2C.Stub Stub { get; set; } = new S2C.Stub();

    public static string Room_name;

    public static bool isReady = false;

    public Card LastCard;

    public static Card Selected;
    public static bool MyTurn { get; set; }
    public static List<Card> Cards { get; set; } = new List<Card>();

    [Header("InputFields")]
    public TMP_InputField Ip;

    public GameObject Client_Parent;
    public GameObject Clients;

    private void Awake()
    {
        Screen.SetResolution(160 * 5, 90 * 5, false);
        MyTurn = false;
    }
    void Start()
    {
        Param.serverIP = "119.196.246.120";
        Param.protocolVersion = new Nettention.Proud.Guid("{E54C4938-8BFC-4443-87F3-386C1AA388F0}");
        Param.serverPort = 6475;

        Stub.Start = OnStart;
        Stub.TurnStart = OnTurnStart;
        Stub.LastCard = OnLastCard;
        Stub.Down = OnDown;
        Stub.Draw = OnDraw;
        Stub.ChangeSymbol = OnChangeSymbol;
        Stub.Rank = OnRank;
        Stub.ExcludeGame = OnExcludeGame;
        Stub.NowCardsCount = OnNowCardsCount;

        C.JoinServerCompleteHandler = OnJoinServer;

        C.AttachProxy(Proxy);
        C.AttachStub(Stub);
    }

    private bool OnNowCardsCount(HostID remote, RmiContext rmiContext, HostID client, int count)
    {
        // 다른 클라이언트들의 카드수가 날라옴
        // count -> 카드수
        // client -> count만큼 카드를 들고있는 클라이언트
        GameObject c = GameObject.Find($"{client}");
        if (c)
        {
            c.transform.Find("Cards").GetComponent<TextMeshProUGUI>().text = count.ToString();
        }
        else//null
        {
            c = Instantiate(Clients);
            c.name = $"{client}";
            c.transform.Find("ID").GetComponent<TextMeshProUGUI>().text = client.ToString();
            c.transform.Find("Cards").GetComponent<TextMeshProUGUI>().text = count.ToString();
            c.transform.SetParent(Client_Parent.transform);
            c.GetComponent<RectTransform>().localScale = UnityEngine.Vector3.one;
        }
        return true;
    }

    private void OnJoinServer(ErrorInfo info, ByteArray replyFromServer)
    {
        Debug.Log("Connect!");
        UiMgr.Instance.GoLobby();
    }

    public void Connect()
    {
        Param.serverIP = Ip.text;
        C.Connect(Param);
    }

    private bool OnDown(HostID remote, RmiContext rmiContext, int symbol, int num)
    {
        foreach (var card in Cards)
        {
            if (card.Symbol == symbol && card.Num == num)
            {
                Cards.Remove(card);
                if (Cards.Count > 0)
                    Cards[Cards.Count - 1].SetCard(Cards[Cards.Count - 1].Symbol, Cards[Cards.Count - 1].Num);
                Destroy(card.gameObject);
                Proxy.ChangeSymbol(HostID.HostID_Server, RmiContext.ReliableSend, CardDown.choose);
                Proxy.NowCardsCount(HostID.HostID_Server, rmiContext, Cards.Count);
                return true;
            }
        }
        return false;
    }

    private bool OnLastCard(HostID remote, RmiContext rmiContext, int symbol, int num)
    {
        LastCard.SetCard(symbol, num, false);
        return true;
    }

    private bool OnDraw(HostID remote, RmiContext rmiContext, int symbol, int num)
    {
        GameObject card = new GameObject($"{symbol}, {num}");
        card.AddComponent<Card>().SetCard(symbol, num);
        if (Cards.Count > 0)
        {
            BoxCollider2D prev_box = Cards[Cards.Count - 1].GetComponent<BoxCollider2D>();
            prev_box.offset = new Vector2(-40, 0);
            prev_box.size = new Vector2(125, 300);
        }
        Cards.Add(card.GetComponent<Card>());
        Proxy.NowCardsCount(HostID.HostID_Server, rmiContext, Cards.Count);
        return true;
    }

    private bool OnChangeSymbol(HostID remote, RmiContext rmiContext, int symbol)
    {
        LastCard.SetCard(symbol, LastCard.Num, false);
        return true;
    }

    private bool OnStart(HostID remote, RmiContext rmiContext)
    {
        UiMgr.Instance.GoGame();
        Debug.Log("Game Start!");
        for (int i = 0; i < 7; i++)
            Proxy.Draw(HostID.HostID_Server, RmiContext.ReliableSend);
        return true;
    }

    private bool OnTurnStart(HostID remote, RmiContext rmiContext)
    {
        MyTurn = true;
        UiMgr.Instance.Down.GetComponent<Image>().color = new Color(255, 255, 0, 255);
        Proxy.NowCardsCount(HostID.HostID_Server, rmiContext, Cards.Count);
        return true;
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
