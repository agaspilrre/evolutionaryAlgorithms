using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase encargada de guardar variables constantes que utiliza el algoritmo evolutivo
/// </summary>
public static class Const
{

    //cadenciaDisparo VelocidadBala tamañoNave tamañoProyectil velocidadX  x y  distancia

    public static float[] minValues = {0.5f, 9.0f,0.05f,0.50f,5.0f,-8.0f,-2.5f,1.1f};

    public static float[] maxValues = {2.0f, 15.0f,0.2f,1.0f,15.0f,8.0f,3.2f,6.0f};

    public static short generationSize = 5;

    public static float elegibleParents = 0.1f;
    public static byte numberOfparents = 2;
    public static float mutationRatio = 0.3f;

    public static float firstSpawnTime = 0.1f, spawnInterval = 0.5f;

}
