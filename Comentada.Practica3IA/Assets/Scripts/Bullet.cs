using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase encargada de gestionar el comportamiento de las balas del player
/// </summary>
public class Bullet : MonoBehaviour {


    Rigidbody2D rb;
    public float speed;
    float lifeTime;

    // Use this for initialization
    /// <summary>
    /// Funcion donde obtenemos el componente rigidbody de la bala
    /// </summary>
    void Start () {

        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
    /// <summary>
    /// funcion que controla el movimiento y la destruccion de este objeto bala
    /// </summary>
	void Update () {


        rb.velocity = new Vector2(0, 1 * speed);
        lifeTime += Time.deltaTime;

        if(lifeTime>2)
        {
            Destroy(this.gameObject);
        }

    }
}
