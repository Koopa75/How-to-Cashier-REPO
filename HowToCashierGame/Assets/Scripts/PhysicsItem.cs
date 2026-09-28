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

    public string Name; // Temporary, should be changed to a more proper database system instead of using prefab fields
    public int Cost; // Temporary

    [HideInInspector]
    public bool isScanned;
}
