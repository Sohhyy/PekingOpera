using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine.Video;
public class GameStartTrigger : MonoBehaviour, IMixedRealityTouchHandler
{
    // Start is called before the first frame update
    public GameObject Video;
    public GameObject Man;
    public GameObject Environment;
    public GameObject Tablechair;
    public GameObject Selector;
    public GameObject Plane;
    public GameObject brush;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTouchStarted(HandTrackingInputEventData eventData)
    {

        StartCoroutine(GameStart());

    }
    public void OnTouchCompleted(HandTrackingInputEventData eventData)
    {
        Debug.Log("TouchCompleted");
    }
    public void OnTouchUpdated(HandTrackingInputEventData eventData)
    {
        //Debug.Log("TouchUpdated");
    }

    IEnumerator GameStart()
    {
        Debug.Log("11");
        GetComponent<BoxCollider>().enabled = false;
        SoundMgr.Instance.PlaySound(5);
        foreach (Transform child in this.transform)
        {
            if (child.GetComponent<MeshRenderer>() != null)
            {
                GameMananger.Instance.StartTransparent(child.gameObject);
            }

        }
        yield return new WaitForSeconds(2f);
        //this.gameObject.SetActive(false);
        Plane.SetActive(true);
        Video.GetComponent<VideoPlayer>().Play();
        yield return new WaitForSeconds(14f);
        Destroy(Video);
        Destroy(Plane);
        Man.SetActive(true);
        Environment.SetActive(true);
        Tablechair.SetActive(true);
        Selector.SetActive(true);
        brush.SetActive(true);
        SoundMgr.Instance.PlayBGM();

    }
}
