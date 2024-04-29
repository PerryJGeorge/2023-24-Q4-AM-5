using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Block_Push : MonoBehaviour
{

    
    public GameObject player;
    public GameObject me;
    public void MoveMe()
    {
        if (push_button.dir==1)
        {
            
            me.transform.Translate(new Vector3(1,0,0));
        }
        if (push_button.dir == -1)
        {
            me.transform.Translate(new Vector3(-1, 0, 0));
        }
        if (push_button.dir == 2)
        {
            me.transform.Translate(new Vector3(0, 1, 0));
        }
        if (push_button.dir == -2)
        {
            me.transform.Translate(new Vector3(0, -1, 0));
        }
    }
}
