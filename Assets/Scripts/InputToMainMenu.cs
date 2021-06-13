using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MainMenu))]
public class InputToMainMenu : MonoBehaviour
{
    public InputActionReference RestartGame;
    public MainMenu MainMenu;

    public void Awake()
    {
        MainMenu ??= GetComponent<MainMenu>();
        RestartGame.action.performed += (e) =>
        {
            if (e.action.triggered)
            {
                MainMenu.StartButton();
            }
        };
    }

    public void Update()
    {
    }
}
