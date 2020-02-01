using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int m_TurnCounter = 0;

    public int m_CurrentYear = 0;
    const int INITIAL_YEAR = 2019;
    const int FINAL_YEAR = 2030;

    public List<EventObject> m_EventList = new List<EventObject>();
    public EventObject m_CurrentEvent;

    public float m_CurrentGlobalTemperature = 1.15f;
    public long m_CurrentPopulation = 7800000000;
    public int m_CurrentHappiness = 50;

    public const float YEARLY_TEMP_INCREASE = 0.7f;

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

    public bool m_bIsChoiceBeingShown = false;
    public bool m_bIsConsequenceBeingShown = false;
    public bool m_bGameEnded = false;

    public TopDisplayUIManager m_TopUIManager;

    // Use this for initialization
	void Start ()
    {
        SetAllButtonsEnabled(false);

        m_CurrentYear = INITIAL_YEAR;

        SetUpTurn(m_EventList[0]);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if ((Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))&& m_bIsTextBeingShown == true)
        {
            m_DialogueManager.DisplayNextSentence();
        }
	}

    private void LateUpdate()
    {
        m_TopUIManager.UpdateHappinessText(m_CurrentHappiness);
        m_TopUIManager.UpdatePopulationText(m_CurrentPopulation);
        m_TopUIManager.UpdateTemperatureText(m_CurrentGlobalTemperature);
    }

    public void IncreaseCurrentYear()
    {
        m_CurrentYear++;
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

    public void SetUpTurn(EventObject aEvent)
    {
        IncreaseCurrentYear();

        if (m_CurrentYear > FINAL_YEAR)
        {
            PlayerWinned("Congradulations, you have made it to the year 2030!");
            return;
        }

        m_CurrentEvent = aEvent;

        SetAllButtonsEnabled(false);
        m_bIsTextBeingShown = true;
        m_bIsChoiceBeingShown = true;
        m_bIsConsequenceBeingShown = false;

        m_DialogueManager.StartDialogue(aEvent.m_EventText);
        //Create some sort of logic to put in random events into spots that don't have any

    }

    public void OnChoicePressed(int aButtonID)
    {
        m_bIsChoiceBeingShown = false;

        List<ConsequenceObject> consequences = m_CurrentEvent.m_ChoicesFromEvent[aButtonID].m_Consequences;

        int randomConsequence = Random.Range(0, consequences.Count);

        ConsequenceObject consequence = consequences[randomConsequence];

        ProcessConsequence(consequence);

    }

    public void ProcessConsequence(ConsequenceObject aConsequence)
    {
        SetAllButtonsEnabled(false);
        m_bIsConsequenceBeingShown = true;
        m_bIsTextBeingShown = true;
        m_DialogueManager.StartDialogue(aConsequence.m_ConsequenceText);

        m_CurrentGlobalTemperature += aConsequence.m_TempEffect;

        if (m_CurrentGlobalTemperature >= 5.0f)
        {
            m_CurrentGlobalTemperature = 5.0f;

            List<string> loseText = new List<string>();
            loseText.Add("You have lost.");
            loseText.Add("The average global temperature has hit 5.0C");
            loseText.Add("The effects around the globe are devistating.");
            loseText.Add("Ocean Acidity levels by 2050 will increase by over 50% killing large populations of oceanlife.");
            loseText.Add("All ice in the north and south pole have melted by 2050.");
            loseText.Add("The frequency of warm weather extremes over land has increased by 600% by 2050.");
            loseText.Add("The average drought length has become over 1 year.");
            loseText.Add("All land animals, plantation, and insects have lost more than half of their species.");
            loseText.Add("Damages from floods are at an annual cost of over 12 trillion dollars");

            PlayerFailed(loseText);
        }

        m_CurrentPopulation += aConsequence.m_PopulationEffect;

        if (m_CurrentPopulation <= 0)
        {
            m_CurrentPopulation = 0;

            List<string> loseText = new List<string>();
            loseText.Add("You have lost.");
            loseText.Add("Every human on Earth is dead.");

            PlayerFailed(loseText);
        }

        m_CurrentHappiness += aConsequence.m_HappinessEffect;

        if (m_CurrentHappiness <= 0)
        {
            m_CurrentHappiness = 0;

            List<string> loseText = new List<string>();
            loseText.Add("You have lost.");
            loseText.Add("The global population is unhappy with you and has kicked you out of office from decision making.");
        }

        if (m_CurrentHappiness >= 100)
        {
            m_CurrentHappiness = 100;
        }
    }

    public void OnDialogEnded()
    {
        m_bIsTextBeingShown = false;

        if (m_bGameEnded == true)
        {
            //Navigate to main menu
        }

        if (m_bIsConsequenceBeingShown)
        {
            SetUpTurn(m_EventList[Random.Range(0, m_EventList.Count)]);
            return;
        }

        if (m_bIsChoiceBeingShown)
        {
            List<ChoiceObject> choices = m_CurrentEvent.m_ChoicesFromEvent;

            EnableAmountOfChoices(choices.Count);

            for (int i = 0; i < choices.Count; i++)
            {
                m_ChoiceButtons[i].GetComponentInChildren<Text>().text = choices[i].m_ChoiceText;
            }
        }
    }

    public void PlayerFailed(List<string> aFailText)
    {
        m_bIsChoiceBeingShown = false;
        m_bIsConsequenceBeingShown = false;
        m_bIsTextBeingShown = true;
        m_bGameEnded = true;

        m_DialogueManager.StartDialogue(aFailText);
    }

    public void PlayerWinned(string aWinText)
    {
        m_bIsChoiceBeingShown = false;
        m_bIsConsequenceBeingShown = false;
        m_bIsTextBeingShown = true;
        m_bGameEnded = true;

        int score = 0;
        score += m_CurrentHappiness * 5;
        score += (int)(6354 / m_CurrentGlobalTemperature);
        score += (int)(m_CurrentPopulation / score);

        List<string> winText = CompileStatisticDataMessagesToSend();
        winText.Add(aWinText);
        winText.Add("Your score was: " + score + "!");
        winText.Add("Thank you for playing.");

        m_DialogueManager.StartDialogue(winText);
    }

    public List<string> CompileStatisticDataMessagesToSend()
    {
        List<string> returnMessages = new List<string>();

        if (m_CurrentGlobalTemperature < 0.0f)
        {
            returnMessages.Add("Somehow, you have made the Earth enter a cooling phase. The effects will benefit the Earth greatly.");
            returnMessages.Add("Just don't create another iceage...");
        }

        if (m_CurrentGlobalTemperature > 0.0f && m_CurrentGlobalTemperature < 1.0f)
        {
            returnMessages.Add("The Earth has decreased it's global temperature. This is benefitial to all life on Earth.");
        }

        if (m_CurrentGlobalTemperature > 1.0f && m_CurrentGlobalTemperature < 1.4f)
        {
            returnMessages.Add("The Earth's temperature has been kept stable. This however can change at any moment.");
            returnMessages.Add("The climate events we experienced throughout the late 2010's will continue happening.");
        }

        if (m_CurrentGlobalTemperature > 1.4f && m_CurrentGlobalTemperature < 1.9f)
        {
            returnMessages.Add("The Earth is still heating and this is not good, effects are being felt around the Earth at this temerature.");
            returnMessages.Add("If we stay in this range, the global sea level will increase by 48cm by 2100, and 59cm by 2300.");
            returnMessages.Add("By 2050, the ocean's acidity level will increase by 17%.");
            returnMessages.Add("The annual maximum daily temperature will increase by 1.7C with the amount of hot days increasing by 16%.");
            returnMessages.Add("The frequency of warm extremes over land will increase by 129%.");
            returnMessages.Add("The length of droughts will increase by an average of 2 months, with around 271 million people exposed to water scarcity.");
            returnMessages.Add("The average flood damage costs from sea level rises will be $10.2 trillion.");
        }

        if (m_CurrentGlobalTemperature > 2.0f && m_CurrentGlobalTemperature < 3.0f)
        {
            returnMessages.Add("The Earth has heated signifigantly, this is a terrible outcome.");
            returnMessages.Add("Sea levels will have risen by an average of 56cm by the year 2100.");
            returnMessages.Add("The acidity level of the sea has risen by 29% which is devistating to all ocean life.");
            returnMessages.Add("The probability of an ice free summer in the arctic has increased by 80%");
            returnMessages.Add("The annual maximum daily temperature will increase by 2.6C across the globe.");
            returnMessages.Add("The frequency of warm weather extremes over land will increase by 345%.");
            returnMessages.Add("The average length of droughts will increase by 4 months leaving about 388 million people exposed to water scarcity.");
        }

        if (m_CurrentGlobalTemperature > 3.0f)
        {
            returnMessages.Add("The Earth is still on a path for destruction, but you have done what you can to help avoid that for longer.");
            returnMessages.Add("These choices were not enough, and more needs to be considered to continue life on Earth.");
        }



        return returnMessages;
    }
}
