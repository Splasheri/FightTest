using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellController : MonoBehaviour
{
    private CellList cells;
    private CellView view;

    public Button startSequence;

    public SliderProxy turnsSlider;
    public SliderProxy activeSlider;
    public SliderProxy charSlider;
    public SliderProxy colSlider;
    public SliderProxy rowSlider;
    public SliderProxy careSlider;
    private List<SliderProxy> sliders;

    private void Start()
    {
        sliders = new List<SliderProxy>() { charSlider, colSlider, rowSlider, careSlider, activeSlider, turnsSlider };
    }    

    public void Restart()
    {
        StopAllCoroutines();
        startSequence.interactable = true;
        StartSimulation();
    }

    private void StartSimulation()
    {

        cells = new CellList(rowSlider.Value,colSlider.Value);
        cells.SetCells(charSlider.Value, State.ally);
        cells.SetCells(activeSlider.Value, State.action);
        cells.Care = careSlider.Value;

        view = this.gameObject.GetComponent<CellView>();
        view.CreateList(cells.List);

        Camera.main.orthographicSize = colSlider.Value >= rowSlider.Value ?
                                       colSlider.Value : rowSlider.Value;
        view.listParent.position = new Vector3(colSlider.Value * -1.5f, rowSlider.Value-1);        

        foreach (var item in sliders)
        {
            item.slider.interactable = true;
        }
    }

    public void ClickToStep()
    {
        StopAllCoroutines();
        foreach (var item in sliders)
        {
            item.slider.interactable = true;
        }
        UpdateView(cells.MakeStep());
    }

    public void ClickToSequence()
    {
        foreach (var item in sliders)
        {
            item.slider.interactable = false;
        }

        StartCoroutine(StepSequence(turnsSlider.Value));

    }

    IEnumerator StepSequence(int turns)
    {
        for (int i = 0; i < turnsSlider.Value; i++)
        {
            UpdateView(cells.MakeStep());
            yield return new WaitForSeconds(0.5f);
        }
        foreach (var item in sliders)
        {
            item.slider.interactable = true;
        }
    }

    public void ClickToExit()
    {
        Application.Quit();
    }

    private void UpdateView(List<int> values)
    {
        foreach (var index in values)
        {
            view.ChangeState(index,cells.List[index].GetState);
        }
    }
}
