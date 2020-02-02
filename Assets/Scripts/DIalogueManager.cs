using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DIalogueManager : MonoBehaviour
{
    public GameManager m_GameManagerRef;

    public Queue<string> m_SentenceQueue;

    public Text m_Dialogue;
    public bool m_bHasFinishedText = false;

	// Use this for initialization
	void Start ()
    {
        m_SentenceQueue = new Queue<string>();
	}

    public void StartDialogue(List<string> aDialogue)
    {
        if (m_SentenceQueue == null)
        {
            m_SentenceQueue = new Queue<string>();
        }

        m_SentenceQueue.Clear();

        m_bHasFinishedText = false;

        foreach (string sentence in aDialogue)
        {
            m_SentenceQueue.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (m_SentenceQueue.Count == 0)
        {
            m_bHasFinishedText = true;
            EndDialogue();
            return;
        }

        string sentence = m_SentenceQueue.Dequeue();
        m_Dialogue.text = sentence;
    }

    public void EndDialogue()
    {
        m_Dialogue.text = "";

        m_GameManagerRef.OnDialogEnded();
    }
}
