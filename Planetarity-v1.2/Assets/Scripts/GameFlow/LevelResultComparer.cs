namespace Planetarity
{
    using LevelBased;
    using UnityEngine;

    public class LevelResultComparer : MonoBehaviour
    {
        private LevelInstaller levelInstaller;
        private WindowsSpawner windowsSpawner;

        private bool isEnd = false;
        
        private void Awake()
        {
            levelInstaller = FindObjectOfType<LevelInstaller>();
            windowsSpawner = FindObjectOfType<WindowsSpawner>();
        }

        public void CheckWin()
        {
            if (isEnd) return;

            if (levelInstaller.enemies.Count <= 0)
            {
                windowsSpawner.SpawnWindow(windowsSpawner.victoryWindow);
                isEnd = true;
            }
        }

        public void Fail()
        {
            if (isEnd) return;

            windowsSpawner.SpawnWindow(windowsSpawner.failWindow);
            
            isEnd = true;
        }

        public void FailByInactive()
        {
            if (isEnd) return;

            windowsSpawner.SpawnWindow(windowsSpawner.failByInactiveWindow);

            isEnd = true;
        }
    }
}