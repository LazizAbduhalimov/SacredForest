using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Client.Game
{
    public class Restarter : MonoBehaviour
    {
        public void Restart()
        {
            SceneManager.LoadScene(0);
        }
    }
}
