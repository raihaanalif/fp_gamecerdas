using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoint : MonoBehaviour
{
    private List<Checkpoint> checkpointsList;
    private void Awake(){
        Transform checkpointTransform = transform.Find("Checkpoint");

        checkpointsList = new List<Checkpoint>();
        foreach(Transform checkpointSingleTransform in checkpointTransform){
            Checkpoint checkpoint = checkpointSingleTransform.GetComponent<Checkpoint>();
            checkpoint.setTrackCheckpoint(this);
            checkpointsList.Add(checkpoint);
        }
    }

    public void PlayerThroughCheckpoint(Checkpoint checkpoint){
        Debug.Log(checkpointsList.IndexOf(checkpoint));
    }
}
