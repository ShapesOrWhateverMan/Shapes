using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : Shapes
{

    public void Start()
    {
        goodOrBad = true;
        base.Start();
        setCharPos(6);
    }

    //Cloud's ability is passive: She can fly whenever the environment is windy
    public override void ability()
    {
        Debug.Log("F was pressed");
    }
}
