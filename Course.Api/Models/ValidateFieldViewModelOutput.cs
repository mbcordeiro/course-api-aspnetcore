﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Models
{
    public class ValidateFieldViewModelOutput
    {
        public IEnumerable<string> Erros { get; private set; }

        public ValidateFieldViewModelOutput(IEnumerable<string> erros)
        {
            Erros = erros;
        }

    }
}
