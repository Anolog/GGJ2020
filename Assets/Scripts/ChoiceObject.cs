using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChoiceObject", menuName = "Choice")]
public class ChoiceObject : ScriptableObject
{
    public string m_ChoiceText = "";

    public List<ConsequenceObject> m_Consequences = new List<ConsequenceObject>();
}
