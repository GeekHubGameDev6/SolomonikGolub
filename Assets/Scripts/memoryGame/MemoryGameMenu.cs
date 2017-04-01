using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MemoryGame
{

    public class MemoryGameMenu : MonoBehaviour
    {
        public void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);

        }
        public void TriggerMenu(int i)
        {
            switch (i)
            {
                case (0):
                    SceneManager.LoadScene("Level");
                    break;
                case (1):
                    Application.Quit();
                    break;

            }
        }

    }
}
