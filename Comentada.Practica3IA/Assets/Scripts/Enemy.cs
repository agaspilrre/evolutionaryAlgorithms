using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase encargada de gestionar el comportamiento del enemigo y de asignar sus cromosomas para el algoritmo evolutivo
/// </summary>
public class Enemy : MonoBehaviour {
    GameObject instanceEshoot;
    public GameObject EnemyBullet;
    public float cadenciaDisparo;
    public float speedShoot;
    public float scaleBullet;

	private Transform bulletParent;

    float timer;

    //cadenciaDisparo VelocidadBala tamañoNave tamañoProyectil velocidadX  x y  distancia
    public float[] chromosomes;

    [SerializeField] private float aptitud = 0, tiempoNacimiento;
    [SerializeField] public short generacion = 0;

    private Rigidbody2D rb;
    private float dist, distMax;
    private Vector3 puntoOrigen;

	private bool directionChangeDone;

    /// <summary>
    /// funcion que genera valores aleatorios entre el min y el max value en el array de cromosomas
    /// </summary>
    public void GenerateChromosome()
    {
		bulletParent = GameObject.Find ("Bullets").transform;
        chromosomes = new float[Const.minValues.Length];
        for (byte i = 0; i < chromosomes.Length; i++)
        {
            chromosomes[i] = Random.Range(Const.minValues[i], Const.maxValues[i]);
        }
    }

	
	// Update is called once per frame
    /// <summary>
    /// funcion que controla la cadencia de disparo de la nave enemiga en funcion del time
    /// controla el cambio de sentido cuando la nave pasa del limite de movimiento
    /// </summary>
	void Update () {

        timer += Time.deltaTime;
        if (timer >= cadenciaDisparo)
        {
            enemyShoot();
            timer = 0;
        }

        //cuando llega a la distancia max de movimiento cambia de direccion(solo en X)
		if ( !directionChangeDone && (puntoOrigen - transform.position).magnitude >= distMax)
        {
			directionChangeDone = true;
			Invoke ("resetDirectionChange", 0.5f);
            rb.velocity *= -1;
		}
    }

    /// <summary>
    /// funcion que crea un animal a partir del cromosoma obtenido como parametro no coge el array generado aleatoriamente
    /// </summary>
    /// <param name="newChromosome"></param>
    public void createAnimal(float[] newChromosome)
    {
        chromosomes = newChromosome;
        createAnimal();

    }

    /// <summary>
    /// Funcion que asigna un rigibody a la nave enemiga y establece los cromosomas en cada una de sus propiedades, movimiento, cadencia, tamaño etc
    /// </summary>
    public void createAnimal()
    {
        //cadenciaDisparo VelocidadBala tamañoNave tamañoProyectil velocidadX  x y  distancia
        rb = GetComponent<Rigidbody2D>();

        cadenciaDisparo = chromosomes[0];
        speedShoot = chromosomes[1];
        this.transform.localScale = new Vector2(chromosomes[2], chromosomes[2]);
        scaleBullet = chromosomes[3];
        rb.velocity = new Vector2(chromosomes[4], 0);
        transform.position = new Vector3(chromosomes[5], chromosomes[6], 0);
        puntoOrigen = transform.position;
        distMax = chromosomes[7];
       


        tiempoNacimiento = Time.timeSinceLevelLoad;//?¿?¿?¿?

    }

    /// <summary>
    /// Funcion que instancia las balas del enemigo
    /// le pasa al script de balas quien es la nave enemiga que esta disparando
    /// modifica el tamaño y la velocidad de las balas que dispara
    /// </summary>
    public void enemyShoot()
    {

        instanceEshoot = Instantiate(EnemyBullet, new Vector3(transform.position.x, transform.position.y - 1, 0), Quaternion.identity);
		instanceEshoot.transform.SetParent (bulletParent);

        instanceEshoot.GetComponent<BulletEnemy>().setEnemyShoot(this.gameObject);
        instanceEshoot.GetComponent<BulletEnemy>().setSpeed(speedShoot);
        instanceEshoot.GetComponent<BulletEnemy>().setScale(scaleBullet);
    }

    /// <summary>
    /// Funcion que se encarga de detectar la colision de la bala del player en la bala enemiga
    /// Destruye la nave y los proyectiles disparados por la misma
    /// elimina el objeto nave enemiga de todas las listas.
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="bullet")
        {
            //GameController.instances.poblacion.Remove(this);
            if (instanceEshoot != null)
                Destroy(instanceEshoot.gameObject);

			GameController controller = GameObject.Find ("GameController").GetComponent<GameController> ();
			controller.removeEnemyFromList(this);
			controller.NewEnemy ();
            
			Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }

    /// <summary>
    /// Funcion que chekea por tiempo un bool para la condicion de cambiar direcciond e la nave evitando asi que entre en bucle el movimiento de la nave
    /// </summary>
	private void resetDirectionChange()
	{
		directionChangeDone = false;
	}

}
