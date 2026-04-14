using System;

public partial class CardView
{
    private void SetUIViewTree()
    {
        _uiDocument.visualTreeAsset = CurrentState switch
        {
            State.SMALL => CardSmallVT,
            State.DETAILS => CardDetailsVT,
            _ => throw new ArgumentOutOfRangeException(nameof(CurrentState), CurrentState.ToString()),
        };
    }
}
