using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class mapGenerate : MonoBehaviour {
    public List<List<GameObject>> textures = new List<List<GameObject>>();
    List<GameObject> mapTextures = new List<GameObject>();
    //GameObject[,] textures = new GameObject[2, 2];
    List<List<string>> textureNames = new List<List<string>>() { new List<string>() { "GameJar", "GameJar" }, new List<string>() { "Cube", "Cube" } }; //teksturos kiekvienam lygiui
    public int[,] textureLimit = new int[,] { { 10, 10 }, { 1, 1 } }; //teksturu skaicius kiekvienam lygiui
    List<string> mapNames = new List<string>() { "Tile" }; //map tile pavadinimai
    List<int> mapTextureRenderLimit = new List<int>() { 7 }; //map tile skaicius kiekvienam pavadinimui
    //public GameObject hiImPlayer; //cia player sveikinasi su skaitytoju
    Vector3 firstCoords = new Vector3(0, 0f, 0f); //I KAIRE
    Vector3 lastCoords = new Vector3(0, 0, 0f); //I DESINE
    public Vector3 v3Null = new Vector3(0, 0, 0); //niekam tikes vektorius
    public string priorMapTileName = "";
    public int priorMapTileNr = 0;
    float minGap = 0.1f;
    int[] levels = new int[] { 1, 2 }; //zaidimo lygiai
    float[,] distanceBetweenTextures = new float[,] { { 1.251f, 3.346f }, { 1.151f, 2.1346f } }; //not implemented
    int currLvl = 0;
    // Use this for initialization
    void Start()
    {
        //inicijuojam mapa pagal teksturas
        for (int i = 0; i < mapNames.Count; i++)
        {
            for (int j = 0; j < mapTextureRenderLimit[i]; j++)
            {
                // Inicijuoja
                mapTextures.Add(Instantiate(Resources.Load("Prefabs/" + mapNames[i], typeof(GameObject)) as GameObject));
                // Name priskiria
                mapTextures[j].name = mapNames[i] + j.ToString();
                mapTextures[j].transform.position = v3Null;
            }
        }
        for (int i = 0; i < mapNames.Count; i++)
        {
            for (int j = 0; j < mapTextures.Count; j++)
            {
                MeshRenderer mrMapTextures = mapTextures[j].GetComponent<MeshRenderer>();
                Vector3 mapTextureV3 = mrMapTextures.bounds.size;
                Debug.Log(mapTextureV3);
                mapTextureV3 = new Vector3(0f, 0f, 9f); //qucik fix nes prefab be dydzio
                Debug.Log(mapTextureV3);
                Debug.Log(mapTextureV3);
                lastCoords += mapTextureV3;
                mapTextures[j].transform.position = new Vector3(0, 0, lastCoords.z);
                
                //generateAllFuckingShitPls(mapTextures[j].name, mapTextures[j].transform, lastCoords);
            }
        }
    }

    void generateAllFuckingShitPls(string mapName, Transform parent, Vector3 coords)
    {
        //for (int k = 0; k < levels.Length; k++)
        //{
        if (currLvl >= textures.Count)
        {
            textures.Add(new List<GameObject>());
        }
        for (int z = 0; z < textureNames[currLvl].Count; z++)
        {
            for (int m = 0; m < textureLimit[currLvl, z]; m++)
            {
                //GameObject gameObj = GameObject.Find(mapName); //(mapName);
                //MeshRenderer mrGameObj = gameObj.GetComponent<MeshRenderer>();
                //Vector3 mapTextureV3 = mrGameObj.bounds.size;
                //Debug.Log(mrGameObj.bounds.size);
                //mapTextureV3
                Vector3 mapTextureV3 = new Vector3(8f, 0f, 9f); //qucik fix nes prefab be dydzio
                Debug.Log(coords);
                //Debug.Log(textures.Count + " " + textures[textures.Count - 1]);
                textures[textures.Count - 1].Add(Instantiate(Resources.Load("Prefabs/" + textureNames[currLvl][z], typeof(GameObject)) as GameObject));
                textures[textures.Count - 1][textures[textures.Count - 1].Count - 1].transform.position = new Vector3(
                            UnityEngine.Random.Range(-2f, 2f), //coords.x - mapTextureV3.x / 2 , coords.x + mapTextureV3.x / 2
                            UnityEngine.Random.Range(0, 0), UnityEngine.Random.Range(coords.z - 9f + minGap, coords.z - minGap));
                textures[textures.Count - 1][textures[textures.Count - 1].Count - 1].name = mapName + "_" + currLvl.ToString() + "_" + (textures[textures.Count - 1].Count - 1).ToString();
                textures[textures.Count - 1][textures[textures.Count - 1].Count - 1].transform.parent = parent;
            }
        }
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collide)
    {
        // Tile Name nepakeicia
        if (priorMapTileName == "")
        {
            priorMapTileName = collide.gameObject.name;
            for (int i = 0; i < mapNames.Count; i++)
            {
                if (collide.gameObject.name.Contains(mapNames[i]) == true)
                {
                    int.TryParse(priorMapTileName.Replace(mapNames[i], ""), out priorMapTileNr);
                }
            }
        }
        else
        {
            for (int i = 0; i < mapNames.Count; i++)
            {
                if (collide.gameObject.name.Contains(mapNames[i]) == true)
                {
                    int currMapTileNr = 0;
                    int.TryParse(collide.gameObject.name.Replace(mapNames[i], ""), out currMapTileNr);
                    //Debug.Log(currMapTileNr);
                    if (priorMapTileNr == currMapTileNr)
                    {
                        Debug.Log("This is the wriong wey Bruddas Brater");
                    }

                    if (priorMapTileNr < currMapTileNr)
                    {
                        string firstTileName = mapTextures[mapTextures.Count - 1].name;  // mapTextures[0].name;
                        Destroy(mapTextures[0]);
                        mapTextures.RemoveAt(0);
                        mapTextures.Add(Instantiate(Resources.Load("Prefabs/" + mapNames[i], typeof(GameObject)) as GameObject));

                        int nr = int.Parse(firstTileName.Replace(mapNames[i], ""));
                        nr++;
                        firstTileName = mapNames[i] + nr;

                        mapTextures[mapTextures.Count - 1].name = firstTileName;
                        MeshRenderer mr = mapTextures[mapTextures.Count - 1].GetComponent<MeshRenderer>();
                        Vector3 mapTextureV3 = new Vector3(0f, 0f, 9f); //qucik fix nes prefab be dydzio
                        lastCoords += mapTextureV3;
                        firstCoords += mapTextureV3;
                        mapTextures[mapTextures.Count - 1].transform.position = new Vector3(0, 0, lastCoords.z);
                        //generateAllFuckingShitPls(mapTextures[mapTextures.Count - 1].name, mapTextures[mapTextures.Count - 1].transform, lastCoords);
                        priorMapTileNr = currMapTileNr;
                    }

                    else if (priorMapTileNr > currMapTileNr)
                    {
                        string lastTileName = mapTextures[0].name; //mapTextures[mapTextures.Count - 1].name;
                        Debug.Log(mapTextures[mapTextures.Count - 1]);
                        Destroy(mapTextures[mapTextures.Count - 1]);
                        mapTextures.RemoveAt(mapTextures.Count - 1);
                        mapTextures.Insert(0, Instantiate(Resources.Load("Prefabs/" + mapNames[i], typeof(GameObject)) as GameObject));

                        int nr = int.Parse(lastTileName.Replace(mapNames[i], ""));
                        nr--;
                        lastTileName = mapNames[i] + nr;

                        mapTextures[0].name = lastTileName;
                        MeshRenderer mr = mapTextures[0].GetComponent<MeshRenderer>();
                        
                        //generateAllFuckingShitPls(mapTextures[0].name, mapTextures[0].transform, firstCoords);
                        Vector3 mapTextureV3 = new Vector3(0f, 0f, 9f); //qucik fix nes prefab be dydzio
                        mapTextures[0].transform.position = new Vector3(0, 0, firstCoords.z);
                        firstCoords -= mapTextureV3;
                        lastCoords -= mapTextureV3;
                        
                        priorMapTileNr = currMapTileNr;
                    }
                }
            }
        }
    }
}
