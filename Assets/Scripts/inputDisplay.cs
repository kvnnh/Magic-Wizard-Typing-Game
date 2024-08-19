using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class inputDisplay : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenu;
    private bool isPaused;

    public Player player;
    public TMP_Text displayText;
    private string currentInput = "";
    public AudioClip[] typingSFXClips;

    void Start()
    {
        if (displayText == null)
        {
            Debug.LogError("Display Text is not assigned in Start.");
        }
        else
        {
            Debug.Log("Display Text is assigned correctly in Start.");
        }
    }

    void Update()
    {
        isPaused = pauseMenu.activeSelf;

        if (player.isDead)
        {
            displayText.text = "";
            currentInput = "";
            return;
        }

        if (isPaused)
        {
            return;
        }

        try
        {
            foreach (char c in Input.inputString)
            {
                Debug.Log("Current input character: " + c);

                if (c == '\b')
                {
                    if (currentInput.Length != 0)
                    {
                        typingSFX();
                        currentInput = currentInput.Substring(0, currentInput.Length - 1);
                    }
                }
                else if ((c == '\n') || (c == '\r'))
                {
                    typingSFX();
                    Debug.Log("Enter key pressed. Current input: " + currentInput);
                    currentInput = "";
                }
                else
                {
                    typingSFX();
                    currentInput += c;
                }

                displayText.text = currentInput;
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error in InputDisplay script: " + ex.Message);
        }
    }

    private void typingSFX()
    {
        int randomIndex = Random.Range(0, typingSFXClips.Length);
        AudioClip clipToPlay = typingSFXClips[randomIndex];
        AudioSource.PlayClipAtPoint(clipToPlay, Vector3.zero);
    }
}
