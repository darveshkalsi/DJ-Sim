
using UnityEngine;

public class RandomSpotlightEffects : MonoBehaviour
{
    [Header("Spotlight Reference")]
    public Light beamLight;

    [Header("Rotation Speed")]
    public float minRotationSpeed = 15f;
    public float maxRotationSpeed = 100f;

    [Header("Dance Floor Rotation")]
    public float floorRotationX = 0f;
    public float floorRotationY = 0f;
    public float floorRotationZ = 0f;

    [Header("Rotation Variation")]
    public float maxXVariation = 20f;
    public float maxYVariation = 45f;

    [Header("Time On Dance Floor")]
    public float minFloorTime = 8f;
    public float maxFloorTime = 15f;

    [Header("Occasional Random Movement")]
    public float minRandomTime = 2f;
    public float maxRandomTime = 4f;

    [Range(0f, 1f)]
    public float floorMovementChance = 0.8f;

    [Header("Brightness Settings")]
    public float minIntensity = 100f;
    public float maxIntensity = 120f;

    [Header("Range Settings")]
    public float lightRange = 30f;
    public float spotAngle = 60f;

    [Header("Random Colour Timing")]
    public float minColorTime = 2f;
    public float maxColorTime = 5f;

    [Header("Colour Transition Speed")]
    public float minColorSpeed = 1f;
    public float maxColorSpeed = 4f;

    [Header("Nightclub Colours")]
    public Color[] nightclubColors =
    {
        new Color(0.6f, 0f, 1f),
        new Color(0f, 0.2f, 1f),
        new Color(1f, 0f, 0.1f),
        new Color(0f, 1f, 0.2f),
        new Color(0f, 1f, 1f),
        new Color(1f, 0f, 0.7f),
        new Color(1f, 0.5f, 0f),
        Color.white
    };

    private Quaternion initialRotation;
    private Quaternion targetRotation;

    private float currentRotationSpeed;
    private float directionTimer;

    private Color targetColor;
    private float colorTimer;
    private float currentColorSpeed;

    void Start()
    {
        initialRotation = transform.localRotation;

        if (beamLight == null)
            beamLight = GetComponentInChildren<Light>();

        if (beamLight == null)
        {
            Debug.LogWarning("No BeamLight found on " + gameObject.name);
            enabled = false;
            return;
        }

        beamLight.type = LightType.Spot;
        beamLight.range = lightRange;
        beamLight.spotAngle = spotAngle;
        beamLight.shadows = LightShadows.None;

        ChooseNewDirection();
        ChooseNewRotationSpeed();

        ChooseNewColor();
        ChooseNewIntensity();
    }

    void Update()
    {
        // Smooth spotlight rotation
        transform.localRotation = Quaternion.RotateTowards(
            transform.localRotation,
            targetRotation,
            currentRotationSpeed * Time.deltaTime
        );

        directionTimer -= Time.deltaTime;

        if (directionTimer <= 0f)
        {
            ChooseNewDirection();
            ChooseNewRotationSpeed();
        }

        // Smooth colour transition
        beamLight.color = Color.Lerp(
            beamLight.color,
            targetColor,
            currentColorSpeed * Time.deltaTime
        );

        colorTimer -= Time.deltaTime;

        if (colorTimer <= 0f)
        {
            ChooseNewColor();
            ChooseNewIntensity();
        }
    }

    void ChooseNewDirection()
    {
        bool pointTowardsFloor =
            Random.value < floorMovementChance;

        float x;
        float y;
        float z = floorRotationZ;

        if (pointTowardsFloor)
        {
            x = floorRotationX +
                Random.Range(-maxXVariation, maxXVariation);

            y = floorRotationY +
                Random.Range(-maxYVariation, maxYVariation);

            directionTimer = Random.Range(
                minFloorTime, maxFloorTime
            );
        }
        else
        {
            x = Random.Range(-60f, 60f);
            y = Random.Range(-180f, 180f);

            directionTimer = Random.Range(
                minRandomTime, maxRandomTime
            );
        }

        targetRotation = initialRotation *
            Quaternion.Euler(x, y, z);
    }

    void ChooseNewRotationSpeed()
    {
        currentRotationSpeed = Random.Range(
            minRotationSpeed, maxRotationSpeed
        );
    }

    void ChooseNewColor()
    {
        targetColor = nightclubColors[
            Random.Range(0, nightclubColors.Length)
        ];

        currentColorSpeed = Random.Range(
            minColorSpeed, maxColorSpeed
        );

        colorTimer = Random.Range(
            minColorTime, maxColorTime
        );
    }

    void ChooseNewIntensity()
    {
        beamLight.intensity = Random.Range(
            minIntensity, maxIntensity
        );
    }
}
