using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : Shapes
{

    public void Start()
    {
        goodOrBad = true;
        base.Start();
        setCharPos(0);
    }

    //Heal the entire party
    public override void ability()
    {
        Debug.Log("F was pressed");
        
    }
}
