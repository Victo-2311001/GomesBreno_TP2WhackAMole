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
}
