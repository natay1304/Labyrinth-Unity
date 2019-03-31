using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] private PlayerNotes _playerNotes;
    [SerializeField] private NotesList _notesList;
    [SerializeField] private int _noteNumber;

    public int NoteNumer => _noteNumber;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Записка подобрана");
            _playerNotes.AddFoundNote(_noteNumber);
            Destroy(gameObject);
        }
    }

    public NoteData GetNoteData()
    {
        return _notesList.GetNote(_noteNumber);
    }
}
