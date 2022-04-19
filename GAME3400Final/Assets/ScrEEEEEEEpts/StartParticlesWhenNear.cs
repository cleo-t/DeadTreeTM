using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartParticlesWhenNear : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particleSystem;
    [SerializeField]
    private float range = 5;
    [SerializeField]
    private string playerTag = "Player";

    private GameObject player;
    private float initialEmission;

    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag(this.playerTag);

        this.initialEmission = this.particleSystem.emission.rateOverTime.constant;
    }

    void Update()
    {
        ParticleSystem.EmissionModule em = this.particleSystem.emission;
        em.rateOverTime = Vector3.Distance(this.player.transform.position, this.transform.position) < this.range
            ? this.initialEmission
            : 0;
    }
}
