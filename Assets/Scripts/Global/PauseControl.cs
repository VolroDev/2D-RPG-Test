using UnityEngine;

public class PauseControl : MonoBehaviour
{
    public bool gameIsPaused;
    public static void PauseGame()
    {
        Time.timeScale = 0;
        //gameIsPaused = true;
    }
    public static void ResumeGame()
    {
        Time.timeScale = 1;
        //gameIsPaused = false;
    }
}
