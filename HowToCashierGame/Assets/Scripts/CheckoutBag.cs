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
            if (!item.dragging)
            {
                if (item.Bag())
                {
                    // Bagging sound effect
                }
                else
                {
                    item.GetComponent<Rigidbody>().AddForce((item.transform.position - transform.position + 1f * Vector3.up).normalized * 100f);
                }
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        PhysicsItem item = other.gameObject.GetComponent<PhysicsItem>();
        if (item != null)
        {
            if (!item.dragging)
            {
                if (item.Bag())
                {
                    // Bagging sound effect
                }
                else
                {
                    item.GetComponent<Rigidbody>().AddForce((Random.onUnitSphere + 1f * Vector3.up) * Time.fixedDeltaTime * 1000f);
                }
            }
        }
    }
}
