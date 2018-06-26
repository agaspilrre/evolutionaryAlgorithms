using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase encargada de gestionar el comportamiento de la bala enemiga
/// </summary>
public class BulletEnemy : MonoBehaviour {

    Rigidbody2D rb;
    public float speed;
    float lifeTime;

   
    GameObject whatEnemyShoot;//para reconocer que enmigo ha disparado esta bala
    /// <summary>
    /// Funcion que setea la velocidad de la bala
    /// </summary>
    /// <param name="_speed"></param>
    public void setSpeed(float _speed)
    {
        speed = _speed;
    }

    /// <summary>
    /// Funcion que setea el tamaño de la bala
    /// </summary>
    /// <param name="_scale"></param>
    public void setScale(float _scale)
    {
        transform.localScale=new Vector2(_scale,_scale);
    }

    /// <summary>
    /// Funcion que recibe como parametro el objeto que ha instanciado este objeto bala
    /// </summary>
    /// <param name="_enemy"></param>
    public void setEnemyShoot(GameObject _enemy)
    {
        whatEnemyShoot = _enemy;
    }

    // Use this for initialization
    /// <summary>
    /// funcion que obtiene el rigidbody de la bala
    /// </summary>
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    /// <summary>
    /// Funcion que controla el movimiento de la bala y su destruccio  cuando ha pasado un determinado tiempo
    /// </summary>
    void Update()
    {


        rb.velocity = new Vector2(0, -1 * speed);
        lifeTime += Time.deltaTime;

        if (lifeTime > 2)
        {
            Destroy(this.gameObject);
        }

    }
}
