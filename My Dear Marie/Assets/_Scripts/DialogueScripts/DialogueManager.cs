using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;
    public Image portraitImage;

	public Animator animator;

	private Queue<Sentence> sentences;

	// Use this for initialization
	void Start () {
		sentences = new Queue<Sentence>();
	}

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) DisplayNextSentence();
    }

    public void StartDialogue (Dialogue dialogue)
	{
		animator.SetBool("IsOpen", true);

		//nameText.text = dialogue.name;

		sentences.Clear();

		foreach (Sentence sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
            DialogueTrigger.activated = false;
            return;
		}

		Sentence sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (Sentence sentence)
	{
        if (sentence.character != null)
        {
            nameText.text = sentence.character.name;
            portraitImage.sprite = sentence.character.characterPortrait;
        }

		dialogueText.text = "";
		foreach (char letter in sentence.line.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
	}

}
