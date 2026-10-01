using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PhysicsItem : MonoBehaviour
{
    public void Start()
    {
        item = new ItemModel();
        item.Prop = this;
        item.Cost = Cost;
        InteractionController.instance.AddItem(item);
    }

    public void Update()
    {
        if (transform.localPosition.y < 0.5f)
        {
            if (item.lame)
            {
                Destroy(gameObject);
                InteractionController.instance.RemoveItem(item);
            }
            else
            {
                transform.localPosition = new Vector3(-0.75f, 1.5f, 0f);
            }
        }
    }

    public void Pickup()
    {
        if (dragging) return;
        dragging = true;
        gameObject.layer = 8;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void UpdateDraggingPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public void Drop()
    {
        if (!dragging) return;
        dragging = false;
        gameObject.layer = 7;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    public bool Scan()
    {
        if (item.scanned || item.lame) return false;
        item.scanned = true;
        InteractionController.instance.UpdateCost();
        return true;
    }

    public bool Bag()
    {
        if (item.bagged || !item.scanned) return false;
        item.bagged = true;
        gameObject.SetActive(false);
        InteractionController.instance.UpdateCost();
        return true;
    }

    public string Name; // Temporary, should be changed to a more proper database system instead of using prefab fields
    public int Cost; // Temporary

    [HideInInspector]
    public ItemModel item;

    [HideInInspector]
    public bool dragging;
}
