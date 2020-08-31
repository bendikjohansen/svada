using Pemacy.Svada.Generator.Models;
using Pemacy.Svada.Generator.Web.Controllers.Word.Models;

namespace Pemacy.Svada.Generator.Web.Controllers.Word
{
    public interface IWordConverter
    {
        WordResponse Convert(WordModel model);
        WordModel Convert(WordCreateRequest request, int categoryId);
        WordModel Convert(WordUpdateRequest request, int categoryId);
    }
}
