using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrushButton : MonoBehaviour
{

    public UnityEvent OnBrushCollison;
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
        if (other.gameObject.tag == "Brush")
        {
            Debug.Log("Collision with brush");
            OnBrushCollison.Invoke();
        }
    }
}
