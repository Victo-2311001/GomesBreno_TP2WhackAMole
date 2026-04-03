using System;
using UnityEngine;

public class ReactionMole : MonoBehaviour
{
    Spawn spawn;

    [SerializeField] private float tempsDespawn = 1.5f;

    private bool estMorte = false;

    void Start()
    {
        Invoke("Despawn", tempsDespawn);
    }

    private void Despawn()
    {
        spawn.LibererAcces();
        Destroy(gameObject);
    }

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
            CancelInvoke("Despawn");
            spawn.LibererAcces();
            GameController.Instance.AjouterPoint();
            Debug.Log("Mole morte");
            Destroy(gameObject);
        }
    }
}