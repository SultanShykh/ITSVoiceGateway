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

        public ActionResult Index(int Id = 1) 
        {
            CampaignProcessing.View_Campaigans(Id, out List<dynamic> campaignModel, out int totalPages);

            ViewBag.PageNumber = Id;
            ViewBag.totalPages = totalPages;

            return View(campaignModel);
        }

        [HttpGet]
        public ActionResult Edit(int Id) 
        {
            List<BaseAction> callFlowList = new List<BaseAction>();
            List<SelectListItem> files = BaseAction.GetSelectListItems("file", Session["username"].ToString());
            List<SelectListItem> folders = BaseAction.GetSelectListItems("folder", Session["username"].ToString());

            var result = ActionProcessing.GetActionData();

            dynamic DynamicJson = JsonConvert.DeserializeObject(result);
            dynamic temp;
            foreach (var item in DynamicJson)
            {
                switch (item.Action.ToString())
                {
                    case "Callback":
                        callFlowList.Add(JsonConvert.DeserializeObject<Callback>(item.ToString()));
                        break;
                    case "Beep":
                        temp = JsonConvert.DeserializeObject<Beep>(item.ToString());
                        temp.OptionalFilePathItems = files;
                        temp.OptionalFilePath = "";
                        callFlowList.Add(temp);

                        break;
                    case "WaveFile":
                        temp = JsonConvert.DeserializeObject<WaveFile>(item.ToString());
                        temp.FilePathItems = files;
                        temp.FilePath = "";
                        callFlowList.Add(temp);

                        break;
                    case "ParameterizedWaveFile":
                        temp = JsonConvert.DeserializeObject<ParameterizedWaveFile>(item.ToString());
                        temp.OptionalFolderItems = folders;
                        temp.OptionalFolder = "";

                        temp.ParameterTypeItems = BaseAction.GetSelectListItems(temp.ParameterType); ;
                        temp.ParameterType = "";
                        callFlowList.Add(temp);

                        break;
                    case "TTS":
                        callFlowList.Add(JsonConvert.DeserializeObject<TTS>(item.ToString()));
                        break;
                    case "Input":
                        temp = JsonConvert.DeserializeObject<Input>(item.ToString());
                        temp.TypeItems = BaseAction.GetSelectListItems(temp.Type);
                        temp.Type = "";

                        callFlowList.Add(temp);

                        break;
                    case "InputVerify":
                        temp = JsonConvert.DeserializeObject<InputVerify>(item.ToString());
                        temp.VerificationTypeItems = BaseAction.GetSelectListItems(temp.VerificationType);
                        temp.VerificationType = "";

                        temp.ResponseTypeItems = BaseAction.GetSelectListItems(temp.ResponseType);
                        temp.ResponseType = "";

                        callFlowList.Add(temp);
                        break;
                    case "InputBoolResponse":
                        temp = JsonConvert.DeserializeObject<InputBoolResponse>(item.ToString());
                        temp.TrueFilePathItems = files;
                        temp.TrueFilePath = "";

                        temp.FalseFilePathItems = files;
                        temp.FalseFilePath = "";
                        callFlowList.Add(temp);

                        break;
                    case "InputStringResponseTTS":
                        callFlowList.Add(JsonConvert.DeserializeObject<InputStringResponseTTS>(item.ToString()));
                        break;
                    case "InputStringResponseWaveFile":
                        temp = JsonConvert.DeserializeObject<InputStringResponseWaveFile>(item.ToString());
                        temp.FilePathItems = files;
                        temp.FilePath = "";
                        callFlowList.Add(temp);

                        break;
                }
            }

            CampaignModel campModel = new CampaignModel();
            campModel.CallFlowQueue = callFlowList;

            return View(campModel);
        }

        [HttpGet]
        public ActionResult CampaignCDR(int Id=1, int? callType = null, string start = null, string end = null, int? callResponse = null)
        {
            callType = callType == null ? null:callType;
            start = string.IsNullOrEmpty(start) ? null : start;
            end = string.IsNullOrEmpty(end) ? null : end;
            callResponse = callResponse == null ? null : callResponse;

            CampaignProcessing.View_CampaignCDR(Id, callType, start, end, callResponse, out List<dynamic> CampaignCDR, out int totalPages);
            
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
            List<SelectListItem> files = BaseAction.GetSelectListItems("file", Session["username"].ToString());
            List<SelectListItem> folders = BaseAction.GetSelectListItems("folder", Session["username"].ToString());

            var result = ActionProcessing.GetActionData();

            dynamic DynamicJson = JsonConvert.DeserializeObject(result);
            dynamic temp;
            foreach (var item in DynamicJson) 
            {
                switch (item.Action.ToString())
                {
                    case "Callback":
                        callFlowList.Add(JsonConvert.DeserializeObject<Callback>(item.ToString()));
                        break;
                    case "Beep":
                        temp = JsonConvert.DeserializeObject<Beep>(item.ToString());
                        temp.OptionalFilePathItems = files;
                        temp.OptionalFilePath = "";
                        callFlowList.Add(temp);

                        break;
                    case "WaveFile":
                        temp = JsonConvert.DeserializeObject<WaveFile>(item.ToString());
                        temp.FilePathItems = files;
                        temp.FilePath = "";
                        callFlowList.Add(temp);

                        break;
                    case "ParameterizedWaveFile":
                        temp = JsonConvert.DeserializeObject<ParameterizedWaveFile>(item.ToString());
                        temp.OptionalFolderItems = folders;
                        temp.OptionalFolder = "";

                        temp.ParameterTypeItems = BaseAction.GetSelectListItems(temp.ParameterType); ;
                        temp.ParameterType = "";
                        callFlowList.Add(temp);

                        break;
                    case "TTS":
                        callFlowList.Add(JsonConvert.DeserializeObject<TTS>(item.ToString()));
                        break;
                    case "Input":
                        temp = JsonConvert.DeserializeObject<Input>(item.ToString());
                        temp.TypeItems = BaseAction.GetSelectListItems(temp.Type);
                        temp.Type = "";

                        callFlowList.Add(temp);

                        break;
                    case "InputVerify":
                        temp = JsonConvert.DeserializeObject<InputVerify>(item.ToString());
                        temp.VerificationTypeItems = BaseAction.GetSelectListItems(temp.VerificationType);
                        temp.VerificationType = "";

                        temp.ResponseTypeItems = BaseAction.GetSelectListItems(temp.ResponseType);
                        temp.ResponseType = "";

                        callFlowList.Add(temp);
                        break;
                    case "InputBoolResponse":
                        temp = JsonConvert.DeserializeObject<InputBoolResponse>(item.ToString());
                        temp.TrueFilePathItems = files;
                        temp.TrueFilePath = "";

                        temp.FalseFilePathItems = files;
                        temp.FalseFilePath = "";
                        callFlowList.Add(temp);

                        break;
                    case "InputStringResponseTTS":
                        callFlowList.Add(JsonConvert.DeserializeObject<InputStringResponseTTS>(item.ToString()));
                        break;
                    case "InputStringResponseWaveFile":
                        temp = JsonConvert.DeserializeObject<InputStringResponseWaveFile>(item.ToString());
                        temp.FilePathItems = files;
                        temp.FilePath = "";
                        callFlowList.Add(temp);

                        break;
                }
            }

            CampaignModel campModel = new CampaignModel();
            campModel.CallFlowQueue = callFlowList;

            return View(campModel);
        }
        [HttpPost]
        public ActionResult CreateCampaign(CampaignModel model) 
        {
            List<BaseAction> callflowqueue = new List<BaseAction>();

            if (model != null) 
            {
                if (model.CallActions != null) 
                {
                    foreach (var v in model.CallActions)
                    {
                        var obj = JsonConvert.SerializeObject(v);

                        switch (v["Action"].ToString())
                        {
                            case "Callback":
                                callflowqueue.Add(JsonConvert.DeserializeObject<Callback>(obj));
                                break;
                            case "Beep":
                                callflowqueue.Add(JsonConvert.DeserializeObject<Beep>(obj));
                                break;
                            case "WaveFile":
                                callflowqueue.Add(JsonConvert.DeserializeObject<WaveFile>(obj));
                                break;
                            case "ParameterizedWaveFile":
                                callflowqueue.Add(JsonConvert.DeserializeObject<ParameterizedWaveFile>(obj));
                                break;
                            case "TTS":
                                callflowqueue.Add(JsonConvert.DeserializeObject<TTS>(obj));
                                break;
                            case "Input":
                                callflowqueue.Add(JsonConvert.DeserializeObject<Input>(obj));
                                break;
                            case "InputVerify":
                                callflowqueue.Add(JsonConvert.DeserializeObject<InputVerify>(obj));
                                break;
                            case "InputBoolResponse":
                                callflowqueue.Add(JsonConvert.DeserializeObject<InputBoolResponse>(obj));
                                break;
                            case "InputStringResponseTTS":
                                callflowqueue.Add(JsonConvert.DeserializeObject<InputStringResponseTTS>(obj));
                                break;
                            case "InputStringResponseWaveFile":
                                callflowqueue.Add(JsonConvert.DeserializeObject<InputStringResponseWaveFile>(obj));
                                break;
                        }
                    }
                    foreach (var rec in callflowqueue) rec.ValidateRequest(() => TryValidateModel(rec, rec.GetType().Name));
                }
            }

            if (!ModelState.IsValid) 
            {
                return Json(new { status=false, message= string.Join("\n", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) });
            }

            model.CallFlow = JsonConvert.SerializeObject(callflowqueue);
            model.UserId = Convert.ToInt32(Session["UserId"]);
            model.CreatedDateTime = DateTime.Now;

            try
            {
                CampaignProcessing.CreateCampaign(model);
            }
            catch (Exception ex) 
            {
                return Json(new { status = false, message = ex.Message.ToString() });
            }

            return Json(new { status=true, message="Campaign Added Successfully"});
        }
    }
}