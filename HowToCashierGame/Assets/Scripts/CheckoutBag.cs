using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CheckoutBag : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        PhysicsItem item = other.gameObject.GetComponent<PhysicsItem>();
        if (item != null)
        {
            if (item.isScanned && !item.isBeingDragged)
            {
                item.gameObject.SetActive(false);
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        PhysicsItem item = other.gameObject.GetComponent<PhysicsItem>();
        if (item != null)
        {
            if (item.isScanned && !item.isBeingDragged)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}
