using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Note")]
public class NoteData : ScriptableObject
{
    [SerializeField] private int _number;
    [TextArea(10, 20)][SerializeField] private string _message;

    public int Number => _number;
    public string Message => _message;
}
