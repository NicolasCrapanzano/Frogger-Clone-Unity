using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float _time;
    private float _extratime;
    [SerializeField]
    private GameObject[] _enemies;
    private int _enemy;
    private GameObject _aEnemy;
    private EnemyBehaviour _enemyBehaviour;
    private int _randSpawn;
    private int _eDir,_eSpd,_eskin;
    public bool isaActive=true;
    public static int levelState=0;
    private int _level;
    //variables that controls the spawning
    private int _minSpawn,_maxSpawn;

    private void Start()
    {
        //Debug.Log(this.gameObject.name + "Inicio");
        if(this.gameObject.transform.position.y >=-5 && this.gameObject.transform.position.y <=5)
        {
            //the spawner is in the first zone
            _extratime = 2f;
            
            _level = 1;
            if (this.gameObject.CompareTag("Spawner1"))
            {
                _enemy = 0;
                _eSpd = 4;
                _eskin = 0;
            }
            else if (this.gameObject.CompareTag("Spawner2"))
            {
                _enemy = 0;
                _eskin = 1;
                //change skin
                _eSpd = 8;
            }


        }
        else if(this.gameObject.transform.position.y > 5  && this.gameObject.transform.position.y <= 15)
        {
            //the spawner is in the second zone (water)
            _extratime = 1f;
            
            if (this.gameObject.CompareTag("Spawner1"))
            {
                _enemy = 1;
                _eSpd = 4;
            }else if(this.gameObject.CompareTag("Spawner2"))
            {
                _enemy = 1;
                _eSpd = 6;
            }
            isaActive = false;
            _level = 2;
        }
    }
    private void Parameters()
    {
        //Direction of the enemy
        if (this.gameObject.transform.position.x > 0)//left
        {
            _eDir = 0;
            _enemyBehaviour.ChangeDir(_eDir,_eSpd,_eskin);
        }else //right
        {
            _eDir = 1;
            _enemyBehaviour.ChangeDir(_eDir, _eSpd,_eskin);
           
            //_aEnemy.SendMessage("ChangeDir", _eDir);
        }
        //speed / tipe of enemy
       

    }

    void Update()
    {
        if(levelState == 1)
        {
            if(_level==1)
            {
                isaActive = false;   
            }
            if(_level==2)
            {
                isaActive = true;
            }
        }
        if (isaActive == true)
        {
            if(_level==1)
            {
                Spawner1();
            }else if(_level == 2)
            {
                Spawner2();
            }
        }
    }
    private void Spawner1()
    {
        if (_time <= Time.time)
        {

            _time = Time.time + _extratime; 
            _randSpawn = Random.Range(0, 100);

            if (_maxSpawn <= 4)
            {
                if (_randSpawn > 0 && _randSpawn < 40)
                {
                    _maxSpawn++;
                    Spawning(_enemy);
                }
                else
                {
                    _minSpawn++;
                }
            }
            else
            {
                _maxSpawn = 0;
            }
            if (_minSpawn >= 4)
            {
                _time = Time.time;
            }
        }
    }
    private void Spawner2() //change from random to prestablished?
    {
        if (_time <= Time.time)
        {

            _time = Time.time + _extratime;
            _randSpawn = Random.Range(0, 100);

           
                if (_randSpawn >= 0 && _randSpawn < 30)
                {
                    _maxSpawn++;
                    Spawning(_enemy);
                }
                else if (_randSpawn >= 30 && _randSpawn <= 100)
                {
                    _maxSpawn++;
                    Spawning(_enemy+1);
                }
                else
                {
                    _minSpawn++;
                }
           
        }
        
        
    }
    private void Spawning(int sp)
    {
        _minSpawn = 0;
        _aEnemy = Instantiate(_enemies[sp], transform.position, Quaternion.identity);
        _enemyBehaviour = _aEnemy.GetComponent<EnemyBehaviour>();
        Parameters();
    }
}
