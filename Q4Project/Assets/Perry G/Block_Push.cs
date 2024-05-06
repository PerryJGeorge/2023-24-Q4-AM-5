using UnityEngine;

public class Block_Push : MonoBehaviour
{
    public GameObject player;
    public GameObject me;

    public void MoveMe()
    {
        if (push_button.dir == 1)
        {
            me.transform.Translate(Vector3.right);
        }
        else if (push_button.dir == -1)
        {
            me.transform.Translate(Vector3.left);
        }
        else if (push_button.dir == 2)
        {
            me.transform.Translate(Vector3.up);
        }
        else if (push_button.dir == -2)
        {
            me.transform.Translate(Vector3.down);
        }
    }
}
