using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopDisplayUIManager : MonoBehaviour
{
    public GameObject m_TemperatureText;
    public GameObject m_PopulationText;
    public GameObject m_HappinessText;


    public void UpdateTemperatureText(float aTemperature)
    {
        m_TemperatureText.GetComponent<Text>().text = "Avg. Global Temperature: " + aTemperature + "C";
    }

    public void UpdatePopulationText(long aPopulation)
    {
        m_PopulationText.GetComponent<Text>().text = "Global Population: " + aPopulation;
    }

    public void UpdateHappinessText(int aHappiness)
    {
        m_HappinessText.GetComponent<Text>().text = "Happiness: " + aHappiness + "%";
    }
}
