using UnityEngine;

public class CellManager : MonoBehaviour
{
    void OnGazeEnter()
    {
        GetComponent<Renderer>().enabled = true;
    }

    void OnGazeLeave()
    {
        GetComponent<Renderer>().enabled = false;
    }
}
