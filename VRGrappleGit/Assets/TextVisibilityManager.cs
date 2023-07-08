using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextVisibilityManager : MonoBehaviour
{
    public TextMeshProUGUI[] textElements;
    private int currentIndex = 0;

    private void Start()
    {
        // Hide all text elements except the first one
        for (int i = 1; i < textElements.Length; i++)
        {
            textElements[i].gameObject.SetActive(false);
        }
    }

    public void ShowNextText()
    {
        // Hide the current text
        textElements[currentIndex].gameObject.SetActive(false);

        // Increment the index to show the next text
        currentIndex++;

        // If we reach the end, loop back to the beginning
        if (currentIndex >= textElements.Length)
        {
            currentIndex = 0;
        }

        // Show the next text
        textElements[currentIndex].gameObject.SetActive(true);
    }
}