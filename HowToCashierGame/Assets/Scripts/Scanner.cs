using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Scanner : MonoBehaviour
{
    public AudioSource scanSound;
    public SoundSystem scannerSystem;

    public void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        PhysicsItem item = other.gameObject.GetComponent<PhysicsItem>();
        if (item != null)
        {
            if (!item.item.scanned)
            {
                if (item.Scan())
                {
                    scanSound.time = 0.7f;
                    scanSound.Play();
                }
                else
                {
                    // Fail to scan sound effect
                }
            }
        }
    }
}
