using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyGroup : MonoBehaviour
{
    [Header("Stuff that changes")]
    [SerializeField]
    public float radiusPercent = 1;
    [SerializeField]
    public float lightPercent = 0.2f;

    [Header("Stuff that doesn't")]
    [SerializeField]
    private GameObject fireflyPrefab;
    [SerializeField]
    private Light fireflyLight;
    [SerializeField]
    private int count = 8;
    [SerializeField]
    private float minHeightOffset = -1;
    [SerializeField]
    private float maxHeightOFfset = 1;
    [SerializeField]
    private float minRadius = 1;
    [SerializeField]
    private float maxRadius = 6;
    [SerializeField]
    private float minSpeed = 0.1f;
    [SerializeField]
    private float maxSpeed = 0.4f;
    [SerializeField]
    private float minWaviness = 0.05f;
    [SerializeField]
    private float maxWaviness = 1;

    private List<FireflyMovement> fireflies;
    private float prevRadiusPercent;
    private float prevLightPercent;
    private float initialLightIntensity;

    void Start()
    {
        this.fireflies = new List<FireflyMovement>();
        for(int i = 0; i < this.count; i++)
        {
            this.fireflies.Add(this.NewFirefly());
        }
        this.prevRadiusPercent = this.radiusPercent;
        this.prevLightPercent = this.lightPercent;
        this.initialLightIntensity = this.fireflyLight.intensity;
    }

    private FireflyMovement NewFirefly()
    {
        Vector3 heightOffset = Vector3.up * Random.Range(this.minHeightOffset, this.maxHeightOFfset);
        GameObject obj = Instantiate(this.fireflyPrefab, this.transform.position + heightOffset, Quaternion.identity, this.transform);
        FireflyMovement fm = obj.GetComponent<FireflyMovement>();

        fm.radius = Random.Range(this.minRadius, this.maxRadius);
        fm.linearRadialSpeed = Random.Range(this.minSpeed, this.maxSpeed);
        fm.radialPhaseOffset = this.RandomPhase();

        fm.verticalAmplitude = this.RandomWavyValue() * 0.5f;
        fm.verticalFrequency = this.RandomWavyValue() * 0.5f;
        fm.verticalPhaseOffset = this.RandomPhase();

        fm.horizontalAmplitude = this.RandomWavyValue() * 0.5f;
        fm.horizontalFrequency = this.RandomWavyValue() * 0.5f;
        fm.verticalPhaseOffset = this.RandomPhase();

        return fm;
    }

    private float RandomPhase()
    {
        return Random.Range(0, Mathf.PI * 2);
    }

    private float RandomWavyValue()
    {
        return Random.Range(this.minWaviness, this.maxWaviness);
    }

    void Update()
    {
        this.radiusPercent = Mathf.Clamp(this.radiusPercent, 0, 1);
        if (this.radiusPercent == 0)
        {
            this.radiusPercent = this.prevRadiusPercent;
        }
        if (this.prevRadiusPercent != this.radiusPercent)
        {
            this.UpdateFireflies(this.prevRadiusPercent, this.radiusPercent);
        }
        this.prevRadiusPercent = this.radiusPercent;

        this.lightPercent = Mathf.Clamp(this.lightPercent, 0, 1);
        if (this.lightPercent == 0)
        {
            this.lightPercent = this.prevLightPercent;
        }
        this.prevLightPercent = this.lightPercent;
        this.fireflyLight.intensity = this.initialLightIntensity * this.lightPercent;
    }

    private void UpdateFireflies(float oldVal, float newVal)
    {
        float ratio = newVal / oldVal;
        foreach(FireflyMovement fm in this.fireflies)
        {
            fm.radius *= ratio;
            fm.linearRadialSpeed *= ratio;
        }
    }
}
