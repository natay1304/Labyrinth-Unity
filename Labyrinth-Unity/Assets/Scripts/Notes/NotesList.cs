using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Notes/NoteList")]
public class NotesList : ScriptableObject
{
    [SerializeField] private List<NoteData> _notes;

    public IEnumerable<NoteData> GetNotes() => _notes;

    public NoteData GetNote(int noteNumber)
    {
        return _notes.FirstOrDefault(x => x.Number == noteNumber);
    }
}
