using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Test { 
public class PauseMenuHandler : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private float distance = 0.2f; // Player.forward mapped to the horizontalk plane cross forward with the horizontal plane and normalize
    [SerializeField] private float menuHeight = 1.2f;

    [SerializeField]
    [Tooltip("This would be the opening and closing pause Menu action")]
    InputActionReference _interactPauseMenu;

    private bool menuOpened = false;

    private void Awake()
    {
        var interactPauseAction = GetInputAction(_interactPauseMenu);
        interactPauseAction.canceled += HandlePauseMenu;
    }

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    private void HandlePauseMenu(InputAction.CallbackContext actionReference)
    {
        print("Handling pause Menu");
        if(menuOpened)
        {
            pauseMenu.SetActive(false);
            menuOpened = false;
        }
        else
        {
            Vector3 newPosition = playerTransform.position + (playerTransform.transform.forward * distance);
            transform.position = new Vector3(newPosition.x, menuHeight, newPosition.z);
            transform.LookAt(playerTransform);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            pauseMenu.SetActive(true);
            menuOpened = true;
        }
    }

    static InputAction GetInputAction(InputActionReference actionReference)
    {
        return actionReference != null ? actionReference.action : null;
    }
}

}