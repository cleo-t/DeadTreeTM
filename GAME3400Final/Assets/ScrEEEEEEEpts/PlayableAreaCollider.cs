using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableAreaCollider : MonoBehaviour
{
    [SerializeField]
    private int playerLayer = 3;
    [SerializeField]
    private int groundLayer = 6;

    private void OnTriggerEnter(Collider other)
    {
        Physics.IgnoreLayerCollision(this.playerLayer, this.groundLayer, false);
    }

    private void OnTriggerExit(Collider other)
    {
        Physics.IgnoreLayerCollision(this.playerLayer, this.groundLayer, true);
    }
}
