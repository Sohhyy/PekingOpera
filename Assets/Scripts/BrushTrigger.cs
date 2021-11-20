using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
public class BrushTrigger : MonoBehaviour, IMixedRealityTouchHandler
{
    // Start is called before the first frame update
    public BoxCollider bc1;
    public BoxCollider bc2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTouchStarted(HandTrackingInputEventData eventData)
    {
        GetComponent<HandTrace>().enabled = true;
        GetComponent<NearInteractionTouchable>().enabled = false;
        
        bc1.enabled = false;
        bc2.enabled = true;
        GetComponent<BrushTrigger>().enabled = false;
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
