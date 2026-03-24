using UnityEngine;

public class Spawn : MonoBehaviour
{
    public bool spawnDisponible {  get; private set; }

    public void BloquerAcces()
    {
        spawnDisponible = false;
    }

    public void LibererAcces()
    {
        spawnDisponible = true;
    }
}
