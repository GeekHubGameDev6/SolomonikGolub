using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MemoryGame
{

    public class MemoryGameMenu : MonoBehaviour
    {
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
