using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CheckoutBag : MonoBehaviour
{

    public AudioSource baggingSoundEffect;
    public SoundSystem baggingVolume;
    private bool canGrabCard = false;
    private int itemsInBag = 0;



    public void OnTriggerEnter(Collider other)
    {
        PhysicsItem item = other.gameObject.GetComponent<PhysicsItem>();
        if (item != null)
        {
            if (!item.dragging)
            {
                if (item.Bag())
                {
                    baggingSoundEffect.volume = baggingVolume.CheckSoundEffectSliderValue() * baggingVolume.CheckMasterSliderValue();
                    baggingSoundEffect.Play();

                    itemsInBag++;
                        
                    if (itemsInBag == InteractionController.instance.GetTotalItems())
                    {
                        canGrabCard = true;
                    }
                    

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
                    baggingSoundEffect.volume = baggingVolume.CheckSoundEffectSliderValue() * baggingVolume.CheckMasterSliderValue();
                    baggingSoundEffect.Play();

                    itemsInBag++;

                    if (itemsInBag == InteractionController.instance.GetTotalItems())
                    {
                        canGrabCard = true;
                    }
                }
                else
                {
                    item.GetComponent<Rigidbody>().AddForce((Random.onUnitSphere + 1f * Vector3.up) * Time.fixedDeltaTime * 1000f);
                }
            }
        }
    }

    public bool GetCanGrabCard()
    {
        return canGrabCard;
    }
    public void SetCanGrabCard(bool cardGrabStatus) 
    {
        canGrabCard = cardGrabStatus;
    }

}
