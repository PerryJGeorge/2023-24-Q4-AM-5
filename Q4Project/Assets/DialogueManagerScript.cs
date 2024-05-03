using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class DialogueManagerScript : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public GameObject player;
    public Animator animator;
    private Queue<string> sentences;
    [SerializeField] private List<Sprite> profilePictures; // List of profile pictures for each character
    [SerializeField] private Image image;

    // Start is called before the first frame update
    void Awake()
    {
        sentences = new Queue<string>();
        image.preserveAspect = true;
    }

    public void StartDialogue(DialogueScript dialogue, int characterIndex)
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

        // Set the profile picture based on the character index
        if (characterIndex >= 0 && characterIndex < profilePictures.Count)
        {
            image.sprite = profilePictures[characterIndex];
        }

        DisplayNextSentence();
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

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
        player.GetComponent<PlayerCtrl>().enabled = true;
        player.GetComponent<PlayerCtrl>().movSpeed = 10;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
