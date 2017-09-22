using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets
{
    public enum DayTime
    {
        DAY,
        NIGHT
    }

    class Game : MonoBehaviour
    {
        public static DayTime dayTime = DayTime.DAY;
        public static string Seed;

        public Transform characterPrefab;
        public GameObject bunnyPrefab;
        public GameObject ghostPrefab;
        public LevelProcessor lpObj;
        public Transform characterTransform;
        private int skyboxIndex = 0;
        private float lastXPosi = 0;
        AudioSource audio;
        AudioClip acSundown;
        AudioClip acDoD;
        AudioClip acOutcast;
        AudioClip acMorning;
        GameObject lt;
        List<int> materialIndexes = new List<int>();
        Texture[] textures1 = new Texture[4];
        Texture[] textures2 = new Texture[4];


        public void Start()
        {
            Seed = System.Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            Random.InitState(Seed.GetHashCode());

            lt = GameObject.Find("Directional Light");
            textures1[0] = Resources.Load("dirt stones") as Texture;
            textures2[0] = Resources.Load("leafs dark") as Texture;
            textures1[1] = Resources.Load("dirt stones eary yellow") as Texture;
            textures2[1] = Resources.Load("dirt stones leafs more") as Texture;
            textures1[2] = Resources.Load("grey dirt stones") as Texture;
            textures2[2] = Resources.Load("granit grass") as Texture;
            textures1[3] = Resources.Load("dirt stones") as Texture;
            textures2[3] = Resources.Load("sand stones") as Texture;

            int rnd = (int)(Random.Range(0f, 0.3f) * 10);
            Ground.texture1 = textures1[rnd];
            Ground.texture2 = textures2[rnd];

            #region LevelProcessor

            GameObject levelProcessor = new GameObject();
            lpObj = levelProcessor.AddComponent<LevelProcessor>();
            lpObj.ProcessLevel(bunnyPrefab, ghostPrefab);

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

            if (characterTransform.position.x >= (lpObj.columns * lpObj.entireLevelSectionCount - 20))
            {
                EventManager.GetInstance().PublishEvent(new GenerateSectionEvent());
                lpObj.ProcessLevel(bunnyPrefab, ghostPrefab);

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
                lt.GetComponent<Light>().intensity = 0.5f;
                int rnd = (int)(Random.Range(0f, 0.3f) * 10);
                Ground.texture1 = textures1[rnd];
                Ground.texture2 = textures2[rnd];
            }
            if (characterTransform.position.x >= 400 + lastXPosi && characterTransform.position.x <= 405 + lastXPosi)
            {
                Material m = Resources.Load("CloudyCrownEvening", typeof(Material)) as Material;
                Debug.Log("Matieral-Name: " + m.name);
                RenderSettings.skybox = m;
                lt.GetComponent<Light>().intensity = 0.3f;
                int rnd = (int)(Random.Range(0f, 0.3f) * 10);
                Ground.texture1 = textures1[rnd];
                Ground.texture2 = textures2[rnd];
            }
            if (characterTransform.position.x >= 600 + lastXPosi && characterTransform.position.x <= 605 + lastXPosi)
            {
                dayTime = DayTime.NIGHT;

                audio.clip = acDoD;
                audio.Play();
                Material m = Resources.Load("CloudyCrownMidnight", typeof(Material)) as Material;
                Debug.Log("Matieral-Name: " + m.name);
                RenderSettings.skybox = m;
                lt.GetComponent<Light>().intensity = 0.0f;
                int rnd = (int)(Random.Range(0f, 0.3f) * 10);
                Ground.texture1 = textures1[rnd];
                Ground.texture2 = textures2[rnd];
            }

            if (characterTransform.position.x >= 800 + lastXPosi && characterTransform.position.x <= 805 + lastXPosi)
            {
                dayTime = DayTime.DAY;

                audio.clip = acOutcast;
                audio.Play();
                Material m = Resources.Load("CloudyCrownDaybreak", typeof(Material)) as Material;
                Debug.Log("Matieral-Name: " + m.name);
                RenderSettings.skybox = m;
                lt.GetComponent<Light>().intensity = 0.6f;
                int rnd = (int)(Random.Range(0f, 0.3f) * 10);
                Ground.texture1 = textures1[rnd];
                Ground.texture2 = textures2[rnd];
            }
            if (characterTransform.position.x >= 1000 + lastXPosi && characterTransform.position.x <= 1005 + lastXPosi)
            {
                audio.clip = acMorning;
                audio.Play();
                Material m = Resources.Load("CloudyCrownMidday", typeof(Material)) as Material;
                Debug.Log("Matieral-Name: " + m.name);
                RenderSettings.skybox = m;
                lastXPosi = characterTransform.position.x;
                lt.GetComponent<Light>().intensity = 1f;
                fillMaterialIndex();
                int rnd = (int)(Random.Range(0f, 0.3f) * 10);
                Ground.texture1 = textures1[rnd];
                Ground.texture2 = textures2[rnd];
            }
        }

        private void fillMaterialIndex()
        {
            for (int i = 0; i < materialIndexes.Count - 1; i++)
                materialIndexes[i] = i;
        }
    }
}