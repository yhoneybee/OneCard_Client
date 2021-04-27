using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Card : MonoBehaviour
{
    public void SetCard(int symbol, int num, bool set_parent = true)
    {
        Symbol = symbol;
        Num = num;

        RectTransform card_rtt = GetComponent<RectTransform>();
        BoxCollider2D card_box = GetComponent<BoxCollider2D>();

        GetComponent<Image>().sprite = Resources.Load<Sprite>($"Card/{Symbol}/{Num}");
        GetComponent<Rigidbody2D>().gravityScale = 0;

        if (set_parent)
            card_rtt.SetParent(GameObject.Find("Cards").GetComponent<RectTransform>().transform);
        else
            Debug.Log($"{Symbol} / {Num}");

        card_rtt.sizeDelta = new Vector2(200, 300);
        card_rtt.localScale = Vector3.one;

        card_box.isTrigger = true;
        card_box.offset = new Vector2(0, 0);
        card_box.size = new Vector2(200, 300);
    }
    public int Symbol { get; set; }
    public int Num { get; set; }
}
