using UnityEngine;

public class LogbookPageController : MonoBehaviour
{
    [SerializeField] private GameObject[] pages;
    [SerializeField] private GameObject previousButton;
    [SerializeField] private GameObject nextButton;

    private int currentPage = 0;

    private void OnEnable()
    {
        ShowPage(0);
    }

    public void NextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            ShowPage(currentPage + 1);
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            ShowPage(currentPage - 1);
        }
    }

    private void ShowPage(int pageIndex)
    {
        currentPage = pageIndex;

        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == currentPage);
        }

        previousButton.SetActive(currentPage > 0);
        nextButton.SetActive(currentPage < pages.Length - 1);
    }
}