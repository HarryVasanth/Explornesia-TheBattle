using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    
    public GameObject Slogan;
    public GameObject Logo;

    public static int Nationality;
    public GameObject[] Panels;

    public InputField[] OptionsIF;

    public Sprite[] LandTutorial;
    public Sprite[] SeaTutorial;
    public Image DisplayTutorial;
    
    private string _roomName;
    private float _battleTime;
    private float _spyTime;
    private float _turnTime;
    private float _fPoints;

    private int _index;
    private int _tutorialIndex;
    private Sprite[] Tutorial;
    public GameObject[] Buttons;

    private float _timer;
    private float _intervalTimer;
    private bool _isAnimationExit;

	// Use this for initialization
	private void Start ()
    {
        _roomName = "Explornesia" + Random.Range(0, 6);
        _battleTime = 180.0f;
        _spyTime = 30.0f;
        _turnTime = 60.0f;
        _timer = 10.0f;
        _intervalTimer = 10.0f;
        _fPoints = 150;

        Buttons[0].SetActive(true);
        Buttons[1].SetActive(false);
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer > 0 && !Input.GetKeyDown("space"))
        {
            Slogan.transform.localScale = Vector3.Lerp(new Vector3(1.0f, 1.0f, 1), new Vector3(1.2f, 1.2f, 1f), Time.time / _intervalTimer);
            Logo.transform.localScale = Vector3.Lerp(new Vector3(1.2f, 1.2f, 1f), new Vector3(1.0f, 1.0f, 1), Time.time / _intervalTimer);
            return;
        }

        if (!_isAnimationExit)
        {
            Slogan.SetActive(false);
            Panels[0].SetActive(true);
            _isAnimationExit = true;
        }
    }

    public void SetTeamSelection(int index)
    {
        Nationality = index;
    }

    public void Confirm()
    {
        _roomName = OptionsIF[0].text;
        float result = 0.0f;
        float.TryParse(OptionsIF[1].text, out result);
        if (result != 0.0f)
        {
            _battleTime = result * 60f;
        }
        float spyResult = 0.0f;
        float.TryParse(OptionsIF[1].text, out spyResult);
        if (result != 0.0f)
        {
            _spyTime = spyResult;
        }
        float turnResult = 0.0f;
        float.TryParse(OptionsIF[1].text, out turnResult);
        if (result != 0.0f)
        {
            _turnTime = turnResult * 60f;
        }
        PlayerController.FPointsUpdated = true;
    }

    public void SetBattle ( int index )
    {
        switch (index)
        {
            case 0:
                _index = 1;
                break;
            case 1:
                _index = 2;
                break;
            case 2:
                OptionsIF[0].text = _roomName;
                OptionsIF[1].text = (_battleTime / 60.0f).ToString();
                OptionsIF[2].text = _spyTime.ToString();
                OptionsIF[3].text = (_turnTime / 60.0f).ToString();
                OptionsIF[4].text = (_fPoints).ToString();
                SetActive(2);
                break;
            default:
                Debug.Log("Error");
                break;
        }
    }
    
    public void LoadScene()
    {
        SceneManager.LoadScene(_index);
    }
    
    public void SetRoomName (string text)
    {
        _roomName = text;
        NetworkManagerSea.RoomName = _roomName;
    }

    public void SetBattleTime(string text)
    {
        float result = 0.0f;
        float.TryParse(text, out result);
        if (result != 0.0f) {
            _battleTime = result * 60f;
        }
        else {
            _battleTime = 180.0f;
        }
        MainCanvasBehaviour.TimeLeft = _battleTime;
    }

    public void SetSpyTime(string text)
    {
        float result = 0.0f;
        float.TryParse(text, out result);
        if (result != 0.0f)
        {
            _spyTime = result ;
        }
        else
        {
            _spyTime = 30.0f;
        }
        PlayerController.SpyTime = _spyTime;
    }

    public void SetTurnTime(string text)
    {
        float result = 0.0f;
        float.TryParse(text, out result);
        if (result != 0.0f)
        {
            _turnTime = result * 60f;
        }
        else
        {
            _turnTime = 180.0f;
        }
        MainCanvas.TargetTime = _turnTime;
    }

    public void SetFPoints(string text)
    {
        float result = 0.0f;
        float.TryParse(text, out result);
        if (result != 0.0f && result <= 150 && result >= 50)
        {
            _fPoints = result;
        }
        else
        {
            _fPoints = 150f;
        }
        PlayerController.FPointsUpdated = true;
        PlayerController.FPointsMain = _fPoints;
    }

    public void SetTutorial( int index )
    {
        _tutorialIndex = 0;
        if ( index == 0)
        {
            Tutorial = (Sprite[])SeaTutorial.Clone();
        } else
        {
            Tutorial = (Sprite[])LandTutorial.Clone();
        }
        DisplayTutorial.sprite = Tutorial[_tutorialIndex];
    }

    public void Next()
    {
        if(_tutorialIndex < Tutorial.Length-1)
        {
            _tutorialIndex++;
            DisplayTutorial.sprite = Tutorial[_tutorialIndex];
        } else
        {
            _tutorialIndex = 0;
            Buttons[0].SetActive(false);
            Buttons[1].SetActive(true);

        }
    }
    
    public void SetActive(int index)
    {
        Panels[index].SetActive(true);
    }
}
