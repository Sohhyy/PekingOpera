using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour
{

    public GameObject FinishButton;
    public GameObject ShareButton;
    public int surfacesPainted = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name== "PaintBox")
        {
            gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material = other.gameObject.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material;
            SoundMgr.Instance.PlaySound(0);
        }

        if (other.gameObject.tag == "Face")
        {
            other.gameObject.GetComponent<MeshRenderer>().material= gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material;
            SoundMgr.Instance.PlayDrawSound();
            if (other.gameObject.transform.GetChild(0).gameObject.activeSelf)
            {
                other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                surfacesPainted++;
                if(surfacesPainted >= 11)
                {
                    FinishButton.SetActive(true);
                    ShareButton.SetActive(true);
                }
            }
        }
    }
}
