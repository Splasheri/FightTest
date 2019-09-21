using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellView : MonoBehaviour
{
    public Material emptyMaterial;
    public Material exploredMaterial;
    public Material allyMaterial;
    public Material actionMaterial;
    public GameObject quad;
    public Transform listParent;

    private List<GameObject> cells;

    private void Start()
    {
        cells = new List<GameObject>();
    }

    public void CreateList(List<Cell> cells)
    {
        for (int i = 0; i < cells.Count; i++)
        {
            if (this.cells.Count <= i)
            {
                this.cells.Add(CreateCell(new Vector3((float)cells[i].x, (float)cells[i].y, 0)));
            }
            ChangeState(i, cells[i].GetState);
        }
    }

    private GameObject CreateCell(Vector3 position)
    {
        var quad = GameObject.Instantiate(this.quad, listParent, false);
        quad.transform.localPosition = position;
        return quad;
    }
    
    public void ChangeState(int index, State state)
    {
        switch (state)
        {
            case State.empty:
                cells[index].GetComponent<MeshRenderer>().material = emptyMaterial;
                break;
            case State.explored:
                cells[index].GetComponent<MeshRenderer>().material = exploredMaterial;
                break;
            case State.action:
                cells[index].GetComponent<MeshRenderer>().material = actionMaterial;
                break;
            case State.ally:
                cells[index].GetComponent<MeshRenderer>().material = allyMaterial;
                break;
        }
    }
}
