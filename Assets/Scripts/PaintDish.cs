using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;

public class PaintDish : MonoBehaviour, IMixedRealityTouchHandler
{
    public Material ColorMaterial;
    public GameObject PaintSurface;
    public GameObject OuterDish;
    private bool isSelected;
    private Color originalColor;
    // Start is called before the first frame update
    void Start()
    {
        //Set own color
        PaintSurface.GetComponent<Renderer>().material.color = ColorMaterial.color;
        originalColor = OuterDish.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Select()
    {
        PaintSettings.Instance.SetPaintDish(this);
        OuterDish.GetComponent<Renderer>().material.color = ColorMaterial.color;
        isSelected = true;
    }

    public void Deselect()
    {
        OuterDish.GetComponent<Renderer>().material.color = originalColor;
        isSelected = false;
    }

    public void OnTouchStarted(HandTrackingInputEventData eventData)
    {
        if (!isSelected)
        {
            Select();
        }
    }
    public void OnTouchCompleted(HandTrackingInputEventData eventData)
    {
        Debug.Log("TouchCompleted");
    }
    public void OnTouchUpdated(HandTrackingInputEventData eventData)
    {
        //Debug.Log("TouchUpdated");
    }
}
