using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryBoard : MonoBehaviour
{
    // Start is called before the first frame update
    public Material[] materials;
    private int count;

    void Start()
    {
        count = 0;
        StartCoroutine(StartChange());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartChange()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            if (count == (materials.Length-1))
            {
                SceneManager.LoadScene("Final");
            }
            else
            {
                count++;
                GetComponent<MeshRenderer>().material = materials[count];
            }
        }
        
    }
        

}
