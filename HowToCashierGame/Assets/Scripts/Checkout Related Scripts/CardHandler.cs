using System.Collections;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static ChoosePaymentMethod;

public class CardHandler : MonoBehaviour
{
    public GameObject customerCard;
    public Transform customerCardPos;


    public UnityEngine.UI.Button grabCardButton;
    public UnityEngine.UI.Button swipCardButton;

    public CheckoutBag checkoutBag;
    public ChoosePaymentMethod choosePaymentMethod;
    public CustomerSpawner customerSpawner;

    private bool hasCardBeenGrabbed = false;
    private bool hasSwiped = false;

    public Transform StartSwipePosition;
    public Transform EndSwipePosition;
    public Transform InHandPosition;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 originalCardPos;

    public float timerPerSwipe = 0.5f;
    public float cardMoveSpeed = 0.01f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        customerCard.SetActive(false);
        grabCardButton.interactable = false;
        grabCardButton.gameObject.SetActive(false);
        swipCardButton.interactable = false;
        swipCardButton.gameObject.SetActive(false);

        startPosition = StartSwipePosition.localPosition;
        endPosition = EndSwipePosition.localPosition;

        originalCardPos = customerCardPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCanGrabCardStatus();
        if (hasCardBeenGrabbed)
        {
            grabCardButton.gameObject.SetActive(false);
            grabCardButton.interactable = false;
        }
    }


    public void CheckCanGrabCardStatus()
    {
        GameObject currentCustomer = customerSpawner.GetCurrentCustomer();

        if (currentCustomer == null)
            return;

        CustomerBehavior customerBehavior = currentCustomer.GetComponent<CustomerBehavior>();

        if (checkoutBag.GetCustomerReadyToPay() && customerBehavior.paymentMethod == PaymentMethod.Card)
        {
            grabCardButton.interactable = true;
            grabCardButton.gameObject.SetActive(true);
            customerCard.SetActive(true);
        }
    }

    public void ActivateCard()
    {

        Vector3 newCardPos = InHandPosition.position;
        customerCard.transform.position = newCardPos;


        hasCardBeenGrabbed = true;
        EnableCardSwipeButton();
    }

    public void DisableCard()
    {
        hasCardBeenGrabbed = false;
        customerCard.SetActive(false);
    }

    public void EnableCardSwipeButton()
    {
        swipCardButton.interactable = true;
        swipCardButton.gameObject.SetActive(true);
    }

    public void DisableCardSwipeButton()
    {
        swipCardButton.interactable = false;
        swipCardButton.gameObject.SetActive(false);
    }

    public void StartCardSwipe()
    {
        Vector3 newCardPos = StartSwipePosition.position;
        customerCard.transform.position = newCardPos;
        StartCoroutine(StartCardSwipeMove());


    }

    
    public IEnumerator StartCardSwipeMove()
    {
        while (Vector3.Distance(customerCardPos.localPosition, endPosition) > 0.01f)
        {
            customerCardPos.localPosition = Vector3.MoveTowards(
                customerCardPos.localPosition,
                endPosition,
                cardMoveSpeed * Time.deltaTime);

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        // Move back out
        while (Vector3.Distance(customerCardPos.localPosition, startPosition) > 0.01f)
        {
            customerCardPos.localPosition = Vector3.MoveTowards(
                customerCardPos.localPosition,
                startPosition,
                cardMoveSpeed * Time.deltaTime);

            yield return null;
        }
        yield return new WaitForSeconds(0.3f);
        Vector3 returnToOrigPosition = originalCardPos;
        customerCard.transform.position = returnToOrigPosition;
        hasSwiped = true;
        InteractionController.instance.UpdateCost();
        DisableCardSwipeButton();
    }


    public bool GetHasSwiped()
    {
        return hasSwiped;
    }
    
    public void SetHasSwiped(bool didSwipe)
    {
        hasSwiped = didSwipe;
    }
}
