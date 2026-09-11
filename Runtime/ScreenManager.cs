using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance;

    private GameObject currentScreen = null;

    void Awake()
    {
        Instance = this;
    }

    public void OpenScreen(GameObject screen)
    {
        // If screen is already open, do nothing
        if (currentScreen != null)
            currentScreen.SetActive(false);

        currentScreen = screen;
        currentScreen.SetActive(true);
    }

    public void CloseScreen()
    {
        if (currentScreen != null)
        {
            currentScreen.SetActive(false);
            currentScreen = null;
        }
    }

    public bool IsScreenOpen()
    {
        return currentScreen != null;
    }
}
