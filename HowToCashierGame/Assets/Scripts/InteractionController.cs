using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InteractionController : MonoBehaviour
{
    public static InteractionController instance
    {
        get
        {
            return _instance;
        }
    }

    private static InteractionController _instance;

    public void Start()
    {
        _instance = this;
        Total = 0;
        items = new List<ItemModel>();
    }

    public int Total
    {
        get
        {
            return total;
        }
        set
        {
            total = value;
            minidisplay.SetValue(total);
        }
    }

    public void AddCost(int amt)
    {
        Total += amt;
    }

    public Minidisplay minidisplay;

    private int total;

    private List<ItemModel> items;
}
