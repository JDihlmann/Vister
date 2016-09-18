using UnityEngine;
using System.Collections;

public class TennisBladeRotate : MonoBehaviour {

    private bool rotate = false;
    private float powerSpeed = 0;
    private float currentSpeed = 0;
    public float speedMultiplier = 4.5f;



	// Use this for initialization
	void OnEnable() {
        RightControllerEventManger.onRightButtonUp += DisableRotation;
        RightControllerEventManger.onRightButtonDown += EnableRotation;
    }

    void OnDisable() {
        RightControllerEventManger.onRightButtonUp -= DisableRotation;
        RightControllerEventManger.onRightButtonDown -= EnableRotation;
    }

    void EnableRotation(float triggerX) {
        rotate = true;
        powerSpeed = triggerX * speedMultiplier; 
    }

    void DisableRotation() {
        rotate = false; 
    }

	void FixedUpdate () {
        if (rotate) {
            RotateWithSpeed();
        } else {
            StopRotation();
        }
    }

    void RotateWithSpeed() {
        if (currentSpeed < powerSpeed) {
            currentSpeed = powerSpeed;
        } else {
            currentSpeed -= 0.01f;
        }

        transform.Rotate(currentSpeed, 0, 0);
    }

    void StopRotation() {
        if (currentSpeed > 0) {
            currentSpeed -= 0.01f;
            transform.Rotate(currentSpeed, 0, 0);
        }
    }
}
