using UnityEngine;
using System.Collections;

public class HitFeedback : MonoBehaviour {

	public SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }

	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.name.Equals("Ball"))
		{
			StartCoroutine(LongVibration(0.4f, 200));
		}
	}

	// taken from https://steamcommunity.com/app/358720/discussions/0/405693392914144440/
	// TriggerHapticPulse takes in the strength but will trigger only for one frame
	IEnumerator LongVibration(float length, float strength)
	{
		for (float i = 0; i < length; i += Time.deltaTime)
		{
			controller.TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
			yield return null;
		}
	}

}
