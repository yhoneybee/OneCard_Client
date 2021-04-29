using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDown : MonoBehaviour
{
    Vector3 touch_pos;
    Camera cam;
    public static GameObject Select;
    private void Start()
    {
        cam = GameObject.Find("Cam").GetComponent<Camera>();
    }
    private void Update()
    {
        if (Input.touchCount >= 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                touch_pos = cam.ScreenToWorldPoint(touch.position);
                touch_pos = new Vector3(touch_pos.x, touch_pos.y, GameObject.Find("Canvas").transform.position.z - 5);

                Ray2D ray = new Ray2D(touch_pos, transform.forward);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                Debug.DrawRay(touch_pos, ray.direction, Color.red, 0.3f);

                if (hit)
                {
                    if (Select != null)
                        Select.transform.position -= new Vector3(0, 1, 0);
                    Select = hit.transform.gameObject;
                    Select.transform.position += new Vector3(0, 1, 0);
                    Debug.Log(Select.name);
                }
            }
        }
    }
    public void Down()
    {
        Card card = Select.GetComponent<Card>();
        Client.Proxy.Down(Nettention.Proud.HostID.HostID_Server, Nettention.Proud.RmiContext.ReliableSend, card.Symbol, card.Num);
    }
}
