namespace dang
{
    public class Enemy
    {
        public string entityType;
        public int maxHealth;
        public float runSpeed;
        public float attackDamage;
        public float attackRange;
        public float attackSpeed;

        public Enemy(string entityType, int maxHealth, float runSpeed, float attackDamage, float attackRange, float attackSpeed)
        {
            this.entityType = entityType;
            this.maxHealth = maxHealth;
            this.runSpeed = runSpeed;
            this.attackDamage = attackDamage;
            this.attackRange = attackRange;
            this.attackSpeed = attackSpeed;
        }

        public Enemy()
        {
        }


    }
}
