using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{

    public GameObject[] allInstances;
    private void Awake()
    {
        allInstances = GameObject.FindGameObjectsWithTag("Terrain");
    }
    void Start()
    {


        Collider thisCollider = GetComponent<Collider>();

        foreach (GameObject instance in allInstances)
        {
            Debug.Log(instance.name);

            if (instance != this.gameObject)  //ensure it's not ignoring itself
            {
                Collider otherCollider = instance.GetComponent<Collider>();
                if (otherCollider != null)
                {
                    Physics.IgnoreCollision(thisCollider, otherCollider);
                }
            }
        }
    }

    private void Update()
    {

    }

}
