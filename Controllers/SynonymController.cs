using Microsoft.AspNetCore.Mvc;

namespace synonym_server.Controllers;

[ApiController]
[Route("[controller]")]
public class SynonymController : ControllerBase
{
  private readonly ILogger<SynonymController> _logger;
  private readonly string _thesaurusUriTemplate;
  private readonly IConfiguration _config;

  public SynonymController(ILogger<SynonymController> logger, IConfiguration config)
  {
    _logger = logger;
    _thesaurusUriTemplate = "https://www.dictionaryapi.com/api/v3/references/thesaurus/json/{0}?key=" + config["THESAURUS_API_KEY"];
    _config = config;
  }

  [HttpGet()]
  public async Task<Synonyms> Get([FromQuery] string stem, [FromQuery] string functionalLabel)
  {
    _logger.LogDebug("request received");
    using HttpClient client = new();

    var resp = (await client.GetFromJsonAsync<ThesaurusResponse[]>(string.Format(_thesaurusUriTemplate, stem.ToLower())))
      ?.Where(r => String.Equals(r.Fl, functionalLabel, StringComparison.CurrentCultureIgnoreCase))
      ?.FirstOrDefault();

    if (resp == null) return Synonyms.NullSynonyms;
    return new Synonyms(resp.Meta.Syns?.SelectMany(s => s)?.ToArray() ?? Array.Empty<string>(), resp.Fl, resp.ShortDef);
  }
}
