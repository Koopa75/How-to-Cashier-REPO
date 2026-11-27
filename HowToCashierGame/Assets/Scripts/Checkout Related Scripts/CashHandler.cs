using NUnit.Framework;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

public class CashHandler : MonoBehaviour
{
    public CheckoutBag checkoutBag;
    public ChoosePaymentMethod choosePaymentMethod;
    public CustomerSpawner customerSpawner;

    public Transform SpawnPos1;
    public Transform SpawnPos2;
    public Transform SpawnPos3;
    public Transform SpawnPos4;
    public Transform SpawnPos5;
    public Transform CashHolder;

    private Transform[] SpawnPositions = new Transform[5];

    private List<GameObject> CashObjects = new List<GameObject>();

    public UnityEngine.UI.Button grabCashButton;

    public GameObject Cash;

    private bool HasNotCreatedStack = true;
    private bool hasGrabbedCash = false;




    float totalPrice;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Start()
    {
        SpawnPositions[0] = SpawnPos1;
        SpawnPositions[1] = SpawnPos2;
        SpawnPositions[2] = SpawnPos3;
        SpawnPositions[3] = SpawnPos4;
        SpawnPositions[4] = SpawnPos5;

        grabCashButton.gameObject.SetActive(false);
        grabCashButton.interactable = false;
        totalPrice = 0;
    }

    // Update is called once per frame
    void Update()
    {

        CheckCanGrabCashStatus();
        if (hasGrabbedCash)
        {
            grabCashButton.gameObject.SetActive(false);
            grabCashButton.interactable = false;
        }
    }

    public void SetTotalPrice(float Total)
    {
        totalPrice = Total;
        //Debug.Log("Total Price To Pay: " + (float)InteractionController.instance.Total / 100);
    }



    public void CheckCanGrabCashStatus()
    {
        GameObject currentCustomer = customerSpawner.GetCurrentCustomer();

        if (currentCustomer == null)
            return;

        CustomerBehavior customerBehavior = currentCustomer.GetComponent<CustomerBehavior>();

        if (checkoutBag.GetCustomerReadyToPay() && customerBehavior.paymentMethod == PaymentMethod.Cash)
        {
            if (HasNotCreatedStack)
            {
                CreateCashStack();
            }
            grabCashButton.interactable = true;
            grabCashButton.gameObject.SetActive(true);

        }
    }

    public void CreateCashStack()
    {
        if (totalPrice < 10)
        {
            Debug.Log("A Cash Object has Been Added");
            GameObject CashItem = Instantiate(Cash, SpawnPositions[0].position, SpawnPositions[0].rotation, CashHolder);
            CashObjects.Add(CashItem);

        }
        else if (totalPrice >= 10 && totalPrice < 20)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject CashItem = Instantiate(Cash, SpawnPositions[i].position, SpawnPositions[i].rotation, CashHolder);
                CashObjects.Add(CashItem);
            }
            Debug.Log("2 Cash Objects has Been Added");
        }
        else if (totalPrice >= 20 && totalPrice < 30)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject CashItem = Instantiate(Cash, SpawnPositions[i].position, SpawnPositions[i].rotation, CashHolder);
                CashObjects.Add(CashItem);
            }
            Debug.Log("3 Cash Objects has Been Added");
        }
        else if (totalPrice >= 30 && totalPrice < 40)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject CashItem = Instantiate(Cash, SpawnPositions[i].position, SpawnPositions[i].rotation, CashHolder);
                CashObjects.Add(CashItem);
            }
            Debug.Log("4 Cash Objects has Been Added");
        }
        else if (totalPrice >= 40 && totalPrice < 50)
        {
            for (int i = 0; i < SpawnPositions.Length; i++)
            {
                GameObject CashItem = Instantiate(Cash, SpawnPositions[i].position, SpawnPositions[i].rotation, CashHolder);
                CashObjects.Add(CashItem);
            }
            Debug.Log("5 Cash Objects has Been Added");
        }
        HasNotCreatedStack = false;
    }



    public void RemoveCashStack()
    {
        for(int i = 0; i < CashObjects.Count; i++)
        {
            Destroy(CashObjects[i]);
        }
        CashObjects.Clear();
        hasGrabbedCash = true;

    }

}
