using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounding : MonoBehaviour
{
    
    //Encapsulated fields that are used in properties
    private Vector2 upPaddleBounds;
    private Vector2 lowPaddleBounds;
    
   
  
    // Property that sets the upper bound of the gameScreen.
    public Vector3 UpPaddleBounds
    {
        set
        {
            upPaddleBounds = value;
        }
        get
        {
            return new Vector2(12.82f, 4.45f);
        }
    }
    // Property that sets the lower bound of the gameScreen.
    public Vector3 LowPaddleBounds
    {
        set
        {
            lowPaddleBounds = value;
        }
        get
        {
            return new Vector2(12.82f, -4.98f);
        }

    }
    //Member variables
    public bool goingUp;
    public bool goingDown;
    // Start is called before the first frame update
    void Start()
    {
        goingDown = false;
        goingUp = false;
 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
