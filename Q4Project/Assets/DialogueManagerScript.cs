using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DialogueManagerScript : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public GameObject player;
    public Animator animator;
    private Queue<string> sentences;

    // Start is called before the first frame update
    void Awake()
    {
        sentences = new Queue<string>();
        Image.sprite = characterSprite;
        Image.preserveAspect = true;
    }

    public void StartDialogue (DialogueScript dialogue)
    {
        player.GetComponent<PlayerCtrl>().movSpeed = 0;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    [SerializeField] private Sprite characterSprite;
    [SerializeField] private Image Image;

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        player.GetComponent<PlayerCtrl>().movSpeed = 10;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
