using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {

    public static UiManager instance;
    public GameObject PausePanel;
    public GameObject PauseOptionPanel;
    public GameObject Crosshair;
    public GameObject Lose_Panel;
    public GameObject Win_Panel;

    public bool CursorVisibility;

    public InputField InputCode;

    public int Try_Chance ;

    public int Correct_Code;
    public int Writed_Code;

    public GameObject Code_Panel;
    public Image PressE;

    // Use this for initialization
    void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        Pause();
        CursorState();
    
		
	}
    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !PausePanel.activeSelf && !PauseOptionPanel.activeSelf && !Lose_Panel.activeSelf && !Win_Panel.activeSelf)
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0;
            gameObject.GetComponent<Camera_Manager>().enabled = false;
            CursorVisibility = true;
            Crosshair.SetActive(false);
        }
        else
        if (Input.GetKeyDown(KeyCode.Escape) && PausePanel.activeSelf )
        {
            PausePanel.SetActive(false);
            Time.timeScale = 1;
            gameObject.GetComponent<Camera_Manager>().enabled = true;
            CursorVisibility = false;
            Crosshair.SetActive(true);
        }

    }

    public void Continue()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        CursorVisibility = false;
        gameObject.GetComponent<Camera_Manager>().enabled = true;
        Crosshair.SetActive(true);
        Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.ClickSound);
    }
    public void Retry()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
        Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.ClickSound);
    }
    public void PauseOption()
    {
        PauseOptionPanel.SetActive(true);
        PausePanel.SetActive(false);
        Crosshair.SetActive(false);
        Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.ClickSound);


    }
    public void BackToPause()
    {
        PausePanel.SetActive(true);
        PauseOptionPanel.SetActive(false);
        Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.ClickSound);

    }
    public void PauseQuit()
    {
        SceneManager.LoadScene(0);
        Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.ClickSound);

    }


    public void CursorState()
    {

        if (CursorVisibility)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }
    }








    public void Chose_9()
    {
        if (InputCode.text.Length < 4)
        {
            InputCode.text = InputCode.text + "9";
        }
        Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.ClickSound);
    }

    public void Chose_8()
    {
        if (InputCode.text.Length < 4)
        {
            InputCode.text = InputCode.text + "8";
        }
        Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.ClickSound);
    }

    public void Chose_7()
    {
        if (InputCode.text.Length < 4)
        {
            InputCode.text = InputCode.text + "7";
        }
        Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.ClickSound);
    }

    public void Chose_6()
    {
        if (InputCode.text.Length < 4)
        {
            InputCode.text = InputCode.text + "6";
        }
        Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.ClickSound);
    }

    public void Chose_5()
    {
        
        if (InputCode.text.Length < 4)
        {
            InputCode.text = InputCode.text + "5";
        }
        Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.ClickSound);
    }

    public void Chose_4()
    {
        if (InputCode.text.Length < 4)
        {
            InputCode.text = InputCode.text + "4";
        }
        Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.ClickSound);
    }

    public void Chose_3()
    {
        if (InputCode.text.Length < 4)
        {
            InputCode.text = InputCode.text + "3";
        }
        Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.ClickSound);
    }
    public void Chose_2()
    {
        if (InputCode.text.Length < 4)
        {
            InputCode.text = InputCode.text + "2";
        }
        Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.ClickSound);
    }
    public void Chose_1()
    {
        if (InputCode.text.Length < 4)
        {
            InputCode.text = InputCode.text + "1";
        }
        Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.ClickSound);
    }
    public void Chose_0()
    {
        if (InputCode.text.Length < 4)
        {
            InputCode.text = InputCode.text + "0";
        }
        Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.ClickSound);
    }

    public void Delete()
    {
        if (InputCode.text.Length == 1)
        {
            InputCode.text = "";
        }
        if (InputCode.text.Length == 2)
        {
            InputCode.text = InputCode.text[0] + "";
        }
        if (InputCode.text.Length == 3)
        {
            InputCode.text = InputCode.text[0] + "" + InputCode.text[1];
        }
        if (InputCode.text.Length == 4)
        {
            InputCode.text = InputCode.text[0] + "" + InputCode.text[1] + "" + InputCode.text[2];
        }
        Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.ClickSound);

        print("aaaaa");
       
    }


    public void Enter()
    {
        if (Try_Chance > 0)
        {


            Writed_Code = int.Parse(InputCode.text);

            if (Writed_Code == Correct_Code )
            {
                print("true");
                Code_Panel.SetActive(false);
                PlayerController.instance.Dooranim3.SetBool("Open_DoorR3", true);
                PlayerController.instance.Dooranim4.SetBool("Open_DoorL3", true);
                Camera.main.GetComponent<Camera_Manager>().enabled = true;
                CursorVisibility = false;
                Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.FirstDoorSound);
            }
            else
            {
                print("flase");
                Try_Chance--;
                InputCode.text = "";
                Writed_Code = 0;
                InputCode.placeholder.GetComponent<Text>().text = "Try Again"; 
            }
        }
        else
        {
            print("mession faild !! ");
            Lose_Panel.SetActive(true);
            CursorVisibility = true;
            Code_Panel.SetActive(false);
            Crosshair.SetActive(false);
            Try_Chance = 3;

        }
        Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.ClickSound);
    }
}
