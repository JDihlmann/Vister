using UnityEngine;
using System.Collections;

public class BallReseter : MonoBehaviour {


    public Rigidbody rgbd;
    public Vector3 startPosition; 

    void Start() {
        rgbd = GetComponent<Rigidbody>();
    }

    void OnEnable() {
        RightControllerEventManger.onTouchpadPress += SetVelocity;
    }

    void OnDisable() {
        RightControllerEventManger.onTouchpadPress -= SetVelocity;
    }

    void SetVelocity() {
        rgbd.velocity = new Vector3(0, 0, 0);
        rgbd.MovePosition(startPosition);
    }
    
}
