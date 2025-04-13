using UnityEngine;

namespace dang
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private int live = 10;

        public int TotalLives { get; set; }

        void Start()
        {
            TotalLives = live;
        }

        private void ReduceLives()
        {
            TotalLives--;

            if (TotalLives <= 0)
            {
                TotalLives = 0;
            }
        }

        void OnEnable()
        {
            EnemiesController.OnEndReached += ReduceLives;
        }

        void OnDisable()
        {
            EnemiesController.OnEndReached -= ReduceLives;
        }
    }
}
