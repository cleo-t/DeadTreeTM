using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToSwapScenes : MonoBehaviour
{
    public string sceneToGoTo;
    public bool isOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isOpen)
        {
            SceneManager.LoadScene(sceneToGoTo);
        }
    }
}
