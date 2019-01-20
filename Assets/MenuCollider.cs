using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

namespace MagicLeap
{
    [RequireComponent(typeof(ControllerConnectionHandler))]
    public class MenuCollider : MonoBehaviour
    {

            #region Private Variables
            public ControllerConnectionHandler _controllerConnectionHandler;

            private Camera _camera;

            // MobileApp-specific variables
            private bool _isCalibrated = false;
            private Quaternion _calibrationOrientation = Quaternion.identity;
            private const float MOBILEAPP_FORWARD_DISTANCE_FROM_CAMERA = 0.75f;
            private const float MOBILEAPP_UP_DISTANCE_FROM_CAMERA = -0.1f;
            #endregion

            #region Unity Methods
            /// <summary>
            /// Initialize variables, callbacks and check null references.
            /// </summary>
            void Start()
            {
                _controllerConnectionHandler = GetComponent<ControllerConnectionHandler>();

                //_camera = Camera.main;

                MLInput.OnControllerButtonUp += HandleOnButtonUp;
            }

            /// <summary>
            /// Update controller input based feedback.
            /// </summary>
            void Update()
            {

            }

            private void OnDestroy()
            {
                MLInput.OnControllerButtonUp -= HandleOnButtonUp;
            }
            #endregion

            #region Event Handlers
            /// <summary>
            /// For Mobile App, this initiates/ends the recalibration when the home tap event is triggered
            /// </summary>
            /// <param name="controllerId">The id of the controller.</param>
            /// <param name="button">The button that is being released.</param>
            private void HandleOnButtonUp(byte controllerId, MLInputControllerButton button)
            {

                

                MLInputController controller = _controllerConnectionHandler.ConnectedController;
                if (_controllerConnectionHandler.IsControllerValid(controllerId) &&
                    controller.Type == MLInputControllerType.MobileApp &&
                    button == MLInputControllerButton.HomeTap)
                {
                    if (!_isCalibrated)
                    {
                        _calibrationOrientation = transform.rotation * Quaternion.Inverse(controller.Orientation);
                    }
                    _isCalibrated = !_isCalibrated;
                }
            }
		#endregion


	}
}
