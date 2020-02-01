using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChoiceObject", menuName = "Choice")]
public class ChoiceObject : ScriptableObject
{
    [TextArea(3, 10)]
    public List<string> m_ChoiceText = new List<string>();

    public List<ConsequenceObject> m_Consequences;
}
