using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bettercut : MonoBehaviour
{
    public DialogueTriggerScript Dcut;
    public PlayerCtrl Pctrl;
    // Start is called before the first frame update
    void Start()
    {
        Pctrl.movSpeed = 0;
        Dcut.TriggerDialogue();
    }
}
