using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoundaryTP : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = new Vector3(113.4f, 150f, 212.19f);
            player.GetComponent<PlayerMovement>().ResetPlayerVelocity();
            player.GetComponent<CharacterController>().enabled = true;
        }
    }
}
