// %BANNER_BEGIN%
// ---------------------------------------------------------------------
// %COPYRIGHT_BEGIN%
//
// Copyright (c) 2018 Magic Leap, Inc. All Rights Reserved.
// Use of this file is governed by the Creator Agreement, located
// here: https://id.magicleap.com/creator-terms
//
// %COPYRIGHT_END%
// ---------------------------------------------------------------------
// %BANNER_END%

using System.Collections;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using UnityEngine.UI;

namespace MagicLeap
{
    /// <summary>
    /// This class provides examples of how you can use haptics and LEDs
    /// on the Control.
    /// </summary>
    [RequireComponent(typeof(ControllerConnectionHandler))]
    public class ControllerFeedbackExample : MonoBehaviour
    {

        public GameObject buttonMenu;
        private bool menuActive;
        public GameObject watsonText;
        public bool watsonBool;
        public GameObject notification;

        public GameObject clone;

        #region Private Variables
        private ControllerConnectionHandler _controllerConnectionHandler;

        private int _lastLEDindex = -1;
        #endregion

        #region Const Variables
        private const float TRIGGER_DOWN_MIN_VALUE = 0.2f;

        // UpdateLED - Constants
        private const float HALF_HOUR_IN_DEGREES = 15.0f;
        private const float DEGREES_PER_HOUR = 12.0f / 360.0f;

        private const int MIN_LED_INDEX = (int)(MLInputControllerFeedbackPatternLED.Clock12);
        private const int MAX_LED_INDEX = (int)(MLInputControllerFeedbackPatternLED.Clock6And12);
        private const int LED_INDEX_DELTA = MAX_LED_INDEX - MIN_LED_INDEX;
        #endregion

        #region Unity Methods
        /// <summary>
        /// Initialize variables, callbacks and check null references.
        /// </summary>
        void Start()
        {
            menuActive = false;
            watsonBool = true;

            _controllerConnectionHandler = GetComponent<ControllerConnectionHandler>();

            MLInput.OnControllerButtonUp += HandleOnButtonUp;
            MLInput.OnControllerButtonDown += HandleOnButtonDown;
            MLInput.OnTriggerDown += HandleOnTriggerDown;
            MLInput.OnTriggerUp += MLInput_OnTriggerUp;
        }

        /// <summary>
        /// Update controller input based feedback.
        /// </summary>
        void Update()
        { 

            UpdateLED();

        }

        IEnumerator notifyTeacher(){
            notification.SetActive(true);
            yield return new WaitForSeconds(2f);
            notification.SetActive(false);
        }

