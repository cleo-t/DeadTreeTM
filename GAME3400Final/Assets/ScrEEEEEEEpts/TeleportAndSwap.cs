using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAndSwap : MonoBehaviour
{
    private static bool teleported;

    [SerializeField]
    private string playerTag = "Player";
    [SerializeField]
    private GameObject destinationTeleport;
    [SerializeField]
    private bool invertRotationOnTeleport = true;
    [SerializeField]
    private float cooldown = 0.1f;

    private GameObject player;
    private float timeUntilActive;

    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag(this.playerTag);
        teleported = false;
        this.timeUntilActive = 0;
    }

    private void Update()
    {
        this.timeUntilActive -= Time.deltaTime;
        this.timeUntilActive = Mathf.Max(0, this.timeUntilActive);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!teleported && this.timeUntilActive <= 0)
        {
            teleported = true;
            this.timeUntilActive = this.cooldown;
            Vector3 t = this.transform.position;
            this.player.GetComponent<CharacterController>().enabled = false;
            this.player.GetComponent<Collider>().enabled = false;
            this.transform.position = this.destinationTeleport.transform.position;
            this.player.transform.position = this.destinationTeleport.transform.position;
            if (this.invertRotationOnTeleport)
            {
                this.player.transform.rotation *= Quaternion.AngleAxis(180, Vector3.up);
            }
            this.destinationTeleport.transform.position = t;
            this.player.GetComponent<CharacterController>().enabled = true;
            this.player.GetComponent<Collider>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        teleported = false;
    }
}
