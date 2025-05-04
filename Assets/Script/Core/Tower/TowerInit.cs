using System.Collections.Generic;
using UnityEngine;

namespace dang
{
    public class TowerInit : MonoBehaviour
    {
        [Header("Turret Data")]
        [SerializeField] TowerData towerData;
        [SerializeField] private GameObject towerSprite;
        [SerializeField] private GameObject archerPos;
        private GameObject archerPrefab;
        private SpriteRenderer archerSpriteRenderer;
        private string towerName;
        private float towerDamage;
        public float towerAttackSpeed;
        private float towerAttackRange;

        void Start()
        {
            Init();
        }

        void Init()
        {
            if (towerData == null)
            {
                Debug.LogError("TowerData is not assigned!");
                return;
            }

            if (towerSprite == null)
            {
                Debug.LogError("TowerSprite is not assigned!");
                return;
            }

            if (archerPos == null)
            {
                Debug.LogError("ArcherPos is not assigned!");
                return;
            }

            towerSprite.GetComponent<SpriteRenderer>().sprite = towerData.towerSprite;

            archerPrefab = towerData.archerPrefab;
            GameObject archerInstance = Instantiate(archerPrefab, archerPos.transform.position, Quaternion.identity, archerPos.transform);

            archerSpriteRenderer = archerInstance.GetComponent<SpriteRenderer>();
            if (archerSpriteRenderer != null)
            {
                archerSpriteRenderer.sortingLayerName = "Archer";
            }

            towerName = towerData.towerName;
            towerDamage = towerData.damage;
            towerAttackSpeed = towerData.attackSpeed;
            towerAttackRange = towerData.range;

            GetComponent<CircleCollider2D>().radius = towerAttackRange;
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, towerAttackRange / 1.33f);
        }
    }
}
