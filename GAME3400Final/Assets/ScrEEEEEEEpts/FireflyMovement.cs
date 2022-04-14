using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyMovement : MonoBehaviour
{
    private static float twoPi = Mathf.PI * 2;

    [Header("Circular Movement")]
    [SerializeField]
    public float radius = 5;
    [SerializeField]
    public float linearRadialSpeed = 4;
    [SerializeField]
    public float radialPhaseOffset = 0;

    [Header("Vertical Oscillation")]
    [SerializeField]
    public float verticalAmplitude = 0.5f;
    [SerializeField]
    // Specified in Hz
    public float verticalFrequency = 1;
    [SerializeField]
    public float verticalPhaseOffset = 0;

    [Header("Horizontal Oscillation")]
    [SerializeField]
    public float horizontalAmplitude = 0.5f;
    [SerializeField]
    // Specified in Hz
    public float horizontalFrequency = 1;
    [SerializeField]
    public float horizontalPhaseOffset = 0;

    private float radialTheta;
    private float verticalTheta;
    private float horizontalTheta;

    private float dRadialTheta;
    private float dVerticalTheta;
    private float dHorizontalTheta;

    private Vector3 origin;

    void Start()
    {
        this.radialTheta = 0;
        this.verticalTheta = 0;
        this.horizontalTheta = 0;

        this.origin = this.transform.position;
    }

    void Update()
    {
        this.UpdateDeltas();
        this.TickClock();
        this.transform.position = this.origin 
            + this.RadialOffset() 
            + this.VerticalOffset() 
            + this.HorizontalOffset();
    }

    private void UpdateDeltas()
    {
        this.dRadialTheta = this.linearRadialSpeed / this.radius;
        this.dVerticalTheta = this.verticalFrequency * twoPi;
        this.dHorizontalTheta = this.horizontalFrequency * twoPi;
    }

    private void TickClock()
    {
        this.radialTheta += this.dRadialTheta * Time.deltaTime;
        this.radialTheta %= twoPi;

        this.verticalTheta += this.dVerticalTheta * Time.deltaTime;
        this.verticalTheta %= twoPi;

        this.horizontalTheta += this.dHorizontalTheta * Time.deltaTime;
        this.horizontalTheta %= twoPi;
    }

    private Vector3 RadialOffset()
    {
        return (Vector3.right * this.radius * Mathf.Cos(this.radialTheta + this.radialPhaseOffset))
            + (Vector3.forward * this.radius * Mathf.Sin(this.radialTheta + this.radialPhaseOffset));
    }

    private Vector3 VerticalOffset()
    {
        return Vector3.up * this.verticalAmplitude * Mathf.Sin(this.verticalTheta + this.verticalPhaseOffset);
    }

    private Vector3 HorizontalOffset()
    {
        return this.RadialOffset().normalized * this.horizontalAmplitude * Mathf.Cos(this.horizontalTheta + this.horizontalPhaseOffset);
    }
}
