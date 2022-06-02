using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class CarDrivenAgent : Agent
{
    [SerializeField] private TrackCheckpoints trackCheckpoints;
    [SerializeField] private Transform spawnPosition;

    private CarDriver carDriver;

    private void Awake()
    {
        carDriver = GetComponent<CarDriver>();
    }

    private void Start(){
        trackCheckpoints.OnPlayerCorrectCheckpoint += TrackCheckpoints_OnPlayerCorrectCheckpoint;
        trackCheckpoints.OnPlayerWrongCheckpoint += TrackCheckpoints_OnPlayerWrongCheckpoint;
    }

    private void TrackCheckpoints_OnPlayerWrongCheckpoint(object sender, CarCheckpointEventArgs e)
    {
        if(e.carTransform == transform){
            AddReward(-1f);
        }
    }

    private void TrackCheckpoints_OnPlayerCorrectCheckpoint(object sender, CarCheckpointEventArgs e)
    {
        if(e.carTransform == transform){
            AddReward(1f);
        }
    }

    public override void OnEpisodeBegin()
    {
        transform.position = spawnPosition.position;
        transform.rotation = spawnPosition.rotation;
        trackCheckpoints.ResetCheckpoint(transform);
        carDriver.StopCompletely();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Vector3 checkpointForward = trackCheckpoints.GetNextCheckpoint(transform);
        float directionDot = Vector3.Dot(transform.forward, checkpointForward);
        sensor.AddObservation(directionDot);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float fowardAmount = 0f;
        float turnAmount = 0f;

        switch(actions.DiscreteActions[0]){
            case 0: fowardAmount = 0f; break;
            case 1: fowardAmount = +1f; break;
            case 2: fowardAmount = -1f; break;
        }
        switch(actions.DiscreteActions[1]){
            case 0: turnAmount = 0f; break;
            case 1: turnAmount = +1f; break;
            case 2: turnAmount = -1f; break;
        }
        carDriver.SetInputs(fowardAmount, turnAmount);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        int fowardAmount = 0;
        if(Input.GetKey(KeyCode.W)){
            fowardAmount = 1;
        }
        else if(Input.GetKey(KeyCode.S)){
            fowardAmount = 2;
        }

        int turnAmount = 0;
        if(Input.GetKey(KeyCode.A)){
            turnAmount = 1;
        }
        else if(Input.GetKey(KeyCode.D)){
            turnAmount = 2;
        }

        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = fowardAmount;
        discreteActions[1] = turnAmount;
    }
}
