using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public bool EnterInCollision = false;
    public GameObject collisionObject;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionObject = collision.gameObject;
        Debug.Log("We entered to " + collision.tag);
        EnterInCollision = true;

        NPCInteraction npc = collision.gameObject.GetComponent<NPCInteraction>();

        if (NPCInteraction.npcStatusEnum.friendly == npc.npcStatus)
        {

        }
        else if (NPCInteraction.npcStatusEnum.unfriendly == npc.npcStatus) 
        {
            npc.EventBeforeDestruction();
            Destroy(GameObject.Find(npc.npcName));
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collisionObject = null;
        Debug.Log("We exit to " + collision.tag);
        EnterInCollision = false;
    }
}
