using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    public PlayerCtrl player;
    public DialogueTriggerScript DTS;
    public flick scenechange;

    // Start is called before the first frame update
    void Start()
    {
        player.enabled = false;
        DTS.TriggerDialogue();
    }
}
