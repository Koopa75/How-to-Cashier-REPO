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
    public int SpawnChance = 7;


    private float SpawnCustomerTimer = 0;

    private List<GameObject> CurrentCustomers = new List<GameObject>();

    //private Animator CustomerAnimations;

    public static CustomerSpawner Instance;

    void Start()
    {
        //CustomerAnimations = GetComponent<Animator>();
        Transform NewCustomerPos = FirstInLinePosition;
        Debug.Log(FirstInLinePosition.localPosition);
        for (int i = 0; i < 2; i++)
        {
            SpawnCustomer();
        }

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
        GameObject Customer = Instantiate(CustomerPrefab[CustomerChoice], GetLinePosition(CurrentCustomers.Count), Quaternion.identity);
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
        int counter = 0;
        if (CurrentCustomers.Count > 0)
        {
            for (int i = 0; i < CurrentCustomers.Count; i++)
            {
                Vector3 targetPosition = new Vector3(FirstInLinePosition.position.x - (1 * i), FirstInLinePosition.position.y, FirstInLinePosition.position.z);
                CurrentCustomers[i].GetComponent<CustomerBehavior>().NewTargetPosition(targetPosition);

                Debug.Log("Customer " + i + "'s position: " + CurrentCustomers[i].transform.position);
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
        int value = UnityEngine.Random.Range(0, SpawnChance);
        if (value < 3)
        {
            SpawnCustomer();
        }
    }


    public Vector3 GetLinePosition(int index)
    {
        return FirstInLinePosition.position + Vector3.left * index * customerDistance;
    }

}
