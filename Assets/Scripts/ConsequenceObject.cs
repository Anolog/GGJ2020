using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConsequenceObject", menuName = "Consequence")]
public class ConsequenceObject : ScriptableObject
{
    [TextArea(3, 10)]
    public List<string> m_ConsequenceText;

    public float m_TempEffect = 0;
    public int m_PopulationEffect = 0;
    public int m_HappinessEffect = 0;
}
