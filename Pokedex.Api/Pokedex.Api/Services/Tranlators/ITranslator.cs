using System.Threading.Tasks;

namespace Pokedex.Api.Services.Tranlators
{
    /// <summary>
    /// Translator service
    /// </summary>
    public interface ITranslator
    {
        /// <summary>
        /// Translate the passed in text to Yoda
        /// </summary>
        /// <param name="textToTranslate"></param>
        /// <returns></returns>
        Task<string> TranslateToYoda(string textToTranslate);

        /// <summary>
        /// Translate the passed in text to Shakespeare
        /// </summary>
        /// <param name="textToTranslate"></param>
        /// <returns></returns>
        Task<string> TranslateToShakespeare(string textToTranslate);
    }
}
