using UnityEngine;

public enum TutorialStep
{
    None,
    BuildSequence,
    SendSequence,
    ConfirmContact,
    Completed
}

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    [Header("Tutorial Settings")]
    [SerializeField] private bool startTutorialOnPlay = true;

    [Header("Tutorial State")]
    [SerializeField] private TutorialStep currentStep = TutorialStep.None;
    [SerializeField] private bool tutorialCompleted = false;

    public TutorialStep CurrentStep
    {
        get { return currentStep; }
    }

    public bool TutorialCompleted
    {
        get { return tutorialCompleted; }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (startTutorialOnPlay && !tutorialCompleted)
        {
            StartTutorial();
        }
    }

 public void StartTutorial()
{
    tutorialCompleted = false;
    symbolsEntered = 0;

    SetStep(TutorialStep.BuildSequence);

    Debug.Log("TUTORIAL: Drücke drei Symboltasten, um eine Sequenz zu bauen.");
}

private int symbolsEntered = 0;

public void NotifySymbolPressed()
{
    if (currentStep != TutorialStep.BuildSequence)
    {
        return;
    }

    symbolsEntered++;

    if (symbolsEntered < 3)
    {
        return;
    }

    SetStep(TutorialStep.SendSequence);

    Debug.Log("TUTORIAL: Gut. Sende die Sequenz jetzt mit Enter.");
}

    public void NotifyTransmissionSent()
    {
        if (currentStep != TutorialStep.SendSequence)
        {
            return;
        }

        SetStep(TutorialStep.ConfirmContact);

        Debug.Log("TUTORIAL: Warte auf die Antwort. Eine identische Antwort wird als Kontakt gespeichert.");
    }

    public void NotifyContactConfirmed()
    {
        if (currentStep != TutorialStep.ConfirmContact)
        {
            return;
        }

        CompleteTutorial();
    }

    public void CompleteTutorial()
    {
        tutorialCompleted = true;
        SetStep(TutorialStep.Completed);

        Debug.Log("TUTORIAL: Abgeschlossen. Ziel: Dokumentiere 5 bestätigte Kontakte vor Ende von Tag 15.");
    }

    public void ResetTutorial()
    {
        tutorialCompleted = false;
        SetStep(TutorialStep.None);

        Debug.Log("TUTORIAL: Zurückgesetzt.");
    }

    private void SetStep(TutorialStep newStep)
    {
        currentStep = newStep;
        Debug.Log("Tutorial Step: " + currentStep);
    }
}