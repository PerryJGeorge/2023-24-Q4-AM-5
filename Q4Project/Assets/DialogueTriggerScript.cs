using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerScript : MonoBehaviour
{
    public DialogueScript dialogue;
    public int characterIndex; // Add the characterIndex variable

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManagerScript>().StartDialogue(dialogue, characterIndex); // Pass the characterIndex argument
    }
}
