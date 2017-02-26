using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningMovement : MonoBehaviour {

    public int moveSpeed = 100;

    // Update is called once per frame
    void Update() {
        //TODO: Add animation map to LightningStrike Prefab
        Destroy(gameObject, 2);
    }
}
