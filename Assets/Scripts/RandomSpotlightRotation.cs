
using UnityEngine;

public class RandomSpotlightRotation : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 30f;

    [Header("Random Angle Limits")]
    public float minX = -45f;
    public float maxX = 45f;

    public float minY = -60f;
    public float maxY = 60f;

    private Quaternion startingRotation;
    private Quaternion targetRotation;

    private void Start()
    {
        startingRotation = transform.localRotation;
        SetRandomRotation();
    }

    private void Update()
    {
        transform.localRotation = Quaternion.RotateTowards(
            transform.localRotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );

        if (Quaternion.Angle(
            transform.localRotation,
            targetRotation) < 1f)
        {
            SetRandomRotation();
        }
    }

    private void SetRandomRotation()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        targetRotation = startingRotation *
            Quaternion.Euler(randomX, randomY, 0f);
    }
}
