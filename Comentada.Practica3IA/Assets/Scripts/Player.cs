using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// Clase encargada de controlar los inputs del player asi como su respuesta y comportamiento a los mismos
/// </summary>
public class Player : MonoBehaviour {

    public float speed;
    public int life;
    public Text lifeText;
    public Vector2 targetR;
    public Vector2 targetL;

    GameObject instanceShoot;

    public GameObject bullet;

	private Transform bulletParent;


    // Use this for initialization
    /// <summary>
    /// funcion que obtiene el objeto vacio bullet para guardar las instancias de bala dentro de este objeto
    /// </summary>
    void Start () {
		bulletParent = GameObject.Find ("Bullets").transform;

       
		
	}
	
	// Update is called once per frame
    /// <summary>
    /// controla el movimiento de la nave limitandolo dentro de la pantalla
    /// controla en general los inputs que tiene la nave
    /// pinta el texto de la vida que tiene la nave en todo momento
    /// </summary>
	void Update () {

        lifeText.text = "LIFE: " + life;

       //MOVIMIENTO
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //rb.MovePosition(transform.position + transform.right * Time.deltaTime);
            Vector3 aux = Vector3.MoveTowards(transform.position, targetR, speed);

            transform.position = aux;
        }
       
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // rb.MovePosition(transform.position - transform.right * Time.deltaTime);
            Vector3 aux = Vector3.MoveTowards(transform.position, targetL, speed);

            transform.position = aux;
        }


        //DISPARO

        if(Input.GetKeyDown(KeyCode.Space))
        {
            //llamar a la funcion de disparo
            Shoot();
        }
    }

    /// <summary>
    /// funcion que instancia los proyectiles de la nave y le asigna un objeto donde se guardan al instanciarse
    /// </summary>
    public void Shoot()
    {
        instanceShoot = Instantiate(bullet, new Vector3(transform.position.x,transform.position.y+1,0), Quaternion.identity);
		instanceShoot.transform.SetParent (bulletParent);
    }

    /// <summary>
    /// Funcion que controla si hemos sido dañados por el proyectil enemigo y resta vida en consecuencia.
    /// comprueba si la vida llega a cero para reiniciar el nivel
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="bulletEnemy")
        {
            life--;
            Destroy(collision.gameObject);
			if (life <= 0) 
			{
				SceneManager.LoadScene ("Scene1");
			}
				
        }
    }

    //public void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "bulletEnemy")
    //    {
          
    //    }
    //}


}



