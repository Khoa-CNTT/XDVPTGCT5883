using UnityEngine;

namespace dang
{
    public class SpawnInteract : MonoBehaviour
    {
        private Renderer objectRenderer;
        private Color originalColor;
        private Color triggerColor = new Color(0.867f, 0.486f, 0.486f); // #DD7C7C

        void Start()
        {
            objectRenderer = GetComponent<Renderer>();
            if (objectRenderer != null)
            {
                originalColor = objectRenderer.material.color;
            }
        }

        void OnMouseEnter()
        {
            if (objectRenderer != null)
            {
                objectRenderer.material.color = triggerColor;
            }
        }

        void OnMouseExit()
        {
            if (objectRenderer != null)
            {
                objectRenderer.material.color = originalColor;
            }
        }
    }
}