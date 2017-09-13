using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets
{
    class Game : MonoBehaviour
    {

        public Transform characterPrefab;
        void Start()
        {
            Debug.Log("---------- Start ----------");
            Debug.Log("----                   ----");

            #region LevelProcessor
            GameObject levelProcessor = new GameObject();
            var lpObj = levelProcessor.AddComponent<LevelProcessor>();
            lpObj.ProcessLevel();
            #endregion

			transform.position = lpObj.startingPosition;

            #region Character
            var go = Instantiate(characterPrefab, transform.position, transform.rotation);
            SimpleCharacterControl sCC = go.GetComponent<SimpleCharacterControl>();
            sCC.m_jumpForce = lpObj.maxJumpHeight * 2; // TODO: Find right multiply and maybe addition
            #endregion
        }
    }
}
