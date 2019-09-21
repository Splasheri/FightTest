using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    empty,
    explored,
    ally,
    action,
    enemy
}

public class Cell
{
    private State state;
    public  State GetState {get {return state; } }

    private int row, col;
    public int x { get { return col; } }
    public int y { get { return row; } }

    private Action onStateChange;

    public Cell(State _state, int x, int y)
    {
        state = _state;
        row = y;
        col = x;
    }

    public void ChangeState(State _state)
    {
        state = _state;
        if (onStateChange!=null)
        {
            onStateChange();
            onStateChange = null;
        }
    }

    public void SetAction(Action action)
    {
        onStateChange = action;
    }
    
    public void ChangePosition(int x, int y)
    {
        this.col = x;
        this.row = y;
    }
}
