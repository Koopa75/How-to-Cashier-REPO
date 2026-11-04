using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomerBehavior : MonoBehaviour
{

    public static CustomerBehavior Instance;

    public GameObject Customer;

    private Animator CustomerAnimations;
    

    public Vector3 targetPosition;
    public float moveSpeed = 1f;

    private bool moving;
    void Start()
    {
        CustomerAnimations = GetComponent<Animator>();
        targetPosition = transform.position;
    }

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        moving = Vector3.Distance(transform.position, targetPosition) > 0.01f;

        CustomerAnimations.SetBool("IsMoving", moving);

        if (moving)
        {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        Debug.Log("Is the Customer Walking: " + moving);
    }

    public void NewTargetPosition(Vector3 newTargetPos)
    {
        targetPosition = newTargetPos;
    }



}
