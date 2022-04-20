using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StepSounds : MonoBehaviour
{
    public bool active
    {
        get {
            return this.currentlyActive;
        }
        set
        {
            this.currentlyActive = value;
            if (!value)
            {
                this.secondsUntilStep = this.secondsPerStep;
            }
        }
    }

    public StepType type
    {
        get
        {
            return this.currentType;
        }
        set
        {
            if ((int)value < 0 || (int)value > (int)StepType.Leaves)
            {
                return;
            } else
            {
                this.currentType = value;
            }
        }
    }

    public enum StepType
    {
        Grass = 0,
        HardFloor,
        Leaves
    }

    [Serializable]
    private struct StepListByType
    {
        public StepType type;
        public List<AudioClip> clips;
    }

    [SerializeField]
    private List<StepListByType> stepClips;
    [SerializeField]
    private float stepsPerSecond = 2.25f;
    [SerializeField]
    private float volume = 1;
    [SerializeField]
    private StepType initialType = StepType.Grass;

    private Dictionary<StepType, List<AudioClip>> stepClipDict;
    private float secondsPerStep;

    private StepType currentType;
    private bool currentlyActive;
    private float secondsUntilStep;

    void Start()
    {
        this.stepClipDict = new Dictionary<StepType, List<AudioClip>>();
        foreach(StepListByType slbt in this.stepClips)
        {
            if (!this.stepClipDict.ContainsKey(slbt.type))
            {
                this.stepClipDict.Add(slbt.type, new List<AudioClip>(slbt.clips));
            } else
            {
                this.stepClipDict[slbt.type].AddRange(slbt.clips);
            }
        }

        this.secondsPerStep = 1 / (this.stepsPerSecond == 0 ? 1 : this.stepsPerSecond);
        this.secondsUntilStep = this.secondsPerStep;

        this.currentType = this.initialType;
    }

    void Update()
    {
        if (this.currentlyActive)
        {
            this.secondsUntilStep -= Time.deltaTime;
            while (this.secondsUntilStep <= 0)
            {
                this.secondsUntilStep += this.secondsPerStep;
                this.PlayStepSound();
            }
        }
    }

    private void PlayStepSound()
    {
        if (this.stepClipDict.TryGetValue(this.currentType, out List<AudioClip> clips))
        {
            int randIndex = UnityEngine.Random.Range(0, clips.Count);
            AudioClip randClip = clips[randIndex];
            AudioSource.PlayClipAtPoint(randClip, this.transform.position, this.volume);
        }
    }
}
