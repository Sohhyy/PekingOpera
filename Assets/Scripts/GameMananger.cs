using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Experimental.UI;


public class GameMananger : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameMananger Instance = null;
    public GameObject Colorbox;
    public GameObject FinishButton;
    public GameObject ShareButton;
    public GameObject SurfaceSelector;
    public GameObject Table;
    public GameObject Brush;
    public GameObject floatingbrush;
    private int paintcount;
    bool flag;
    public GameObject dialog1;
    public GameObject dialog2;
    public GameObject dialog3;
    public ScreenshotCamera screenshotCamera;
    public MixedRealityKeyboard keyboard;
    private object mail;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        paintcount = 0;
        flag = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Final" && flag)
        {
            StartGame();
            flag = false;
        }
    }
    public void AddPaintCount()
    {
        paintcount++;
        if (paintcount == 11)
        {
            FinishButton.SetActive(true);
        }
    }

    public void StartGame()
    {
        StartCoroutine(Beginning());
    }

    public void Finish()
    {
        StartCoroutine(Endding());
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }


    IEnumerator Beginning()
    {
        yield return new WaitForSeconds(1f);
        SoundMgr.Instance.PlayDialogue();
        //yield return new WaitForSeconds(1f);

        dialog1.SetActive(true);
        yield return new WaitForSeconds(5f);

        dialog1.SetActive(false);
        dialog2.SetActive(true);
        yield return new WaitForSeconds(3f);
        dialog2.SetActive(false);
        dialog3.SetActive(true);
        yield return new WaitForSeconds(8f);
        floatingbrush.SetActive(true);
        floatingbrush.GetComponent<BoxCollider>().enabled = true;
        dialog3.SetActive(false);
        foreach (Transform child in floatingbrush.transform)
        {
            if (child.GetComponent<MeshRenderer>() != null)
            {
                GameMananger.Instance.StartAppear(child.gameObject);
            }

        }
        //keyboard.ShowKeyboard();
        //SendEmail();

    }
    IEnumerator Endding()
    {
        //Colorbox.SetActive(false);
        //Button.SetActive(false);
        //SurfaceSelector.SetActive(false);
        //Table.SetActive(false);
        SoundMgr.Instance.StopBGM();
        SoundMgr.Instance.PlayDialogue();
        yield return new WaitForSeconds(SoundMgr.Instance.dialogues[SoundMgr.Instance.dialogueIndex - 1].length + 1f);
        //yield return new WaitForSeconds(1f);

        //Hide UI and Take Screenshot
        FinishButton.SetActive(false);
        ShareButton.SetActive(false);
        SurfaceSelector.SetActive(false);
        screenshotCamera.TakeScreenshot();
        //Leave scene
        LoadScene("Endding Scene");

    }


    public void StartMainScene()
    {
        LoadScene("Final");
    }

    public void RestartGame()
    {
        LoadScene("Title Screen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator Transparent(Material i, float smoothness, float duration)
    {

        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.
        while (progress < 1)
        {

            i.color = Color.Lerp(new Color(i.color.r, i.color.g, i.color.b, 1), new Color(i.color.r, i.color.g, i.color.b, 0), progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }

    }

    IEnumerator Appear(Material i, float smoothness, float duration)
    {

        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.
        while (progress < 1)
        {

            i.color = Color.Lerp(new Color(i.color.r, i.color.g, i.color.b, 0), new Color(i.color.r, i.color.g, i.color.b, 1), progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }

    }

    public void StartTransparent(GameObject i)
    {
        StartCoroutine(Transparent(i.GetComponent<MeshRenderer>().material, 0.01f, 2f));
        //foreach(Transform child in Brush.transform)
        //{
        //    if (child.GetComponent<MeshRenderer>()!=null){
        //        StartCoroutine(Transparent(child.GetComponent<MeshRenderer>().material, 0.01f, 2f));
        //    }

        //}

    }

    public void StartAppear(GameObject i)
    {
        StartCoroutine(Appear(i.GetComponent<MeshRenderer>().material, 0.01f, 2f));


    }

    public void ShareButtonFunction()
    {
        SurfaceSelector.SetActive(false);
        FinishButton.SetActive(false);
        ShareButton.SetActive(false);
        screenshotCamera.TakeScreenshot();
        keyboard.ShowKeyboard();


    }


    public void CloseShareButtonFunction()
    {
        SurfaceSelector.SetActive(true);
        FinishButton.SetActive(true);
        ShareButton.SetActive(true);
        screenshotCamera.SendEmail("11");
        keyboard.HideKeyboard();


    }

}