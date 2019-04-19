using LanguageExt;
using NSOCRLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WL.Application.Interfaces;

namespace WL.Application.Documents.Ocr {

  public class OcrPdfToText : IPdfToText {

    public Try<string> GetText(FileInfo fileInfo)
       => ()
       => {
         var fullFileName = fileInfo.FullName;

         int res;

         var NsOCR = new NSOCRClass();
         NsOCR.Engine_SetLicenseKey("AB2A4DD5FF2A");
         NsOCR.Engine_Initialize();

         NsOCR.Cfg_Create(out var CfgObj);

         //NsOCR.Cfg_LoadOptions( CfgObj, @"C:\pruebas\niconsoft\Config.dat" );

         NsOCR.Ocr_Create(CfgObj, out var OcrObj);
         NsOCR.Img_Create(OcrObj, out var ImgObj);
         NsOCR.Svr_Create(CfgObj, TNSOCR.SVR_FORMAT_TXT_UNICODE, out var SvrObj);

         #region language and character configuration

         NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "Languages/Spanish", "1");
         NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "Languages/English", "0");

         #endregion language and character configuration

         #region ghostScript configuration and pdf support

         NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "Main/GhostScriptDLL", @"gsdll64.dll");
         NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "Main/PdfDPI", "600");
         NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "Main/PdfByExt", "2");

         #endregion ghostScript configuration and pdf support

         #region thread configuration

         NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "Main/MaxKernels", "16");
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

         NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "Main/CharFactors", "[|!#$%&/()=?¿¡[]{}`^¬ 0.25][ñÑ 1.5][abcdefghijklmnopqrstuvxyz.,123456789 1.2]");
         NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "Zoning/ZonesFactor", "0.5");
         NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "Zoning/OneColumn", "1"); // 0 = more columns, 1 = just one column
         NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "Zoning/FindTables", "1"); // 0 = do not find tables, 1 = search for tables
         NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "Linezer/RemoveGarbage", "1"); // 1 = apply algorithm to remove garbage, 0 = do not apply algorithm
         NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "Linezer/RemoveGarbage", "1"); // 1 = apply algorithm to remove garbage, 0 = do not apply algorithm
         NsOCR.Cfg_SetOption(CfgObj, TNSOCR.BT_DEFAULT, "Dictionaries/UseDictionary", "1"); // 0= do not use dictionary, 1 = use dictionary

         #endregion optimizations

         //NsOCR.Cfg_SaveOptions( CfgObj, @"C:\pruebas\Config.dat" );

         res = NsOCR.Img_LoadFile(ImgObj, fullFileName);

         if (res > TNSOCR.ERROR_FIRST) {
           //insert error handler here
           Console.WriteLine("Error First in OcrPdfToText");
         } else if (res == TNSOCR.ERROR_CANNOTLOADGS) {
           Console.WriteLine("Cannot load GS");
         }

         Console.WriteLine("Stating OCR");

         //res = NsOCR.Img_OCR(ImgObj, TNSOCR.OCRSTEP_FIRST, TNSOCR.OCRSTEP_LAST, TNSOCR.OCRFLAG_NONE);
         var pages = NsOCR.Img_GetPageCount(ImgObj);
         res = NsOCR.Ocr_ProcessPages(ImgObj, SvrObj, 0, pages, 0, TNSOCR.OCRFLAG_NONE);
         Console.WriteLine($"The image has {pages} pages");
         Console.WriteLine("OCR has finished");

         if (res > TNSOCR.ERROR_FIRST) {
           Console.WriteLine("Error First in OcrPdfToText");
         }

         //NsOCR.Img_GetImgText(ImgObj, out txt, TNSOCR.FMT_EXACTCOPY);
         NsOCR.Svr_GetText(SvrObj, -1, out var txt);

         NsOCR.Engine_Uninitialize();

         return txt;
       };
  }
}