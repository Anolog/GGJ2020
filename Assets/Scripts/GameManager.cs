using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    

    // Use this for initialization
	void Start ()
    {
        m_CurrentYear = INITIAL_YEAR;
        ProcessEvent();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void IncreaseCurrentYear()
    {
        m_TurnCounter++;
        m_CurrentYear += m_TurnCounter;
    }

    public void ProcessEvent()
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
}
