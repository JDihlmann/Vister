using UnityEngine;
using System.Collections;

public class MoveWithController : MonoBehaviour {


    //private SteamVR_TrackedObject trackedObj;
    //private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    public Transform controller;
    private Rigidbody rgbd;


    // Use this for initialization
    void Start () {
        rgbd = GetComponent<Rigidbody>();
        //trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void FixedUpdate() {
        if (controller == null) {
            //Debug.Log("Controller not reachable");
            return;
        }

        // Make rigidbody follow controller.


        Vector3 position = controller.position;
        //position.x += .3f;
        //position.z += -0.2f;
        rgbd.MovePosition(position);


        Quaternion rotation = controller.rotation * Quaternion.Euler(-90f, 0f, 0f);


        //Debug.Log(rotation);
        rgbd.MoveRotation(rotation);



        //transform.position = controller.position; 
 }
}
