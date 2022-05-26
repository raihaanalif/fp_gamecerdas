using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class CarDrivenAgent : Agent{
    // [SerializeField] private TrackCheckpoint checkpoint;
    // [SerializeField] private Transform spawnPoint;

    // private CarDriver carDriver;

    // private void Awake(){
    //     carDriver = GetComponent<CarDriver>();
    // }

    // private void Start(){
    //     checkpoint.OnCarCorrectCheckpointReached += Checkpoint_OnCarCorrectCheckpointReached;
    //     checkpoint.OnCarWrongCheckpointReached += Checkpoint_OnCarWrongCheckpointReached;
    // }

    // private void Checkpoint_OnCarWrongCheckpointReached(){
    //     if(e.carTransform == Transform){
    //         AddReward(-1f);
    //     }
    // }

    // private void Checkpoint_OnCarCorrectCheckpointReached(){
    //     if(e.carTransform == Transform){
    //         AddReward(1f);
    //     }
    // }

    // public override void onEpisodeBegin(){
    //     transform.position = spawnPoint.position + new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
    //     transform.forward = spawnPoint.forward;
    //     checkpoint.ResetCheckpoint(transform);
    //     carDriver.StopCompeletly();
    // }

    // public override void CollectObservations(VectorSensor sensor){
    //     Vector3 checkpointFoward = checkpoint.GetNextCheckpoint(transform).transform.forward;
    //     float directionOut = Vector3.Dot(transform.forward, checkpointFoward);
    //     sensor.AddObservation(directionOut);
    // }

    // public override void onActionReceived(ActionBuffers action){
    //     float fowardAmount = 0f;
    //     float turnAmount = 0f;

    //     switch(action.DiscreteActions[0]){
    //         case 0: fowardAmount = 1f; break;
    //         case 1: fowardAmount = +1f; break;
    //         case 2: fowardAmount = -1f; break;
    //     }
    //     switch(action.DiscreteActions[1]){
    //         case 0: turnAmount = 1f; break;
    //         case 1: turnAmount = +1f; break;
    //         case 2: turnAmount = -1f; break;
    //     }

    //     carDriver.SetInput(fowardAmount, turnAmount);
    // }

    // public override void Heuristic(in ActionBuffers actionOut){
    //     int fowardAction = 0;
    //     if(Input.GetKey(KeyCode.W))fowardAction = 1;
    //     if(Input.GetKey(KeyCode.S))fowardAction = 2;

    //     int turnAction = 0;
    //     if(Input.GetKey(KeyCode.A))turnAction = 1;
    //     if(Input.GetKey(KeyCode.D))turnAction = 2;

    //     ActionSegment<float> discreteActions = actionOut.DiscreteActions;
    //     discreteActions[0] = fowardAction;
    //     discreteActions[1] = turnAction;
    // }
}

