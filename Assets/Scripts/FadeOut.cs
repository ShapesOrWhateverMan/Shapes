using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour {
    SpriteRenderer renderer = null;
    public float fadeOutRate = 0.95f;

    void Start() {
        renderer = GameObject.FindGameObjectWithTag("LightningStrike").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        //Fade out
        if (gameObject != null && renderer != null) {
            float r = renderer.color.r;
            float g = renderer.color.g;
            float b = renderer.color.b;

            renderer.color = new Color(r * fadeOutRate, g*fadeOutRate, b*fadeOutRate, 1f);
        }
            
    }
}
