using UnityEngine;
using System.Collections;

public class Magnet : MonoBehaviour {

    private Rigidbody rgbd;
    public float velocityMultiplier;
    
	void Start () {
        rgbd = transform.GetComponent<Rigidbody>();
	}

    void OnEnable() {
        LeftControllerEventManger.onLeftButtonDown += SetVelocity;

    }

    void OnDisable() {
        LeftControllerEventManger.onLeftButtonDown += SetVelocity;
    }

	
	void SetVelocity (Vector3 position) {
       
        Vector3 velocity = position - transform.position;

       
        
        if(velocity.magnitude >= 1) {
            float multiplier = Mathf.Exp(velocity.magnitude);
            rgbd.velocity = velocity * multiplier;
        } 
        
        
        
    }
}
