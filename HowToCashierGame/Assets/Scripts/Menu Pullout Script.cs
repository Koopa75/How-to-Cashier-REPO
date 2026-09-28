using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class MenuPulloutScript : MonoBehaviour
{

    public GameObject barButton;
    public GameObject inGameMenuPanel;

    private Vector3 currentPosition;
    private float movePanelMax = 500f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPosition = inGameMenuPanel.transform.position;
        inGameMenuPanel.SetActive(false);
        barButton.SetActive(true);
    }

    void Update()
    {
        
    }



    public void OpenGameMenuPanel()
    {
        inGameMenuPanel.SetActive(true);

        Vector3 startPos = inGameMenuPanel.transform.position;
        Vector3 endPos = new Vector3(
            startPos.x - movePanelMax,
            startPos.y,
            startPos.z
        );

        StartCoroutine(SlidePanelIn(inGameMenuPanel, endPos, 0.4f));

        barButton.SetActive(false);
    }

    public void CloseGameMenuPanel()
    {

        Vector3 startPos = inGameMenuPanel.transform.position;
        Vector3 endPos = new Vector3(
            startPos.x + movePanelMax,
            startPos.y,
            startPos.z
        );

        StartCoroutine(SlidePanelOut(inGameMenuPanel, endPos, 0.4f));

        barButton.SetActive(true);
    }


    public IEnumerator SlidePanelIn(GameObject menuPanel, Vector3 targetPos, float duration)
    {
        Vector3 startPos = menuPanel.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float time = elapsed / duration;

            menuPanel.transform.position = Vector3.Lerp(startPos, targetPos, time);

            yield return null;
        }

        menuPanel.transform.position = targetPos;
    }

    public IEnumerator SlidePanelOut(GameObject menuPanel, Vector3 targetPos, float duration)
    {
        Vector3 startPos = menuPanel.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float time = elapsed / duration;

            menuPanel.transform.position = Vector3.Lerp(startPos, targetPos, time);

            yield return null;
        }

        menuPanel.transform.position = targetPos;
        inGameMenuPanel.SetActive(false);
    }
}
