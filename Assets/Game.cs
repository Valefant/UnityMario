using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets
{
    class Game : MonoBehaviour
    {
        public Transform characterPrefab;
        public LevelProcessor lpObj;
        public Transform characterTransform;
        private int skyboxIndex = 0;
        private float lastXPosi = 0;
        AudioSource audio;
        AudioClip acSundown;
        AudioClip acDoD;
        AudioClip acOutcast;
        AudioClip acMorning;





        public void Start()
        {
            gameObject.AddComponent<DisplaySeed>();
            
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
            sCC.m_moveSpeed = 5;
            characterTransform.position = lpObj.startingPosition;
            characterTransform.rotation = Quaternion.AngleAxis(90, new Vector3(0, 1, 0));
            Debug.Log("starting position = " + lpObj.startingPosition);

            Camera camera = this.GetComponent<Camera>();

            camera.transform.parent = characterTransform.transform;
            Vector3 localPosition = camera.transform.localPosition;
            localPosition.x = 10;
            camera.transform.localPosition = localPosition;

            #endregion

            #region Skybox and Music

            Material m = Resources.Load("CloudyCrownMidday", typeof(Material)) as Material;
            Debug.Log("Matieral-Name: " + m.name);
            RenderSettings.skybox = m;

            audio = gameObject.AddComponent<AudioSource>();
            AudioClip vv = Resources.Load("Songs/Morning") as AudioClip;
            acSundown = Resources.Load("Songs/Sundown") as AudioClip;
            acDoD = Resources.Load("Songs/Dance Of Death") as AudioClip;
            acOutcast = Resources.Load("Songs/Outcast") as AudioClip;
            acMorning = Resources.Load("Songs/Morning") as AudioClip;

            audio.clip = vv;
            audio.loop = true;
            audio.volume = 0.1f;
            audio.Play();


            #endregion
        }

        void Update()
        {           
            if (characterTransform.position.y <= 0)
            {
                SceneManager.LoadScene("Hub");
                EventManager.GetInstance().PublishEvent(new PickupEvent(0));
                SimpleCharacterControl.canMove = false;
            }

            if (characterTransform.position.x >= (lpObj.columns * lpObj.levelCount - 20))
            {
                EventManager.GetInstance().PublishEvent(new GenerateSectionEvent());
                lpObj.ProcessLevel();

                if (lpObj.levelSections.Count >= 6)
                {
                    foreach (List<GameObject> level in lpObj.levelSections.GetRange(0, 2))
                    {
                        foreach (GameObject gameObject in level)
                        {
                            Destroy(gameObject);
                        }
                    }

                    lpObj.levelSections.RemoveRange(0, 2);
                }
            }

            if (characterTransform.position.x >= 200 + lastXPosi && characterTransform.position.x <= 205 + lastXPosi)
            {
                audio.clip = acSundown;
                audio.Play();
                Material m = Resources.Load("CloudyCrownSunset", typeof(Material)) as Material;
                Debug.Log("Matieral-Name: " + m.name);
                RenderSettings.skybox = m;
            }
            if (characterTransform.position.x >= 400 + lastXPosi && characterTransform.position.x <= 405 + lastXPosi)
            {
                Material m = Resources.Load("CloudyCrownEvening", typeof(Material)) as Material;
                Debug.Log("Matieral-Name: " + m.name);
                RenderSettings.skybox = m;
            }
            if (characterTransform.position.x >= 600 + lastXPosi && characterTransform.position.x <= 605 + lastXPosi)
            {
                audio.clip = acDoD;
                audio.Play();
                Material m = Resources.Load("CloudyCrownMidnight", typeof(Material)) as Material;
                Debug.Log("Matieral-Name: " + m.name);
                RenderSettings.skybox = m;
            }

            if (characterTransform.position.x >= 800 + lastXPosi && characterTransform.position.x <= 805 + lastXPosi)
            {
                audio.clip = acOutcast;
                audio.Play();
                Material m = Resources.Load("CloudyCrownDaybreak", typeof(Material)) as Material;
                Debug.Log("Matieral-Name: " + m.name);
                RenderSettings.skybox = m;
            }
            if (characterTransform.position.x >= 1000 + lastXPosi && characterTransform.position.x <= 1005 + lastXPosi)
            {
                audio.clip = acMorning;
                audio.Play();
                Material m = Resources.Load("CloudyCrownMidday", typeof(Material)) as Material;
                Debug.Log("Matieral-Name: " + m.name);
                RenderSettings.skybox = m;
                lastXPosi = characterTransform.position.x;
            }
        }
    }
}