
using UnityEngine;

public class RightCustomScroll : MonoBehaviour
{
    public Transform lowPoint;
    public Transform highPoint;

    public AudioSource RSpeaker;

    public float normalizedValue;

    bool iaGrabbing = false;

    public void Update()
    {
        if (iaGrabbing == true)
        {
            if (gameObject.tag == "RightVolumeController")
            {
                normalizedValue = Mathf.InverseLerp(
                    lowPoint.position.z,
                    highPoint.position.z,
                    transform.position.z
                );

                Debug.Log(normalizedValue);
                RSpeaker.volume = normalizedValue;
            }
        }
    }

    public void VolumeController()
    {
        iaGrabbing = !iaGrabbing;
    }
}
