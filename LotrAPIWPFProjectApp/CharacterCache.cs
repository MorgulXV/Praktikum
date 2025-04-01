using LotrAPIWPFProjectApp;

namespace LotrAPIWPFApp
{
    public class CharacterCache
    {
        public static IList<CharacterDocs>? characterCache { get; set; } = new List<CharacterDocs>();
    }
}