using UnityEngine;
using System.Collections;

public class BallReseter : MonoBehaviour {


    public Rigidbody rgbd;
    public Vector3 startPosition; 

    void Start() {
        rgbd = GetComponent<Rigidbody>();
    }

    void OnEnable() {
        RightControllerEventManger.onTouchpadPress += Reseter;
    }

    void OnDisable() {
        RightControllerEventManger.onTouchpadPress -= Reseter;
    }

    void Reseter() {
        rgbd.velocity = new Vector3(0, 0, 0);
        rgbd.position = startPosition;
        transform.GetComponent<Renderer>().material.color = Color.blue;
    }
    
}
