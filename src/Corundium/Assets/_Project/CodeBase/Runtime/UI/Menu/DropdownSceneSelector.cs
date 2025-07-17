using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

public class DropdownSceneSelector : MonoBehaviour
{
    public event Action<string> SceneSelected;

    [SerializeField] private TMP_Dropdown _dropdown;
    private List<string> _scenes;

    private const int HiddenScenesCount = 2;

    [Inject]
    public void Construct(BuildScenesProvider provider)
    {
        _scenes = provider.Scenes.Skip(HiddenScenesCount).ToList();
    }

    private void Start()
    {
        SetOptions(_scenes);
        InvokeSceneSelected(0);
    }

    private void OnEnable() =>
        _dropdown.onValueChanged.AddListener(InvokeSceneSelected);

    private void OnDisable() =>
        _dropdown.onValueChanged.RemoveListener(InvokeSceneSelected);

    private void InvokeSceneSelected(int index) =>
        SceneSelected?.Invoke(_scenes[index]);

    private void SetOptions(List<string> sceneNames)
    {
        List<TMP_Dropdown.OptionData> options = new(sceneNames.Count);

        foreach (string sceneName in sceneNames)
            options.Add(new(sceneName));

        _dropdown.ClearOptions();
        _dropdown.AddOptions(options);
    }
}