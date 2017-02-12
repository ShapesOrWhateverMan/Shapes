using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {

    public Transform[] backgrounds; //List of all objects to be parallaxed
    private float[] parallaxScales; //proportion of camera's movement to move the background by
    public float smoothing = 1f;    //parallax "smoothness"

    private Transform cam;          //Main camera's transform
    private Vector3 previousCamPos; //position of camera in previous frame

    void Awake() {
        //set up camera
        cam = Camera.main.transform;
    }

	// Use this for initialization
	void Start () {
        previousCamPos = cam.position;

        //assign parallax scale as a function of background's z position
        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++) {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < backgrounds.Length; i++) {
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
            //x-position = position + parallax pos
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
            //fade between current position and target position using lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        previousCamPos = cam.position;
	}
}
