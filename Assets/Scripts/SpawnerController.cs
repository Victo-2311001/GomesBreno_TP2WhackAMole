using System.Collections;
using UnityEngine;
using static GameController;

/// <summary>
/// Classe responsable pour choisir, vérifier et bloquer un spawn et instancier une mole
/// </summary>
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

    //Choisir le spawn pour la prochaine mole aléatoirememt
    private bool ChoisirSpawn()
    {
        int tentatives = 0;
        //Chercher un spawn disponible *le tentatives sert à limiter le nombre de loops pour éviter des bugs*
        while (!spawnChoisi && tentatives < 10)
        {
            //Random pour poigner un spawn aléatoire dans les tableau de spawns
            int spawnAleatoire = Random.Range(0, spawnsPossibles.Length);

            //Si le spawn choisi aléatoirement est disponible
            if (spawnsPossibles[spawnAleatoire].spawnDisponible)
            {
                //Prendre le spawn
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

    //Lancer la function qui spawn les moles avec un delay aléatoire
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            if (GameController.Instance.etatActuel == EtatJeu.EnJeu)
            {
                SpawnerMole();
            }
            float attente = Random.Range(1f, 3f);
            yield return new WaitForSeconds(attente);
        }
    }
}
