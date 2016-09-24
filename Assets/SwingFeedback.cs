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

		float strengthPercentage = 1 - (racketForce - airDrag);
		controller.TriggerHapticPulse((ushort)Mathf.Lerp(0, 2000, strengthPercentage));
	}

	//void OnCollisionEnter(Collision collision)
	//{
	//	if (collision.collider.name.Equals("Ball"))
	//	{
	//		StartCoroutine(LongVibration(0.4f, 200));
	//	}
	//}

	//// taken from https://steamcommunity.com/app/358720/discussions/0/405693392914144440/
	//// TriggerHapticPulse takes in the strength but will trigger only for one frame
	//IEnumerator LongVibration(float length, float strength)
	//{
	//	for (float i = 0; i < length; i += Time.deltaTime)
	//	{ 
	//		controller.TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
	//		yield return null;
	//	}
	//}


}
