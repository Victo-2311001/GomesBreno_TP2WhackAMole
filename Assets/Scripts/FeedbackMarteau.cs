using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

/// <summary>
/// Classe responsable de gèrer les feedbacks envoyés par le marteau quand on le prends et quand on touche une mole
/// </summary>
[RequireComponent(typeof(XRGrabInteractable))]
[RequireComponent(typeof(AudioSource))]
public class FeedbackMarteau : MonoBehaviour
{
    [Header("Haptique")]
    [SerializeField] private float amplitudeGrab = 1.0f;
    [SerializeField] private float dureeGrab = 0.1f;

    private XRGrabInteractable grabInteractable;
    private AudioSource audioSource;
    [SerializeField] private AudioClip sonCollision;

    //Récuperer components nécessaires pour les feedbacks
    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        audioSource = GetComponent<AudioSource>();

        // Configurer l'AudioSource pour du son positionnel
        audioSource.spatialBlend = 1f; // 100% 3D
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        audioSource.maxDistance = 5f;
    }

    void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrabEntered);
        grabInteractable.selectExited.AddListener(OnGrabExited);
    }

    void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabEntered);
        grabInteractable.selectExited.RemoveListener(OnGrabExited);
    }

    //Quand on prends le marteau
    private void OnGrabEntered(SelectEnterEventArgs args)
    {
        // Jouer le son à la position de l'objet
        audioSource.Play();

        // Récupérer le contrôleur depuis l'interactor
        var controller = args.interactorObject.transform.GetComponent<XRBaseInputInteractor>();

        controller.SendHapticImpulse(amplitudeGrab, dureeGrab);
    }

    //Quand on lache le marteau
    private void OnGrabExited(SelectExitEventArgs args)
    {
        // Vibration plus courte et moins forte au relâchement
        var controller = args.interactorObject.transform.GetComponent<XRBaseInputInteractor>();

        controller.SendHapticImpulse(amplitudeGrab * 0.3f, dureeGrab * 0.5f);
    }

    //Feedback sonoro quand marteau touche une mole
    //*Gèré par le marteau pour éviter delay dans le destroy de la mole quand elle est touchée*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mole"))
        {
            audioSource.PlayOneShot(sonCollision);
        }
    }
}
