using UnityEngine;

namespace dang
{
    public class Enemy : MonoBehaviour
    {
        public string entityType;
        public int maxHealth;
        public float runSpeed;

        public Enemy()
        {
        }

        public Enemy(string entityType, int maxHealth, float runSpeed)
        {
            this.entityType = entityType;
            this.maxHealth = maxHealth;
            this.runSpeed = runSpeed;
        }
    }
}
