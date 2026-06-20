using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HallucinationUI : MonoBehaviour
{
    [SerializeField] private GameObject[] hallucinationImages;

    private Dictionary<int, GameObject> scheduledHallucinations =
        new Dictionary<int, GameObject>();

    private int lastShownDay = -1;
    private bool hallucinationScheduled = false;

    private void Start()
    {
        foreach (GameObject image in hallucinationImages)
        {
            if (image != null)
                image.SetActive(false);
        }

        GenerateRandomDays();
    }

    private void Update()
    {
        if (GameManager.Instance == null)
            return;

        int currentDay = GameManager.Instance.CurrentDay;

        if (currentDay == lastShownDay)
            return;

        if (scheduledHallucinations.ContainsKey(currentDay) &&
            !hallucinationScheduled)
        {
            hallucinationScheduled = true;

            StartCoroutine(
                DelayedHallucination(
                    scheduledHallucinations[currentDay],
                    currentDay));
        }
    }

    private void GenerateRandomDays()
    {
        List<int> availableDays = new List<int>();

        for (int i = 1; i <= 15; i++)
            availableDays.Add(i);

        foreach (GameObject image in hallucinationImages)
        {
            if (image == null)
                continue;

            int randomIndex =
                Random.Range(0, availableDays.Count);

            int chosenDay =
                availableDays[randomIndex];

            availableDays.RemoveAt(randomIndex);

            scheduledHallucinations.Add(
                chosenDay,
                image);
        }
    }

    private IEnumerator DelayedHallucination(
        GameObject image,
        int day)
    {
        float delay =
            Random.Range(15f, 45f);

        yield return new WaitForSeconds(delay);

        lastShownDay = day;

        yield return StartCoroutine(
            ShowHallucination(image));

        hallucinationScheduled = false;
    }

    private IEnumerator ShowHallucination(GameObject image)
    {
        image.SetActive(true);

        yield return new WaitForSeconds(3f);

        image.SetActive(false);
    }
}