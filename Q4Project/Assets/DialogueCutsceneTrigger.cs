using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCutsceneTrigger : MonoBehaviour
{
    public DialogueScript dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueCutsceneManager>().StartDialogue(dialogue);
    }
}