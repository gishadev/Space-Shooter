using UnityEngine;

public class ReflectHandler : MonoBehaviour
{
    [SerializeField] private Transform playerTrans;
    [SerializeField] private float boundsOffset;

    public float MaxY => _cam.orthographicSize + boundsOffset;
    public float MaxX => _cam.orthographicSize * Screen.width / Screen.height + boundsOffset;

    Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void LateUpdate()
    {
        if (!IsInView(playerTrans.position))
            Reflect(playerTrans);
    }

    private bool IsInView(Vector2 position)
        => position.x > -MaxX && position.x < MaxX && position.y > -MaxY && position.y < MaxY;
    private void Reflect(Transform trans)
    {
        var reflectedPos = trans.position * -Vector2.one;
        var sign = new Vector2(Mathf.Sign(reflectedPos.x), Mathf.Sign(reflectedPos.y));
        trans.position = reflectedPos + (0.1f * -sign);
    }
}
