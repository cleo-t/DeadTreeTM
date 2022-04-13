using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiEmissionIncrease : MonoBehaviour
{
    [SerializeField]
    private string playerTag = "Player";
    [SerializeField]
    private float baseEmissionCoeff = 1;
    [SerializeField]
    private float maxEmissionCoeff = 5;
    [SerializeField]
    private float baseAlphaCoeff = 0.25f;
    [SerializeField]
    private float maxAlphaCoeff = 1;
    [SerializeField]
    private float range = 50;
    [SerializeField]
    private float emissionQFactor = 1;
    [SerializeField]
    private float alphaQFactor = 0.5f;

    private List<ParticleSystem> systems;
    private List<float> baseEmissionRates;
    private List<float> baseAlphas;

    private GameObject player;

    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag(this.playerTag);

        this.range = this.range == 0 ? 1 : this.range;

        this.systems = new List<ParticleSystem>(this.GetComponentsInChildren<ParticleSystem>());
        this.baseEmissionRates = new List<float>();
        this.baseAlphas = new List<float>();
        foreach(ParticleSystem system in this.systems)
        {
            this.baseEmissionRates.Add(system.emission.rateOverTime.constant);
            this.baseAlphas.Add(system.main.startColor.color.a);
        }
    }

    void Update()
    {
        float t = this.DistanceCoeff(); ;
        this.UpdateEmissions(t);
    }

    private float DistanceCoeff()
    {
        float toPlayer = Vector3.Distance(this.transform.position, this.player.transform.position);
        return toPlayer > this.range ? 0 : 1 - (toPlayer / this.range);
    }

    private void UpdateEmissions(float t)
    {
        for(int i = 0; i < this.systems.Count; i++)
        {
            float baseEmission = this.baseEmissionRates[i];
            float newCoef = Mathf.Lerp(this.baseEmissionCoeff, this.maxEmissionCoeff, Mathf.Pow(t, this.emissionQFactor));
            float newEmission = baseEmission * newCoef;

            float baseAlpha = this.baseAlphas[i];
            float newAlphaCoef = Mathf.Lerp(this.baseAlphaCoeff, this.maxAlphaCoeff, Mathf.Pow(t, this.alphaQFactor));
            float newAlpha = baseAlpha * newAlphaCoef;

            ParticleSystem system = this.systems[i];
            ParticleSystem.MainModule mm = system.main;
            Color c = mm.startColor.color;
            c.a = newAlpha;
            mm.startColor = c;
            ParticleSystem.EmissionModule em = system.emission;
            em.rateOverTime = newEmission;
        }
    }
}
