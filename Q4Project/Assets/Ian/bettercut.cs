using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bettercut : MonoBehaviour
{
    public DialogueTriggerScript Dcut;
    // Start is called before the first frame update
    void Start()
    {
        Dcut.TriggerDialogue();
    }
}
