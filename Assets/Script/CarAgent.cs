using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(DecisionRequester))]
public class CarAgent : Agent
{
    private float moveSpeed = 16;
    private float turnSpeed = 90;
    private Rigidbody rb;
    private Vector3 recallPos;
    private Quaternion recallRot;
    private string currentWall;
    private Checkpoints checkpoint = null;

    public override void Initialize()
    {
        rb = this.GetComponent<Rigidbody>();
        checkpoint = GameObject.Find("Checkpoints").GetComponent<Checkpoints>();
        recallPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        recallRot = new Quaternion(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z, this.transform.rotation.w);
    }
    public override void OnEpisodeBegin()
    {
        this.transform.position = recallPos;
        this.transform.rotation = recallRot;
        currentWall = "checkpointSingle";
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        float move = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;

        if (move < 0)
            discreteActions[0] = 0;
        else if (move == 0)
            discreteActions[0] = 1;
        else if (move > 0)
            discreteActions[0] = 2;

        if (turn < 0)
            discreteActions[1] = 0;
        else if (turn == 0)
            discreteActions[1] = 1;
        else if (turn > 0)
            discreteActions[1] = 2;
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        //  Vector Action:
        //      Space Type: Discrete
        //          Branches Size: 2
        //              Branch 0 Size: 3 values (0=reverse, 1=noaction, 2=forward)
        //              Branch 1 Size: 3 values (0=turnleft, 1=noaction, 2=turnright)

        switch (actions.DiscreteActions[0])
        {
            case 0: //back
                rb.AddRelativeForce(Vector3.back * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
                AddReward(-0.01f);
                break;
            case 1: //noaction
                AddReward(-0.3f);
                break;
            case 2: //forward
                rb.AddRelativeForce(Vector3.forward * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
                AddReward(0.0001f);
                break;
        }

        switch (actions.DiscreteActions[1])
        {
            case 0: //left
                this.transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
                break;
            case 1: //noaction
                break;
            case 2: //right
                this.transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint") == true)
        {
            if (other.name == currentWall)
            {
                Debug.Log("hit " + other.name);
                AddReward(1.0f);
                EndEpisode();
                //CurrentWall = CheckpointScript.GetNextCheckpointName(CurrentWall);
                //if (CurrentWall == "wall (0)")
                //{
                //    AddReward(1.0f);
                //    EndEpisode();
                //}
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Barrier") == true)
        {
            Debug.Log("hit " + collision.gameObject.name);
            AddReward(-1.0f);
            EndEpisode();
        }
    }

    private void onTriggerExit(Collider other){
        if(other.CompareTag("Track")!=true){
            AddReward(-1.0f);
            EndEpisode();
        }
    }    
}
