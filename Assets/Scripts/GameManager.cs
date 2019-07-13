using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    private Camera _mainCamera;
    [SerializeField]
    private Text _gameOver, _historyText, _buttonText;
    private int _count,test,_aLevel,_playerLives;
    [SerializeField]
    private int _collectedMoney;
    private float _endDelay,_hTimer,_gameTime;
    private bool _camerM=false,_textPhase2;
    public bool gameOver=false;
    [SerializeField]
    private GameObject _history,_objective,_heart,_heart2,_heart3,_button,_button1;
    private static bool _isHistoryOn;
    [SerializeField]
    private Text _timer,_ObjectiveTxt;
    [SerializeField]
    private static Vector3 _cameraPos;

    void Start()
    {
        Spawner.levelState = 0;
        _objective.SetActive(false);
        if(_isHistoryOn==false)
        {
            _history.SetActive(true);
        }else
        {
            _history.SetActive(false);
        }
        _mainCamera = FindObjectOfType<Camera>();
        _playerLives = 3;
        _count = 39;
        _endDelay = 0;
        _hTimer = Time.time + 3f;
        _gameTime = Time.time + 50f;
    }

    // Update is called once per frame
    void Update()
    {
        
        switch(_playerLives)
        {
            case 3:
                _heart3.SetActive(true);
                _heart2.SetActive(true);
                _heart.SetActive(true);
                break;
            case 2:
                _heart3.SetActive(false);
                _heart2.SetActive(true);
                _heart.SetActive(true);
                break;
            case 1:
                _heart3.SetActive(false);
                _heart2.SetActive(false);
                _heart.SetActive(true);
                break;
            case 0:
                _heart3.SetActive(false);
                _heart2.SetActive(false);
                _heart.SetActive(false);
                break;
        }
        if(_camerM == true)
        {
            NextZone(_aLevel);
        }
        if(_playerLives <= 0)
        {
            gameOver = true;
        }
        if(gameOver == true)
        {
            _gameOver.gameObject.SetActive(true);
            if (_endDelay <= 0)
            {
                _endDelay = Time.time + 2f;
            }
            if (_endDelay < Time.time)
            {
                Debug.Log("recargando escena");
                SceneManager.LoadScene(0); //reloading de scene brokes a lot of stuff, it will be easily if the game just warp you when you lose health and only re load if u lost all of them
            }
        }else
        {
            
            test = (int)Time.timeSinceLevelLoad;
            _timer.text = " 8 :" + test;
            if (test >= 60)  // the time is broken, when you pass the limits a loop initiate and you end up stuck loosing over and over, limit of 3 lives?
            {
                gameOver = true;
            }
            if (_isHistoryOn == false && _hTimer < Time.time)
            {

                _history.SetActive(false);
                _isHistoryOn = true;
            }
        }
        if(_isHistoryOn == true)
        {
            _objective.SetActive(true);
        }
    }
    public void EndOfGame()
    {
        //init of the end lines
        _historyText.text = "Qué venís a comprar pibe";
        _history.SetActive(true);
        _button.SetActive(true);
        _buttonText.text = "Una cerveza";
        //wait for input (button)
    }
    public void Buttons(int ID)
    {
        
        if(ID==0)
        {
            if (_textPhase2 == false)
            {
                if (_collectedMoney == 6)
                {
                    _textPhase2 = true;
                    _historyText.text = "Son 70 mangos flaco";
                    _buttonText.text = "Como, pero si estaba 60 ayer";
                } else if (_collectedMoney < 6)
                {
                    _textPhase2 = true;
                    _historyText.text = "Son 70 mangos flaco";
                    _buttonText.text = "Uh, no junte todos los billetes, no me dejas fiado?";
                    //te hechan
                } else if (_collectedMoney > 6)
                {
                    _textPhase2 = true;
                    _historyText.text = "Son 70 mangos flaco";
                    _buttonText.text = "Toma, me sobra la guita por que rompí el juego";
                }
            }else
            {
                if(_collectedMoney == 6)
                {
                    _historyText.text = "Aumento flaco, compra algo o tomatela";
                    _buttonText.text = "Se acabo la joda, a tomar agua. . .";
                }else if(_collectedMoney <6)
                {
                    _historyText.text = "No se fía acá, lee el cartel y tomatela";
                    _buttonText.text = "Que ortiva. . .";
                }else if(_collectedMoney >6)
                {
                    _historyText.text = "No se como conseguiste es guita pero te felicito, toma";
                    _buttonText.text = "Despues de pasar la parte de los troncos lo merezco . . .";
                }
            }
        }else if(ID==1)
        {

        }
    }
        

    public void LifeLoss()
    {
        _playerLives--;
        Debug.Log(_playerLives);
    }
    public void PickUp()
    {
        //start when a bill is picked up

        //adds 1 to the counter
        _collectedMoney++;
        _ObjectiveTxt.text = "Objetivo : recolecta " + _collectedMoney + "/6 billetes";
        //plays a sound
        //destroys the GO
    }
    public void NextZone(int l)
    {
        _camerM = true;
        _aLevel = l;
        //Debug.Log(_aLevel);
        if (_count != 0)
        {
            
            if (_aLevel == 1)//actual level
            {
                _mainCamera.transform.position = new Vector3(0, _mainCamera.transform.position.y + 15f * Time.deltaTime, _mainCamera.transform.position.z);
                _count--;
            }
            else if(_aLevel == 2)
            {
                //Debug.Log("lev 2 c");
                _mainCamera.transform.position = new Vector3(0, _mainCamera.transform.position.y + 15f * Time.deltaTime, _mainCamera.transform.position.z);
                _count--;
            }
        }else if(_count == 0)
        {
            _camerM = false;
            if (_aLevel == 1)
            {
                Spawner.levelState = 1;
                _cameraPos = _mainCamera.transform.position;
            }else
            {
                Spawner.levelState = 2;
                _cameraPos = _mainCamera.transform.position;
            }
            _count = 39;
        }
        //disable car spawners
        
        //block the map so player cant go back
    }
}
