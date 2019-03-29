using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NotesSaveDemonstration : MonoBehaviour
{
    [SerializeField] private NotesList _notesList;
    [SerializeField] private PlayerNotes _playerNotes;

    private List<int> _foundNotesNombers;
    private void Awake()
    {
        _foundNotesNombers = _playerNotes.GetFoundNotesID();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (_foundNotesNombers.Count == 0)
            {
                Debug.Log("Ни одной записки не найдено");
            }
            else
            {
                string notesInfo = "";
                for (int i = 0; i < _foundNotesNombers.Count; i++)
                {
                    notesInfo += "Записка №" + _notesList.GetNotes().FirstOrDefault(x => x.Number == _foundNotesNombers[i]).Number + ". ";
                }
                Debug.Log(notesInfo);
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            _playerNotes.ClearFoundNotes();
            Debug.Log("Найденные записки удалены");
        }
    }
}
