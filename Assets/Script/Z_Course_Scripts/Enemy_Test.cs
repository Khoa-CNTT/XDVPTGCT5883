using UnityEngine;

namespace dang
{

    public class Enemy_Test : MonoBehaviour
    {
        public int target = 0;
        public Transform exitPoint;
        public Transform[] wayPoints;
        public float navTimeUpdate;
        public float currentNavTime;
        private Transform enemy;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            enemy = GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            if (wayPoints != null)
            {
                currentNavTime += Time.deltaTime;

                if (currentNavTime > navTimeUpdate)
                {
                    if (target < wayPoints.Length)
                    {
                        enemy.position = Vector2.MoveTowards(enemy.position, wayPoints[target].position, currentNavTime);
                    }
                    else
                    {
                        enemy.position = Vector2.MoveTowards(enemy.position, exitPoint.position, currentNavTime);
                    }

                    currentNavTime = 0;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Point")
            {
                target++;
            }
        }
    }
}
