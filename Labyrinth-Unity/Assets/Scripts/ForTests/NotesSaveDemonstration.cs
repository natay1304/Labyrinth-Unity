using UnityEngine;
using System.Linq;

public class NotesSaveDemonstration : MonoBehaviour
{
    [SerializeField] private NotesList _notesList;
    [SerializeField] private PlayerNotes _playerNotes;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (_playerNotes.GetFoundNotesID().Count() == 0)
            {
                Debug.Log("Ни одной записки не найдено");
            }
            else
            {
                string notesInfo = "";

                foreach (int number in _playerNotes.GetFoundNotesID())
                {
                    notesInfo += "Записка №" + _notesList.GetNotes().FirstOrDefault(x => x.Number == number).Number + ". ";
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
