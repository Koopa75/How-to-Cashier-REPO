using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Scanner : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        PhysicsItem item = other.gameObject.GetComponent<PhysicsItem>();
        if (item != null)
        {
            if (!item.item.scanned)
            {
                if (item.Scan())
                {
                    // Scanning sound effect
                }
                else
                {
                    // Fail to scan sound effect
                }
            }
        }
    }
}
