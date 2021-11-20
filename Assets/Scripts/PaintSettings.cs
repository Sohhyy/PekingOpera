using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintSettings : MonoBehaviour
{
    private static PaintSettings _instance;

    public static PaintSettings Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PaintSettings>(); ;
            }
            return _instance;
        }

    }

    public Material[] colorMaterials;
    public Material defaultMaterial;
    public Material currentMaterial;
    private PaintDish selectedDish;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setMaterial(int colorIndex)
    {
        
        try
        {
            currentMaterial = colorMaterials[colorIndex];
        }
        catch (System.IndexOutOfRangeException e)
        {
            currentMaterial = defaultMaterial;
        }
        
    }

    /* setMaterial(Material)
     * Paint dishes will pass their material directly instead of using indexes
     */
    public void SetMaterial(Material paintMaterial)
    {
        currentMaterial = paintMaterial;
    }

    public void SetPaintDish(PaintDish dish)
    {
        //Unselect old dish
        if(selectedDish)
        {
            selectedDish.Deselect();
        }
        selectedDish = dish;
        currentMaterial = dish.ColorMaterial;
    }

}
