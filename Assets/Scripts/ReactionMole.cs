using System;
using UnityEngine;

public class ReactionMole : MonoBehaviour
{
    Spawn spawn;

    //Récupere le spawn pour le libérer quand Mole est frappé
    public void InitialiserSpawn(Spawn monSpawn)
    {
        spawn = monSpawn;
    }

    //Détecter le contact du marteau
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Marteau"))
        {
            Destroy(gameObject);
            Debug.Log("Mole morte");
            spawn.LibererAcces();
        }
    }
}