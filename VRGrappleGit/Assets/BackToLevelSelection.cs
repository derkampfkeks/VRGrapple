using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BackToLevelSelection : MonoBehaviour
{

    public InputActionProperty levelSelection;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (levelSelection.action.WasPressedThisFrame())
        {
            SceneManager.LoadScene("Grapple Tutorial");
        }
    }
}