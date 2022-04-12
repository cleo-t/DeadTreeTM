using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringAnchor : MonoBehaviour
{
    [SerializeField]
    private Transform anchorPoint;
    [SerializeField]
    private List<string> ignoreLayers;
    [SerializeField]
    private float maxOffset = 2.75f;
    [SerializeField]
    private bool pushForwards = false;

    void Start()
    {

    }

    void Update()
    {
        this.transform.position = this.GetBestPoint();
    }

    private Vector3 GetBestPoint()
    {
        Ray ray = new Ray(this.anchorPoint.position, this.GetPushDirection());
        if (Physics.Raycast(ray, out RaycastHit hit, this.maxOffset, this.RayMask())) {
            return hit.point;
        }
        return this.anchorPoint.position + (this.GetPushDirection() * this.maxOffset);
    }

    private Vector3 GetPushDirection()
    {
        return this.pushForwards ? this.transform.forward : -this.transform.forward;
    }

    private int RayMask()
    {
        return ~LayerMask.GetMask(this.ignoreLayers.ToArray());
    }
}
