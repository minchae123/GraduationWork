using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResearchTable : PlayerMain
{
    [SerializeField] GameObject _researchPanel;
    [SerializeField] TMP_Dropdown _curTrashDropdown;
    [SerializeField] TMP_Text _resultText;
    [SerializeField] List<ObjectType> _curTrash;

    private void Awake()
    {
        _curTrashDropdown.ClearOptions();

        List<string> trashNames = new List<string>();
        foreach (ObjectType trash in _curTrash)
        {
            trashNames.Add(trash._ObjectName);
        }

        _curTrashDropdown.AddOptions(trashNames);
    }

    private void Start()
    {
    }




    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_isKeyDown)
        {
            _researchPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _researchPanel.SetActive(false);
        _isKeyDown = false;
    }

    public void ResearchBtn()
    {
        _resultText.text = _curTrash[_curTrashDropdown.value]._ObjectName;
    }
}
