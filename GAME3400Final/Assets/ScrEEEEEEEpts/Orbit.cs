using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField]
    private float radius = 50;
    [SerializeField]
    private float radialSpeed = 0.1f;
    [SerializeField]
    private float eventFrequency = 20;
    [SerializeField]
    private float eventVariance = 10;
    [SerializeField]
    private float eventOffset;

    private Vector3 initialPos;
    private float theta;
    private float sinceLastEvent;
    private float nextEventVariance;

    private void Start()
    {
        this.initialPos = this.transform.position;
        this.sinceLastEvent = -eventOffset;
        this.nextEventVariance = Random.Range(-this.eventVariance, this.eventVariance);
    }

    void Update()
    {
        this.theta += this.radialSpeed * Time.deltaTime;
        this.theta %= 2 * Mathf.PI;
        this.transform.position = ((this.transform.right * Mathf.Cos(theta)) + (this.transform.forward * Mathf.Sin(theta))) * this.radius;

        this.eventOffset += Time.deltaTime;
        if (this.sinceLastEvent >= this.eventFrequency + this.nextEventVariance)
        {

        }
    }

    private void OnEvent()
    {
        this.sinceLastEvent = 0;
        this.nextEventVariance = Random.Range(-this.eventVariance, this.eventVariance);

        this.SendMessage("OnOrbitEvent");
    }
}
