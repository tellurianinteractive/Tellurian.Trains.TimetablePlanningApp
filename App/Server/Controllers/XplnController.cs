﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimetablePlanning.Importers.Xpln;
using TimetablePlanning.Importers.Xpln.DataSetProviders;

namespace TimetablePlanning.App.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class XplnController : ControllerBase
{
    public XplnController(ILogger<OdsDataSetProvider> datasetLogger, ILogger<XplnDataImporter> importerLogger)
    {
        DatasetLogger = datasetLogger;
        ImporterLogger = importerLogger;
    }

    public ILogger<OdsDataSetProvider> DatasetLogger { get; }
    public ILogger<XplnDataImporter> ImporterLogger { get; }

    [HttpPost, Route("validate")]
    public IActionResult Validate([FromForm] List<IFormFile> files)
    {
        if (files.Count == 0) return BadRequest("No file provided.");
        if (files.Count > 1) return BadRequest("Only one file per upload.");
        var file = files[0];
        if (file.Length == 0) return BadRequest("No file content provided.");
        if (Path.GetExtension(file.Name).Equals(".ods", StringComparison.OrdinalIgnoreCase)) return BadRequest("Not an .ODS file.");
        using var stream = file.OpenReadStream();
        var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        memoryStream.Position = 0;
        var provider = new OdsDataSetProvider(DatasetLogger);
        using var importer = new XplnDataImporter(memoryStream, provider, ImporterLogger);
        var importResult = importer.ImportSchedule(Path.GetFileNameWithoutExtension(file.Name));
        return Ok(importResult);
    }
}
