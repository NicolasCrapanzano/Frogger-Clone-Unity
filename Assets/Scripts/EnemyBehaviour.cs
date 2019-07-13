using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int dir,_askin;
    public float spd;
    private SpriteRenderer _sr;
    private float r, g, b;
    [SerializeField]
    private Sprite[] _skin;
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        if (this.gameObject.CompareTag("Car1"))
        {
            
            r = Random.Range(0f, 1f);
            g = Random.Range(0f, 1f);
            b = Random.Range(0f, 1f);
            _sr.color = new Color(r, g, b, 1f);
            _sr.sprite = _skin[_askin];
        }
       else if(this.gameObject.CompareTag("testtronco")||this.gameObject.CompareTag("Water"))
        {
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if(dir == 0 && this.gameObject.transform.position.x <= -10)
        {
            Destroy(this.gameObject);
        }
        else if (dir == 1 && this.gameObject.transform.position.x >= 10)
        {
            Destroy(this.gameObject);
        }
    }
    private void Movement()
    {
        if (dir == 0) //to left
        {
            transform.position = transform.position - new Vector3(spd, 0, 0) * Time.deltaTime;
        }
        else if(dir == 1)//to right
        {
            transform.position = transform.position + new Vector3(spd, 0, 0) * Time.deltaTime;
            _sr.flipX = true;
        }
    }
    public void ChangeDir(int d,int s,int c) //Pre-set parameters for the car
    {
        dir = d;
        spd = s;
        _askin = c;
    }
   
}
