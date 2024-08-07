﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordListChecker
{
    public interface IPasswordListDeserializer
    {
        Task<IEnumerable<string>> DeserializeAsync(TextReader textReader);
    }
}
