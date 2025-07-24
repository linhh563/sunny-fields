using UnityEngine;

namespace Management
{
    public class MainMenuManager : MonoBehaviour
    {
        public static void ExitGame()
        {
            // Application.Quit();

            // test quit game in unity editor
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
