using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintSurfaceSwitcher : MonoBehaviour
{

    private static PaintSurfaceSwitcher _instance;

    public static PaintSurfaceSwitcher Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PaintSurfaceSwitcher>(); ;
            }
            return _instance;
        }

    }

    public PaintOnTouch[] PaintableSurfaces;
    private int surfaceIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SetupPainting");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectSurface(int index)
    {
        if(index < 0 || index >= PaintableSurfaces.Length)
        {
            index = 0;
        }
        foreach (PaintOnTouch surface in PaintableSurfaces)
        {
            surface.Deactivate();
        }
        PaintableSurfaces[index].Activate();
    }

    public void NextSurface()
    {
        PaintableSurfaces[surfaceIndex].Deactivate();
        if (surfaceIndex < PaintableSurfaces.Length - 1)
        {
            surfaceIndex++;
        }
        else
        {
            surfaceIndex = 0;
        }
        PaintableSurfaces[surfaceIndex].Activate();
    }

    IEnumerator SetupPainting()
    {
        yield return new WaitForSeconds(1f);
        foreach (PaintOnTouch surface in PaintableSurfaces)
        {
            surface.Deactivate();
        }
        surfaceIndex = 0;
        PaintableSurfaces[0].Activate();
    }
}
