using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    public TutorialText dialogue;

    public enum TutorialStage
    {
        WalkToRegister,
        ScanItem,
        CheckoutCustomer,
        Complete
    }

    public TutorialStage currentStage;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        dialogue.StartDialogue(new string[]
        {
            "Welcome to the store!",
            "Walk over to the register."
        });

        currentStage = TutorialStage.WalkToRegister;
    }

    public void ReachedRegister()
    {
        if (currentStage != TutorialStage.WalkToRegister)
            return;

        dialogue.StartDialogue(new string[]
        {
            "This is the register.",
            "Press E to scan an item."
        });

        currentStage = TutorialStage.ScanItem;
    }

    public void ScannedItem()
    {
        if (currentStage != TutorialStage.ScanItem)
            return;

        dialogue.StartDialogue(new string[]
        {
            "Great!",
            "Now ring up the customer."
        });

        currentStage = TutorialStage.CheckoutCustomer;
    }

    public void CustomerCheckedOut()
    {
        if (currentStage != TutorialStage.CheckoutCustomer)
            return;

        dialogue.StartDialogue(new string[]
        {
            "Excellent!",
            "You're ready to run the store."
        });

        currentStage = TutorialStage.Complete;
    }
}

