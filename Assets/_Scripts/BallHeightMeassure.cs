using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallHeightMeassure : MonoBehaviour {


    private List<float> pos;

	// Use this for initialization
	void Start () {
        pos = new List<float>();
    }
    
    void FixedUpdate() {
        pos.Add(transform.position.y);
    }


    void OnCollisionEnter(Collision collided) {

        float max = 0;

        foreach (float item in pos) {
            if(item > max) {
                max = item;
            }
        }

        pos.Clear();
        Debug.Log("Max height: " + max);
    }
}
