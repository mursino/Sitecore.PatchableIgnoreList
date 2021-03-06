﻿using Sitecore.Collections;
using Sitecore.Pipelines.HttpRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.PatchableIgnoreList
{
    public class ProcessPatchedIgnores : HttpRequestProcessor
    {
        private List<string> _paths = new List<string>();
        
        public override void Process(HttpRequestArgs args)
        {
            string filePath = args.Url.FilePath;

            foreach (string path in _paths)
            {
                if (filePath.StartsWith(path, StringComparison.OrdinalIgnoreCase))
                {
                    args.AbortPipeline();
                    return;
                }
            }
        }

        public void AddPaths(string path)
        {
            if (!string.IsNullOrEmpty(path) && !_paths.Contains(path))
            {
                _paths.Add(path);
            }
        }
    }
}