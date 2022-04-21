using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFirefliesWithPlayer : MonoBehaviour
{
    [SerializeField]
    private string playerTag = "Player";
    [SerializeField]
    private FireflyGroup fireflyGroup;
    [SerializeField]
    private float fireflyMovementQ = 0.9f;
    [SerializeField]
    private float fireflyLerpSpeed = 1;
    [SerializeField]
    private float initialTriggerRadius = 12;

    private GameObject player;

    private float constrictedRadius;

    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.constrictedRadius = 12;
    }

    void Update()
    {
        float toPlayer = this.LateralDistance(this.player.transform.position, this.transform.position);
        this.constrictedRadius = Mathf.Max(Mathf.Min(this.constrictedRadius, toPlayer), 0);
        this.fireflyGroup.radiusPercent = Mathf.Lerp(this.fireflyGroup.radiusPercent, Mathf.Pow(this.constrictedRadius / this.initialTriggerRadius, this.fireflyMovementQ), this.fireflyLerpSpeed * Time.deltaTime);
    }

    private float LateralDistance(Vector3 a, Vector3 b)
    {
        a.y = 0;
        b.y = 0;
        return Vector3.Distance(a, b);
    }
}
