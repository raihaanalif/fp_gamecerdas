using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private TrackCheckpoint trackCheckpoint;
    
    private void onTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<CarDriver>(out CarDriver carDriver))
        {
            trackCheckpoint.PlayerThroughCheckpoint(this, other.transform);
        }
    }

    public void setTrackCheckpoint(TrackCheckpoint trackCheckpoint)
    {
        this.trackCheckpoint = trackCheckpoint;
    }
}
