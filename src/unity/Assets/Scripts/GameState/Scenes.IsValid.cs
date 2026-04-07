using UnityEngine;

public partial class Scenes
{
    /// <summary>Verifica se essa cena é válida no unity</summary>
    private bool IsValid(string sceneName)
    {
        if (sceneName == "BootstrapScene")
        {
            _logger.Error?.Log("Should not load BootstrapScene");
            return false;
        }
        var isValid = Application.CanStreamedLevelBeLoaded(sceneName);
        if (isValid)
        {
            return true;
        }
        _logger.Error?.Log($"Scene '{sceneName}' is not in build settings");
        return false;
    }
}
