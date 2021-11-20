using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public GameObject ObjectToColor;
    public Material myMaterial;
    private Material targetMaterial;
    // Start is called before the first frame update
    void Start()
    {
        //myMaterial = GetComponent<Renderer>().material;
        targetMaterial = ObjectToColor.GetComponent<Renderer>().material;
        Debug.Log(targetMaterial.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* SetColor
     * Public method to be called by color changing buttons/interactable objects
     * Saves color and (at least for testing) applies to own material
     */
    public void SetColor()
    {
        //currentColor = color;
        targetMaterial.color = myMaterial.color;
    }
}
