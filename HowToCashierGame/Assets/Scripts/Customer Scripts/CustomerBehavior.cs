using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine;
using static ChoosePaymentMethod;

public class CustomerBehavior : MonoBehaviour
{
    private Animator CustomerAnimations;

    public PaymentMethod paymentMethod;

    public Vector3 targetPosition;
    public float moveSpeed = 1f;

    private bool moving;
    private bool isLeaving = false;
    private bool isJoiningLine = false;

    private void OnAwake()
    {
        CustomerAnimations = GetComponent<Animator>();
    }
    
    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if (!isLeaving && !isJoiningLine)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime);

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
        }
    }

    public void NewTargetPosition(Vector3 newTargetPos)
    {
        if (!isLeaving)
        {
            targetPosition = newTargetPos;
        }
    }

    public void LeaveStore(Vector3 leavePosition, float leaveSpeed)
    {
        if (!isLeaving)
        {
            StartCoroutine(LeaveStoreRoutine(leavePosition, leaveSpeed));
        }
    }

    private IEnumerator LeaveStoreRoutine(Vector3 leavePosition, float leaveSpeed)
    {
        isLeaving = true;

        CustomerAnimations.SetBool("IsMoving", true);

        transform.rotation = Quaternion.Euler(0f, 90f, 0f);

        while (Vector3.Distance(transform.position, leavePosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                leavePosition,
                leaveSpeed * Time.deltaTime);

            yield return null;
        }

        Destroy(gameObject);
    }

    public void JoinLine(Vector3 moveToPos, float joinSpeed)
    {
        transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        StartCoroutine(MoveNewCustomerToPos(moveToPos, joinSpeed));
    }
    private IEnumerator MoveNewCustomerToPos(Vector3 moveToPos, float joinSpeed)
    {
        isJoiningLine = true;

        if (CustomerAnimations == null)
        {
            CustomerAnimations = GetComponent<Animator>();
        }
        CustomerAnimations.SetBool("IsMoving", true);

        while (Vector3.Distance(transform.position, moveToPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveToPos, joinSpeed * Time.deltaTime);
            yield return null;
        }

        CustomerAnimations.SetBool("IsMoving", false);

        targetPosition = moveToPos;

        isJoiningLine = false;
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

}
