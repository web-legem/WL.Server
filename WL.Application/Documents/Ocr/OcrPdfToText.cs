using LanguageExt;

using NSOCRLib;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using WL.Application.Interfaces;

namespace WL.Application.Documents.Ocr {

  public class OcrPdfToText : IPdfToText {

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Try<string> GetText(FileInfo fileInfo)
      => ()
      => {
        var fullFileName = fileInfo.FullName;

        int res;

        var NsOCR = new NSOCRClass();
        NsOCR.Engine_SetLicenseKey("AB2A4DD5FF2A");
        NsOCR.Engine_Initialize();

        NsOCR.Cfg_Create(out var CfgObj);

        NsOCR.Cfg_LoadOptions(CfgObj, Path.Combine(
          Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
           @"OcrConfig.dat"));
        Console.WriteLine("configuracion cargada");

        NsOCR.Ocr_Create(CfgObj, out var OcrObj);
        NsOCR.Img_Create(OcrObj, out var ImgObj);
        NsOCR.Svr_Create(CfgObj, TNSOCR.SVR_FORMAT_TXT_UNICODE, out var SvrObj);

        #region ghostScript configuration and pdf support

        NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "Main/GhostScriptDLL", Path.Combine(
        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"gsdll64.dll"));
        NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "Main/PdfDPI", "600");
        NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "Main/PdfByExt", "2");

        #endregion ghostScript configuration and pdf support

        #region thread configuration

        NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "Main/MaxKernels", "8");
        NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "Main/NumKernels", "0");

        #endregion thread configuration

        #region performance configuration

        NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "ImgAlizer/Inversion", "0");
        NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "ImgAlizer/AutoRotate", "0");
        NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "ImgAlizer/AutoScale", "1");
        NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "ImgAlizer/NoiseFilter", "0");
        NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "Main/FastMode", "0");
        NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "Main/GrayMode", "1");
        NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "Zoning/OneZone", "1");
        NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "WordAlizer/SplitCombine", "0");

        #endregion performance configuration

        #region optimizations

        NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "Zoning/ZonesFactor", "0.5");
        NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "Zoning/OneColumn", "1"); // 0 = more columns, 1 = just one column
        NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "Zoning/FindTables", "1"); // 0 = do not find tables, 1 = search for tables
        NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "Linezer/RemoveGarbage", "1"); // 1 = apply algorithm to remove garbage, 0 = do not apply algorithm
        NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "Dictionaries/UseDictionary", "1"); // 0= do not use dictionary, 1 = use dictionary

        #endregion optimizations

        res = NsOCR.Img_LoadFile(ImgObj, fullFileName);

        if (res > TNSOCR.ERROR_FIRST) {
          Console.WriteLine("Error First in OcrPdfToText");
        }
        if (res == TNSOCR.ERROR_FILENOTFOUND) {
          Console.WriteLine("Error");
        }
        if (res == TNSOCR.ERROR_LOADFILE) {
          Console.WriteLine("Error");
        }
        if (res == TNSOCR.ERROR_SAVEFILE) {
          Console.WriteLine("Error");
        }
        if (res == TNSOCR.ERROR_MISSEDIMGLOADER) {
          Console.WriteLine("Error");
        }
        if (res == TNSOCR.ERROR_NOBLOCKS) {
          Console.WriteLine("Error");
        }

        if (res == TNSOCR.ERROR_BLOCKNOTFOUND) {
          Console.WriteLine("Error");
        }

        if (res == TNSOCR.ERROR_INVALIDINDEX) {
          Console.WriteLine("Error");
        }
        if (res == TNSOCR.ERROR_INVALIDPARAMETER) {
          Console.WriteLine("Error");
        }
        if (res == TNSOCR.ERROR_FAILED) {
          Console.WriteLine("Error");
        }
        if (res == TNSOCR.ERROR_INVALIDBLOCKTYPE) {
          Console.WriteLine("Errro");
        }
        if (res == TNSOCR.ERROR_LOADINGDICTIONARY) {
          Console.WriteLine("Error");
        }
        if (res == TNSOCR.ERROR_LOADCHARBASE) {
          Console.WriteLine("Erro");
        }
        if (res == TNSOCR.ERROR_NOMEMORY) {
          Console.WriteLine("Erro");
        }
        if (res == TNSOCR.ERROR_CANNOTLOADGS) {
          Console.WriteLine("Erro");
        }
        if (res == TNSOCR.ERROR_CANNOTPROCESSPDF) {
          Console.WriteLine("Erro");
        }
        if (res == TNSOCR.ERROR_NOIMAGE) {
          Console.WriteLine("Erro");
        }
        if (res == TNSOCR.ERROR_MISSEDSTEP) {
          Console.WriteLine("Erro");
        }
        if (res == TNSOCR.ERROR_OUTOFIMAGE) {
          Console.WriteLine("Erro");
        }
        if (res == TNSOCR.ERROR_EXCEPTION) {
          Console.WriteLine("Erro");
        }
        if (res == TNSOCR.ERROR_NOTALLOWED) {
          Console.WriteLine("Erro");
        }
        if (res == TNSOCR.ERROR_NODEFAULTDEVICE) {
          Console.WriteLine("Erro");
        }
        if (res == TNSOCR.ERROR_NOTAPPLICABLE) {
          Console.WriteLine("Erro");
        }
        if (res == TNSOCR.ERROR_MISSEDBARCODEDLL) {
          Console.WriteLine("Erro");
        }
        if (res == TNSOCR.ERROR_PENDING) {
          Console.WriteLine("Erro");
        }
        if (res == TNSOCR.ERROR_OPERATIONCANCELLED) {
          Console.WriteLine("Erro");
          ;
        }
        if (res == TNSOCR.ERROR_TOOMANYLANGUAGES) {
          Console.WriteLine("Erro");
        }
        if (res == TNSOCR.ERROR_OPERATIONTIMEOUT) {
          Console.WriteLine("Erro");
        }
        if (res == TNSOCR.ERROR_LOAD_ASIAN_MODULE) {
          Console.WriteLine("Erro");
        }
        if (res == TNSOCR.ERROR_LOAD_ASIAN_LANG) {
          Console.WriteLine("Erro");
        }

        if (res == TNSOCR.ERROR_INVALIDOBJECT) {
          Console.WriteLine("Erro");
        }
        if (res == TNSOCR.ERROR_TOOMANYOBJECTS) {
          Console.WriteLine("Erro");
        }
        if (res == TNSOCR.ERROR_DLLNOTLOADED) {
          Console.WriteLine("Erro");
        }
        if (res == TNSOCR.ERROR_DEMO) {
          Console.WriteLine("Erro");
        }

        Console.WriteLine("Stating OCR");

        var pages = NsOCR.Img_GetPageCount(ImgObj);
        res = NsOCR.Ocr_ProcessPages(ImgObj, SvrObj, 0, pages, 0, TNSOCR.OCRFLAG_NONE);
        Console.WriteLine($"The image has {pages} pages");
        Console.WriteLine("OCR has finished");

        if (res > TNSOCR.ERROR_FIRST) {
          Console.WriteLine("Error First in OcrPdfToText");
        }

        NsOCR.Svr_GetText(SvrObj, -1, out var txt);

        NsOCR.Img_Destroy(ImgObj);
        NsOCR.Ocr_Destroy(OcrObj);
        NsOCR.Svr_Destroy(SvrObj);
        NsOCR.Cfg_Destroy(CfgObj);

        NsOCR.Engine_Uninitialize();

        //var txt = "";
        return txt;
      };
  }
}