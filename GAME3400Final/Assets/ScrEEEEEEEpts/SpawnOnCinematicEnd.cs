using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnCinematicEnd : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToSpawn;

    void Start()
    {
        this.objectToSpawn.SetActive(false);
        CameraRevolve.OnCinematicFinish += this.OnCinematicFinish;
    }

    private void OnDestroy()
    {
        CameraRevolve.OnCinematicFinish -= this.OnCinematicFinish;
    }

    private void OnCinematicFinish()
    {
        this.objectToSpawn.SetActive(true);
    }
}
