using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI dialogueName;
    [SerializeField] private GameObject dialogueObject;

    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void StartDialogue(Dialogue[] dialogues, string name)
    {
        StartCoroutine(DisplayDialogue(dialogues, name));
    }

    private IEnumerator DisplayDialogue(Dialogue[] dialogues, string name)
    {
        dialogueObject.SetActive(true);
        dialogueName.text = name;


        for (int i = 0; i < dialogues.Length; i++)
        {
            float timer = 0f;
            dialogueText.text = dialogues[i].text;
            while (!IsNextPressed(timer) && timer < dialogues[i].displayTime)
            {
                timer += Time.deltaTime;
                yield return null;
            }
        }

        dialogueText.text = "";
        dialogueName.text = "";
        dialogueObject.SetActive(false);
    }

    private bool IsNextPressed(float timer)
    {
        return playerControls.Controls.NextText.WasPressedThisFrame() && timer > 0.5f;
    }

    private void Start()
    {
        /*Dialogue[] dialogues;
        Dialogue dialogue0 = new Dialogue();
        dialogue0.displayTime = 6f;
        dialogue0.text = "Hi welcome to your new school!";
        Dialogue dialogue1 = new Dialogue();
        dialogue1.displayTime = 3f;
        dialogue1.text = "Press WASD to move around!";
        Dialogue dialogue2 = new Dialogue();
        dialogue2.displayTime = 3f;
        dialogue2.text = "Press left click to shoot!";
        Dialogue dialogue3 = new Dialogue();
        dialogue3.displayTime = 3f;
        dialogue3.text = "Press right click to reflect bullets!";
        Dialogue dialogue4 = new Dialogue();
        dialogue4.displayTime = 3f;
        dialogue4.text = "Press L Shift to dash!";
        Dialogue dialogue5 = new Dialogue();
        dialogue5.displayTime = 3f;
        dialogue5.text = "Shoot the door to start, good luck!";

        dialogues = new Dialogue[]
        {
            dialogue0,
            dialogue1,
            dialogue2,
            dialogue3,
            dialogue4,
            dialogue5
        };

        StartDialogue(dialogues, "Tutorial");*/
    }
}

[System.Serializable]
public class Dialogue
{
    [TextArea(3, 10)]
    public string text;
    public float displayTime;
}