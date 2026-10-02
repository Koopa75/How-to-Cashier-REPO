using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public PauseSystem IsGamePaused;
    public SettingsScript mouseSensitivity;
    public MenuPulloutScript isMenuOpen;
    public static PlayerController instance
    {
        get
        {
            return _instance;
        }
    }

    private static PlayerController _instance;

    public void Start()
    {
        _instance = this;
        clickAction = InputSystem.actions.FindAction("Click");
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
        holding = null;
        Physics.IgnoreLayerCollision(7, 8, true);
        Physics.IgnoreLayerCollision(8, 8, true);
        Physics.IgnoreLayerCollision(7, 9, true);
        Physics.IgnoreLayerCollision(8, 9, true);
        Physics.IgnoreLayerCollision(7, 10, true);
        Physics.IgnoreLayerCollision(8, 10, true);
        TransitionCamera(-1);
        freeRoamCamRot = 0f;
    }

    public void FixedUpdate()
    {
        if (IsGamePaused.GetPauseState() == false)
        {
            if (currentCam == -1)
            {
                Vector2 moveDesire = moveAction.ReadValue<Vector2>();
                moveDesire *= 3f;
                moveDesire *= Time.fixedDeltaTime;
                Vector2 moveVector = new Vector2(moveDesire.x * Mathf.Cos(Mathf.Deg2Rad* freeRoamCamRot) + moveDesire.y * Mathf.Sin(Mathf.Deg2Rad * freeRoamCamRot), -moveDesire.x * Mathf.Sin(Mathf.Deg2Rad * freeRoamCamRot) + moveDesire.y * Mathf.Cos(Mathf.Deg2Rad * freeRoamCamRot));
                transform.position += new Vector3(moveVector.x, 0f, moveVector.y);
                Rigidbody rigid = gameObject.GetComponent<Rigidbody>();
                rigid.linearVelocity = new Vector3(0f, rigid.linearVelocity.y, 0f);
                if (transform.position.y <= 0f)
                {
                    transform.position = new Vector3(-1.2f, 0.9f, -3.6f);
                }
            }
        }
    }

    public void Update()
    {
        if (IsGamePaused.GetPauseState() == false)
        {
            if (currentCam == -1)
            {
                Vector2 lookDesire = lookAction.ReadValue<Vector2>() * mouseSensitivity.GetMouseSensitivity();
                lookDesire /= 6f;
                lookDesire *= 120f;
                lookDesire *= Time.deltaTime;
                freeRoamCamRot = (freeRoamCamRot + lookDesire.x) % 360f;
                freeRoamCamTilt = Mathf.Clamp(freeRoamCamTilt + lookDesire.y, -90f, 90f);
                mainCamera.transform.rotation = Quaternion.AngleAxis(freeRoamCamRot, Vector3.up) * Quaternion.AngleAxis(freeRoamCamTilt, Vector3.left);
                if (transform.position.x >= -4.3f && transform.position.x <= -2.2f && transform.position.z >= -1.1f && transform.position.z <= 0.3f)
                {
                    cameraTransitionButtons[0].gameObject.SetActive(true);
                }
                else
                {
                    cameraTransitionButtons[0].TryMouseExit();
                    cameraTransitionButtons[0].gameObject.SetActive(false);
                }
                Vector2 mousePos = Mouse.current.position.ReadValue();
                Ray clickRay = mainCamera.ScreenPointToRay(mousePos);
                RaycastHit hit;
                CameraTransitionButton button = null;
                if (Physics.Raycast(clickRay, out hit, 100f, LayerMask.GetMask("UI")))
                {
                    GameObject obj = hit.collider.gameObject;
                    button = obj.GetComponent<CameraTransitionButton>();
                }
                for (var i = 0; i < cameraTransitionButtons.Length; i++)
                {
                    if (cameraTransitionButtons[i] == button)
                    {
                        cameraTransitionButtons[i].TryMouseEnter();
                    }
                    else
                    {
                        cameraTransitionButtons[i].TryMouseExit();
                    }
                }
                if (clickAction.WasPressedThisFrame())
                {
                    if (button != null)
                    {
                        button.Click();
                    }
                }
            }
            else
            {
                Vector2 mousePos = Mouse.current.position.ReadValue();
                Ray clickRay = mainCamera.ScreenPointToRay(mousePos);
                if (holding != null)
                {
                    if (Physics.Raycast(clickRay, out RaycastHit hit, 100f, LayerMask.GetMask("Click Helper")))
                    {
                        holding.UpdateDraggingPosition(hit.point + holdingObjectOffset);
                    }
                    if (!clickAction.IsPressed() || clickAction.WasReleasedThisFrame())
                    {
                        holding.Drop();
                        holding = null;
                    }
                }
                else
                {
                    RaycastHit hit;
                    CameraTransitionButton button = null;
                    if (Physics.Raycast(clickRay, out hit, 100f, LayerMask.GetMask("UI", "Items", "GhostItems")))
                    {
                        GameObject obj = hit.collider.gameObject;
                        button = obj.GetComponent<CameraTransitionButton>();
                    }
                    for (var i = 0; i < cameraTransitionButtons.Length; i++)
                    {
                        if (cameraTransitionButtons[i] == button)
                        {
                            cameraTransitionButtons[i].TryMouseEnter();
                        }
                        else
                        {
                            cameraTransitionButtons[i].TryMouseExit();
                        }
                    }
                }
                if (clickAction.WasPressedThisFrame())
                {
                    RaycastHit hit;
                    if (Physics.Raycast(clickRay, out hit, 5f, LayerMask.GetMask("Items", "GhostItems")))
                    {
                        GameObject obj = hit.collider.gameObject;
                        PhysicsItem item = obj.GetComponent<PhysicsItem>();
                        if (item != null)
                        {
                            holdingPlaneOffset = hit.point.y;
                            helperPlane.transform.position = new Vector3(helperPlane.transform.position.x, holdingPlaneOffset, helperPlane.transform.position.y);
                            /*
                            if (Physics.Raycast(clickRay, out RaycastHit hit2, 100f, LayerMask.GetMask("Click Helper")))
                            {
                                holdingObjectOffset = obj.transform.position - hit2.point;
                            }*/
                            holdingObjectOffset = obj.transform.position - hit.point;
                            holding = item;
                            holding.Pickup();
                        }
                    }
                    else if (Physics.Raycast(clickRay, out hit, 100f, LayerMask.GetMask("UI")))
                    {
                        GameObject obj = hit.collider.gameObject;
                        CameraTransitionButton button = obj.GetComponent<CameraTransitionButton>();
                        if (button != null)
                        {
                            button.Click();
                        }
                    }
                }
            }
        }
    }

    public void TransitionCamera(int cam)
    {
        if (cam == currentCam) return;
        if (cam == -1)
        {
            freeRoamCamRot = 180f;
            freeRoamCamTilt = 0f;
            
            mainCamera.transform.SetParent(transform);
            mainCamera.transform.localPosition = Vector3.up * 0.55f;
            mainCamera.transform.localRotation = Quaternion.identity;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Collider>().enabled = true;
            for (var i = 0; i < cameraTransitionButtons.Length; i++)
            {
                if (i == 0)
                {
                    cameraTransitionButtons[i].gameObject.SetActive(true);
                }
                else
                {
                    cameraTransitionButtons[i].TryMouseExit();
                    cameraTransitionButtons[i].gameObject.SetActive(false);
                }
            }
            currentCam = -1;
            //InteractionController.instance.UpdateCost();
            return;
        }
        transform.position = new Vector3(-3.2f, 0.9f, -0.6f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Transform controlPoint = cameraControlPoints[cam];
        mainCamera.transform.SetParent(controlPoint);
        mainCamera.transform.localPosition = Vector3.zero;
        mainCamera.transform.localRotation = Quaternion.identity;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Collider>().enabled = false;
        for (var i = 0; i < cameraTransitionButtons.Length; i++)
        {
            if (i == cam)
            {
                cameraTransitionButtons[i].TryMouseExit();
                cameraTransitionButtons[i].gameObject.SetActive(false);
            }
            else
            {
                cameraTransitionButtons[i].gameObject.SetActive(true);
            }
        }
        currentCam = cam;
        InteractionController.instance.UpdateCost();
    }



    public void ChangeCursorState()
    {
        if (currentCam == -1)
        {
            if (isMenuOpen.GetMenuOpenCloseState())
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Debug.Log("Cam should be Visable");
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public Camera mainCamera;
    public GameObject helperPlane;
    public Transform[] cameraControlPoints;
    public CameraTransitionButton[] cameraTransitionButtons;

    [HideInInspector]
    public int currentCam = -2;
    private PhysicsItem holding;
    private Vector3 holdingObjectOffset;
    private float holdingPlaneOffset;

    private InputAction clickAction;
    private InputAction moveAction;
    private InputAction lookAction;
    private float freeRoamCamRot;
    private float freeRoamCamTilt;
}
