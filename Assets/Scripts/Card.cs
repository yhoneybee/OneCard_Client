using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Card : MonoBehaviour
{
    public void SetCard(int symbol, int num)
    {
        Symbol = symbol;
        Num = num;
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Card/{Symbol}/{Num}");
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Card/{1}/{1}");
    }
    public int Symbol { get; set; }
    public int Num { get; set; }
}
