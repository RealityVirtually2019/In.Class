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
            if (Physics.Raycast(Camera.transform.position, _heading, out rayHit, 1000.0f) && rayHit.collider.gameObject.CompareTag("s1"))
            {
                text1.SetActive(true);
                _meshRenderer = rayHit.collider.gameObject.GetComponent<MeshRenderer>();
                //_meshRenderer.material = FocusedMaterial;
            } else {
                //_meshRenderer.material = NonFocusedMaterial;
                text1.SetActive(false);
            }

            // SPHERE 2
            if (Physics.Raycast(Camera.transform.position, _heading, out rayHit, 100.0f) && rayHit.collider.gameObject.CompareTag("s2"))
            {
                text2.SetActive(true);
                _meshRenderer2 = rayHit.collider.gameObject.GetComponent<MeshRenderer>();
                //_meshRenderer2.material = FocusedMaterial;

            } else {
                //_meshRenderer2.material = NonFocusedMaterial;
                text2.SetActive(false);
            }

            // SPHERE 3
            if (Physics.Raycast(Camera.transform.position, _heading, out rayHit, 100.0f) && rayHit.collider.gameObject.CompareTag("s3"))
            {
                text3.SetActive(true);
                _meshRenderer3 = rayHit.collider.gameObject.GetComponent<MeshRenderer>();
                //_meshRenderer3.material = FocusedMaterial;
            } else {
                //_meshRenderer3.material = NonFocusedMaterial; 
                text3.SetActive(false);
            }
        }
    }
    #endregion
}
