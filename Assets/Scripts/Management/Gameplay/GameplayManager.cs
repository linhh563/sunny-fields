using UnityEngine;


namespace Management
{
    public class GameplayManager : MonoBehaviour
    {
        void Start()
        {
            // PauseGame();
        }


        public static void PauseGame()
        {
            Time.timeScale = 0f;
        }


        public static void ResumeGame()
        {
            Time.timeScale = 1;
        }
    }
}
