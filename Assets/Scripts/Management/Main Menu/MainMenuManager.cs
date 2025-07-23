using UnityEngine;

namespace Management
{
    public class MainMenuManager : MonoBehaviour
    {
        public static void ExitGame()
        {
            Debug.Log("Exit game.");
            Application.Quit();
        }
    }
}
