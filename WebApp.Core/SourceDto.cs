﻿namespace WebApp.Core;

public class SourceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string Url { get; set; }
    public string RssUrl { get; set; }
}