using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraRevolve : MonoBehaviour
{
    public static bool active;
    public static Action OnCinematicFinish;

    private enum HeightFunction
    {
        SemiCircle = 0,
        Cos,
        Sin
    }

    [SerializeField]
    private int revolutions = 2;
    [SerializeField]
    private float duration = 10;
    [SerializeField]
    private float radius = 200;
    [SerializeField]
    private float heightOffsetAmplitude = 50;
    [SerializeField]
    private HeightFunction heightOffsetFunction = HeightFunction.SemiCircle;
    [SerializeField]
    private float rotationTransitionSnappiness = 100;

    private Vector3 initialPos;
    private Quaternion initialRotation;
    private float panSecondsLeft;
    private float maxTheta;

    void Start()
    {
        
    }

    void Update()
    {
        if (active)
        {
            this.UpdatePan();
        }
    }

    public void StartPan()
    {
        if (!active)
        {
            active = true;
            this.initialPos = this.transform.position;
            this.initialRotation = this.transform.rotation;
            this.panSecondsLeft = this.duration;
            this.maxTheta = 2 * Mathf.PI * this.revolutions;
        }
    }

    private void UpdatePan()
    {
        this.panSecondsLeft -= Time.deltaTime;
        if (this.panSecondsLeft <= 0)
        {
            active = false;
            if (OnCinematicFinish != null)
            {
                OnCinematicFinish.Invoke();
            }
            return;
        }

        float t = 1 - (this.panSecondsLeft / this.duration);
        Vector3 heightOffset = this.GetHeightOffset(t);
        Vector3 radialOffset = this.GetRadialOffset(t);
        this.transform.position = this.initialPos + heightOffset + radialOffset;

        this.transform.rotation = this.RotationTowardsOrigin(t);
    }

    private Quaternion RotationTowardsOrigin(float t)
    {
        Vector3 toOrigin = this.initialPos - this.transform.position;
        Quaternion originRotation = Quaternion.LookRotation(toOrigin, Vector3.up);
        return Quaternion.Lerp(originRotation, initialRotation, this.RotationLerpCurve(t));
    }

    private float RotationLerpCurve(float t)
    {
        return Mathf.Pow((1 - Mathf.Cos(2 * Mathf.PI * (t - 0.5f))) / 2, this.rotationTransitionSnappiness);
    }

    private Vector3 GetRadialOffset(float t)
    {
        float theta = Mathf.Lerp(0, this.maxTheta, t);
        float radius = this.radius * (1 - Mathf.Cos(theta / this.revolutions)) / 2;
        return radius * ((Vector3.right * Mathf.Cos(theta)) + (Vector3.forward * Mathf.Sin(theta)));
    }

    private Vector3 GetHeightOffset(float t)
    {
        float height;
        switch (this.heightOffsetFunction)
        {
            case HeightFunction.SemiCircle:
                height = this.SemiCircleHeight(t);
                break;
            case HeightFunction.Cos:
                height = this.CosHeight(t);
                break;
            case HeightFunction.Sin:
                height = this.SinHeight(t);
                break;
            default:
                height = 0;
                break;
        }
        return height * Vector3.up;
    }

    private float SemiCircleHeight(float t)
    {
        return this.heightOffsetAmplitude * Mathf.Sqrt(1 - (4 * Mathf.Pow(t - 0.5f, 2)));
    }

    private float CosHeight(float t)
    {
        return this.heightOffsetAmplitude * (1 - Mathf.Cos(2 * Mathf.PI * t)) / 2;
    }

    private float SinHeight(float t)
    {
        return this.heightOffsetAmplitude * Mathf.Sin(3 * Mathf.PI * t);
    }
}
