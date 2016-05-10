using UnityEngine;

public class Cell : MonoBehaviour
{
    private bool isAlive;
    private bool isHover;
    private Renderer cellRenderer;

    private Color aliveColor;
    private Color hoverColor;

    void Awake()
    {
        cellRenderer = GetComponent<Renderer>();
        Die();
    }

    public void SetColors(Color AliveColor, Color HoverColor)
    {
        aliveColor = AliveColor;
        hoverColor = HoverColor;
    }

    public void ChangeState()
    {
        if (isAlive)
        {
            Die();
        }
        else
        {
            Live();
        }
    }

    public void Die()
    {
        cellRenderer.enabled = isAlive = false;
    }

    public void Live()
    {
        cellRenderer.enabled = isAlive = true;
        cellRenderer.material.color = aliveColor;
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    //void OnGazeEnter()
    //{
    //    cellRenderer.enabled = true;
    //}

    //void OnGazeLeave()
    //{
    //    cellRenderer.enabled = false;
    //}

    void OnMouseEnter()
    {
        cellRenderer.enabled = true;
        cellRenderer.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        if (isAlive)
        {
            cellRenderer.material.color = aliveColor;
        }
        else
        {
            cellRenderer.enabled = false;
        }
    }
}