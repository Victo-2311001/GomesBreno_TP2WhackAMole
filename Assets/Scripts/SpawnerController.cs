using System.Collections;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField]
    private GameObject MolePrefab;

    [SerializeField]
    private Spawn[] spawnsPossibles;

    private Spawn spawn;
    private bool spawnChoisi = false;

    private void Awake()
    {
        //S'assurer que tous les spawns sont disponibles
        foreach (var spawner in spawnsPossibles)
        {
            spawner.LibererAcces();
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private bool ChoisirSpawn()
    {
        int tentatives = 0;
        //Chercher un spawn disponible 
        while (!spawnChoisi && tentatives < 10)
        {
            //Random pour poigner un spawn aléatoire dans les tableau de spawns
            int spawnAleatoire = Random.Range(0, spawnsPossibles.Length);

            if (spawnsPossibles[spawnAleatoire].spawnDisponible)
            {
                spawn = spawnsPossibles[spawnAleatoire];
                //Rendre le spawn innacessible 
                spawn.BloquerAcces();
                spawnChoisi = true;
            }
            tentatives++;
        }
        return spawnChoisi;
    }
   
    private void SpawnerMole()
    {
        //Lancer le code qui va choisir le spawn
        ChoisirSpawn();

        if (!spawn)
        {
            return;
        }

        //Instancier la Mole dans le point spawn choisi
        GameObject nouvelleMole = Instantiate(MolePrefab, spawn.transform.position, Quaternion.identity);
        nouvelleMole.GetComponent<ReactionMole>().InitialiserSpawn(spawn);
        spawn = null;
        spawnChoisi = false;

        return;
    }

    IEnumerator SpawnLoop()
    {
        while (!GameController.Instance.partieTerminee)
        {
            SpawnerMole();
            float attente = Random.Range(1f, 3f);
            yield return new WaitForSeconds(attente);
        }
    }
}
