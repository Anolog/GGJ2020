using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int m_TurnCounter = 0;

    public int m_CurrentYear = 0;
    const int INITIAL_YEAR = 2020;
    const int FINAL_YEAR = 2030;
    

    // Use this for initialization
	void Start ()
    {
        m_CurrentYear = INITIAL_YEAR;
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
}
