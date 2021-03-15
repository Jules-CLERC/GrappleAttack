using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private string[] PlanetsArray = { "Pink", "Yellow", "Blue", "Green" };
    //Path in Assets contains all Prefabs
    private string PathPrefabs = "Assets/Prefabs/";
    //Dictionnary with planets colors and objects, useful for instantiate prefab
    private Dictionary<string, Object> PlanetsPrefabs = new Dictionary<string, Object>();
    private Object PlayerPrefab;
    private Object RockPrefab;

    // Start is called before the first frame update
    void Start()
    {        
        GetPrefabs();
        Instantiate(PlayerPrefab);
        InstantiatePlanets(-100, 100, -100, 100);
        //InstantiateCloudsRocks(-100, 100, -100, 100);
    }

    void InstantiateCloudsRocks(int minX, int maxX, int minY, int maxY)
    {
        int nbClouds = Random.Range(5,10);
        int sizeCloud = 10;
        for (int i = 0; i < nbClouds; i++)
        {
            int xTmp = Random.Range(minX, maxX);
            int yTmp = Random.Range(minY, maxY);
            InstantiateCloudRocks(xTmp - sizeCloud / 2, xTmp + sizeCloud / 2, yTmp - sizeCloud / 2, yTmp + sizeCloud / 2);
        }
    }

    void InstantiateCloudRocks(int minX, int maxX, int minY, int maxY)
    {
        int nbRocks = Random.Range(10, 30);
        for(int i = 0; i < nbRocks; i++)
        {
            float xTmp = Random.Range(minX, maxX);
            float yTmp = Random.Range(minY, maxY);
            Instantiate(RockPrefab, new Vector3(xTmp, yTmp, 0), Quaternion.identity);
        }
    }

    void InstantiatePlanets(int minX, int maxX, int minY, int maxY)
    {
        int nbPlanets = Random.Range(10, 100);
        for (int i = 0; i < nbPlanets; i++)
        {
            float xTmp = Random.Range(minX, maxX);
            float yTmp = Random.Range(minY, maxY);
            int randTmp = Random.Range(0, PlanetsArray.Length);
            Instantiate(PlanetsPrefabs[PlanetsArray[randTmp]], new Vector3(xTmp, yTmp, 0), Quaternion.identity);
        }
    }

    void GetPrefabs()
    {
        GetPrefabsPlanets();
        GetPrefabRock();
        GetPrefabPlayer();
    }

    void GetPrefabPlayer()
    {
        PlayerPrefab = AssetDatabase.LoadAssetAtPath(PathPrefabs + "Player.prefab", typeof(GameObject));
    }

    void GetPrefabRock()
    {
        RockPrefab = AssetDatabase.LoadAssetAtPath(PathPrefabs + "Rock.prefab", typeof(GameObject));
    }

    void GetPrefabsPlanets()
    {
        string[] PlanetsArray = { "Pink", "Yellow","Blue", "Green" };
        foreach (string planetString in PlanetsArray)
        {
            Object prefab = AssetDatabase.LoadAssetAtPath(PathPrefabs + "Planets/" + planetString + "Planet.prefab", typeof(GameObject));
            PlanetsPrefabs.Add(planetString, prefab);
        }
    }
}
