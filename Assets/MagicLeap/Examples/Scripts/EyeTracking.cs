using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class EyeTracking : MonoBehaviour
{
    #region Public Variables
    public GameObject Camera;
    public Material FocusedMaterial, NonFocusedMaterial;

    public GameObject text1;
    public GameObject text2;
    public GameObject text3;

    public int eyetrackHits;

    public GameObject s1;
    public GameObject s2;
    public GameObject s3;

    public bool s1bool;
    public bool s2bool;
    public bool s3bool;

    public GameObject text;

    #endregion

    #region Private Variables
    private Vector3 _heading;
    private MeshRenderer _meshRenderer;
    private MeshRenderer _meshRenderer2;
    private MeshRenderer _meshRenderer3;


    #endregion

    #region Unity Methods
    void Start()
    {
        MLEyes.Start();
        s1bool = false;
        s2bool = false;
        s3bool = false;
    }
    private void OnDisable()
    {
        MLEyes.Stop();
    }
    void Update()
    {
        if (MLEyes.IsStarted)
        {
            RaycastHit rayHit;
            _heading = MLEyes.FixationPoint - Camera.transform.position;

            // SPHERE 1

            if (Physics.Raycast(Camera.transform.position, _heading, out rayHit, 1000.0f) && rayHit.collider.gameObject.name == "ButtonCanvas"){
                text.SetActive(false);
            }
            else {
                text.SetActive(true);
            }


            if (Physics.Raycast(Camera.transform.position, _heading, out rayHit, 1000.0f) && rayHit.collider.gameObject.CompareTag("s1") && !s1bool)
            {
                text1.SetActive(true);

                s1.GetComponent<BoxCollider>().enabled = false;
                eyetrackHits++;
                s1bool = true;
                s1.GetComponent<MeshRenderer>().enabled = true;

                if (s3bool && s2bool)
                {
                    s1bool = false;
                    s2bool = false;
                    s3bool = false;
                    s1.GetComponent<BoxCollider>().enabled = true;
                    s2.GetComponent<BoxCollider>().enabled = true;
                    s3.GetComponent<BoxCollider>().enabled = true;
                    s1.GetComponent<MeshRenderer>().enabled = false;
                    s2.GetComponent<MeshRenderer>().enabled = false;
                    s3.GetComponent<MeshRenderer>().enabled = false;
                }

            } else {
                text1.SetActive(false);
            }

            // SPHERE 2
            if (Physics.Raycast(Camera.transform.position, _heading, out rayHit, 100.0f) && rayHit.collider.gameObject.CompareTag("s2") && !s2bool)
            {
                text2.SetActive(true);
                s2.GetComponent<BoxCollider>().enabled = false;
                eyetrackHits++;
                s2bool = true;
                s2.GetComponent<MeshRenderer>().enabled = true;

                if (s1bool && s3bool)
                {
                    s1bool = false;
                    s2bool = false;
                    s3bool = false;
                    s1.GetComponent<BoxCollider>().enabled = true;
                    s2.GetComponent<BoxCollider>().enabled = true;
                    s3.GetComponent<BoxCollider>().enabled = true;
                    s1.GetComponent<MeshRenderer>().enabled = false;
                    s2.GetComponent<MeshRenderer>().enabled = false;
                    s3.GetComponent<MeshRenderer>().enabled = false;
                }

            } else {
                text2.SetActive(false);
            }

            // SPHERE 3
            if (Physics.Raycast(Camera.transform.position, _heading, out rayHit, 100.0f) && rayHit.collider.gameObject.CompareTag("s3") && !s3bool)
            {
                text3.SetActive(true);
                s3.GetComponent<BoxCollider>().enabled = false;
                eyetrackHits++;
                s3bool = true;
                s3.GetComponent<MeshRenderer>().enabled = true;

                if (s1bool && s2bool)
                {
                    s1bool = false;
                    s2bool = false;
                    s3bool = false;
                    s1.GetComponent<BoxCollider>().enabled = true;
                    s2.GetComponent<BoxCollider>().enabled = true;
                    s3.GetComponent<BoxCollider>().enabled = true;
                    s1.GetComponent<MeshRenderer>().enabled = false;
                    s2.GetComponent<MeshRenderer>().enabled = false;
                    s3.GetComponent<MeshRenderer>().enabled = false;


                }
            } else {
                //_meshRenderer3.material = NonFocusedMaterial; 
                text3.SetActive(false);
            }
        }
    }
    #endregion
}
