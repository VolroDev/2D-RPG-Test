using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private Text goldCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        goldCount.text = GameObject.Find("Player").GetComponent<PlayerInventory>().gold.ToString();

    }
}
