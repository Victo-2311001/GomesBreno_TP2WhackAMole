using System;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

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

    public EtatJeu etatActuel { get; private set; }

    [SerializeField] private float dureePartie = 60f;
    private float tempsEcoule;
    private bool timerActif;

    private int points = 0;
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
        partieTerminee = false;
    }

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

            if (tempsEcoule >= dureePartie)
            {
                TerminerPartie();
            }
        }
    }

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

    public void AjouterPoint()
    {
        points++;
        Debug.Log("Score: " + points);
    }

    private void TerminerPartie()
    {
        timerActif = false;
        int score = Mathf.Max(100, 1000 - Mathf.FloorToInt(tempsEcoule) * 10);
        texteScoreFinal.text = $"Score : {score}";
        ChangerEtat(EtatJeu.GameOver);
    }

    public void Rejouer()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    private void AfficherTimer()
    {
        int minutes = Mathf.FloorToInt(tempsEcoule / 60f);
        int secondes = Mathf.FloorToInt(tempsEcoule % 60f);
        texteTimer.text = $"{minutes:00}:{secondes:00}";
    }
}
