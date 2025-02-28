using UnityEngine;

public class ExitMiniGame : MonoBehaviour
{
  public void Exit()
    {
        Debug.Log("Exiting Mini Game...");

        #if UNITY_EDITOR
                // Stop Play Mode in the Unity Editor
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    // Quit the application in a built APK
                    Application.Quit();
        #endif
    }
}
