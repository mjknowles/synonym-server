
public class Synonyms
{
  public readonly static Synonyms NullSynonyms = new(Array.Empty<string>(), String.Empty, Array.Empty<string>());
  public IEnumerable<string> Syns { get; set; }

  public string FunctionalLabel { get; set; }

  public string[] Definitions { get; set; }

  public Synonyms(IEnumerable<string> syns, string functionalLabel, string[] definitions)
  {
    Syns = syns;
    FunctionalLabel = functionalLabel;
    Definitions = definitions;
  }

}
