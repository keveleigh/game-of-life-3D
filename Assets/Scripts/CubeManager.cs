using GameOfLife.Simulation;
using UnityEngine;

[RequireComponent(typeof(GameOfLifeSim))]
public class CubeManager : MonoBehaviour
{
    private GameOfLifeSim gameSimulation;

    void Start()
    {
        gameSimulation = GetComponent<GameOfLifeSim>();
        if (gameSimulation == null)
        {
            Debug.Log("Please add a GameOfLifeSim script on the " + name + " GameObject");
        }
        else
        {
            Cell[,] cells = gameSimulation.GetCells();
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(0); j++)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    cube.GetComponent<Renderer>().enabled = false;
                    cube.AddComponent<CellManager>();
                    cube.transform.SetParent(transform);
                    Vector3 newPosition = new Vector3((i - (cells.GetLength(0)/2)) / 10.0f, (j - (cells.GetLength(0) / 2)) / 10.0f, 0);
                    cube.transform.localPosition = newPosition;
                }
            }
        }
    }

    void Update()
    {
        //if (gameSimulation != null)
        //{
        //    Cell[,] cells = gameSimulation.GetCells();
        //    for (int i = 0; i < 100; i++)
        //    {
        //        for (int j = 0; j < 100; j++)
        //        {
        //            if (cells[i,j].getState() == 1)
        //            {
        //                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //                cube.transform.SetParent(transform);
        //                cube.transform.localPosition.Set(i, j, 0);
        //            }
        //        }
        //    }
        //}
    }
}
