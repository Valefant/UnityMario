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
            sCC.m_jumpForce = lpObj.maxJumpHeight * 3; // TODO: Find right multiply and maybe addition
            sCC.m_moveSpeed = 4;
            go.position = lpObj.startingPosition;
            go.rotation = Quaternion.AngleAxis(90, new Vector3(0,1,0));
            Debug.Log("starting position = " + lpObj.startingPosition);

			Camera camera = this.GetComponent<Camera> ();

			camera.transform.parent = go.transform;
			Vector3 localPosition = camera.transform.localPosition;
			localPosition.x = 10;
			camera.transform.localPosition = localPosition;
			Debug.Log("ctp: " + camera.transform.position);
			#endregion
        }
    }
}
