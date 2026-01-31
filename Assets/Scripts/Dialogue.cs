using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    public string charKey;
    private float repeatDialogueInterval = 30;
    public GameObject dialogueCanvas;
    public GameObject dialogueBox;
    public TextMeshProUGUI textbox;
    public DialogueInfo dialogueInfo;

    private bool playingDialogue = false;
    private int dialogueLevel = 0;
    private float repeatDialogueTimestamp = 0;
    private DialogueInfo dialogue;

    void Start()
    {
        dialogue = GameManager.GM.dialogue[GameManager.GM.day];        
    }

    void Update()
    {
        if (dialogueLevel == 1 && Time.time - repeatDialogueTimestamp >= repeatDialogueInterval)
            PlayDialogue();
    }

    public void PlayDialogue(int level=-1)
    {
        if (level == -1)
            level = dialogueLevel;

        switch (level)
        {
            case 0:
                StartCoroutine(PlayLines(dialogue.startLines));
                dialogueLevel++;
                break;
            case 1:
                StartCoroutine(PlayLines(dialogueInfo.repeatLines));
                break;
            case 2:
                StartCoroutine(PlayLines(dialogueInfo.endLines));
                break;
            case 3:
                StartCoroutine(PlayLines(dialogueInfo.interruptLines));
                break;
        }
    }

    IEnumerator PlayLines(List<string> lines)
    {
        dialogueBox.SetActive(true);
        playingDialogue = true;

        // Play dialogue line by line
        int lineIndex = 0;
        while (lineIndex < lines.Count)
        {
            string line = lines[lineIndex];

            // Play line character by character, unless interrupted by Enter key
            textbox.text = "";
            foreach (char letter in line.ToCharArray())
            {
                textbox.text += letter;
                yield return new WaitForSeconds(0.015f);
            }
            lineIndex++;
        }

        playingDialogue = false;
        dialogueBox.SetActive(false);
    }
}

public class DialogueInfo
{
    public List<string> startLines;
    public List<string> endLines;
    public List<string> repeatLines;
    public List<string> interruptLines;
}