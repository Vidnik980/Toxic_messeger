using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoad
{
    public static void ChangeScene(int number)
    { 
        SceneManager.LoadScene(number);
    }
    public static void Exit()
    {
        Application.Quit();
    }
}
