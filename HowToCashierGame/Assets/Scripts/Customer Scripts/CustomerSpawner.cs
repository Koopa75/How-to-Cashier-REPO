using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class CustomerSpawner : MonoBehaviour
{
    public DayScript dayScript;
    public ChoosePaymentMethod choosePaymentMethod;

    public GameObject[] CustomerPrefab;

    public Transform FirstInLinePosition;
    public Transform customerLeaveToPos;
    public Transform customerSpawnPos;
    public Transform CustomerHolder;

    public float customerLeaveSpeed = 4f;
    public int customerDistance = 1;
    public int MaxCustomers = 3;
    public int SpawnInterval = 5;
    public int SpawnChance = 7;
    public float moveInSpeed = 2.5f;



    private float SpawnCustomerTimer = 0;

    private List<GameObject> CurrentCustomers = new List<GameObject>();

    //private Animator CustomerAnimations;

    public static CustomerSpawner Instance;

    void Start()
    {
        Transform NewCustomerPos = FirstInLinePosition;
        Debug.Log(FirstInLinePosition.localPosition);
        SpawnCustomer();

    }

    private void Awake()
    {
        Instance = this;
    }


    private void Update()
    {
        SpawnCustomerTimer += Time.deltaTime;
        if (CurrentCustomers.Count < MaxCustomers)
        {
            if (SpawnCustomerTimer >= SpawnInterval)
            {
                RandCusSpawnChance();
                SpawnCustomerTimer = 0;
            }
        }
    }

    private void SpawnCustomer()
    {
        int CustomerChoice = UnityEngine.Random.Range(0, CustomerPrefab.Length);
        GameObject Customer = Instantiate(CustomerPrefab[CustomerChoice], customerSpawnPos.position, Quaternion.identity);
        Customer.transform.SetParent(CustomerHolder);

        CustomerBehavior customerBehavior = Customer.GetComponent<CustomerBehavior>();
        customerBehavior.paymentMethod = choosePaymentMethod.ChoosePaymentType();

        Vector3 MoveToPos = GetLinePosition(CurrentCustomers.Count);
        customerBehavior.JoinLine(MoveToPos, moveInSpeed);

        Debug.Log(Customer.name + " will pay with " + customerBehavior.paymentMethod);

        CurrentCustomers.Add(Customer);

    }
   

    public void RemoveCustomer()
    {
        GameObject leavingCustomer = CurrentCustomers[0];

        CurrentCustomers.RemoveAt(0);

        leavingCustomer.GetComponent<CustomerBehavior>()
            .LeaveStore(customerLeaveToPos.position, customerLeaveSpeed);

        if (CurrentCustomers.Count == 0)
        {
            SpawnCustomer();
        }

        MoveUpCustomers();
    }




    public void MoveUpCustomers()
    {

        int counter = 0;
        if (CurrentCustomers.Count > 0)
        {
            for (int i = 0; i < CurrentCustomers.Count; i++)
            {
                Vector3 targetPosition = new Vector3(FirstInLinePosition.position.x - (1 * i), FirstInLinePosition.position.y, FirstInLinePosition.position.z);
                CurrentCustomers[i].GetComponent<CustomerBehavior>().NewTargetPosition(targetPosition);
                counter++;
            }

        }
    }

    public Transform GetCustomerSpawnPos()
    {
        FirstInLinePosition.localPosition = Vector3.zero;
        Transform NewCustomerPos = FirstInLinePosition;
        float positionDistortion = 0;
        for (int i = 0; i < CurrentCustomers.Count; i++)
        {
            positionDistortion++;
        }
        Vector3 updatedCusPos = new Vector3(NewCustomerPos.position.x - positionDistortion, NewCustomerPos.position.y, NewCustomerPos.position.z);
        NewCustomerPos.position = updatedCusPos;
        return NewCustomerPos;
    }

    public void RandCusSpawnChance()
    {
        if (!dayScript.GetIsDayOver())
        {
            int value = UnityEngine.Random.Range(0, SpawnChance);
            if (value < 3)
            {
                SpawnCustomer();
            }
        }
    }


    public Vector3 GetLinePosition(int index)
    {
        return FirstInLinePosition.position + Vector3.left * index * customerDistance;
    }


    public int GetCustomerListSize()
    {
        return CurrentCustomers.Count;
    }

    public GameObject GetCurrentCustomer()
    {
        if (CurrentCustomers.Count > 0)
        {
            return CurrentCustomers[0];
        }

        return null;
    }

    public PaymentMethod GetCurrentCustomerPaymentMethod()
    {
        if (CurrentCustomers.Count == 0)
        {
            return PaymentMethod.Card;
        }

        return CurrentCustomers[0].GetComponent<CustomerBehavior>().paymentMethod;
    }


}
