using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour {

    public event EventHandler<CarCheckpointEventArgs> OnPlayerCorrectCheckpoint;
    public event EventHandler<CarCheckpointEventArgs> OnPlayerWrongCheckpoint;
    
    private List<CheckpointSingle> checkpointsList;
    [SerializeField] private List<Transform> carTransformList;

    private List<CheckpointSingle> checkpointSingleList;
    private List<int> nextCheckpointSingleIndexList;

    private void Awake() {
        Transform checkpointsTransform = transform.Find("Checkpoints");

        checkpointSingleList = new List<CheckpointSingle>();
        foreach (Transform checkpointSingleTransform in checkpointsTransform) {
            CheckpointSingle checkpointSingle = checkpointSingleTransform.GetComponent<CheckpointSingle>();

            checkpointSingle.SetTrackCheckpoints(this);

            checkpointSingleList.Add(checkpointSingle);
        }

        nextCheckpointSingleIndexList = new List<int>();
        foreach (Transform carTransform in carTransformList) {
            nextCheckpointSingleIndexList.Add(0);
        }
    }

    public void CarThroughCheckpoint(CheckpointSingle checkpointSingle, Transform carTransform) {
        int nextCheckpointSingleIndex = nextCheckpointSingleIndexList[carTransformList.IndexOf(carTransform)];
        if (checkpointSingleList.IndexOf(checkpointSingle) == nextCheckpointSingleIndex) {
            // Correct checkpoint
            Debug.Log("Correct");
            CheckpointSingle correctCheckpointSingle = checkpointSingleList[nextCheckpointSingleIndex];
            correctCheckpointSingle.Hide();

            nextCheckpointSingleIndexList[carTransformList.IndexOf(carTransform)]
                = (nextCheckpointSingleIndex + 1) % checkpointSingleList.Count;
            OnPlayerCorrectCheckpoint?.Invoke(this, EventArgs.Empty as CarCheckpointEventArgs);
        } else {
            // Wrong checkpoint
            Debug.Log("Wrong");
            OnPlayerWrongCheckpoint?.Invoke(this, EventArgs.Empty as CarCheckpointEventArgs);

            CheckpointSingle correctCheckpointSingle = checkpointSingleList[nextCheckpointSingleIndex];
            correctCheckpointSingle.Show();
        }
    }

    protected void onCarCheckPoint(CarCheckpointEventArgs e){
        EventHandler<CarCheckpointEventArgs> handler = OnPlayerCorrectCheckpoint;
    }

    public Vector3 GetNextCheckpoint(Transform carTransform){
        int nextCheckpointIndex = nextCheckpointSingleIndexList[carTransformList.IndexOf(carTransform)];
        return checkpointsList[nextCheckpointIndex].transform.forward;
    }

    public void ResetCheckpoint(Transform carTransform){
        int currentCheckpointIndex = nextCheckpointSingleIndexList[carTransformList.IndexOf(carTransform)];
        if(currentCheckpointIndex == 0){
            CheckpointSingle currentCheckpointSingle = checkpointSingleList[currentCheckpointIndex];
            currentCheckpointSingle.Show();
        }
    }

}

   public class CarCheckpointEventArgs : EventArgs{
        public Transform carTransform;
    }
