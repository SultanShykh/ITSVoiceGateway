using ITSVoice.Codebase;
using ITSVoice.Helper;
using ITSVoice.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITSVoice.Controllers
{
    [Auth("master", "admin", "user")]
    public class CampaignController : Controller
    {
        // GET: Campaign
        [HttpGet]
        public ActionResult CampaignCDR(int Id=1, int? callType = null, string start = null, string end = null, int? callResponse = null)
        {
            callType = callType == null ? null:callType;
            start = string.IsNullOrEmpty(start) ? null : start;
            end = string.IsNullOrEmpty(end) ? null : end;
            callResponse = callResponse == null ? null : callResponse;

            CampaignProcessing.View_CampaignCDR(Id, callType, start, end, callResponse, out List<CampaignCDRModel> CampaignCDR, out int totalPages);
            
            ViewBag.PageNumber = Id;
            ViewBag.totalPages = totalPages;
            ViewBag.callType = callType;
            ViewBag.start = start;
            ViewBag.end = end;
            ViewBag.callResponse = callResponse;

            return View(CampaignCDR);
        }

        public ActionResult CreateCampaign() 
        {
            List<BaseAction> callFlowList = new List<BaseAction>();
            List<SelectListItem> files = BaseAction.GetSelectListItems("file", "sultan");
            List<SelectListItem> folders = BaseAction.GetSelectListItems("folder", "sultan");

            var result = ActionProcessing.GetActionData();

            dynamic DynamicJson = JsonConvert.DeserializeObject(result);

            foreach (var item in DynamicJson) 
            {
                switch (item.Action.ToString())
                {
                    case "Callback":
                        callFlowList.Add(JsonConvert.DeserializeObject<Callback>(item.ToString()));
                        break;
                    case "Beep":
                        var b = JsonConvert.DeserializeObject<Beep>(item.ToString());
                        b.OptionalFilePathItems = files;
                        b.OptionalFilePath = "";
                        callFlowList.Add(b);

                        break;
                    case "WaveFile":
                        var w = JsonConvert.DeserializeObject<WaveFile>(item.ToString());
                        w.FilePathItems = files;
                        w.FilePath = "";
                        callFlowList.Add(w);

                        break;
                    case "ParameterizedWaveFile":
                        var p = JsonConvert.DeserializeObject<ParameterizedWaveFile>(item.ToString());
                        p.OptionalFolderItems = folders;
                        p.OptionalFolder = "";
                        callFlowList.Add(p);

                        break;
                    case "TTS":
                        callFlowList.Add(JsonConvert.DeserializeObject<TTS>(item.ToString()));
                        break;
                    case "Input":
                        var ip = JsonConvert.DeserializeObject<Input>(item.ToString());
                        ip.TypeItems = BaseAction.GetSelectListItems(ip.Type);
                        ip.Type = "";

                        callFlowList.Add(ip);

                        break;
                    case "InputVerify":
                        var iv = JsonConvert.DeserializeObject<InputVerify>(item.ToString());
                        iv.VerificationTypeItems = BaseAction.GetSelectListItems(iv.VerificationType);
                        iv.VerificationType = "";

                        iv.ResponseTypeItems = BaseAction.GetSelectListItems(iv.ResponseType);
                        iv.ResponseType = "";

                        callFlowList.Add(iv);
                        break;
                    case "InputBoolResponse":
                        var ibr = JsonConvert.DeserializeObject<InputBoolResponse>(item.ToString());
                        ibr.TrueFilePathItems = files;
                        ibr.TrueFilePath = "";

                        ibr.FalseFilePathItems = files;
                        ibr.FalseFilePath = "";
                        callFlowList.Add(ibr);

                        break;
                    case "InputStringResponseTTS":
                        callFlowList.Add(JsonConvert.DeserializeObject<InputStringResponseTTS>(item.ToString()));
                        break;
                    case "InputStringResponseWaveFile":
                        var i = JsonConvert.DeserializeObject<InputStringResponseWaveFile>(item.ToString());
                        i.FilePathItems = files;
                        i.FilePath = "";
                        callFlowList.Add(i);

                        break;
                }
            }

            return View(callFlowList);
        }
        [HttpPost]
        public ActionResult CreateCampaign(List<IDictionary<string, string>> model) 
        {
            return View();
        }
    }
}