using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;
using System;

public class TreeHealing : MonoBehaviour
{
    public event Action OnTreeHealFinished;

    [Header("General")]
    [SerializeField]
    private string playerTag = "Player";
    [SerializeField]
    private float healDuration = 5;
    [SerializeField]
    private AudioClip healClip;
    [SerializeField]
    private float healClipDelay = 1;
    [SerializeField]
    private float healClipVolume = 1;

    [Header("VFX")]
    [SerializeField]
    private VisualEffect risingLeavesFX;
    [SerializeField]
    private VisualEffect healedFX;

    [Header("Fireflies")]
    [SerializeField]
    private FireflyGroup fireflyGroup;
    [SerializeField]
    private float fireflyRadiusChangeQ = 0.9f;
    [SerializeField]
    private float minRadiusPercent = 0.1f;

    [Header("Glow")]
    [SerializeField]
    private Image flashbangImage;
    [SerializeField]
    private Color flashbangColor = Color.yellow;
    [SerializeField]
    private float flashbangQ = 2.5f;
    [SerializeField]
    private float brightnessQ = 1.5f;

    [Header("Leaves")]
    [SerializeField]
    private GroundLeaves groundLeaves;
    [SerializeField]
    private float leafQ = 1;

    private bool playerReached;
    private bool healingDone;
    private float healSecondsRemaining;
    private float initialFireflyRadius;
    private float initialFireflyLightPercent;
    private float initialLeafPercent;

    void Start()
    {
        this.playerReached = false;
        this.healingDone = false;
        this.initialFireflyRadius = this.fireflyGroup.radiusPercent;
        this.initialFireflyLightPercent = this.fireflyGroup.lightPercent;
        this.initialLeafPercent = this.groundLeaves.leafPercent;

        this.healedFX.Stop();
        this.risingLeavesFX.Stop();
    }

    void Update()
    {
        if (this.playerReached)
        {
            this.healSecondsRemaining += !this.healingDone ? -Time.deltaTime : Time.deltaTime;
            float t = 1 - (this.healSecondsRemaining / this.healDuration);

            if (!this.healingDone)
            {
                // this.fireflyGroup.radiusPercent = this.FireflyRadius(t);
                this.groundLeaves.leafPercent = this.LeafPercent(t);
            }
            this.fireflyGroup.lightPercent = this.FireflyLight(t);
            this.flashbangImage.color = this.FlashbangColor(t);

            if (!this.healingDone && this.healSecondsRemaining <= 0)
            {
                this.healingDone = true;
                this.risingLeavesFX.Stop();
                this.healedFX.Play();
                if (this.OnTreeHealFinished != null)
                {
                    this.OnTreeHealFinished.Invoke();
                }
            }
            if (this.healSecondsRemaining > this.healDuration)
            {
                this.playerReached = false;
            }
        }
    }

    private float FireflyRadius(float t)
    {
        t = Mathf.Pow(t, this.fireflyRadiusChangeQ);
        return Mathf.Lerp(this.initialFireflyRadius, this.minRadiusPercent, t);
    }

    private Color FlashbangColor(float t)
    {
        Color c = this.flashbangColor;
        c.a = Mathf.Pow(t, this.flashbangQ);
        return c;
    }

    private float FireflyLight(float t)
    {
        t = Mathf.Pow(t, this.brightnessQ);
        return Mathf.Lerp(this.initialFireflyLightPercent, 1, t);
    }

    private float LeafPercent(float t)
    {
        return 1 - Mathf.Pow(t, this.leafQ);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(this.playerTag))
        {
            this.playerReached = true;
            this.GetComponent<Collider>().enabled = false;

            this.healSecondsRemaining = this.healDuration;
            this.risingLeavesFX.Play();
            StartCoroutine(this.PlayHealingSFX());
        }
    }

    private IEnumerator PlayHealingSFX()
    {
        yield return new WaitForSeconds(this.healClipDelay);
        AudioSource.PlayClipAtPoint(this.healClip, this.transform.position, this.healClipVolume);
    }
}
