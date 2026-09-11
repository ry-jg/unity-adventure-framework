using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class CombinationLock : MonoBehaviour
{
    public int[] correctCode = { 1, 2, 3 };

    public GameObject lockScreen;
    public TMP_Text[] dialDisplays;
    public Button[] upButtons;
    public Button[] downButtons;
    public Button submitButton;
    public Button closeButton;

    public string lockedMessage = "The lock won't budge.";
    public string wrongMessage = "That's not right.";
    public string correctMessage = "The lock clicks open!";

    public UnityEvent OnUnlocked;

    private int[] currentValues;
    private bool solved = false;

    void Start()
    {
        currentValues = new int[correctCode.Length];

        for (int i = 0; i < dialDisplays.Length; i++)
            dialDisplays[i].text = "0";

        // Set up button listeners
        for (int i = 0; i < correctCode.Length; i++)
        {
            int index = i;
            if (i < upButtons.Length && upButtons[i] != null)
                upButtons[i].onClick.AddListener(() => Increment(index));
            if (i < downButtons.Length && downButtons[i] != null)
                downButtons[i].onClick.AddListener(() => Decrement(index));
        }

        if (submitButton != null)
            submitButton.onClick.AddListener(Submit);
        if (closeButton != null)
            closeButton.onClick.AddListener(Close);

        lockScreen.SetActive(false);
    }

    public void OpenLock()
    {
        // If already solved, just show the message
        if (solved)
        {
            NotificationManager.Instance.Show(correctMessage);
            return;
        }
        ScreenManager.Instance.OpenScreen(lockScreen);
    }

    void Increment(int index)
    {
        currentValues[index] = (currentValues[index] + 1) % 10;
        dialDisplays[index].text = currentValues[index].ToString();
    }

    void Decrement(int index)
    {
        currentValues[index] = (currentValues[index] + 9) % 10;
        dialDisplays[index].text = currentValues[index].ToString();
    }

    void Submit()
    {
        // Check if the current code matches the correct code
        for (int i = 0; i < correctCode.Length; i++)
        {
            if (currentValues[i] != correctCode[i])
            {
                NotificationManager.Instance.Show(wrongMessage);
                return;
            }
        }

        // Code is correct
        solved = true;
        NotificationManager.Instance.Show(correctMessage);
        Close();
        OnUnlocked.Invoke();
    }

    void Close()
    {
        ScreenManager.Instance.CloseScreen();
    }
}
