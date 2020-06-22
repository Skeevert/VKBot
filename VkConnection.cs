using System;
using System.Collections.Generic;
using System.Text;
using DotNet.RestApi.Client;
using System.Net.Http;
using System.Linq;

namespace VKBot
{
	static class VkConnection
	{
		private static string _communityToken;
		private static string _serviceToken;
		private static readonly Uri _baseUri = new Uri("https://api.vk.com/method/");

		public static void Initialize (string communityToken, string serviceToken)
		{
			_communityToken = communityToken;
			_serviceToken = serviceToken;
		}

		static string MethodForm(string methodName, Dictionary<string, string> parameters, string version = "v=5.110")
		{
			StringBuilder sb = new StringBuilder($"{methodName}?{version}&");

			foreach (KeyValuePair<string, string> parameter in parameters)
			{
				sb.Append($"{parameter.Key}={parameter.Value}&");
			}
			return sb.ToString();
		}

		public static void SendMessage(string peerId, string message)
		{
			Dictionary<string, string> data = new Dictionary<string, string>();
			Random rand = new Random();
			RestApiClient client = new RestApiClient(_baseUri);

			data.Add("random_id", rand.Next().ToString());
			data.Add("peer_id", peerId);
			data.Add("message", message);
			data.Add("access_token", _communityToken);
			var response = client.SendJsonRequest(HttpMethod.Get, new Uri(MethodForm("messages.send", data),  UriKind.Relative), null).Result;
			Console.WriteLine(response);
		}

		public static string GetLastCommentId(string groupId, string topicId)
		{
			return GetLastComments(groupId, topicId).Single().Id.ToString();
		}

		public static List<Comment> GetLastComments(string groupId, string topicId, string startCommentId = null)
		{
			Dictionary<string, string> data = new Dictionary<string, string>();
			RestApiClient client = new RestApiClient(_baseUri);

			data.Add("group_id", groupId);
			data.Add("topic_id", topicId);
			if (!string.IsNullOrEmpty(startCommentId))
			{
				data.Add("start_comment_id", startCommentId);
				data.Add("offset", "1");
			}
			else
			{
				data.Add("sort", "desc");
				data.Add("count", "1");
			}
			data.Add("access_token", _serviceToken);
			var response = client.SendJsonRequest(HttpMethod.Get, new Uri(MethodForm("board.getComments", data),  UriKind.Relative), null).Result;
			Console.WriteLine(response.Content.ReadAsStringAsync().Result);
			var deserializedResponse = response.DeseriaseJsonResponse<Comments>();
			return deserializedResponse.Response.Items;
		}
	}
}
