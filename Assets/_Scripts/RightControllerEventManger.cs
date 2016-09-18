using UnityEngine;
using System.Collections;

public class RightControllerEventManger : MonoBehaviour {

    // Event Handler
    public delegate void touchpadPress();
    public static event touchpadPress onTouchpadPress;

    public delegate void rightButtonUp();
    public static event rightButtonUp onRightButtonUp;

    public delegate void rightButtonDown(float triggerX);
    public static event rightButtonDown onRightButtonDown;


    // VR Handler
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    private Valve.VR.EVRButtonId touchpadButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;


    public bool triggerButtonUp = false;
    public bool triggerButtonDown = false;
    public bool triggerButtonPressed = false;
    public bool touchpadButtonPressed = false;

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }


    private Transform rotortransform;


    void Start() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void FixedUpdate() {
        if(controller == null) {
            Debug.Log("Controller not reachable");
            return;
        }
        

        triggerButtonUp = controller.GetPressUp(triggerButton);
        triggerButtonDown = controller.GetPressDown(triggerButton);
        triggerButtonPressed = controller.GetPress(triggerButton);
        touchpadButtonPressed = controller.GetPressDown(touchpadButton);




        if (triggerButtonUp && onRightButtonUp != null) {
            Debug.Log("Up");
            onRightButtonUp();
        }

        if (triggerButtonDown && onRightButtonDown != null) {
            Debug.Log("Down");
        }

        if (touchpadButtonPressed && onTouchpadPress != null) {
            Debug.Log("Touchpad Pressed");
            onTouchpadPress();
        }


        float triggerX = controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis1).x;
        if (onRightButtonDown != null) {
            onRightButtonDown(triggerX);
        }
    }

}



