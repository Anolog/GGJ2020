using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int m_TurnCounter = 0;

    public int m_CurrentYear = 0;
    const int INITIAL_YEAR = 2020;
    const int FINAL_YEAR = 2030;

    public List<EventObject> m_EventList = new List<EventObject>();

    public float m_CurrentGlobalTemperature = 1.15f;
    public long m_CurrentPopulation = 7800000000;
    public int m_CurrentHappiness = 50;

    const float MAX_GLOBAL_TEMP = 5.0f;
    const int MAX_HAPPINESS = 100;
    const int MIN_HAPPINESS = 0;
    const int MIN_POPULATION = 0;

    public DIalogueManager m_DialogueManager;

    public Canvas m_TextBoxCanvas;
    public Text m_TextBoxText;

    public List<string> TEST_TEXT = new List<string>();

    public bool m_bIsTextBeingShown = false;

    public Button[] m_ChoiceButtons = new Button[4];

    // Use this for initialization
	void Start ()
    {
        SetAllButtonsEnabled(false);

        m_CurrentYear = INITIAL_YEAR;
        //ProcessEvent();

        for (int i = 0; i < 10; i++)
        {
            int randomTextAmount = Random.Range(0, 40);

            TEST_TEXT.Add("TEST TEXT MESSAGE:  " + i + " - HASH CODE FOR SPACE " + randomTextAmount.GetHashCode().ToString());
        }

	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyUp(KeyCode.Space) && m_bIsTextBeingShown == true)
        {
            m_DialogueManager.DisplayNextSentence();


        }
	}

    public void IncreaseCurrentYear()
    {
        m_TurnCounter++;
        m_CurrentYear += m_TurnCounter;
    }

    public void ProcessEventTest()
    {
        //Change this to effect the UI later instead of just the debug for now

        int randomIndex = Random.Range(0, m_EventList.Count -1);

        EventObject randomEvent = m_EventList[randomIndex];

        Debug.Log(randomEvent.m_EventText);
        Debug.Log("Choices are: ");

        for (int i = 0; i < randomEvent.m_ChoicesFromEvent.Count; i++)
        {
            Debug.Log(randomEvent.m_ChoicesFromEvent[i].m_ChoiceText);

            for (int k = 0; k < randomEvent.m_ChoicesFromEvent[i].m_Consequences.Count; k++)
            {
                Debug.Log("Consequences for choice are: ");
                Debug.Log(randomEvent.m_ChoicesFromEvent[i].m_Consequences[k].m_ConsequenceText);
            }
        }

    }


    public void SetAllButtonsEnabled(bool bEnabled)
    {
        for (int i = 0; i < m_ChoiceButtons.Length; i++)
        {
            //m_ChoiceButtons[i].enabled = bEnabled;
            m_ChoiceButtons[i].gameObject.SetActive(bEnabled);
        }
    }

    public void EnableAmountOfChoices(int aAmount)
    {
        if (aAmount > m_ChoiceButtons.Length)
        {
            return;
        }

        else
        {
            SetAllButtonsEnabled(false);

            for (int i = 0; i < aAmount; i++)
            {
                m_ChoiceButtons[i].gameObject.SetActive(true);
            }
        }
    }

    public void SetChoices()
    {
        
    }
}
