using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderProxy : MonoBehaviour
{
    public Slider slider;
    public int Value { get { return SliderValue(); } }

    private void Awake()
    {
        OnChange();
    }

    public void OnChange()
    {
        this.GetComponent<Text>().text = slider.value.ToString();
    }

    private int SliderValue()
    {
        return int.Parse(this.GetComponent<Text>().text);
    }
}
