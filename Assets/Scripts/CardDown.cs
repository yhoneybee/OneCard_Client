using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDown : MonoBehaviour
{
    Vector3 touch_pos;
    Camera cam;
    public static int choose;
    public GameObject ChangeSymbolBtns;
    public static GameObject Select { get; set; }
    private void Awake()
    {
        Select = null;
    }
    private void Start()
    {
        cam = GameObject.Find("Cam").GetComponent<Camera>();
    }
    private void Update()
    {
#if UNITY_ANDROID
        if (Input.touchCount <= 0)
            return;
        
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            touch_pos = cam.ScreenToWorldPoint(touch.position);
#elif UNITY_STANDALONE_WIN
        if (Input.GetMouseButton(0))
            touch_pos = cam.ScreenToWorldPoint(Input.mousePosition);
#endif
        touch_pos = new Vector3(touch_pos.x, touch_pos.y, GameObject.Find("Canvas").transform.position.z - 5);

        Ray2D ray = new Ray2D(touch_pos, transform.forward);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit)
        {
            if (hit.transform.gameObject.name == "LastCard")
                return;

            foreach (var card in Client.Cards)
                card.transform.position = new Vector3(card.transform.position.x, card.transform.parent.position.y, card.transform.position.z);

            Select = hit.transform.gameObject;
            Select.transform.position = new Vector3(Select.transform.position.x, Select.transform.parent.position.y + 1, Select.transform.position.z);

            Card Selected = Select.GetComponent<Card>();

            choose = Selected.Symbol;

            if (Selected.Num == 7)
                ChangeSymbolBtns.SetActive(true);
            else
                ChangeSymbolBtns.SetActive(false);
        }
    }
    public void Choose(int choose) => CardDown.choose = choose;
    public void Down()
    {
        if (Client.MyTurn)
        {
            Client.MyTurn = false;
            Card card = Select.GetComponent<Card>();
            Client.Proxy.Down(Nettention.Proud.HostID.HostID_Server, Nettention.Proud.RmiContext.ReliableSend, card.Symbol, card.Num);
            Client.Proxy.NowCardsCount(Nettention.Proud.HostID.HostID_Server, Nettention.Proud.RmiContext.ReliableSend, Client.Cards.Count);
            Client.Proxy.TurnEnd(Nettention.Proud.HostID.HostID_Server, Nettention.Proud.RmiContext.ReliableSend);
            UiMgr.Instance.Down.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
    }
}
