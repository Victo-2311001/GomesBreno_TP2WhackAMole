using UnityEngine;

public class ReactionMole : MonoBehaviour
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Marteau"))
        {
            Destroy(gameObject);
            Debug.Log("Mole morte");
        }
    }
}