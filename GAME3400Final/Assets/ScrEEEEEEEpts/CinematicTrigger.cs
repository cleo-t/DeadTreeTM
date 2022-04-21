using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicTrigger : MonoBehaviour
{
    [SerializeField]
    private string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(this.playerTag))
        {
            CameraRevolve cr = Camera.main.GetComponent<CameraRevolve>();
            if (cr != null)
            {
                cr.StartPan();
            }
            this.GetComponent<Collider>().enabled = false;
        }
    }
}
