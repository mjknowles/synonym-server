namespace synonym_server;

public class ThesaurusResponse
{
  public ThesaurusResponseMeta Meta { get; set; }
  public string Fl { get; set; }
}

public class ThesaurusResponseMeta
{
  public string[] Stems { get; set; }
  public string[][] Syns { get; set; }
}