using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    private float repeatDialogueInterval = 30;
    public GameObject dialogueBox;
    public TextMeshProUGUI textbox;
    public event Action<int> OnDialogueEnded;

    private bool playingDialogue = false;
    private int dialogueLevel = 0;
    private float repeatDialogueTimestamp = 0;
    private DialogueInfo dialogue;

    void Start()
    {
        GameManager.GM.dialogueScript = this;
        GameManager.GM.animator = GetComponent<Animator>();
        dialogue = GameManager.GM.dialogue[GameManager.GM.day];        
    }

    void Update()
    {
        if (dialogueLevel == 1 && Time.time - repeatDialogueTimestamp >= repeatDialogueInterval && !playingDialogue)
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
                GameManager.GM.animator.SetTrigger("playerIdle");
                break;
            case 1:
                StartCoroutine(PlayLines(dialogue.repeatLines));
                repeatDialogueTimestamp = Time.time;
                break;
            case 2:
                StartCoroutine(PlayLines(dialogue.endLines));
                dialogueLevel = 2;
                break;
            case 3:
                StartCoroutine(PlayLines(dialogue.interruptLines));
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

            textbox.text = "";
            foreach (char letter in line.ToCharArray())
            {
                textbox.text += letter;
                yield return new WaitForSeconds(0.015f);
            }
            lineIndex++;
            yield return new WaitForSeconds(Mathf.Max(2, textbox.text.Length / 5));
        }

        if (dialogueLevel == 0)
        {
            GameManager.GM.PhoneDescribeStart();
            dialogueLevel++;
        }
        else if (dialogueLevel == 2)
            GameManager.GM.DayEnd();

        playingDialogue = false;
        dialogueBox.SetActive(false);
    }
}

public class DialogueInfo
{
    public List<string> startLines;
    public List<string> repeatLines;
    public List<string> endLines;
    public List<string> interruptLines;
}