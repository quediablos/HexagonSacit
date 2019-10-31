using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HexagonSacit
{
    public class UIGameOverController : MonoBehaviour
    {
        public Text textReason;

        void Start()
        {
            textReason.text = Global.reasonGameOver;
        }

        
        void Update()
        {

        }

        public void onPlayAgain()
        {
            SceneManager.LoadScene(Constants.SCENE_MAP);
        }
    }
}

