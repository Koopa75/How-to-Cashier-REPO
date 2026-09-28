using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
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
        holding = null;
        Physics.IgnoreLayerCollision(7, 8, true);
        Physics.IgnoreLayerCollision(8, 8, true);
        TransitionCamera(0);
    }

    public void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray clickRay = mainCamera.ScreenPointToRay(mousePos);
        if (holding != null)
        {
            if (Physics.Raycast(clickRay, out RaycastHit hit, 100f, LayerMask.GetMask("Click Helper")))
            {
                holding.transform.position = hit.point + holdingObjectOffset;
            }
            if (!clickAction.IsPressed() || clickAction.WasReleasedThisFrame())
            {
                holding.gameObject.layer = 7;
                holding.gameObject.GetComponent<Rigidbody>().isKinematic = false;
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
                    holding.gameObject.layer = 8;
                    holding.gameObject.GetComponent<Rigidbody>().isKinematic = true;
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

    public void TransitionCamera(int cam)
    {
        Transform controlPoint = cameraControlPoints[cam];
        mainCamera.transform.position = controlPoint.position;
        mainCamera.transform.rotation = controlPoint.rotation;
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
    }

    public Camera mainCamera;
    public GameObject helperPlane;
    public Transform[] cameraControlPoints;
    public CameraTransitionButton[] cameraTransitionButtons;

    private PhysicsItem holding;
    private Vector3 holdingObjectOffset;
    private float holdingPlaneOffset;

    private InputAction clickAction;
}
