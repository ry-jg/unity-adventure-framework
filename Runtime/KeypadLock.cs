using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class KeypadLock : MonoBehaviour
{
    public string correctCode = "1234";
    public int maxDigits = 4;

    public GameObject keypadScreen;
    public TMP_Text displayText;
    public Button[] digitButtons;
    public Button enterButton;
    public Button clearButton;
    public Button closeButton;

    public string wrongMessage = "Incorrect code.";
    public string correctMessage = "Access granted.";

    public UnityEvent OnUnlocked;

    private string currentInput = "";
    private bool solved = false;

    void Start()
    {
        // Set up button listeners
        for (int i = 0; i < digitButtons.Length; i++)
        {
            string digit = i.ToString();
            digitButtons[i].onClick.AddListener(() => PressDigit(digit));
        }

        // Set up action buttons
        if (enterButton != null) enterButton.onClick.AddListener(Submit);
        if (clearButton != null) clearButton.onClick.AddListener(Clear);
        if (closeButton != null) closeButton.onClick.AddListener(Close);

        keypadScreen.SetActive(false);
        UpdateDisplay();
    }

    public void OpenKeypad()
    {
        // If already solved, just show the message
        if (solved)
        {
            NotificationManager.Instance.Show(correctMessage);
            return;
        }
        currentInput = "";
        UpdateDisplay();
        ScreenManager.Instance.OpenScreen(keypadScreen);
    }

    void PressDigit(string digit)
    {
        if (currentInput.Length >= maxDigits) return;
        currentInput += digit;
        UpdateDisplay();
    }

    void Submit()
    {
        // Check if the entered code is correct
        if (currentInput == correctCode)
        {
            solved = true;
            NotificationManager.Instance.Show(correctMessage);
            Close();
            OnUnlocked.Invoke();
        }
        else
        {
            NotificationManager.Instance.Show(wrongMessage);
            currentInput = "";
            UpdateDisplay();
        }
    }

    void Clear()
    {
        currentInput = "";
        UpdateDisplay();
    }

    void Close()
    {
        ScreenManager.Instance.CloseScreen();
    }

    void UpdateDisplay()
    {
        if (displayText != null)
            displayText.text = currentInput.PadRight(maxDigits, '_');
    }
}
