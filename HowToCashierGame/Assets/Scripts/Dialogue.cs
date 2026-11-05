using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject MrCentImage;
    public GameObject registerScreen;
    public GameObject scanningScreen;
    public GameObject baggingScreen;
    public GameObject checkoutScreen;
    public GameObject settingsScreen;
    public string[] lines;
    public float textSpeed;
    public float textCount = 0;

    private int index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textCount++;
            textComponent.text = string.Empty;
            if(textCount == 7)
            {
                registerScreen.SetActive(true);
            }
            else if (textCount == 8)
            {
                registerScreen.SetActive(false);
                scanningScreen.SetActive(true);
            }
            else if(textCount == 9)
            {
                scanningScreen.SetActive(false);
                baggingScreen.SetActive(true);
            }
            else if(textCount == 10)
            {
                baggingScreen.SetActive(false);
                checkoutScreen.SetActive(true);
            }
            else if(textCount == 11)
            {
                checkoutScreen.SetActive(false);
                settingsScreen.SetActive(true);
            }
            else if(textCount == 12)
            {
                settingsScreen.SetActive(false);
            }
                StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene("Scene 1");
        }
    }
}
