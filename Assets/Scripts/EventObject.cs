using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventObject", menuName = "Event")]
public class EventObject : ScriptableObject
{
    public List<ChoiceObject> m_ChoicesFromEvent = new List<ChoiceObject>();

    [TextArea(3, 10)]
    public List<string> m_EventText;

    public Sprite m_EventImage;

}
