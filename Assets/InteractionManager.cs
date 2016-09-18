using UnityEngine;
using System.Collections;

public class InteractionManager : MonoBehaviour {

    // Steam VR
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }


    void Awake() {
        trackedObj = transform.parent.parent.parent.GetComponent<SteamVR_TrackedObject>();
    } 

    void FixedUpdate() {
        Debug.Log(controller.velocity);

        Vector3 startPoint = trackedObj.transform.position;
        Vector3 endPoint = startPoint + controller.velocity;

        Debug.DrawLine(startPoint, endPoint, Color.red); 

       // Debug.DrawLine( startPoint, endPoint, Color.red);
    }


    void OnTriggerStay(Collider col) {
        //if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger)) {
        //    col.attachedRigidbody.isKinematic = true;
        //    col.gameObject.transform.SetParent(this.gameObject.transform);
        //}

        //if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger)) {
        //    col.gameObject.transform.SetParent(null);
        //    col.attachedRigidbody.isKinematic = false;

        //    tossObject(col.attachedRigidbody);
        //}

        //if (device.GetPress(SteamVR_Controller.ButtonMask.Grip)) {
        //    hitObject(col.attachedRigidbody);
        //}
        

        if (col.name != "Floor") {

            //(Debug.Log("Collider: " + col.name);
            col.gameObject.transform.SetParent(null);
            col.attachedRigidbody.isKinematic = false;

           // Debug.Log(rgbd.velocity);

            tossObject(col.attachedRigidbody);
            

        }
    }

    void tossObject(Rigidbody rigidbody) {
        if(controller ==  null) {
            Debug.Log("Controller isnt initialized");
            return;
        }

        rigidbody.velocity = controller.velocity * 3 ;
        //rigidbody.angularVelocity = controller.angularVelocity * 3;




    }


    //void hitObject(Rigidbody rigidbody) {
    //    rigidbody.velocity = device.velocity * 5;
    //    rigidbody.angularVelocity = device.angularVelocity * 5;
    //}
}