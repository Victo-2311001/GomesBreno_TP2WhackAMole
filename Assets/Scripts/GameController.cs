using System;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

/// <summary>
/// Classe responsable pour gérer les écrans, l'affichage des points et timer, et fonctions principales du jeu
/// </summary>
public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public enum EtatJeu { Menu, EnJeu, GameOver }

    [Header("Canvas")]
    [SerializeField] private GameObject canvasMenu;
    [SerializeField] private GameObject canvasHUD;
    [SerializeField] private GameObject canvasGameOver;

    [Header("Textes")]
    [SerializeField] private TextMeshProUGUI texteTimer;
    [SerializeField] private TextMeshProUGUI texteScoreFinal;
    [SerializeField] private TextMeshProUGUI texteScore;

    public EtatJeu etatActuel { get; private set; }

    [SerializeField] private float dureePartie = 60f;
    private float tempsEcoule;
    private bool timerActif;

    private int points = 0;
    public bool partieTerminee { get; private set; }

    //Singleton pour accèder le GameController dans les autres classes
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
        partieTerminee = false;
    }

    //Afficher le menu principal
    private void Start()
    {
        ChangerEtat(EtatJeu.Menu);
    }

    void Update()
    {
        if (timerActif)
        {
            tempsEcoule += Time.deltaTime;
            AfficherTimer();

            //Terminer jeu si temps écoulé
            if (tempsEcoule >= dureePartie)
            {
                TerminerPartie();
            }
        }
    }

    //Changer l'écran affichée selon l'état du jeu
    public void ChangerEtat(EtatJeu nouvelEtat)
    {
        etatActuel = nouvelEtat;
        canvasMenu.SetActive(etatActuel == EtatJeu.Menu);
        canvasHUD.SetActive(etatActuel == EtatJeu.EnJeu);
        canvasGameOver.SetActive(etatActuel == EtatJeu.GameOver);
    }

    public void CommencerPartie()
    {
        tempsEcoule = 0f;
        timerActif = true;
        partieTerminee = false;
        ChangerEtat(EtatJeu.EnJeu);
    }

    //Function appélée dans la collision du marteau avec une mole pour ajouter et mettre à jour les points
    public void AjouterPoint()
    {
        if(partieTerminee)
        {
            return;
        }

        points++;
        texteScore.text = $"Score : {points}";
    }

    //Afficher l'écran final avec les points
    private void TerminerPartie()
    {
        timerActif = false;
        texteScoreFinal.text = $"Score : {points}";
        Debug.Log("Jeu terminé");
        ChangerEtat(EtatJeu.GameOver);
    }

    //Relancer le menu principal
    public void Rejouer()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    //Convertir et afficher le temps
    private void AfficherTimer()
    {
        int minutes = Mathf.FloorToInt(tempsEcoule / 60f);
        int secondes = Mathf.FloorToInt(tempsEcoule % 60f);
        texteTimer.text = $"{minutes:00}:{secondes:00}";
    }
}
