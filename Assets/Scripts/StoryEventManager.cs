using System.Collections.Generic;
using UnityEngine;

public enum StoryEventType
{
    FirstContact,
    DisturbanceStarts,
    RadarContact,
    HallucinationPhase,
    FinalDiscovery,
    DayFive,
    FinalDay
}

public class StoryEventManager : MonoBehaviour
{
    public static StoryEventManager Instance;

    private HashSet<StoryEventType> triggeredEvents = new HashSet<StoryEventType>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void CheckStoryEvents(int currentDay, int confirmedContacts)
    {
        if (currentDay >= 5)
        {
            TriggerEventOnce(StoryEventType.DayFive);
        }

        if (currentDay >= 15)
        {
            TriggerEventOnce(StoryEventType.FinalDay);
        }

        if (confirmedContacts >= 1)
        {
            TriggerEventOnce(StoryEventType.FirstContact);
        }

        if (confirmedContacts >= 5)
        {
            TriggerEventOnce(StoryEventType.DisturbanceStarts);
        }

        if (confirmedContacts >= 10)
        {
            TriggerEventOnce(StoryEventType.RadarContact);
        }

        if (confirmedContacts >= 15)
        {
            TriggerEventOnce(StoryEventType.HallucinationPhase);
        }

        if (confirmedContacts >= 20)
        {
            TriggerEventOnce(StoryEventType.FinalDiscovery);
        }
    }

    private void TriggerEventOnce(StoryEventType storyEvent)
    {
        if (triggeredEvents.Contains(storyEvent))
        {
            return;
        }

        triggeredEvents.Add(storyEvent);

        Debug.Log("STORY EVENT ausgelöst: " + storyEvent);

        // Hier können später andere Systeme andocken:
        // AudioManager, DistortionManager, RadarEventManager, UI, Dialoge usw.
    }
}