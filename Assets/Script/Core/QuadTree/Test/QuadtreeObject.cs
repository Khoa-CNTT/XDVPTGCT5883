using UnityEngine;

public class QuadtreeObject : MonoBehaviour
{
    private QuadtreeManager manager;

    void Start()
    {
        manager = FindObjectOfType<QuadtreeManager>();
        if (manager != null)
        {
            manager.Register(transform.position);
        }
    }
}
