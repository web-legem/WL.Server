using LanguageExt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WL.Application.Interfaces {

  public interface IPdfToText {

    Try<string> GetText(FileInfo fileInfo);
  }
}