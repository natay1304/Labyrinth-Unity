using UnityEngine;
using System.Linq;

public class NotesSaveDemonstration : MonoBehaviour
{
    [SerializeField] private PlayerNotes _playerNotes;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (_playerNotes.GetFoundNotes().Count() == 0)
            {
                Debug.Log("Ни одной записки не найдено");
            }
            else
            {
                string notesInfo = "";
                foreach (NoteData note in _playerNotes.GetFoundNotes())
                {
                    notesInfo += "Записка №" + note.Number + ". ";
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
