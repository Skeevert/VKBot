using System;
using System.Collections.Generic;
using System.Text;
using DotNet.RestApi.Client;
using System.Net.Http;
using System.Linq;
using System.IO;

namespace VKBot
{
    static class Program
    {
        static void Main(string[] args)
        {
			string communityToken = getValueFromFile("communityToken");
			string serviceToken = getValueFromFile("serviceToken");
            VkConnection.Initialize(communityToken, serviceToken);
			var groupId = "191672477";
			var topicIdList = new List<string> {"44630960"};
			var topicList = topicIdList.Select(topicId => new VkTopic(groupId, topicId)).ToList();
            while (true)
            {
				NudgeGMs(topicList);
                System.Threading.Thread.Sleep(60000);
            }
        }

		static void NudgeGMs(List<VkTopic> topicList)
		{
			var playerMasterPairs = new Dictionary<string, string>
			{
				{"36344454", "77486691"},
			};
			foreach (var topic in topicList)
			{
				var posterIds = topic.GetPosterIds();
				foreach (var posterId in posterIds)
				{
					if (playerMasterPairs.ContainsKey(posterId))
					{
						var masterId = playerMasterPairs[posterId];
						var messageText = $"Слыш, там это, твой игрок напостил\n{topic.GenerateLink()}";
						VkConnection.SendMessage(masterId, messageText);
					}
				}
			}
		}
		static string getValueFromFile(string propertyName)
		{
			using (StreamReader sr = new StreamReader("keys"))
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					if (line.StartsWith(propertyName))
					{
						return line.Substring(line.IndexOf('=') + 1).Trim();
					}
				}
			}
			throw new Exception("Property not found");
		}
    }
}
