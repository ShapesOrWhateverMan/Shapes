using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class IceSpikes : StaticEnemy {
    private SpriteRenderer spriteTransform;
    //Add "slide in" animation
    public int slideUpSpeed = 5;  //Also equals to slideout (vertical) speed
    private float groundY;
    private float maxHeight;
    Random rng;

    //Current status
    private bool isGoingUp;

    // Use this for initialization
    void Start() {
        stats.contactDamage = 25f;
        SpriteRenderer spriteRendr = this.GetComponent<SpriteRenderer>();
        //Max height = ground pos + object height
        groundY = GameObject.FindGameObjectWithTag("IceSpikesSpawnPoint").transform.position.y;
        maxHeight = groundY + spriteRendr.bounds.size.y * 0.9f;
        isGoingUp = true;
    }

    // Update is called once per frame
    void Update() {
        if (isGoingUp && transform.position.y <= maxHeight) {
            transform.Translate(Vector3.up * Time.deltaTime * slideUpSpeed);
            if (transform.position.y >= maxHeight)
                isGoingUp = false;
        }
        else {
            //Slide down after 0.25 sec
            //yield return new WaitForSeconds(0.25f);
            transform.Translate(Vector3.down * Time.deltaTime * 0.5f * slideUpSpeed);
            if(transform.position.y <= groundY)
                Destroy(gameObject);
        }
    }
    /*
     * To make a Higher wave and lower trailing wave, increase up speed, lower down speed.
     * To make a "reverse" wave high trailing wave, do the opposite
     */
}
