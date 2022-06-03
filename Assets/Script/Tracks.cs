using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracks : MonoBehaviour
{
    private void OnTriggerExit(Collider other){
    if(other.TryGetComponent<CarAgent>(out CarAgent carAgent)){
           carAgent.ExitTrack();
        }
    if(other.TryGetComponent<CarController>(out CarController carController)){
           carController.ExitTrack();
        }
    }
    
}
