using System.Collections.Generic;
using Newtonsoft.Json;

namespace VKBot
{
	class VkTopic
	{
		private string _groupId;
		private string _topicId;
		private string _lastCommentId;

		public VkTopic(string groupId, string topicId)
		{
			_groupId = groupId;
			_topicId = topicId;
			_lastCommentId = VkConnection.GetLastCommentId(_groupId, _topicId);
		}

		public HashSet<string> GetPosterIds()
		{
			var posterIds = new HashSet<string>();
			var currentLastCommentId = VkConnection.GetLastCommentId(_groupId, _topicId);

			if (currentLastCommentId == _lastCommentId)
			{
				return posterIds;
			}
			var recentCommentList = VkConnection.GetLastComments(_groupId, _topicId, _lastCommentId);
			System.Console.WriteLine(JsonConvert.SerializeObject(recentCommentList));
			foreach (var comment in recentCommentList)
			{
				//System.Console.WriteLine(JsonConvert.SerializeObject(comment));
				posterIds.Add(comment.FromId.ToString());
			}
			_lastCommentId = currentLastCommentId;
			return posterIds;
		}

		public string GenerateLink()
		{
			return $"https://vk.com/topic-{_groupId}_{_topicId}?post={_lastCommentId}";
		}
	}
}
