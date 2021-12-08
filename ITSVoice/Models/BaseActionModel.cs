using ITSVoice.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;

namespace ITSVoice.Models
{
    public abstract class BaseAction
    {
        [Required]
        [JsonProperty(Order = 0)]
        public string Action { get; set; }
        public abstract void ValidateRequest(Action func);
        public abstract T GetInstance<T>() where T : BaseAction;
        public static string root = WebConfigurationManager.AppSettings["RootPath"].ToString();
        public static List<SelectListItem> GetSelectListItems(string input, string username="")
        {
            List<SelectListItem> items = new List<SelectListItem>();
            List<string>  FilePath = new List<string>();
            List<string> FolderPath = new List<string>();
            

            if (input.Contains("/"))
            {
                return input.Split('/').Select(x => new SelectListItem() { Text = x, Value = x }).ToList();
            }

            if (input == "file")
            {
                //Get {user} directory files list
                FilePath.Clear();
                ProcessFiles(Path.Combine(root, username), ref FilePath, username);
                return FilePath.Select(x => new SelectListItem() { Text = x, Value = x }).ToList();
            }

            if (input == "folder")
            {
                //Get {user} directory list
                FolderPath.Clear();
                ProcessDirectory(Path.Combine(root, username), ref FolderPath, username);
                return FolderPath.Select(x => new SelectListItem() { Text = x, Value = x }).ToList();
            }

            return items;
        }

        public static void ProcessFiles(string targetDirectory, ref List<string> FilePath, string username)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                var substr = fileName.Substring(Path.Combine(root, username).Length);
                FilePath.Add(substr);
            }

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
            {
                ProcessFiles(subdirectory, ref FilePath, username);
            }
                
        }

        public static void ProcessDirectory(string targetDirectory, ref List<string> FolderPath, string username)
        {
            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
            {
                ProcessDirectory(subdirectory, ref FolderPath, username);
                var substr = subdirectory.Substring(Path.Combine(root, username).Length);
                FolderPath.Add(substr);
            }
        }
    }
}
