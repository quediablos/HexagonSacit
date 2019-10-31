using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace HexagonSacit
{
    public class UIMapHud : MonoBehaviour
    {
        public Text textScore;
        public Controller controller;

        void Start()
        {

        }


        void Update()
        {

        }

        private void LateUpdate()
        {
            textScore.text = "Score: " + controller.score;
        }
    }
}

