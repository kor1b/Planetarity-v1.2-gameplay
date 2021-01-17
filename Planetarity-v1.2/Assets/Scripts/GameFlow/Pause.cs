namespace Planetarity
{
    using UnityEngine;

    public class Pause : MonoBehaviour
    {
        private bool isPaused = false;

        public void TogglePause()
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }

        private void PauseGame()
        {
            isPaused = true;
            Time.timeScale = 0;
        }

        private void ResumeGame()
        {
            isPaused = false;
            Time.timeScale = 1;
        }
    }
}