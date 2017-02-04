using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : Shapes
{

    public void Start()
    {
        goodOrBad = true;
        base.Start();
        setCharPos(5);
    }

    //Teleportation
    public override void ability()
    {
        Debug.Log("F was pressed");

    }
}
