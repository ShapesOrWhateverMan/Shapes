using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doughnut : shapes{

	void Start(){
		isGoodOrBad (true);
		base.Start ();
		setCharPos (3); //Doughnut is number three in the doc
	}

	//The doughnuts ability implementation
	public override void ability ()
	{
		Debug.Log ("F was pressed");
	}
}
