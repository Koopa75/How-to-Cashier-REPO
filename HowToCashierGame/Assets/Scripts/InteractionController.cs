using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static ChoosePaymentMethod;

public class InteractionController : MonoBehaviour
{
    public SoundSystem checkoutSoundVolume;
    public CheckoutBag checkoutBag;
    public CustomerSpawner removeCustomer;
    public CardHandler cardHandler;
    public CashHandler cashHandler;


    public AudioSource checkoutSound;
    public TextMeshProUGUI CustomersServedText;
    public int CustomersServed = 0;

    private int totalItems = 0;


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
        difficultyRamping = 3f;
        SpawnNewItems();
    }

    public void Update()
    {
        cashHandler.SetTotalPrice((float) Total / 100);
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
        totalItems++;
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
        totalItems = 0;
        UpdateCost();
    }

    public void UpdateCost()
    {
        int _total = 0;
        bool allBagged = true;
        if (PlayerController.instance.currentCam == -1) allBagged = false;
        foreach (ItemModel item in itemList)
        {
            if (item.scanned && !item.payed) _total += item.Cost;
            if (!item.bagged && !item.lame) allBagged = false;
        }
        //Make it so this is only active if the card has also been swipped

        GameObject currentCustomer = removeCustomer.GetCurrentCustomer();

        if (currentCustomer != null)
        {
            PaymentMethod currentMethod =
                currentCustomer.GetComponent<CustomerBehavior>()
                .paymentMethod;

            switch (removeCustomer.GetCurrentCustomerPaymentMethod())
            {
                case PaymentMethod.Card:

                    checkoutbutton.interactable = allBagged && cardHandler.GetHasSwiped();
                    break;

                    
                case PaymentMethod.Cash:
                    //Debug.Log("Is the Button Interactuable: " + allBagged);
                    checkoutbutton.interactable = allBagged;
                            //cashHandler.GetCorrectChangeGiven();
                    break;  
            }
        }
        else
        {
            checkoutbutton.interactable = false;
        }
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
           
                checkoutSound.volume = checkoutSoundVolume.CheckSoundEffectSliderValue() * checkoutSoundVolume.CheckMasterSliderValue();
                // code for customer paying cost of item
                checkoutSound.time = 1.5f;
                checkoutSound.Play();
            }
        }
        ClearItems();
        checkoutBag.ResetCheckoutBag();
        cardHandler.SetHasSwiped(false);
        removeCustomer.RemoveCustomer();
        cardHandler.DisableCard();

        CustomersServed++;
        CustomersServedText.SetText("Customers Served: " + CustomersServed);

        StartCoroutine(ItemSpawnCountdown(1.5f));
        UpdateCost();
    }

    public void SpawnNewItems()
    {
        int itemSpawnAmount = Random.Range(3, 8);
       // difficultyRamping += 1f; This will be tied to the day
        for (int i = 0; i < (int)itemSpawnAmount; i++)
        {
            Instantiate(itemprefabs[Random.Range(0, itemprefabs.Length)], itemsparent.position + new Vector3(-0.75f, 1.5f, 0f) + Random.insideUnitSphere * 0.25f, Random.rotation, itemsparent);
        }
        
    }

    IEnumerator ItemSpawnCountdown(float timeBeforeSpawn)
    {
        yield return new WaitForSeconds(timeBeforeSpawn);

        SpawnNewItems();
    }




    public int GetTotalItems()
    {
        return totalItems;
    }
    

    public Transform itemsparent;
    public Minidisplay minidisplay;
    public Button checkoutbutton;
    public TextMeshProUGUI allcheckedouttext;

    public PhysicsItem[] itemprefabs;

    private int total;
    private float difficultyRamping; //Gonna Tie this to the current day at some point

    private List<ItemModel> itemList = new List<ItemModel>();
}
