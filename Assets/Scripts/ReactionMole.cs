using System;
using UnityEngine;

/// <summary>
/// Classe responsable pour gèrer les différentes réactions des moles: spawn, toucher et despawn
/// </summary>
public class ReactionMole : MonoBehaviour
{
    Spawn spawn;

    //Delay pour tuer la mole
    [SerializeField] private float tempsDespawn = 1.5f;

    private bool estMorte = false;

    //Assigner la function à la mole
    void Start()
    {
        Invoke("Despawn", tempsDespawn);
    }

    //Si la mole n'st pas tu.e à temps, elle disparâit et libère le spawn
    private void Despawn()
    {
        spawn.LibererAcces();
        Destroy(gameObject);
    }

    //Récuperer le spawn qui a été assigné pour la mole
    public void InitialiserSpawn(Spawn monSpawn)
    {
        spawn = monSpawn;
    }

    //Détecter le contact du marteau
    private void OnCollisionEnter(Collision collision)
    {
        //Éviter plusieurs collisions
        if (estMorte)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Marteau"))
        {
            estMorte = true;
            //Enlever le despawn automatique pour éviter des erreurs 
            CancelInvoke("Despawn");

            //Libérer le spawn pour une autre possible mole
            spawn.LibererAcces();

            //Ajouter un point et ensuite détruire la mole
            GameController.Instance.AjouterPoint();
            Destroy(gameObject);
        }
    }
}