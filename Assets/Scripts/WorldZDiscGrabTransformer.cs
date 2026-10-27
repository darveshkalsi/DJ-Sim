
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class WorldZDiscGrabTransformer : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Transform handTransform;

    private Quaternion startingRotation;
    private float previousHandAngle;
    private float accumulatedAngle;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        handTransform = args.interactorObject.GetAttachTransform(
            args.interactableObject
        );

        startingRotation = transform.rotation;
        accumulatedAngle = 0f;
        previousHandAngle = GetHandAngle();
    }

    private float GetHandAngle()
    {
        Vector3 offset = handTransform.position - transform.position;

        return Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
    }

    private void Update()
    {
        if (handTransform == null)
            return;

        float currentAngle = GetHandAngle();

        accumulatedAngle += Mathf.DeltaAngle(
            previousHandAngle,
            currentAngle
        );

        previousHandAngle = currentAngle;

        transform.rotation =
            Quaternion.AngleAxis(accumulatedAngle, Vector3.forward)
            * startingRotation;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        handTransform = null;
    }
}
