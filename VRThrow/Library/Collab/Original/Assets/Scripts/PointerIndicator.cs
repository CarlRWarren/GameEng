using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using InputTracking = UnityEngine.XR.InputTracking;

public class PointerIndicator : MonoBehaviour
{
    [SerializeField] GameObject pointer;

    void Update()
    {
        Debug.Log("ENter");
        OVRInput.Controller activeController = OVRInput.GetActiveController();
        Debug.Log(activeController);

        Quaternion controllerRotation = OVRInput.GetLocalControllerRotation(activeController);
        Vector3 controllerPosition = OVRInput.GetLocalControllerPosition(activeController);

        Vector3 directionVector = controllerRotation.eulerAngles;
        Ray ray = new Ray(controllerPosition, directionVector);
        Physics.Raycast(ray, out RaycastHit hitInfo);
        pointer.transform.position = hitInfo.transform.position;
    }
}
