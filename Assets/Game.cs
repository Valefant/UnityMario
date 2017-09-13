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
		public LevelProcessor lpObj;
		public Transform characterTransform;


        void Start()
        {
            Debug.Log("---------- Start ----------");
            Debug.Log("----                   ----");

            #region LevelProcessor
            GameObject levelProcessor = new GameObject();
            lpObj = levelProcessor.AddComponent<LevelProcessor>();
            lpObj.ProcessLevel();
            #endregion

			transform.position = lpObj.startingPosition;

            #region Character
			characterTransform = Instantiate(characterPrefab, transform.position, transform.rotation);
			SimpleCharacterControl sCC = characterTransform.GetComponent<SimpleCharacterControl>();
            sCC.m_jumpForce = lpObj.maxJumpHeight * 3; // TODO: Find right multiply and maybe addition
            sCC.m_moveSpeed = 4;
			characterTransform.position = lpObj.startingPosition;
			characterTransform.rotation = Quaternion.AngleAxis(90, new Vector3(0,1,0));
            Debug.Log("starting position = " + lpObj.startingPosition);

			Camera camera = this.GetComponent<Camera> ();

			camera.transform.parent = characterTransform.transform;
			Vector3 localPosition = camera.transform.localPosition;
			localPosition.x = 10;
			camera.transform.localPosition = localPosition;
            #endregion

            #region Skybox and Music
            Material m = Resources.Load("CloudyCrownMidday", typeof(Material)) as Material;
            Debug.Log("Matieral-Name: " + m.name);
            RenderSettings.skybox = m;

            AudioSource audio = gameObject.AddComponent<AudioSource>();
            AudioClip vv = Resources.Load("Songs/Super Mario Bros. medley") as AudioClip;
            audio.PlayOneShot(vv);
            #endregion
        }

        void Update()
		{
			Debug.Log ("Character Position: " + characterTransform.position);

			if (characterTransform.position.x >= (lpObj.columns * lpObj.levelCount * 0.75))
			{
				lpObj.ProcessLevel();
			}
		}
    }
}