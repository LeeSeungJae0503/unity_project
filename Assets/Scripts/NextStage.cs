using System.IO.Pipes;
using UnityEngine;
using UnityEngine.UIElements;

public class NextStage : MonoBehaviour
{
        [SerializeField]
        public float moveSpeed = 2f;
        public ScoreManager scoreManager;
        public GameObject MoveObject;

        public GameObject Stage1Road;
        public GameObject Sky;
        public GameObject constructor;
        public GameObject Spawn1;
        public GameObject Spawn2;

        public GameObject Stage2Road;
        public GameObject Stage2constructor;
        
        public GameObject Stage3Road;
        public GameObject Stage3BackGround;

        public GameObject Stage3spawn1;
        public GameObject Stage3spawn2;
        public GameObject Stage3spawn3;



        public Player player;
        

        void Update()
        {
            float PlayerPosition = player.transform.position.x;
            if (scoreManager.Score > 500f && MoveObject.transform.position.x < 43f)
            {
                
                Vector3 newposition = MoveObject.transform.position;
                newposition.x += moveSpeed * Time.deltaTime;
                MoveObject.transform.position = newposition;

                BackgroundScroll scrollScrpit1 = Sky.GetComponent<BackgroundScroll>();
                scrollScrpit1.scrollspeed = 0f;
                BackgroundScroll scrollScrpit2 = Stage1Road.GetComponent<BackgroundScroll>();
                scrollScrpit2.scrollspeed = 0f;
                BackgroundScroll scrollScrpit3 = constructor.GetComponent<BackgroundScroll>();
                scrollScrpit3.scrollspeed = 0f;
            }
            if (scoreManager.Score > 520f)
            {
                Spawner SpawnerScript1 = Spawn1.GetComponent<Spawner>();
                SpawnerScript1.Check = false;
                Spawner SpawnerScript2 = Spawn2.GetComponent<Spawner>();
                SpawnerScript2.Check = true;
            }

            if (MoveObject.transform.position.x > 42f)
            {
                BackgroundScroll scrollScrpit4 = Stage2constructor.GetComponent<BackgroundScroll>();
                scrollScrpit4.scrollspeed = 0.2f;
                BackgroundScroll scrollScrpit5 = Stage2Road.GetComponent<BackgroundScroll>();
                scrollScrpit5.scrollspeed = 0.3f;
            }

            if (scoreManager.Score > 1000f && MoveObject.transform.position.x < 67f)
            {
                
                Vector3 newposition = MoveObject.transform.position;
                newposition.x += moveSpeed * Time.deltaTime;
                MoveObject.transform.position = newposition;

                BackgroundScroll scrollScrpit2 = Stage2Road.GetComponent<BackgroundScroll>();
                scrollScrpit2.scrollspeed = 0f;
                BackgroundScroll scrollScrpit3 = Stage2constructor.GetComponent<BackgroundScroll>();
                scrollScrpit3.scrollspeed = 0f;
            }
            if (scoreManager.Score > 1020f)
            {
                Spawner SpawnerScript2 = Spawn2.GetComponent<Spawner>();
                SpawnerScript2.Check = false;
                Spawner SpawnerScript3 = Stage3spawn1.GetComponent<Spawner>();
                SpawnerScript3.Check = true;
                Spawner SpawnerScript4 = Stage3spawn2.GetComponent<Spawner>();
                SpawnerScript4.Check = true;
                Spawner SpawnerScript5 = Stage3spawn3.GetComponent<Spawner>();
                SpawnerScript5.Check = true;
            }
            if (MoveObject.transform.position.x > 65f)
            {
                BackgroundScroll scrollScrpit4 = Stage3BackGround.GetComponent<BackgroundScroll>();
                scrollScrpit4.scrollspeed = 0.2f;
                BackgroundScroll scrollScrpit5 = Stage3Road.GetComponent<BackgroundScroll>();
                scrollScrpit5.scrollspeed = 0.3f;
            }
        }

    
}
