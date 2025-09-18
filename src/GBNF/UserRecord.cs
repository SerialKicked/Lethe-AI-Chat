﻿using LetheAISharp;
using LetheAISharp.LLM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace LetheAIChat.GBNF
{

    public class UserRecord : LLMExtractableBase<UserRecord>
    {
        [JsonIgnore] private static string Schema = string.Empty;

        [MinLength(0)]
        [MaxLength(20)]
        [Description("A list of {{user}}'s relations and friends, add relevant details when available")]
        public List<string> Relationships { get; set; } = [];

        [MinLength(0)]
        [MaxLength(20)]
        [Description("A detailed list of {{user}}'s tastes and hobbies")]
        public List<string> Tastes { get; set; } = [];


        [MinLength(0)][MaxLength(30)][Description("A list of other facts about {{user}} not fitting in the other categories")]
        public List<string> UserInfo { get; set; } = [];


        public override async Task<string> GetGrammar()
        {
            if (Schema == string.Empty)
            {
                Schema = await base.GetGrammar();
            }
            return Schema;
        }

        public override string GetQuery()
        {
            var requestedTask = "Write a JSON file containing the following information about {{user}} based on the data shown above:\n";
            var schema = DescriptionHelper.GetAllDescriptionsRecursive<UserRecord>();

            foreach (var prop in schema)
            {
                requestedTask += $"- {prop.Key}: {prop.Value}\n";
            }
            // requestedTask += "\n\nOnly include information that is not already provide in the provided biography.";

            requestedTask = LLMEngine.Bot.ReplaceMacros(requestedTask);
            return requestedTask;
        }
    }
}
