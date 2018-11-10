﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace FMG.NRT.Components.Mvc
{
    public class Languages : ILanguages
    {
        public Language Default
        {
            get;
        }
        public Language Current
        {
            get
            {
                return Supported.Single(language => language.Culture.Equals(CultureInfo.CurrentUICulture));
            }
            set
            {
                Thread.CurrentThread.CurrentCulture = value.Culture;
                Thread.CurrentThread.CurrentUICulture = value.Culture;
            }
        }
        public Language[] Supported
        {
            get;
        }

        private Dictionary<String, Language> Dictionary
        {
            get;
        }

        public Languages(IConfiguration config)
        {
            String path = Path.Combine(config["Application:Path"], config["Languages:Path"]);
            Dictionary = new Dictionary<String, Language>();

            foreach (XElement lang in XElement.Load(path).Elements("language"))
            {
                Language language = new Language();
                language.Culture = new CultureInfo((String)lang.Attribute("culture"));
                language.IsDefault = (Boolean?)lang.Attribute("default") == true;
                language.Abbreviation = (String)lang.Attribute("abbreviation");
                language.Name = (String)lang.Attribute("name");

                Dictionary.Add(language.Abbreviation, language);
            }

            Supported = Dictionary.Select(language => language.Value).ToArray();
            Default = Supported.Single(language => language.IsDefault);
        }

        public Language this[String abbreviation] => Dictionary.TryGetValue(abbreviation ?? "", out Language language) ? language : Default;
    }
}
