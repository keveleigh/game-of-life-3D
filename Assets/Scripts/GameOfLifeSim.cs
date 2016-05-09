using System;
using System.Collections;
using UnityEngine;

namespace GameOfLife.Simulation
{
    public class GameOfLifeSim : MonoBehaviour
    {
        private Cell[,] cells;
        private int[,] neighbors;
        private long lastUpdateTime = DateTime.Now.Ticks;

        void Awake()
        {
            cells = new Cell[300, 300];
            for (int i = 0; i < cells.Length; i++)
            {
                for (int j = 0; j < cells.Length; j++)
                {
                    cells[i, j] = new Cell();
                }
            }
        }

        void Update()
        {
            long currentTime = DateTime.Now.Ticks;
            if ((currentTime - lastUpdateTime) > 1000)
            {
                lastUpdateTime = currentTime;

                neighbors = new int[300, 300];
                for (int i = 0; i < cells.Length; i++)
                {
                    for (int j = 0; j < cells.Length; j++)
                    {
                        neighbors[i, j] = getNumNeighbors(i, j);
                    }
                }

                for (int i = 0; i < cells.Length; i++)
                {
                    for (int j = 0; j < cells.Length; j++)
                    {
                        if (neighbors[i, j] < 2 || neighbors[i, j] > 3)
                        {
                            cells[i, j].die();
                        }
                        else if ((neighbors[i, j] == 2 && cells[i, j].getState() == 1) || (neighbors[i, j] == 3))
                        {
                            cells[i, j].live();
                        }
                    }
                }
            }
        }

        public void updateNeighborsOf(int i, int j)
        {
            neighbors[i + 1, j + 1] = getNumNeighbors(i + 1, j + 1);
            neighbors[i + 1, j] = getNumNeighbors(i + 1, j);
            neighbors[i + 1, j - 1] = getNumNeighbors(i + 1, j - 1);
            neighbors[i, j + 1] = getNumNeighbors(i, j + 1);
            neighbors[i, j - 1] = getNumNeighbors(i, j - 1);
            neighbors[i - 1, j + 1] = getNumNeighbors(i - 1, j + 1);
            neighbors[i - 1, j] = getNumNeighbors(i - 1, j);
            neighbors[i - 1, j - 1] = getNumNeighbors(i - 1, j - 1);
        }

        public int getNumNeighbors(int i, int j)
        {
            int neighbors = 0;
            try
            {
                neighbors += cells[i + 1, j + 1].getState();
            }
            catch (IndexOutOfRangeException)
            {
            }
            try
            {
                neighbors += cells[i + 1, j].getState();
            }
            catch (IndexOutOfRangeException)
            {
            }
            try
            {
                neighbors += cells[i + 1, j - 1].getState();
            }
            catch (IndexOutOfRangeException)
            {
            }
            try
            {
                neighbors += cells[i, j + 1].getState();
            }
            catch (IndexOutOfRangeException)
            {
            }
            try
            {
                neighbors += cells[i, j - 1].getState();
            }
            catch (IndexOutOfRangeException)
            {
            }
            try
            {
                neighbors += cells[i - 1, j + 1].getState();
            }
            catch (IndexOutOfRangeException)
            {
            }
            try
            {
                neighbors += cells[i - 1, j].getState();
            }
            catch (IndexOutOfRangeException)
            {
            }
            try
            {
                neighbors += cells[i - 1, j - 1].getState();
            }
            catch (IndexOutOfRangeException)
            {
            }
            return neighbors;
        }

        public Cell[,] GetCells()
        {
            return cells;
        }
    }

    public class Cell
    {
        private int state;

        public Cell()
        {
            state = 0;
        }

        public void changeState()
        {
            if (state == 0)
            {
                state = 1;
            }
            else
            {
                state = 0;
            }
        }

        public void die()
        {
            state = 0;
        }

        public void live()
        {
            state = 1;
        }

        public int getState()
        {
            return state;
        }
    }
}