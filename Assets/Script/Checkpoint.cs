using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private TrackCheckpoint trackCheckpoint;
    private void onTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Player>(out Player player))
        {
            trackCheckpoint.PlayerThroughCheckpoint(this);
        }
    }

    public void setTrackCheckpoint(TrackCheckpoint trackCheckpoint)
    {
        this.trackCheckpoint = trackCheckpoint;
    }
}
