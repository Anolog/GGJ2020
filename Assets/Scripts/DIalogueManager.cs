using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DIalogueManager : MonoBehaviour
{
    public Queue<string> m_SentenceQueue;

    public Text m_Dialogue;

	// Use this for initialization
	void Start ()
    {
        m_SentenceQueue = new Queue<string>();
	}

    public void StartDialogue(List<string> aDialogue)
    {
        m_SentenceQueue.Clear();

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
            EndDialogue();
            return;
        }

        string sentence = m_SentenceQueue.Dequeue();
        m_Dialogue.text = sentence;
    }

    public void EndDialogue()
    {
        m_Dialogue.text = "";
    }
}
