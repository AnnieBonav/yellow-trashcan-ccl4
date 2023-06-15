using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueHandler : MonoBehaviour
{
    [SerializeField]
    [Tooltip("This is the pressing A action")]
    InputActionReference _pressAAction;

    [SerializeField] private Dialogue dialogue;

    private void Awake()
    {
        var pressA = GetInputAction(_pressAAction);
        pressA.canceled += PressedA;
    }

    private void PressedA(InputAction.CallbackContext context)
    {
        print("PressedA");
        dialogue.ProceedDialogue();
    }

    static InputAction GetInputAction(InputActionReference actionReference)
    {
        return actionReference != null ? actionReference.action : null;
    }

}
