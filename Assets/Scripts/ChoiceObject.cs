using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChoiceObject", menuName = "Choice")]
public class ChoiceObject : ScriptableObject
{
    [TextArea(3, 10)]
    public string m_ChoiceText;

    public List<ConsequenceObject> m_Consequences;

    public bool m_bHasChoiceBeenChosen = false;
}
