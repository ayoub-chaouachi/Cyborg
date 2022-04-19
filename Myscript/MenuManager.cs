using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject OptionPanel;
    public GameObject StartPanel;
    public GameObject HelpPanel;
    public Slider Volume;
    private AudioSource Sound;
    public GameObject Loading_Img;

    public AudioSource SourceSound;
    public AudioClip ClickSound;
    public Toggle res1;
    public Toggle res2;
    public Toggle res3;
    private void Awake()
    {
        Time.timeScale = 1;
        Sound = GetComponent<AudioSource>();
      //  CursorVisibility = false;
    }
    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
       // Sound.volume = Volume.value;
    }
       
    public void StartGame()
    {
        StartCoroutine(Loading(1));
       SourceSound.PlayOneShot(ClickSound);
    }
    public void Option()
    {
        OptionPanel.SetActive(true);
        StartPanel.SetActive(false);
        SourceSound.PlayOneShot(ClickSound);
    }
    public void FromOption()
    {
        OptionPanel.SetActive(false);
        StartPanel.SetActive(true);
        SourceSound.PlayOneShot(ClickSound);
    }
    public void Help()
    {
        HelpPanel.SetActive(true);
        StartPanel.SetActive(false);
       SourceSound.PlayOneShot(ClickSound);
    }
    public void FromHelp()
    {
        HelpPanel.SetActive(false);
        StartPanel.SetActive(true);
       SourceSound.PlayOneShot(ClickSound);
    }
    public void Quit()
    {
        Application.Quit();
        SourceSound.PlayOneShot(ClickSound);
    }

    IEnumerator Loading(int sceneNbr)
    {

        Loading_Img.SetActive(true);
        StartPanel.SetActive(false);

        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(sceneNbr);

    }
    //public void Res1()
   // {
     //  if(res1.isOn)
       // Screen.SetResolution(1980,1080,FullScreenMode.ExclusiveFullScreen,60);
   // }
   // public void Res2()
    //{
     //   if (res2.isOn)
         //   Screen.SetResolution(1366,768, FullScreenMode.ExclusiveFullScreen, 60);
   // }
   // public void Res3()
   // {
     //   if (res3.isOn)
      //      Screen.SetResolution(1280,720, FullScreenMode.ExclusiveFullScreen, 60);
    //}

}
