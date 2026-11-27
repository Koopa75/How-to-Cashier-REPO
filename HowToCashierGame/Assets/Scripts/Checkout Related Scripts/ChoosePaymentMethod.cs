using UnityEngine;
using UnityEngine.Rendering;

public class ChoosePaymentMethod : MonoBehaviour
{
    private PaymentMethod currentPaymentMethod;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public PaymentMethod ChoosePaymentType()
    {
        return (Random.Range(0, 2) == 0)
            ? PaymentMethod.Card
            : PaymentMethod.Cash;
    }


}
