using UnityEngine;

public class MortonTableDebugger : MonoBehaviour
{
    void Start()
    {
        LookUpTable.ExportMortonTableToFile();
    }
}
