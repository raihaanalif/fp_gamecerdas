using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class CarDriverAgent : Agent
{
    [SerializeField] private TrackCheckpoint trackCheckpoint;
    [SerializeField] private Transform spawnPositionTransform;

    private CarDriver carDriver;

    private void Awake()
    {
        carDriver = GetComponent<CarDriver>();
    }

    private void Start()
    {
        trackCheckpoint.onPlayerCorrectlyThroughCheckpoint += TrackCheckpoint_OnPlayerCorrectlyThroughCheckpoint;
        trackCheckpoint.onPlayerIncorrectlyThroughCheckpoint += TrackCheckpoint_OnPlayerIncorrectlyThroughCheckpoint;
    }

    private void TrackCheckpoint_OnPlayerCorrectlyThroughCheckpoint(object sender, TrackCheckpoint.CarCheckpointEventArgs e)
    {
       if(e.carTransform == transform)
        {
            SetReward(1f);
            EndEpisode();
        }
    }

    private void TrackCheckpoint_OnPlayerIncorrectlyThroughCheckpoint(object sender, TrackCheckpoint.CarCheckpointEventArgs e)
    {
        if (e.carTransform == transform)
        {
            SetReward(-1f);
            EndEpisode();
        }
    }

    public override void OnEpisodeBegin()
    {
        transform.position = spawnPositionTransform.position + new Vector3(Random.Range(-5f, +5f), 0f, Random.Range(-5f, 5f));
        transform.forward = spawnPositionTransform.forward;
        trackCheckpoint.ResetCheckpoint();
        carDriver.StopCompletely();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Vector3 checkpointFoward = trackCheckpoint.GetNextCheckpointPosition(transform);
        float directionDot = Vector3.Dot(transform.forward, checkpointFoward);
        sensor.AddObservation(directionDot);
    }

    public override void OnActionReceived(ActionBuffers action)
    {
        float forwardAmount = 0f;
        float turnAmount = 0f;

        switch(action.DiscreteActions[0]){
            case 0:
                forwardAmount = 0f;
                break;
            case 1:
                forwardAmount = +1f;
                break;
            case 2:
                forwardAmount = -1f;
                break;
        }

        switch (action.DiscreteActions[1])
        {
            case 0:
                turnAmount = 0f;
                break;
            case 1:
                turnAmount = +1f;
                break;
            case 2:
                turnAmount = -1f;
                break;
        }

        carDriver.SetInputs(forwardAmount, turnAmount);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        int fowardAction = 0;
        if(Input.GetKey(KeyCode.W)){
            fowardAction = 1;
        }
        else if(Input.GetKey(KeyCode.S)){
            fowardAction = 2;
        }
        
        int turnAction = 0;
        if(Input.GetKey(KeyCode.A)){
            turnAction = 1;
        }
        else if(Input.GetKey(KeyCode.D)){
            turnAction = 2;
        }

        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = fowardAction;
        discreteActions[1] = turnAction;
    }
}
