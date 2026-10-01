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
        difficultyRamping = 4f;
    }

    public void Update()
    {
        
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

    public void AddItem(ItemModel item)
    {
        itemList.Add(item);
    }

    public void RemoveItem(ItemModel item)
    {
        itemList.Remove(item);
    }

    public void ClearItems()
    {
        foreach (ItemModel item in itemList)
        {
            if (item.bagged && item.Prop != null)
            {
                Destroy(item.Prop);
            }
            item.payed = false;
            item.lame = true;
        }
        itemList.Clear();
        UpdateCost();
    }

    public void UpdateCost()
    {
        int _total = 0;
        bool allBagged = true;
        foreach (ItemModel item in itemList)
        {
            if (item.scanned && !item.payed) _total += item.Cost;
            if (!item.bagged && !item.lame) allBagged = false;
        }
        checkoutbutton.interactable = allBagged;
        allcheckedouttext.gameObject.SetActive(allBagged);
        Total = _total;
    }

    public void TryToCheckout()
    {
        foreach (ItemModel item in itemList)
        {
            if (!item.bagged && !item.payed) return;
        }
        foreach (ItemModel item in itemList)
        {
            if (item.bagged)
            {
                // code for customer paying cost of item
                item.payed = true;
            }
        }
        ClearItems();
        SpawnNewItems();
        UpdateCost();
    }

    public void SpawnNewItems()
    {
        difficultyRamping += 1f;
        for (int i = 0; i < (int)difficultyRamping; i++)
        {
            Instantiate(itemprefabs[Random.Range(0, itemprefabs.Length)], new Vector3(-0.75f, 1.5f, 0f) + Random.insideUnitSphere * 0.25f, Random.rotation, itemsparent);
        }
    }

    public Transform itemsparent;
    public Minidisplay minidisplay;
    public Button checkoutbutton;
    public TextMeshProUGUI allcheckedouttext;

    public PhysicsItem[] itemprefabs;

    private int total;
    private float difficultyRamping;

    private List<ItemModel> itemList = new List<ItemModel>();
}
