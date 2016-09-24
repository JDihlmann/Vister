using UnityEngine;
using System.Collections;

public class LeftControllerEventManger : MonoBehaviour {

    // Event Handler
    public delegate void rightButtonUp();
    public static event rightButtonUp onLeftButtonUp;

    public delegate void leftButtonDown(Vector3 position);
    public static event leftButtonDown onLeftButtonDown;


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

        
        if (triggerButtonUp && onLeftButtonUp != null) {
            Debug.Log("Left Up");
            onLeftButtonUp();
        }

        if (triggerButtonDown && onLeftButtonDown != null) {
            Debug.Log("Left Down");
            onLeftButtonDown(transform.position); 
        }

        if (triggerButtonPressed && onLeftButtonDown != null) {
            onLeftButtonDown(transform.position);
        }
    }

}



