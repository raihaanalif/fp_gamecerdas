using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDriverAI : MonoBehaviour
{
    [SerializeField] private Transform targetPositionTransform;
    private CarDriver carDriver;
    private Vector3 targetPosition;

    private void Awake()
    {
        carDriver = GetComponent<CarDriver>();
    }

    private void Update(){
        SetTargetPosition(targetPositionTransform.position);
        float forwardAmount = 0f;
        float turnAmount = 0f;
        float reachedTargetDistance = 7f;
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        if(distanceToTarget > reachedTargetDistance){
            Vector3 dirToMovePosition = (targetPosition - transform.position).normalized;
            float dot = Vector3.Dot(transform.forward, dirToMovePosition);

            if (dot > 0){
                forwardAmount = 1f;
                float stoppingDistance = 30f;
                float stoppingSpeed = 50f;
                if(distanceToTarget < stoppingDistance && carDriver.GetSpeed() > stoppingSpeed){
                    forwardAmount = -1f;
                }
            }
            else{
                float reserveDistance = 25f;
                if(distanceToTarget > reserveDistance)turnAmount = 1f;
                else forwardAmount = -1f;
            }

            float angleToDir = Vector3.SignedAngle(transform.forward, dirToMovePosition, Vector3.up);

            if (angleToDir > 0){
                turnAmount = 1f;
            }
            else{
                turnAmount = -1f;
            }
        }else{
            if(carDriver.GetSpeed() > 15f)forwardAmount = -1f;
            else forwardAmount = 0f;
            turnAmount = 0f;
        }
       
        carDriver.SetInputs(forwardAmount, turnAmount);
    }

    public void SetTargetPosition(Vector3 targetPosition){
        this.targetPosition = targetPosition;
    }
}
