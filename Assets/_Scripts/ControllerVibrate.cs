using UnityEngine;
using System.Collections;

public class ControllerVibrate : MonoBehaviour {
    
    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    
    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    public void Vibrate() {
        StartCoroutine(LongVibration(0.1f, 1));
    }

    // taken from https://steamcommunity.com/app/358720/discussions/0/405693392914144440/
    // TriggerHapticPulse takes in the strength but will trigger only for one frame
    IEnumerator LongVibration(float length, float strength) {
        for (float i = 0; i < length; i += Time.deltaTime) {
            controller.TriggerHapticPulse((ushort)400);
            //controller.TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
            yield return null;
        }
    }
}
