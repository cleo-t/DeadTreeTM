using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLockedDoor : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> clips;
    [SerializeField]
    private float volume = 1;

    private void OnMouseDown()
    {
        if (!this.enabled)
        {
            return;
        }
        int randIndex = Random.Range(0, this.clips.Count);
        AudioSource.PlayClipAtPoint(this.clips[randIndex], this.transform.position, this.volume);
    }
}