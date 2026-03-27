using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private int points = 0;

    [SerializeField] private float dureePartie = 60f;

    private float tempsRestant;
    public bool partieTerminee { get; private set; }

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        CommencerPartie();
    }

    void Update()
    {
        if (partieTerminee) return;

        tempsRestant -= Time.deltaTime;

        if (tempsRestant <= 0f)
        {
            Debug.Log(tempsRestant);
            tempsRestant = 0f;
            TerminerPartie();
        }
    }

    public void CommencerPartie()
    {
        points = 0;
        tempsRestant = dureePartie;
        partieTerminee = false;
    }

    public void AjouterPoint()
    {
        points++;
        Debug.Log("Score: " + points);
    }

    private void TerminerPartie()
    {
        partieTerminee = true;
        Debug.Log("Partie terminée! Score: " + points);
    }
}
