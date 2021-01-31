using DG.Tweening;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] LayerMask collisionMask;
    [SerializeField] float nexDistance;
    [SerializeField] float maxDistance;

    Rigidbody2D rb;
    Vector2 direction;
    RaycastHit2D raycastHit2D;
    bool canPush = true;

    void OnDrawGizmos()
    {
        Gizmos.color= Color.red;
        Gizmos.DrawRay(transform.position, (Vector3)direction* maxDistance);
    }

    public void Push(Vector2 direc)
    {
        direction = direc;
        Debug.Log(CanPush());
        if(CanPush())
        {
            canPush = false;
            transform.DOMove(transform.position + (Vector3) direc * nexDistance, 1f).SetEase(Ease.InOutQuad).OnComplete(()=>canPush=true).Play();
        }
            
    }

    bool CanPush()
    {
        raycastHit2D = Physics2D.Raycast(transform.position, direction, maxDistance, collisionMask);
        return canPush && raycastHit2D.collider == null;
    }
    
}
