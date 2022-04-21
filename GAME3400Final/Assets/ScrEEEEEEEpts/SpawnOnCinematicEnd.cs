using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lol it's actually the tree healing
public class SpawnOnCinematicEnd : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToSpawn;
    [SerializeField]
    private TreeHealing healingTree;

    void Start()
    {
        this.objectToSpawn.SetActive(false);
        this.healingTree.OnTreeHealFinished += this.OnCinematicFinish;
    }

    private void OnDestroy()
    {
        this.healingTree.OnTreeHealFinished -= this.OnCinematicFinish;
    }

    private void OnCinematicFinish()
    {
        this.objectToSpawn.SetActive(true);
    }
}
