using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[System.Serializable]
public struct TextBlock
{
    [SerializeField, TextArea] public string text;
    [SerializeField] public UnityEvent events;
    [SerializeField] public List<ActionToFulfill> actionsToFulfill;
}

[System.Serializable]
public class ActionToFulfill
{
    [SerializeField] public InteractionEvents interactionEvent;
    [SerializeField] public bool hasBeenFulfilled;
}

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private List<TextBlock> textBlocks;
    [SerializeField] private float characterDelay;

    [SerializeField]
    [Tooltip("This is the pressing A action")]
    InputActionReference _pressAAction;

    [SerializeField] private GameObject continueButtonReference;

    private bool _writing = true;
    private bool _canAdvance = false;
    private int _currentDialogue = 0;

    private void Awake()
    {
        var pressA = GetInputAction(_pressAAction);
        pressA.canceled += PressedA;

        TestFlags.InteractionRaised += HandleFlags;
        Ingredient.InteractionRaised += HandleFlags;
    }

    private void OnDisable()
    {
        TestFlags.InteractionRaised -= HandleFlags;
        Ingredient.InteractionRaised -= HandleFlags;
    }

    void Start()
    {
        continueButtonReference.SetActive(false);
        StartCoroutine(NextTextblock());
    }

    private IEnumerator NextTextblock()
    {
        if (_currentDialogue < textBlocks.Count)
        {
            _writing = true;
            textMesh.text = "";
            char[] charArray = textBlocks[_currentDialogue].text.ToCharArray();
            for (int i = 0; i < charArray.Length; i++)
            {
                textMesh.text += charArray[i];
                yield return new WaitForSeconds(characterDelay);
            }

            textBlocks[_currentDialogue].events.Invoke();
            _writing = false;
        }
    }

    public void ProceedDialogue()
    {
        if (!_writing)
        {
            if (_canAdvance)
            {
                print("Can Advance, inside of dialogue.");
                StartCoroutine(NextTextblock());
                _currentDialogue++;
            }
        }
    }

    private void HandleFlags(InteractionEvents interactionEvent)
    {
        print("An interaction was raised: " + interactionEvent);
        for (int i = 0; i < textBlocks[_currentDialogue].actionsToFulfill.Count; i ++) // Iterate through all of the needed actions to ulfill from the current block
        {
            // continuing if the action has been fulfilled would let us have two of the same but would prevent being able to revert a done to a needs to be done (like messing up something and you need to redo it)
            if (textBlocks[_currentDialogue].actionsToFulfill[i].interactionEvent == interactionEvent) // Then it was the same one, so I can raise the flag
            {
                textBlocks[_currentDialogue].actionsToFulfill[i].hasBeenFulfilled = true;
                CheckIfFlagsFulfilled();
                return; // TODO: Make it better so that there could be two of grab ingredient
            }
        }
        
        
    }

    private void CheckIfFlagsFulfilled()
    {
        for (int i = 0; i < textBlocks[_currentDialogue].actionsToFulfill.Count; i++)
        {
            if (textBlocks[_currentDialogue].actionsToFulfill[i].hasBeenFulfilled == false) 
            {
                return; // If any are false, we co not get to enable continue
            }
        }

        EnableContinue();
    }

    private void EnableContinue()
    {
        print("Enabliong continue");
        continueButtonReference.SetActive(true);
        _canAdvance = true;
    }

    private void DisableContinue()
    {
        print("Enabliong continue");
        continueButtonReference.SetActive(false);
        _canAdvance = false;
    }

    public void CheckWhichNeedToBeFulfilled()
    {
        print("Dialogue check. Number: " + textBlocks[_currentDialogue].actionsToFulfill.Count + " dialogue: " + _currentDialogue);

        for(int i = 0; i < textBlocks[_currentDialogue].actionsToFulfill.Count; i++)
        {
            print(textBlocks[_currentDialogue].actionsToFulfill[i].interactionEvent);
        }
        
    }


    private void PressedA(InputAction.CallbackContext context)
    {
        print("PressedA");
        if(_canAdvance) ProceedDialogue();
    }


    static InputAction GetInputAction(InputActionReference actionReference)
    {
        return actionReference != null ? actionReference.action : null;
    }

    

    

    
}
