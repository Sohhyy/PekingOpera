using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceArrowsUI : MonoBehaviour
{

    public Material[] SurfaceMaterials;
    public GameObject Display;
    public GameObject FinishButton;
    //public GameObject ShareButton;

    private int surfaceIndex = 0;
    private Renderer myRenderer;
    // Start is called before the first frame update
    void Start()
    {
        myRenderer = Display.GetComponent<Renderer>();
        UpdateImage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateImage()
    {
        myRenderer.material = SurfaceMaterials[surfaceIndex];
        PaintSurfaceSwitcher.Instance.SelectSurface(surfaceIndex);
        /*
        if(surfaceIndex == SurfaceMaterials.Length - 1)
        {
            //FinishButton.SetActive(true);
        }
        */
    }

    public void NextSurfaace()
    {
        if(surfaceIndex < SurfaceMaterials.Length - 1)
        {
            surfaceIndex++;
        }
        else
        {
            surfaceIndex = 0;
        }
        UpdateImage();
    }

    public void PrevSurface()
    {
        if (surfaceIndex > 0)
        {
            surfaceIndex--;
        }
        else
        {
            surfaceIndex = SurfaceMaterials.Length - 1;
        }
        UpdateImage();
    }

    public void SetSurface(int index)
    {
        surfaceIndex = index;
        UpdateImage();
    }
}
