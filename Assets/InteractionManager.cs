using UnityEngine;
using System.Collections;

public class InteractionManager : MonoBehaviour {

    // Steam VR
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }

    // Line Renderer
    private LineRenderer lineRenderer;

    void Start() {
        trackedObj = transform.parent.parent.parent.GetComponent<SteamVR_TrackedObject>();
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    } 

    void FixedUpdate() {
        //Debug.Log(controller.velocity);

        Vector3 startPoint = controller.transform.pos;
        //startPoint.y += .3f;

       // startPoint = transform.TransformPoint(startPoint);

        Vector3 controllerVelocity = new Vector3(controller.velocity.x, controller.velocity.y, controller.velocity.z);
        Vector3 endPoint = startPoint + controllerVelocity * 2.5f;

        Vector3[] points = new Vector3[2];
        
        points[0] = startPoint;
        points[1] = endPoint;

        lineRenderer.SetPositions(points);
        //Debug.DrawLine(startPoint, endPoint, Color.red); 

        // Debug.DrawLine( startPoint, endPoint, Color.red);
    }


    void OnTriggerStay(Collider col) {

        if (col.name != "Floor" && col.name != "Collider_Real") {

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

        Vector3 startPoint = transform.localPosition;
        startPoint.y += .3f;
        startPoint = transform.TransformPoint(startPoint);


        Vector3 controllerVelocity = new Vector3(controller.velocity.x, controller.velocity.y + .3f, controller.velocity.z);
        rigidbody.velocity = controllerVelocity * 2.5f;
        //rigidbody.angularVelocity = controller.angularVelocity * 3;




    }


    //void hitObject(Rigidbody rigidbody) {
    //    rigidbody.velocity = device.velocity * 5;
    //    rigidbody.angularVelocity = device.angularVelocity * 5;
    //}
}