using UnityEngine;
using System.Collections;

public class SwingFeedback : MonoBehaviour {

	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index);  } }

	private Vector3 lastPoint;

	public Rigidbody referenceRigidBody;
	private BoxCollider racketCollider;

	// Use this for initialization
	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();

		racketCollider = referenceRigidBody.GetComponent<Collider>() as BoxCollider;
	}

	void FixedUpdate()
	{
		// multiply with physics timescale mirroring vive refresh rate, value in m/s
		float fakeVelocity = Vector3.Distance(transform.position, lastPoint) / Time.fixedDeltaTime;
		float racketForce = referenceRigidBody.mass * fakeVelocity / Time.fixedDeltaTime;
		// calculate airdrag with coeeficent (guessed), air density on earth (ca 1.3), the biggest area of the collider and the velocity ^ 2
		float airDrag = 0.5f / 2 * 1.3f * Mathf.Max(racketCollider.size.x, racketCollider.size.y, racketCollider.size.z) * fakeVelocity * fakeVelocity;

		float strength = Mathf.Abs(1 - (racketForce - airDrag));

		if (strength >= 500)
		{
			controller.TriggerHapticPulse((ushort)strength);
		}

		lastPoint = transform.position;
	}	

}
