namespace dang
{
    public class Enemy : Entities
    {
        public Enemy()
        {
            entityType = "Enemy";
            maxHealth = 100;
            runSpeed = 5.0f;
            attackDamage = 10.0f;
            attackRange = 1.0f;
            attackSpeed = 1.0f;
        }
    }
}
