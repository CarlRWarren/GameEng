using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pointer : MonoBehaviour
{
    [SerializeField] GrabObject graber;
    public Vector3 grabPoint;

    public float distance = 10.0f;
    float savedY = 0.0f;
    Vector3 endPosition;
    public LineRenderer lineRenderer = null;
    public LayerMask everythingMask = 0;
    public LayerMask interactabeMask = 0;
    private Transform currentOrigin = null;
    public UnityAction<Vector3, GameObject> OnPointerUpdate = null;
    private GameObject currentObject = null;


    private void Awake()
    {
        PlayerEvents.OnControllerSource += UpdateOrigin;
        PlayerEvents.OnTouchpadDown += ProcessTouchpadDown;
    }
    private void Start()
    {
        SetLineColor();
    }
    private void OnDestroy()
    {
        PlayerEvents.OnControllerSource -= UpdateOrigin;
        PlayerEvents.OnTouchpadDown -= ProcessTouchpadDown;
    }
    private void Update()
    {
        Vector3 hitPoint = UpdateLine();
        currentObject = UpdatePointerStatus();

        OnPointerUpdate?.Invoke(hitPoint, currentObject);
    }

    private Vector3 UpdateLine()
    {
        //create ray
        RaycastHit hit = CreateRaycast(interactabeMask);        
        //check hit
        if (graber.isGrabing)
        {
            Vector2 touchLocation = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
            if (touchLocation.y != 0.0f) savedY = touchLocation.y;
            grabPoint = (currentOrigin.position + (currentOrigin.forward * distance * ((savedY + 1) / 2)));
            endPosition = grabPoint;
            graber.throwForce = ((currentOrigin.forward * distance * 5 * ((savedY + 1) / 2)));
        }
        else
        {
            endPosition = (currentOrigin.position + (currentOrigin.forward * distance));
            if (hit.collider != null) endPosition = hit.point;
        }
        //set position
        lineRenderer.SetPosition(0, currentOrigin.position);
        lineRenderer.SetPosition(1, endPosition);
        return endPosition;
    }
    
    private void UpdateOrigin(OVRInput.Controller controller, GameObject controllerObj)
    {
        //set origin of pointer
        currentOrigin = controllerObj.transform;
        //is laser visible
        if (controller == OVRInput.Controller.Touchpad) lineRenderer.enabled = false;
        else lineRenderer.enabled = true;
    }

    private GameObject UpdatePointerStatus()
    {
        //create ray
        RaycastHit hit = CreateRaycast(interactabeMask);
        //check hit
        if (hit.collider) return hit.collider.gameObject;
        //return
        return null;
    }

    private RaycastHit CreateRaycast(int layer)
    {
        RaycastHit hit;
        Ray ray = new Ray(currentOrigin.position, currentOrigin.forward);
        Physics.Raycast(ray, out hit, distance, layer);
        return hit;
    }

    private void SetLineColor()
    {
        if (!lineRenderer) return;
        Color endColor = Color.white;
        endColor.a = 0.0f;
        lineRenderer.endColor = endColor;
    }

    private void ProcessTouchpadDown()
    {
        if (!currentObject) return;

        Interactable interactable = currentObject.GetComponent<Interactable>();
        interactable.Pressed();
    }
}
