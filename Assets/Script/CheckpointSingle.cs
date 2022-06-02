using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSingle : MonoBehaviour {

    // private TrackCheckpoints trackCheckpoints;
    private MeshRenderer meshRenderer;
    private TrackCheckpoints trackCheckpoints;

    // TrackCheckpoints trackCheckpoints = new TrackCheckpoints();
    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start() {
        Hide();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent<CarDriver>(out CarDriver carDriver)) {
            // Debug.Log("Checkpoint");
            Debug.Log(trackCheckpoints);
            // trackCheckpoints.CarThroughCheckpoint(this, carDriver.transform);
        }
    }

    // private void OnTriggerEnter2D(Collider2D collider) {
    //     if (collider.TryGetComponent<Player_RollSpeed>(out Player_RollSpeed player)) {
    //         trackCheckpoints.CarThroughCheckpoint(this, collider.transform);
    //     }
    // }

    public void SetTrackCheckpoints(TrackCheckpoints trackCheckpoints) {
        this.trackCheckpoints = trackCheckpoints;
    }

    public void Show() {
        meshRenderer.enabled = true;
    }

    public void Hide() {
        meshRenderer.enabled = false;
    }

}
