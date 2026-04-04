using System.Threading.Tasks;
using Game.Core.Utils;

public partial class I18nImpl
{
    private async Task<Result<I18nSection>> LoadFromServer(string locale, string sectionKey)
    {
        await 100.Millis().Delay();
        return "I18n from server is not implemented yet.".AsResult<I18nSection>();
    }
}
