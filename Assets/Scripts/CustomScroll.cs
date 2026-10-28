using UnityEngine;

public class CustomScroll : MonoBehaviour
{
    public Transform lowPoint;
    public Transform highPoint;

    public AudioSource Lspeaker;

    public float normalizedValue;

    bool iaGrabbing = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 
    // Update is called once per frame
    public void Update()
    {
        if(iaGrabbing == true)
        {   
            if(gameObject.tag == "LeftVolumeController")
            {
                normalizedValue = Mathf.InverseLerp(lowPoint.position.z, highPoint.position.z, transform.position.z);
                Debug.Log(normalizedValue);
                Lspeaker.volume = normalizedValue;
            }
        }
    }

    public void VolumeController()
    {
        iaGrabbing = !iaGrabbing;
    }
}
