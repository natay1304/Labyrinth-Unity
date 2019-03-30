using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Notes/Player Notes")]
public class PlayerNotes : ScriptableObject
{
    [SerializeField] private NotesList _notesList;
    private List<int> _foundNotesNombers;

    public IEnumerable<int> GetFoundNotesID() => _foundNotesNombers;

    void Awake()
    {
        _foundNotesNombers = new List<int>();
        LoadFoundNotes();
    }

    public void LoadFoundNotes()
    {
        string[] loadedString = PlayerPrefs.GetString("FoundNotes").Split(",".ToCharArray());

        if (loadedString[0] != "")
        {
            for (int i = 0; i < loadedString.Length; i++)
            {
                _foundNotesNombers.Add(Int32.Parse(loadedString[i]));
            }

            Debug.Log("Записки загружены: " + loadedString.Length);
        }
        else
        {
            Debug.Log("Записок не сохранено");
        }
    }

    public void SaveFoundNotes()
    {
        string loadString = "";

        for (int i = 0; i < _foundNotesNombers.Count; i++)
        {
            loadString += $"{_notesList.GetNotes().FirstOrDefault(x => x.Number == _foundNotesNombers[i]).Number},";
        }
        loadString = loadString.Remove(loadString.Length - 1);

        PlayerPrefs.SetString("FoundNotes", loadString);

        Debug.Log("Записки сохранены");
    }

    public void AddFoundNote(int noteNumber)
    {
        for (int i = 0; i < _foundNotesNombers.Count; i++)
        {
            if (_foundNotesNombers[i] == noteNumber)
            {
                Debug.Log("Такая записка уже есть");
                return;
            }  
        }

        _foundNotesNombers.Add(noteNumber);

        SaveFoundNotes();
    }

    public IEnumerable<NoteData> GetFoundNotes()
    {
        List<NoteData> notes = new List<NoteData>();
        for (int i = 0; i < _foundNotesNombers.Count; i++)
        {
            notes.Add(_notesList.GetNotes().FirstOrDefault(x => x.Number == _foundNotesNombers[i]));
        }
        return notes;
    }

    public void ClearFoundNotes()
    {
        _foundNotesNombers.Clear();
        PlayerPrefs.DeleteKey("FoundNotes");
    }
}
