using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceHighlight : MonoBehaviour
{
    private Material myMaterial;
    public float pulseTime = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        StartCoroutine(Appear(myMaterial, 0.01f, pulseTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        GetComponent<Renderer>().enabled = true;
    }

    public void Deactivate()
    {
        GetComponent<Renderer>().enabled = false;
    }

    IEnumerator Pulse()
    {
        while (true)
        {
            StartCoroutine(Transparent(myMaterial, 0.01f, pulseTime));
            yield return new WaitForSeconds(pulseTime);
            StartCoroutine(Appear(myMaterial, 0.01f, pulseTime));
            yield return new WaitForSeconds(pulseTime);
        }
    }

    IEnumerator Transparent(Material i, float smoothness, float duration)
    {

        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.
        while (progress < 1)
        {
            Debug.Log("Getting Transparent");
            i.color = Color.Lerp(new Color(i.color.r, i.color.g, i.color.b, 0.8f), new Color(i.color.r, i.color.g, i.color.b, 0), progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }
        StartCoroutine(Appear(myMaterial, 0.01f, pulseTime));
    }

    IEnumerator Appear(Material i, float smoothness, float duration)
    {

        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.
        while (progress < 1)
        {

            i.color = Color.Lerp(new Color(i.color.r, i.color.g, i.color.b, 0), new Color(i.color.r, i.color.g, i.color.b, 0.8f), progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }
        StartCoroutine(Transparent(myMaterial, 0.01f, pulseTime));
    }

    /*

    public void StartTransparent(GameObject i)
    {
        StartCoroutine(Transparent(i.GetComponent<MeshRenderer>().material, 0.01f, 2f));
        foreach (Transform child in Brush.transform)
        {
            if (child.GetComponent<MeshRenderer>() != null)
            {
                StartCoroutine(Transparent(child.GetComponent<MeshRenderer>().material, 0.01f, 2f));
            }

        }

    }
    */
}
