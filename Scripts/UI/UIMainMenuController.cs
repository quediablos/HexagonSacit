using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace HexagonSacit
{
    public class UIMainMenuController : MonoBehaviour
    {


        void Start()
        {

        }


        void Update()
        {

        }

        public void onStartPlaying()
        {
            SceneManager.LoadScene(Constants.SCENE_MAP);
        }
    }
}

