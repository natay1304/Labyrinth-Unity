using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindingNotes : MonoBehaviour
{
    public GameObject Note;
    public string Message;
    public int Id;

    private void OnTriggerEnter(Collider other)
    {
        Note.SetActive(false);
        other.GetComponent<Player>().SaveNote(Id, Message);
    }
}
