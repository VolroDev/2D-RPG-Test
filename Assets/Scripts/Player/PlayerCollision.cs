using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public bool EnterInCollision = false;
    public GameObject collisionObject;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionObject = collision.gameObject;
        Debug.Log("We entered to " + collision.tag);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collisionObject = null;
        Debug.Log("We exit to " + collision.tag);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionObject = collision.gameObject;
        Debug.Log("We entered to " + collision.gameObject.tag);
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
}
