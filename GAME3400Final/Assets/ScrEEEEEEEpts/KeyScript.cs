using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject door;

    private void OnMouseDown()
    {
        door.GetComponent<BoxCollider>().isTrigger = true;
        door.GetComponent<DoorToSwapScenes>().isOpen = true;
        Destroy(gameObject, .2f);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.GetComponent<BoxCollider>().isTrigger = true;
            door.GetComponent<DoorToSwapScenes>().isOpen = true;
            Destroy(gameObject, .2f);
        }
    }*/
}
