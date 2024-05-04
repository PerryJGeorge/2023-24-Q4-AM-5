using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    public GameObject player;
    public DialogueCutsceneTrigger DTS;
    public GameObject scenechange;

    void Start()
    {
        player.GetComponent<PlayerCtrl>().movSpeed = 0;
        DTS.TriggerDialogue();
    }
}
