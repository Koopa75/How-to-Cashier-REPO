using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PhysicsItem : MonoBehaviour
{
    public void Start()
    {
        isScanned = false;
        isBeingDragged = false;
    }

    public void Update()
    {
        if (transform.localPosition.y < 0.5f)
        {
            transform.localPosition = new Vector3(-0.75f, 1.5f, 0f);
        }
    }

    public bool Scan()
    {
        if (isScanned) return false;
        isScanned = true;
        InteractionController.instance.AddCost(Cost);
        return true;
    }

    public void Pickup()
    {
        if (isBeingDragged) return;
        isBeingDragged = true;
        gameObject.layer = 8;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void UpdateDrag(Vector3 pos)
    {
        transform.position = pos;
    }

    public void Drop()
    {
        if (!isBeingDragged) return;
        isBeingDragged = false;
        gameObject.layer = 7;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    public string Name; // Temporary, should be changed to a more proper database system instead of using prefab fields
    public int Cost; // Temporary

    [HideInInspector]
    public ItemModel model;

    [HideInInspector]
    public bool isScanned;
    public bool isBeingDragged;
}
