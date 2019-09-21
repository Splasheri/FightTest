using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellList
{
    private List<Cell> allCells;
    private List<Cell> allyCells;

    private int  rows, cols, care;
    public  List<Cell> List { get { return allCells; } }
    public  int  Count { get { return rows * cols; } }
    public  int  Care  { get { return care; } set { care = value; } }   

    private int x(int pos)
    {
        return pos % cols;
    }

    private int y(int pos)
    {
        return pos / cols;
    }

    private int positionOf(Cell cell)
    {
        return cell.y * cols + cell.x;
    }

    public CellList(int _rows, int _cols)
    {
        allCells = new List<Cell>();
        allyCells = new List<Cell>();

        rows = _rows;
        cols = _cols;

        for (int i = 0; i < Count; i++)
        {
            allCells.Add(new Cell(State.empty,x(i),y(i)));
        }
    }

    public void SetCells(int cellAmount, State state)
    {        
        switch (state)
        {
            case State.ally:
                foreach (var pos in GeneratePositions(cellAmount))
                {
                    allCells[pos].ChangeState(State.ally);
                    allyCells.Add(allCells[pos]);
                }
                break;
            case State.action:
                foreach (var pos in GetRandomCells(cellAmount))
                {
                    allCells[pos].ChangeState(State.action);
                    //allCells[pos].SetAction(()=> { Debug.Log("KEK"); });
                }
                break;
        }
    }

    private List<int> GeneratePositions(int cellAmount)
    {
        List<int> genCells = new List<int>();
        int startPosition = UnityEngine.Random.Range(0,rows);
        genCells.Add(startPosition*cols);
        for (int i = 1; i < cellAmount; i++)
        {
            genCells.Add(GetRndNumFromList(GetFreeCells(GetRndNumFromList(genCells),genCells)));
        }
        return genCells;
    }

    private List<int> GetRandomCells(int amount)
    {
        List<int> reCells = new List<int>();
        for (int i = 0; i < amount; i++)
        {
            int rndNum = UnityEngine.Random.Range(0,Count);
            while (reCells.Contains(rndNum) || allCells[rndNum].GetState!=State.empty)
            {
                rndNum = UnityEngine.Random.Range(0, Count);
            }
            reCells.Add(rndNum);
        }
        return reCells;
    }

    private List<int> CellsAround(int position, Predicate<Cell> cond)
    {
        List<int> reCells = new List<int>();
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (!( i == 0 && j == 0 ))
                {
                    int checkCell = position + i * cols + j;
                    if (checkCell<Count && checkCell>=0 && y(checkCell) - y(position) == i && cond(allCells[checkCell]))
                    {
                        if (AlliesNear(checkCell) >= Care)
                        {
                            reCells.Add(checkCell);
                        }
                    }
                }
            }
        }
        return reCells;
    }

    private List<int> GetMoveCells(int pos)
    {
        var posList = CellsAround(pos, (Cell x) => { return ((x.GetState == State.empty || x.GetState == State.action) && AlliesNear(positionOf(x))>=Care); });
        if (posList.Count==0)
        {
            posList = CellsAround(pos, (Cell x) => { return (x.GetState == State.explored && AlliesNear(positionOf(x)) >= Care); });
        }
        if (posList.Count == 0)
        {
            posList.Add(pos);
        }
        return posList;
    }
    
    private List<int> GetFreeCells(int pos, List<int> wrongList)
    {
        return CellsAround(pos, (Cell x) => { return (x.GetState == State.empty && !wrongList.Contains( positionOf(x) ) ); });
    }

    private bool isNear(Cell a, Cell b)
    {
        return Math.Abs(a.x - b.x) <= 1 && Math.Abs(a.y - b.y) <= 1;
    }

    private int AlliesNear(int pos)
    {
        var unit = allCells[pos];
        int number = 0;
        foreach (var ally in allyCells)
        {
            if (isNear(ally, unit))
            {
                number++;
            }
        }
        return number;
    }

    private int GetRndNumFromList(List<int> cellList)
    {
        return cellList[UnityEngine.Random.Range(0, cellList.Count)];
    }

    private void MoveCell(int a, int b)
    {
        Cell ally, position;
        ally = allCells[a];
        position = allCells[b];        

        if (ally.GetState!=State.ally)
        {
            throw new Exception("First parameter must be an ally");
        }

        ally.ChangeState(State.explored);
        position.ChangeState(State.ally);
        allyCells[allyCells.IndexOf(ally)] = position;
    }

    public List<int> MakeStep()
    {
        List<int> updatedCells = new List<int>();
        for (int i = 0; i < allyCells.Count; i++)
        {
            var ally = allyCells[i];
            updatedCells.Add(positionOf(ally));
            int moveCell = GetRndNumFromList(GetMoveCells(positionOf(ally)));
            MoveCell(positionOf(ally), moveCell);
            updatedCells.Add(moveCell);
        }
        return updatedCells;
    }
}
