using UnityEngine;

public class MortonTableDebugger : MonoBehaviour
{
    void Start()
    {
        mortoncode.ExportMortonTableToFile();
    }
}
