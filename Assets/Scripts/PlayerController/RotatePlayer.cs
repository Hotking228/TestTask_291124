using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    public enum RotateDirection
    {
        Right,
        Left
    }
    [SerializeField] private Transform nextPlatform;
    [SerializeField] private RotateDirection direction;

    private void OnTriggerEnter(Collider other)
    {
        Character character = other.transform.parent.GetComponent<Character>();

        if (character != null)
        {
            character.SetRotation(direction == RotateDirection.Right ? 1 : -1, nextPlatform.position);
            gameObject.SetActive(false);
        }
    }
}
