using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;


/* PaintOnTouch
 * Class added to any game object that we want to change material when the player touches it
 */
public class PaintOnTouch : MonoBehaviour, IMixedRealityTouchHandler
{
    private Material myMaterial;
    public Collider myCollider;
    public SurfaceHighlight myHighlight;
    bool ischangedcolor;
    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        //myCollider = GetComponent<SphereCollider>();
        myHighlight.Deactivate();
        //Deactivate();
        ischangedcolor = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* Activate
     * Activates the PaintOnTouch Mesh Collider, allowing painting
     * Called by the PaintSurfaceSwitcher
     */
    public void Activate()
    {
        Debug.Log("Enabling Collider");
        myCollider.enabled = true;
        myHighlight.Activate();
    }

    /* Deactivate
     * Deactivates the PaintOnTouch Mesh Collider
     * Called by the PaintSurfaceSwitcher
     */
    public void Deactivate()
    {
        Debug.Log("Disabling Collider");
        myCollider.enabled = false;
        myHighlight.Deactivate();
    }

    //When changing color, just retrieve public color from PaintSettings singleton
    void ChangeColor()
    {
        myMaterial.color = PaintSettings.Instance.currentMaterial.color;
        SoundMgr.Instance.PlayDrawSound();
        myHighlight.Deactivate();
    }

    //Change color on touch start
    public void OnTouchStarted(HandTrackingInputEventData eventData)
    {
        Debug.Log("TouchStarted");
        ChangeColor();
        if (!ischangedcolor)
        {
            ischangedcolor = true;
            GameMananger.Instance.AddPaintCount();
        }
        //PaintSurfaceSwitcher.Instance.NextSurface();
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
