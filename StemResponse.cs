namespace synonym_server
{
  public class StemResponse
  {
    public string PartOfSpeech { get; }
    public string Stem { get; }
    public StemResponse(string stem, string partOfSpeech)
    {
      Stem = stem;
      PartOfSpeech = partOfSpeech;
    }
  }
}