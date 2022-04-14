using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnClick : MonoBehaviour
{
    [SerializeField]
    private float clickRadius = 2;
    [SerializeField]
    private Transform destination;
    [SerializeField]
    private float destinationForwardOffset = 0.75f;
    [SerializeField]
    private float destinationHeightOffset = 1.14f - 1.5f;
    [SerializeField]
    private string playerTag = "Player";

    private GameObject player;

    private void Start()
    {
        this.player = GameObject.FindGameObjectWithTag(this.playerTag);
    }

    private void OnMouseDown()
    {
        this.TeleportPlayer();
    }

    private void TeleportPlayer()
    {
        this.player.GetComponent<CharacterController>().enabled = false;

        this.player.transform.position = this.destination.position + (this.destination.transform.forward * this.destinationForwardOffset) + (Vector3.up * this.destinationHeightOffset);
        this.player.transform.rotation *= this.destination.rotation * Quaternion.Inverse(this.transform.rotation);
        this.player.transform.rotation *= Quaternion.AngleAxis(180, this.player.transform.up);

        this.player.GetComponent<CharacterController>().enabled = true;
    }
}
