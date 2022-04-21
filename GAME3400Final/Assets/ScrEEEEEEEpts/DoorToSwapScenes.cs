using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToSwapScenes : MonoBehaviour
{
    public string sceneToGoTo;
    public bool isOpen = false;

    private OpenLockedDoor old;

    private void Start()
    {
        this.old = this.GetComponent<OpenLockedDoor>();
    }

    private void Update()
    {
        if (this.old != null)
        {
            this.old.enabled = !this.isOpen;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isOpen)
        {
            SceneManager.LoadScene(sceneToGoTo);
        }
    }
}
