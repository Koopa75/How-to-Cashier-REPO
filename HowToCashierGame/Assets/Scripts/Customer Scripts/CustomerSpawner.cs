using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject[] CustomerPrefab;

    public Transform FirstInLinePosition;
    public Transform CustomerHolder;
    public int customerDistance = 1;
    public int MaxCustomers = 3;
    public int SpawnInterval = 5;

    private float SpawnCustomerTimer = 0;

    private List<GameObject> CurrentCustomers = new List<GameObject>();

    //private Queue<GameObject> CurrentCustomers = new Queue<GameObject>();

    public static CustomerSpawner Instance;
    //public CustomerBehavior customerBehavior;

    void Start()
    {
        Transform NewCustomerPos = FirstInLinePosition;
        Debug.Log(FirstInLinePosition.localPosition);
        for (int i = 0; i < 2; i++)
        {
            SpawnCustomer();
        }

    }


    private void Update()
    {
        SpawnCustomerTimer += Time.deltaTime;
        Debug.Log(SpawnCustomerTimer);
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
        GameObject Customer;
        if (CurrentCustomers.Count == 0)
        {
            Customer = Instantiate(CustomerPrefab[CustomerChoice], FirstInLinePosition.position, FirstInLinePosition.rotation);
        }
        else
        {
            Transform customerPosition = GetCustomerSpawnPos();
            Customer = Instantiate(CustomerPrefab[CustomerChoice], customerPosition.position, customerPosition.rotation);
        }
        Customer.transform.SetParent(CustomerHolder);
        CurrentCustomers.Add(Customer);
    }

    public void RemoveCustomer()
    {
        GameObject.Destroy(CurrentCustomers[0]);
        CurrentCustomers.RemoveAt(0);
        if (CurrentCustomers.Count == 0)
        {
            SpawnCustomer();
        }
        Debug.Log("Current Customers: " + CurrentCustomers.Count + "\nFirst Customer in Line: " + CurrentCustomers[0]);
        MoveUpCustomers();
    }


    public void MoveUpCustomers()
    {
        FirstInLinePosition.localPosition = Vector3.zero;
        if (CurrentCustomers.Count > 0)
        {
            for (int i = 0; i < CurrentCustomers.Count; i++)
            {
                if (i == 0)
                {
                    CurrentCustomers[i].transform.position = FirstInLinePosition.position;
                }
                else
                {
                    Vector3 NewCusPos = new Vector3(FirstInLinePosition.position.x - (1 * i), FirstInLinePosition.position.y, FirstInLinePosition.position.z);
                    CurrentCustomers[i].transform.position = NewCusPos;
                }

                Debug.Log("Customer " + i + "'s position: " + CurrentCustomers[i].transform.position);
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
        int value = UnityEngine.Random.Range(0, 10);
        if (value < 3)
        {
            SpawnCustomer();
        }
    }
}
