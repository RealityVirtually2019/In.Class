using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTurned : MonoBehaviour {

    public float initialYRotation;
    public bool headTurnedRight;
    public bool headTurnedLeft;

    public float headTurnedTime = 3.0f;
    
	// Use this for initialization
	void Start () {
        initialYRotation = this.transform.rotation.y;
        headTurnedRight = false;
        headTurnedLeft = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (this.transform.rotation.y > (initialYRotation + 25.0f) && this.transform.rotation.y >= initialYRotation)
        {
            headTurnedRight = true;
            if (headTurnedRight)
            {
                headTurnedTime -= Time.deltaTime;
                if (headTurnedTime <= 0)
                {
                    Debug.Log("turn left");
                }
            }
        }
        else if (this.transform.rotation.y <= (initialYRotation + 25.0f) && this.transform.rotation.y >= initialYRotation)
        {
            headTurnedRight = false;
            headTurnedTime = 3.0f;
        }
        if (this.transform.rotation.y < (initialYRotation - 25.0f) && this.transform.rotation.y <= initialYRotation)
        {
            headTurnedLeft = true;
        }
        else if (this.transform.rotation.y >= (initialYRotation - 25.0f) && this.transform.rotation.y <= initialYRotation)
        {
            headTurnedLeft = false;
        }
	}
}