        /// <summary>
        /// Stop input api and unregister callbacks.
        /// </summary>
        void OnDestroy()
        {
            if (MLInput.IsStarted)
            {
                MLInput.OnTriggerDown -= HandleOnTriggerDown;
                MLInput.OnControllerButtonDown -= HandleOnButtonDown;
                MLInput.OnControllerButtonUp -= HandleOnButtonUp;
                MLInput.OnTriggerUp -= HandleOnTriggerDown;
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Updates LED on the physical controller based on touch pad input.
        /// </summary>
        private void UpdateLED()
        {
            if (!_controllerConnectionHandler.IsControllerValid())
            {
                return;
            }

            MLInputController controller = _controllerConnectionHandler.ConnectedController;
            if (controller.Touch1Active)
            {
                // Get angle of touchpad position.
                float angle = -Vector2.SignedAngle(Vector2.up, controller.Touch1PosAndForce);
                if (angle < 0.0f)
                {
                    angle += 360.0f;
                }

                // Get the correct hour and map it to [0,6]
                int index = (int)((angle + HALF_HOUR_IN_DEGREES) * DEGREES_PER_HOUR) % LED_INDEX_DELTA;

                // Pass from hour to MLInputControllerFeedbackPatternLED index  [0,6] -> [MAX_LED_INDEX, MIN_LED_INDEX + 1, ..., MAX_LED_INDEX - 1]
                index = (MAX_LED_INDEX + index > MAX_LED_INDEX) ? MIN_LED_INDEX + index : MAX_LED_INDEX;

                if (_lastLEDindex != index)
                {
                    // a duration of 0 means leave it on indefinitely
                    controller.StartFeedbackPatternLED((MLInputControllerFeedbackPatternLED)index, MLInputControllerFeedbackColorLED.BrightCosmicPurple, 0);
                    _lastLEDindex = index;
                }
            }
            else if (_lastLEDindex != -1)
            {
                controller.StopFeedbackPatternLED();
                _lastLEDindex = -1;
            }
        }
        #endregion

        #region Event Handlers

        private void MLInput_OnTriggerUp(byte bte, float fl) {
            MLInputController controller = _controllerConnectionHandler.ConnectedController;

            RaycastHit rayHit;
            //Debug.Log("print");

            if (Physics.Raycast(controller.Position, transform.TransformDirection(Vector3.forward), out rayHit, 1000f))
            {
                // TURN ON/OFF CC
                if (rayHit.collider.gameObject.name == "ccButton")
                {
                    if (watsonBool)
                    {
                        watsonText.GetComponent<Text>().enabled = false;
                        watsonBool = false;
                    }
                    else if (!watsonBool)
                    {
                        watsonText.GetComponent<Text>().enabled = true;
                        watsonBool = true;

                    }
                }
                // QUESTION BUTTON
                if (rayHit.collider.gameObject.name == "questionButton")
                {
                    StartCoroutine(notifyTeacher());
                }
                //Debug.Log(rayHit.collider.gameObject.name);

                // ROTATE 3D OBJECT
                if (rayHit.collider.gameObject.name == "rotateButton")
                {
                    if (GameObject.Find("Pyramids 1") != null){
                        clone = GameObject.Find("Pyramids 1");
                        Vector3 temp = new Vector3(clone.transform.rotation.x,clone.transform.rotation.y, clone.transform.rotation.z);
                        temp.y += 20f;
                        //clone.transform.RotateAround(Vector3.forward ,20f);
                        //clone.transform.rotation.y = temp.y;


                    }
                }
                //Debug.Log(rayHit.collider.gameObject.name);

                // ROTATE 3D OBJECT
                if (rayHit.collider.gameObject.name == "scaleButton")
                {
                    if (GameObject.Find("Pyramids 1") != null)
                    {
                        clone = GameObject.Find("Pyramids 1");
                        clone.transform.localScale += new Vector3(5F, 5f, 0f);

                    }
                }
                //Debug.Log(rayHit.collider.gameObject.name);
            }


        }

        /// <summary>
        /// Handles the event for button down.
        /// </summary>
        /// <param name="controller_id">The id of the controller.</param>
        /// <param name="button">The button that is being pressed.</param>
        private void HandleOnButtonDown(byte controllerId, MLInputControllerButton button)
        {
            MLInputController controller = _controllerConnectionHandler.ConnectedController;


            if (button == MLInputControllerButton.Bumper){
                if (!menuActive)
                {
                    buttonMenu.SetActive(true);
                    menuActive = true;
                    //watsonText.SetActive(false);
                }
                else {
                    //watsonText.SetActive(true);
                    buttonMenu.SetActive(false);
                    menuActive = false;
                }
            }

            if (controller != null && controller.Id == controllerId &&
                button == MLInputControllerButton.Bumper)
            {
                // Demonstrate haptics using callbacks.
                controller.StartFeedbackPatternVibe(MLInputControllerFeedbackPatternVibe.ForceDown, MLInputControllerFeedbackIntensity.Medium);
                // Toggle UseCFUIDTransforms
                controller.UseCFUIDTransforms = !controller.UseCFUIDTransforms;
            }
        }

        /// <summary>
        /// Handles the event for button up.
        /// </summary>
        /// <param name="controller_id">The id of the controller.</param>
        /// <param name="button">The button that is being released.</param>
        private void HandleOnButtonUp(byte controllerId, MLInputControllerButton button)
        {
            MLInputController controller = _controllerConnectionHandler.ConnectedController;
            if (controller != null && controller.Id == controllerId &&
                button == MLInputControllerButton.Bumper)
            {
                // Demonstrate haptics using callbacks.
                controller.StartFeedbackPatternVibe(MLInputControllerFeedbackPatternVibe.ForceUp, MLInputControllerFeedbackIntensity.Medium);
            }
        }

        /// <summary>
        /// Handles the event for trigger down.
        /// </summary>
        /// <param name="controller_id">The id of the controller.</param>
        /// <param name="value">The value of the trigger button.</param>
        private void HandleOnTriggerDown(byte controllerId, float value)
        {
            MLInputController controller = _controllerConnectionHandler.ConnectedController;
            if (controller != null && controller.Id == controllerId)
            {
                MLInputControllerFeedbackIntensity intensity = (MLInputControllerFeedbackIntensity)((int)(value * 2.0f));
                controller.StartFeedbackPatternVibe(MLInputControllerFeedbackPatternVibe.Buzz, intensity);
            }
        }
        #endregion
    }
}
