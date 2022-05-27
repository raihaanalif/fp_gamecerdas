using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoint : MonoBehaviour
{
    public event EventHandler<CarCheckpointEventArgs> onPlayerCorrectlyThroughCheckpoint;
    public event EventHandler<CarCheckpointEventArgs> onPlayerIncorrectlyThroughCheckpoint;
 
    public class CarCheckpointEventArgs : System.EventArgs
    {
        public Transform carTransform;
    }

    [SerializeField] private List<Transform> carTransformList;
    private List<Checkpoint> checkpointsList;
    private List<int> nextCheckpointIndexList;
    private void Awake(){
        Transform checkpointTransform = transform.Find("Checkpoint");

        checkpointsList = new List<Checkpoint>();
        foreach(Transform checkpointSingleTransform in checkpointTransform){
            Checkpoint checkpoint = checkpointSingleTransform.GetComponent<Checkpoint>();
            checkpoint.setTrackCheckpoint(this);
            checkpointsList.Add(checkpoint);
        }

        nextCheckpointIndexList = new List<int>();
        foreach(Transform carTransform in carTransformList){
            nextCheckpointIndexList.Add(0);
        }
    }

    public void PlayerThroughCheckpoint(Checkpoint checkpoint, Transform carTransform){
        int nextCheckpointIndex = nextCheckpointIndexList[carTransformList.IndexOf(carTransform)];
        if(checkpointsList.IndexOf(checkpoint) == nextCheckpointIndex){
            Checkpoint correctCheckpoint = checkpointsList[nextCheckpointIndex];

            nextCheckpointIndexList[carTransformList.IndexOf(carTransform)] 
            = (nextCheckpointIndex + 1) % checkpointsList.Count;
            onPlayerCorrectlyThroughCheckpoint?.Invoke(this, new CarCheckpointEventArgs{carTransform = carTransform});
        }
        else{
            onPlayerIncorrectlyThroughCheckpoint?.Invoke(this, new CarCheckpointEventArgs{carTransform = carTransform});

            Checkpoint correctCheckpoint = checkpointsList[nextCheckpointIndex];
        }
    }

    public Vector3 GetNextCheckpointPosition(Transform carTransform){
        int nextCheckpointIndex = nextCheckpointIndexList[carTransformList.IndexOf(carTransform)];
        return checkpointsList[nextCheckpointIndex].transform.forward;
    }

    public void ResetCheckpoint(){
        checkpointsList.ForEach(checkpoint => checkpoint.gameObject.SetActive(false));
    }

}
