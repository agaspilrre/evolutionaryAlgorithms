using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Clase encargada de instanciar enemigos y de hacer las recombinaciones y mutaciones de estos segun su cromosoma padre
/// </summary>
public class GameController : MonoBehaviour {

    [SerializeField] private GameObject enemy;
    public List<Enemy> poblacion;


    public static GameController instances;

    public Text poblacionval;

	private int generation;
	public Text GenerationText;

	public Transform enemyParent;


    /// <summary>
    /// Funcion encargada de instanciar la primera generacion
    /// </summary>
    void Awake()
    {
		generation = 0;
        if (instances == null)
        {
            instances = this;
        }

        //DontDestroyOnLoad(instances);

        poblacion = new List<Enemy>();
        FirstGeneration();

        //InvokeRepeating("NewEnemy", Const.firstSpawnTime, Const.spawnInterval);
    }

    /// <summary>
    /// Funcion encargada de generar enemigos 
    /// tambien se encarfa de generar el cromosoma
    /// cuenta la generacion que es con cada nacimiento de enemigo
    /// </summary>
    public void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemy);
        Enemy script = newEnemy.GetComponent<Enemy>();
        script.GenerateChromosome();
        script.createAnimal();
        poblacion.Add(script);
		GenerationText.text = "GENERACION: " + generation.ToString();
			
		newEnemy.transform.SetParent (enemyParent);
    }


    /// <summary>
    /// Funcion como la anterior solo que esta recibe un array de chromosomas modificado que es el que se usara para crear la nueva nave
    /// </summary>
    /// <param name="chromosomes"></param>
    public void SpawnEnemy(float[] chromosomes)
    {
        GameObject newEnemy = Instantiate(enemy);
        Enemy script = newEnemy.GetComponent<Enemy>();
        script.GenerateChromosome();
        script.createAnimal(chromosomes);
        poblacion.Add(script);
		generation++;
		GenerationText.text ="GENERACION: " + generation.ToString();
			
		newEnemy.transform.SetParent (enemyParent);

    }

    /// <summary>
    /// Funcion encargada de spawnear enemigos de la primera generacion
    /// </summary>
    public void FirstGeneration()
    {
        for (short i = 0; i < Const.generationSize; i++)
        {
            SpawnEnemy();
        }
    }

    /// <summary>
    /// funcion encargada de la recombinacion y de la mutacion de los enemigos
    /// </summary>
	public void NewEnemy()
    {
        short[] parents = new short[Const.numberOfparents];
        for (short i = 0; i < parents.Length; i++)
        {
            parents[i] = (short)Random.Range(0, poblacion.Count * Const.elegibleParents);
        }

        //recombinacion
        float[] chromosomes = new float[Const.minValues.Length];
        for (byte i = 0; i < chromosomes.Length; i++)
        {
            short parent = (short)Random.Range(0, parents.Length);
            chromosomes[i] = poblacion[parents[parent]].chromosomes[i];
        }

        //mutacion
        for (byte i = 0; i < chromosomes.Length; i++)
        {
            if (Random.Range(0f, 1f) <= Const.mutationRatio)
                chromosomes[i] = Random.Range(Const.minValues[i], Const.maxValues[i]);
        }

			
        //crear enemigo
        SpawnEnemy(chromosomes);
    }

    // Update is called once per frame
    /// <summary>
    /// Muestra el texto que lleva la cuenta de la poblacion total de enemigos
    /// </summary>
    void Update()
    {
        poblacionval.text = "POBLACION: " + poblacion.Count.ToString();
    }


    /// <summary>
    /// Funcion que elimina a un enemigo de la lista
    /// </summary>
    /// <param name="enemy"></param>
	public void removeEnemyFromList(Enemy enemy)
	{
		//print (enemy.getAptitude());
		poblacion.Remove (enemy);

	}
}
