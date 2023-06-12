using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public enum HandType
{
    Left,
    Right
}

public class VRGrab : MonoBehaviour
{
    [SerializeField]
    [Tooltip("This would be the Grabbing action")]
    InputActionReference _grabbingAction;

    [SerializeField]
    [Tooltip("This would be the Pointing action")]
    InputActionReference _pointingAction;

    public HandType handType;
    public float thumbMoveSpeed = 0.1f;

    [SerializeField] private Animator _gloveAnimator;
    private UnityEngine.XR.InputDevice inputDevice;

    private float indexValue;
    private float thumbValue;
    private float threeFingersValue;

    private void Awake()
    {
        // inputDevice = GetInputDevice();

        var grabbingAction = GetInputAction(_grabbingAction);
        var pointAction = GetInputAction(_pointingAction);
        grabbingAction.performed += Grab;
        grabbingAction.canceled += UnGrab;

        pointAction.performed += Point;
        pointAction.canceled += UnPoint;
    }

    private void Start()
    {
        ResetAnimations();
    }

    private void ResetAnimations()
    {
        _gloveAnimator.SetFloat("Thumb", 0);
        _gloveAnimator.SetFloat("ThreeFingers", 0);
        _gloveAnimator.SetFloat("Index", 0);
    }
    void Update()
    {
        //AnimateHand();
    }

    static InputAction GetInputAction(InputActionReference actionReference)
    {
        return actionReference != null ? actionReference.action : null;
    }

    private void Point(InputAction.CallbackContext context)
    {
        _gloveAnimator.SetFloat("Index", 1);
    }

    private void UnPoint(InputAction.CallbackContext context)
    {
        _gloveAnimator.SetFloat("Index", 0);
    }

    private void Grab(InputAction.CallbackContext context)
    {
        _gloveAnimator.SetFloat("ThreeFingers", 1);
    }

    private void UnGrab(InputAction.CallbackContext context)
    {
        _gloveAnimator.SetFloat("ThreeFingers", 0);
    }

    private UnityEngine.XR.InputDevice GetInputDevice()
    {
        InputDeviceCharacteristics controllerCharacteristic = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller;

        if (handType == HandType.Left)
        {
            controllerCharacteristic = controllerCharacteristic | InputDeviceCharacteristics.Left;
        }
        else
        {
            controllerCharacteristic = controllerCharacteristic | InputDeviceCharacteristics.Right;
        }

        List<UnityEngine.XR.InputDevice> inputDevices = new List<UnityEngine.XR.InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristic, inputDevices);

        return inputDevices[0];
    }

    void AnimateHand()
    {
        inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out indexValue);
        inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.grip, out threeFingersValue);

        inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryTouch, out bool primaryTouched);
        inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryTouch, out bool secondaryTouched);

        if (primaryTouched || secondaryTouched)
        {
            thumbValue += thumbMoveSpeed;
        }
        else
        {
            thumbValue -= thumbMoveSpeed;
        }

        thumbValue = Mathf.Clamp(thumbValue, 0, 1);

        _gloveAnimator.SetFloat("Index", indexValue);
        _gloveAnimator.SetFloat("ThreeFingers", threeFingersValue);
        _gloveAnimator.SetFloat("Thumb", thumbValue);
    }
}