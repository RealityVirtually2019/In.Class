using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadIn : MonoBehaviour {

    public GameObject everythingElse;
    public GameObject s1;
    public GameObject s2;
    public GameObject s3;

    // Use this for initialization
    void Start () {
        StartCoroutine(loadScreen());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator loadScreen(){
        
        yield return new WaitForSeconds(2.5f);
        this.gameObject.SetActive(false);
        everythingElse.GetComponent<Text>().enabled = true;
        s1.SetActive(true);
        s2.SetActive(true);
        s3.SetActive(true);
    }
}
