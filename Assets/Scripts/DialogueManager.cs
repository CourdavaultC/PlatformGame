
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    // Variable type liste d'attente
    private Queue<string> sentences;

    public static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de DialogueManager dans la scène");
            return;
        }

        instance = this;

        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);
        nameText.text = dialogue.name;

        // Pour être sûre que la file d'attente est bien vide.
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            // On envoie l'élément dans la file d'attente
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        // On récupère le prochaine élément de la file d'attente.
        string sentence = sentences.Dequeue();
        
        // S'assure que l'écriture en cours s'arrête.
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    // Effet machine à écrire
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
    }
}
