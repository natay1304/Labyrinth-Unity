using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Notes/NoteList")]
public class NotesList : ScriptableObject
{
    [SerializeField] private List<NoteData> _notes;

    public IEnumerable<NoteData> GetNotes() => _notes;
}
