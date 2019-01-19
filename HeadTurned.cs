using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTurned : MonoBehaviour {

    public float initialYRotation;
    public bool headTurnedRight;
    public bool headTurnedLeft;

    public float headTurnedTime = 3.0f;

    public GameObject exclamation;

	// Use this for initialization
	void Start () {
        initialYRotation = this.transform.rotation.y;
        headTurnedRight = false;
        headTurnedLeft = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (this.transform.rotation.y > (initialYRotation + 0.25f) && this.transform.rotation.y >= initialYRotation)
        {
            headTurnedRight = true;
            if (headTurnedRight)
            {
                headTurnedTime -= Time.deltaTime;
                if (headTurnedTime <= 0)
                {
                    StartCoroutine(enableDuration());
                    Debug.Log("turn left");
                    headTurnedTime = 3.0f;
                    headTurnedRight = false;
                }
            }
        }
        else if (this.transform.rotation.y <= (initialYRotation + 0.25f) && this.transform.rotation.y >= initialYRotation)
        {
            headTurnedRight = false;
            headTurnedTime = 3.0f;
        }
        if (this.transform.rotation.y < (initialYRotation - 0.25f) && this.transform.rotation.y <= initialYRotation)
        {
            headTurnedLeft = true;
            if (headTurnedLeft)
            {
                headTurnedTime -= Time.deltaTime;
                if (headTurnedTime <= 0)
                {
                    StartCoroutine(enableDuration());
                    Debug.Log("turn left");
                    headTurnedTime = 3.0f;
                    headTurnedLeft = false;
                }

            }
        }
        else if (this.transform.rotation.y >= (initialYRotation - 0.25f) && this.transform.rotation.y <= initialYRotation)
        {
            headTurnedLeft = false;
            headTurnedTime = 3.0f;
        }
	}

    IEnumerator enableDuration()
    {
        exclamation.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        exclamation.SetActive(false);
    }
}
