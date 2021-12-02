using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;
    bool hasBeenTriggered;
    public static bool activated;

    public void TriggerDialogue ()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<MarieController>();

        if (player && !hasBeenTriggered)
        {
            TriggerDialogue();
            hasBeenTriggered = true;
            activated = true;
        }
    }

}
