using System;
using System.Collections.Generic;
using UnityEngine;

public enum StoryEventType
{
    DayFive,
    FirstContact,
    DisturbanceStarts,
    RadarContact,
    HallucinationPhase,
    FinalDiscovery,
    FinalDay
}

public class StoryEventManager : MonoBehaviour
{
    public static StoryEventManager Instance;

    private HashSet<StoryEventType> triggeredEvents = new HashSet<StoryEventType>();

    public event Action<StoryEventType> OnStoryEventTriggered;

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
        CheckDayEvents(currentDay);
        CheckContactEvents(confirmedContacts);
    }

    private void CheckDayEvents(int currentDay)
    {
        if (currentDay >= 5)
        {
            TriggerEventOnce(StoryEventType.DayFive);
        }

        if (currentDay >= 15)
        {
            TriggerEventOnce(StoryEventType.FinalDay);
        }
    }

    private void CheckContactEvents(int confirmedContacts)
    {
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

        OnStoryEventTriggered?.Invoke(storyEvent);
    }

    public bool HasEventTriggered(StoryEventType storyEvent)
    {
        return triggeredEvents.Contains(storyEvent);
    }

    public void ResetStoryEvents()
    {
        triggeredEvents.Clear();
        Debug.Log("Story Events wurden zurückgesetzt.");
    }
}