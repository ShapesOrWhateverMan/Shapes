using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour {

    public ArrayList partyList = new ArrayList(3);

    public int[][] relationshipMap = new int[10][];

    enum Shapes { square, circle, triangle,
        donut, rectangle, hexagon,
        cloud, cross, pentagon,
        lightning };

    

    private void initRelationshipMap()
    {
        for (int i = 0; i < relationshipMap.Length; i++)
        {
            relationshipMap[i] = new int[i + 1];
        }
        fillRelaPoint();
    }

    // fill the relationshipMap's elements with the number in the charactor chart
    private void fillRelaPoint()
    {
        relationshipMap[0][0] = 0;
        
    }

    public bool addShape(Shapes shape)
    {
        if (!partyList.Contains(shape) && partyList.Count < 3)
        {
            partyList.Add(shape);
            return true;
        }
        else
            return false;
    }

    public void join(/*player action*/ Shapes shape)
    {
        if (/*player action == true */) {
            if (addShape(shape))
            {
                //message display: new shape add to the party
            }
            else
            {
                //message display: fail to add shape
            }
        }
        else
        {
            //message display: player refuse to add the shape
        }
            
    }

	// Use this for initialization
	void Start () {
        initRelationshipMap();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
