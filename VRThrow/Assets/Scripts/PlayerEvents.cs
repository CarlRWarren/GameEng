using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEvents : MonoBehaviour
{
    [SerializeField]

    #region Events
    public static UnityAction OnTouchpadUp = null;
    public static UnityAction OnTouchpadDown = null;
    public static UnityAction<OVRInput.Controller, GameObject> OnControllerSource = null;
    #endregion
    #region Anchors
    public GameObject leftAnchor;
    public GameObject rightAnchor;
    public GameObject headAnchor;
    #endregion
    #region Input
    private Dictionary<OVRInput.Controller, GameObject> controllerSets = null;
    private OVRInput.Controller inputSource = OVRInput.Controller.None;
    private OVRInput.Controller controller = OVRInput.Controller.None;
    private bool inputActive = true;
    #endregion

    private void Awake()
    {
        OVRManager.HMDMounted += PlayerFound;
        OVRManager.HMDUnmounted += PlayerLost;
        controllerSets = CreateControllerSets();
    }
    private void OnDestroy()
    {
        OVRManager.HMDMounted -= PlayerFound;
        OVRManager.HMDUnmounted -= PlayerLost;
    }

    void Update()
    {
        //Check for active input
        if (!inputActive) return;
        //Checking if controller exists
        CheckForController();
        //Check for input source
        CheckInputSource();
        //Check for actual input
        Input();
    }

    private void CheckForController()
    {
        OVRInput.Controller controllerCheck = controller;
        //right
        if (OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote))
            controllerCheck = OVRInput.Controller.RTrackedRemote;
        //left
        if (OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote))
            controllerCheck = OVRInput.Controller.LTrackedRemote;

        //if none headset
        if (!OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote)&&
            !OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote))
            controllerCheck = OVRInput.Controller.Touchpad;

        //update
        controller = UpdateSource(controllerCheck, controller);
    }

    private void CheckInputSource()
    {
        //update
        inputSource = UpdateSource(OVRInput.GetActiveController(), inputSource);
    }

    private void Input()
    {
        //touchpad down
        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
        {
            OnTouchpadDown?.Invoke();
        }
        //touchpad up
        if (OVRInput.GetUp(OVRInput.Button.PrimaryTouchpad))
        {
            OnTouchpadUp?.Invoke();
        }
    }

    private OVRInput.Controller UpdateSource(OVRInput.Controller check, OVRInput.Controller prev)
    {
        //check if values are same return
        if (check == prev) return prev;
        //get controller obj
        GameObject controllerObj = null;
        controllerSets.TryGetValue(check, out controllerObj);
        //if no controller obj set to head
        if(controllerObj==null)  controllerObj = headAnchor;
        //send out event
        OnControllerSource?.Invoke(check, controllerObj);
        //return new val
        return check;
    }

    private void PlayerFound()
    {
        inputActive = true;
    }
    private void PlayerLost()
    {
        inputActive = false;
    }

    private Dictionary<OVRInput.Controller, GameObject> CreateControllerSets()
    {
        Dictionary<OVRInput.Controller, GameObject> newSets = new Dictionary<OVRInput.Controller, GameObject>()
        {
            {OVRInput.Controller.LTrackedRemote,leftAnchor },
            { OVRInput.Controller.RTrackedRemote,rightAnchor },
            { OVRInput.Controller.Touchpad,headAnchor }
        };
        return newSets;
    }
}
