
public class Synonyms
{
  public static Synonyms NullSynonyms = new Synonyms(Array.Empty<string>(), String.Empty);
  public IEnumerable<string> Syns { get; set; }

  public Synonyms(IEnumerable<string> syns, string functionalLabel)
  {
    Syns = syns;
    FunctionalLabel = functionalLabel;
  }

  public string FunctionalLabel { get; set; }
}
