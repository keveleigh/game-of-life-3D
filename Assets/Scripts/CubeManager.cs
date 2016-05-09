using UnityEngine;
using System.Collections;
using GameOfLife.Simulation;

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
    }

    void Update()
    {
        if (gameSimulation != null)
        {
            Cell[,] cells = gameSimulation.GetCells();
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    if (cells[i,j].getState() == 1)
                    {
                        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        cube.transform.SetParent(transform);
                        cube.transform.localPosition.Set(i, j, 0);
                    }
                }
            }
        }
    }
}
