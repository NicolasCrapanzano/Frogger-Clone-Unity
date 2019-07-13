using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavoiur : MonoBehaviour
{
    private float count;
    float speed;
    private Rigidbody2D rb2D;
    private GameManager _gM;
    private  int _level;
    private float _startDelay;
    private bool _up, _down, _left, _right;
    private GameObject _startPoint;
    public bool platform = false;
    private EnemyBehaviour _enemy;
    private int _dir;
    private Rigidbody2D rb;
    private static Vector3 _checkPoint;

    // Start is called before the first frame update
    void Start()
    {

        _checkPoint = this.gameObject.transform.position;
        _startPoint = GameObject.Find("StartPoint");
        _startDelay = Time.time + 1f;
        _gM = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        count = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_level == 0 && transform.position.y > 4 && transform.position.y < 5)
        {
            _level = 1;
            _gM.NextZone(_level);
            //camera pos to Y : 10,08
            rb.MovePosition(rb.position + Vector2.up);
            _checkPoint = this.gameObject.transform.position + new Vector3(0, 1, 0);
            Debug.Log("Guardando check Point en . . . " + this.gameObject.transform.position);
            Debug.Log(_level);
        }
        if (_level == 1 && transform.position.y >= 14 && transform.position.y <= 15.5f)
        {
            _level = 2;
            _gM.NextZone(_level);
            rb.MovePosition(rb.position + Vector2.up);
            _checkPoint = this.gameObject.transform.position + new Vector3(0, 1, 0);
            Debug.Log("Guardando check Point en . . . " + this.gameObject.transform.position);
            Debug.Log(_level);
        }

        if (_startDelay < Time.time)
        {
            Move();
        }
    }
    void Move()
    {
        if (Input.GetKeyDown(KeyCode.W) )//up
        {
            rb.MovePosition(rb.position + Vector2.up);

        }
        if (Input.GetKeyDown(KeyCode.S) )//down
        {
            rb.MovePosition(rb.position + Vector2.down);

        }
        if (Input.GetKeyDown(KeyCode.A) )//left
        {
            rb.MovePosition(rb.position + Vector2.left);

        }
        if (Input.GetKeyDown(KeyCode.D) )//right
        {
            rb.MovePosition(rb.position + Vector2.right);

        }
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            //Debug.Log("agua");
            //Destroy(this.gameObject); loss 1 life
            this.gameObject.transform.position = _checkPoint;
            _gM.LifeLoss();
        }
        if(other.gameObject.CompareTag("Car1"))
        {
            //Destroy(this.gameObject); loss 1 life
            this.gameObject.transform.position = _checkPoint;
            _gM.LifeLoss();
        }
    }
    private void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.CompareTag("LimitSide"))
        {

            //Destroy(this.gameObject); loss 1 life
            this.gameObject.transform.position = _checkPoint;
            _gM.LifeLoss();
        }
        if(trig.gameObject.CompareTag("Money"))
        {
            _gM.PickUp();
            Destroy(trig.gameObject);
        }
        if(trig.gameObject.CompareTag("End"))
        {
            Debug.Log("The end");
            _gM.EndOfGame();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("testtronco"))
        {
            _enemy = collision.GetComponent<EnemyBehaviour>();
            if(_enemy != null)//null check
            {
                
                    _dir = _enemy.dir;
                    
                    if (count <= Time.time)
                    {
                        //Debug.Log("s");
                        if (_dir == 0)
                        {
                            count =  Time.time + 0.25f;
                            //Debug.Log(count);
                            rb.MovePosition(rb.position + Vector2.left);
                        } else
                        {

                            count =  Time.time + 0.25f;
                            //Debug.Log(count);
                            rb.MovePosition(rb.position + Vector2.right);

                        }
                    }
                
            }else
            {
                Debug.Log("Null object");
            }

            
           
        }
       
    }

}
